using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class Array_
    {
        public static int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            // https://leetcode.cn/problems/next-greater-element-i/description/
            Stack<int> stack = new Stack<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            Dictionary<int, int> valueIndex = new Dictionary<int, int>();

            // 遍历 nums2，构建单调栈
            for (int i = 0; i < nums2.Length; i++)
            {
                valueIndex[nums2[i]] = i;
                // 当前元素大于栈顶元素时，弹出栈顶元素并更新字典
                while (stack.Count > 0 && nums2[stack.Peek()] < nums2[i])
                {
                    int pop = stack.Pop();
                    dict[pop] = nums2[i];
                }
                // 当前元素入栈
                stack.Push(i);
            }

            // 栈中剩余的元素没有下一个更大元素，设置为 -1
            while (stack.Count > 0)
            {
                int pop = stack.Pop();
                dict[pop] = -1;
            }

            int[] result = new int[nums1.Length];
            for (int i = 0;i < nums1.Length;i++)
            {
                result[i] = dict[valueIndex[nums1[i]]];
            }
            return result;
        }
        public static int MaxSumMinProduct(int[] nums)
        {
            // https://leetcode.cn/problems/maximum-subarray-min-product/description/
            // 先用单调栈
            Stack<LinkedList<int>> stack = new Stack<LinkedList<int>>();        // 一定要记住加入的是下标
            Dictionary<int, (int left, int right)> dict = new Dictionary<int, (int left, int right)>();
            for (int i = 0; i < nums.Length; i++)
            {
                if(stack.Count > 0)
                {
                    if (nums[stack.Peek().Last.Value] < nums[i])
                    {
                        var curr = new LinkedList<int>();
                        curr.AddLast(i);
                        stack.Push(curr);
                        continue;
                    }
                    else if(nums[stack.Peek().Last.Value] == nums[i])
                    {
                        stack.Peek().AddLast(i);
                        continue;
                    }

                    while (stack.Count != 0)
                    {
                        if (nums[stack.Peek().Last.Value] < nums[i])
                        {
                            var curr = new LinkedList<int>();
                            curr.AddLast(i);
                            stack.Push(curr);
                            break;

                        }
                        else if (nums[stack.Peek().Last.Value] == nums[i])
                        {
                            stack.Peek().AddLast(i);
                            break;
                        }
                        else
                        {
                            var pop = stack.Pop();
                            if (stack.Count == 0)
                            {
                                foreach (var item in pop)
                                {
                                    dict.Add(item, (-1, i));
                                }
                            }
                            else
                            {
                                foreach (var item in pop)
                                {
                                    dict.Add(item, (stack.Peek().Last.Value, i));
                                }
                            }
                            if(stack.Count == 0)
                            {
                                var curr = new LinkedList<int>();
                                curr.AddLast(i);
                                stack.Push(curr);
                                break;
                            }
                        }                       
                    }                                      
                }
                else
                {
                    var curr = new LinkedList<int>();
                    curr.AddLast(i);
                    stack.Push(curr);
                }
            }

            while(stack.Count != 0)
            {
                var pop = stack.Pop();
                if (stack.Count == 0)
                {
                    foreach (var item in pop)
                    {
                        dict.Add(item, (-1, -1));
                    }
                }
                else
                {
                    foreach (var item in pop)
                    {
                        dict.Add(item, (stack.Peek().Last.Value, -1));
                    }
                }
            }


            ulong max = 0;
            // 开始计算和乘
            for (int i = 0; i < nums.Length; i++)
            {
                (int left, int right) = dict[i];
                right = right == -1 ? nums.Length : right;
                ulong sum = 0;
                for (int j = left + 1; j < right; j++)
                {
                    sum += (ulong)nums[j];
                }
                ulong mul = sum * (ulong)nums[i];
                max = max > mul ? max : mul;
            }
            return (int) (max % (Math.Pow(10, 9) + 7));
        }
        public static int[] MaxSlidingWindow(int[] nums, int k)
        {
            // https://leetcode.cn/problems/sliding-window-maximum/
            // 滑动窗口
            if (nums.Length == 0 || k == 0 || k > nums.Length)
                return null;
            int l = 0;
            int r = 0;
            int[] result = new int[nums.Length - k + 1];
            int index = 0;
            LinkedList<int> list = new LinkedList<int>();       // 这里用链表实现双端队列
            
            for (; r < k; r++)
            {
                while (list.Count > 0 && nums[list.Last.Value] <= nums[r])      // 严格保持队列中的  大 -> 小
                {
                    list.RemoveLast();
                }
                list.AddLast(r);
            }
            result[index] = nums[list.First.Value];


            while (r < nums.Length)
            {               
                while (list.Count > 0 && nums[list.Last.Value] <= nums[r])      // 严格保持队列中的  大 -> 小
                {
                    list.RemoveLast();
                }
                list.AddLast(r);

                if(list.First.Value == l)
                    list.RemoveFirst();
                l++;

                index++;
                result[index] = nums[list.First.Value];
                r++;
            }
            return result;

            #region AI优化
            //if (nums.Length == 0 || k == 0)
            //    return new int[0];

            //int n = nums.Length;
            //int[] result = new int[n - k + 1];
            //LinkedList<int> deque = new LinkedList<int>();

            //for (int i = 0; i < n; i++)
            //{
            //    // 移除队列中所有小于当前元素的索引
            //    while (deque.Count > 0 && nums[deque.Last.Value] <= nums[i])
            //    {
            //        deque.RemoveLast();
            //    }

            //    // 添加当前元素的索引到队列
            //    deque.AddLast(i);

            //    // 移除队列中不在窗口范围内的索引
            //    if (deque.First.Value <= i - k)
            //    {
            //        deque.RemoveFirst();
            //    }

            //    // 当窗口大小达到 k 时，记录当前窗口的最大值
            //    if (i >= k - 1)
            //    {
            //        result[i - k + 1] = nums[deque.First.Value];
            //    }
            //}

            //return result;
            #endregion
        }
        public static int NumIslands(char[][] grid)
        {
            // https://leetcode.cn/problems/number-of-islands/description/
            // O(N * M)
            int result = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j < grid[0].Length; j++)         // 注意交错数组就是这样遍历的
                {
                    if (grid[i][j] == '1')
                    { 
                        result++;
                        NumIslandsInfect2(i, j, grid);
                    }
                }
            }
            return result;
        }
        public static void NumIslandsInfect(int x, int y, char[][] grid)
        {
            // BFS遍历
            Queue<(int, int)> queue = new Queue<(int, int)>();
            //HashSet<(int, int)> visited = new HashSet<(int, int)>();

            queue.Enqueue((x, y));
            //visited.Add((x, y));
            while (queue.Count > 0)
            {
                (int currX, int currY) = queue.Dequeue();
                if(currY - 1 >= 0 && grid[currX][currY - 1] == '1')
                {
                    queue.Enqueue((currX, currY - 1));
                    grid[currX][currY - 1] = '2';
                }
                if (currY + 1 < grid[0].Length && grid[currX][currY + 1] == '1')
                {
                    queue.Enqueue((currX, currY + 1));
                    grid[currX][currY + 1] = '2';
                }
                if (currX - 1 >= 0 && grid[currX - 1][currY] == '1')
                {
                    queue.Enqueue((currX - 1, currY));
                    grid[currX - 1][currY] = '2';
                }
                if (currX + 1 < grid.GetLength(0) && grid[currX + 1][currY] == '1')
                {
                    queue.Enqueue((currX + 1, currY));
                    grid[currX + 1][currY] = '2';
                }
            }
        }
        public static void NumIslandsInfect2(int x, int y, char[][] grid)
        {
            // 用递归写  优雅   还是得进入递归后再判断
            if (x < 0 || y < 0 || x >= grid.GetLength(0) || y >= grid[0].Length || grid[x][y] != '1')
                return;
            grid[x][y] = '2';
            NumIslandsInfect2(x + 1, y, grid);
            NumIslandsInfect2(x - 1, y, grid);
            NumIslandsInfect2(x, y + 1, grid);
            NumIslandsInfect2(x, y - 1, grid);
        }
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            // https://leetcode.cn/problems/median-of-two-sorted-arrays/description/
            List<int> list = new List<int>();
            int index1 = 0;
            int index2 = 0;
            
            while(index1 < nums1.Length || index2 < nums2.Length)
            {
                if (index1 == nums1.Length)
                {
                    for (; index2 < nums2.Length; index2++)
                    {
                        list.Add(nums2[index2]);
                    }
                    break;
                }
                else if (index2 == nums2.Length)
                {
                    for (; index1 < nums1.Length; index1++)
                    {
                        list.Add(nums1[index1]);
                    }
                    break;
                }

                if (nums1[index1] <= nums2[index2])
                {
                    list.Add(nums1[index1]);
                    index1++;
                }
                else
                {
                    list.Add(nums2[index2]);
                    index2++;
                }
            }

            if(list.Count % 2 == 0)
            {
                int left = (list.Count >> 1) - 1;
                return (list[left] + list[left + 1]) / 2d;
            }
            else
            {
                return (double)list[list.Count >> 1];
            }
        }
        public static double FindMedianSortedArrays2(int[] nums1, int[] nums2)
        {
            // log(m+n)的解法之后再看
            return 0;

        }
    }
}
