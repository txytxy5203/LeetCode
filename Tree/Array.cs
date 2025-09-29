using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class Array_
    {
        public static int LongestSubarray(int[] nums)
        {
            // https://leetcode.cn/problems/longest-subarray-of-1s-after-deleting-one-element/description/
            int zero = 0;
            int ans = 0;
            int left = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                // in
                if (nums[i] == 0)
                    zero++;

                // out
                while (zero > 1)
                {
                    if (nums[left] == 0)
                        zero--;
                    left++;
                }
                // update
                ans = Math.Max(ans, i - left + 1);
            }
            return ans - 1;
        }
        public static int[] GetSubarrayBeauty(int[] nums, int k, int x)
        {
            // https://leetcode.cn/problems/sliding-subarray-beauty/description/
            int[] result = new int[nums.Length - k + 1];
            ListNode head = new ListNode(int.MaxValue);
            ListNode curr = head;
            int num = k - 1;
            while(num > 0)
            {
                ListNode node = new ListNode(int.MaxValue);
                curr.next = node;
                curr = curr.next;
                num--;
            }
            curr = head;

            for (int i = 0; i < nums.Length; i++)
            {
                // in
                ListNode node = null;
                while (nums[i] > curr.val)
                {
                    node = curr;
                    curr = curr.next;
                }
                ListNode add = new ListNode(nums[i]);
                node.next = add;
                add.next = curr;

                int left = i - k + 1;
                if (left < 0)
                    continue;
                // update
                if (head.next.val < 0)
                    result[left] = head.next.val;
                else
                    result[left] = 0;

                // out
                node = head;
                add = head;
                while (node.val != nums[left])
                {
                    add = node;
                    node = node.next;
                }
                add.next = node.next;
            }
            return result;
        }
        public static int[] Decrypt(int[] code, int k)
        {
            // https://leetcode.cn/problems/defuse-the-bomb/description/
            int curr = 0;
            int[] result = new int[code.Length];
            if(k > 0)
            {

                for(int i = 1;i < code.Length + k; i++)
                {
                    // 入
                    if (i >= code.Length)
                    {
                        curr += code[i - code.Length];
                    }
                    else
                    {
                        curr += code[i];
                    }

                    int left = i - k;       // 这里的left不需要加1了  因为是不带自己这个的
                    if(left < 0)
                        continue;
                    
                    // 更新
                    result[left] = curr;
                    

                    // 出
                    if(left + 1 >= code.Length)
                        curr -= code[left + 1 - code.Length];
                    else
                        curr -= code[left + 1];
                }
            }
            else if (k < 0)
            {
                // TODO
                for (int i = 0 + k + 1; i < code.Length; i++)
                {
                    // 入
                    if (i <= 0)
                        curr += code[i - 1 + code.Length];
                    else
                        curr += code[i - 1];

                    if (i < 0)
                        continue;
                    // 更新
                    result[i] = curr;

                    // 出
                    if (i + k < 0)
                        curr -= code[i + k + code.Length];
                    else
                        curr -= code[i + k];
                }
            }          
            return result;
        }
        public static int MinSwaps(int[] nums)
        {
            // https://leetcode.cn/problems/minimum-swaps-to-group-all-1s-together-ii/description/
            int k = nums.Sum();
            int min = int.MaxValue;
            int curr = 0;
            for (int i = 0; i < nums.Length + k; i++)
            {
                // 入
                if(i >= nums.Length)
                {
                    if (nums[i - nums.Length] == 0)
                        curr++;
                }
                else
                {
                    if (nums[i] == 0)
                        curr++;
                }
                
                int left = i - k + 1;
                if (left < 0)
                    continue;

                // 更新
                min = Math.Min(curr, min);

                // 出
                if(left >= nums.Length)
                {
                    if (nums[left - nums.Length] == 0)
                        curr--;
                }
                else
                {
                    if (nums[left] == 0)
                        curr--;
                }                
            }
            return min;
        }
        public static long MaxProfit(int[] prices, int[] strategy, int k)
        {
            // https://leetcode.cn/problems/best-time-to-buy-and-sell-stock-using-strategy/description/
            // 不能AC   我还是两次for的思路 纯纯的不行
            long total = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                total += prices[i] * strategy[i];
            }

            long max = total;
            for (int i = 0;i <= prices.Length - k; i++)
            {
                long curr = 0;
                long currOrigin = 0;
                for (int j = 0; j < k; j++)
                {
                    if(j >= k / 2)       // 后 k/2个是1
                    {
                        curr += prices[i + j];
                    }
                    currOrigin += prices[i + j] * strategy[i + j];
                }
                max = Math.Max(max, curr + total - currOrigin);
            }
            return max;
        }
        public static long MaxProfit2(int[] prices, int[] strategy, int k)
        {
            long total = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                total += prices[i] * strategy[i];
            }


            long curr = 0;
            long currOrigin = 0;
            long max = total;
            for (int i = 0; i < prices.Length; i++)
            {
                // 入
                if(i >= k / 2)
                    curr += prices[i];
                currOrigin += prices[i] * strategy[i];

                int left = i - k + 1;
                if (left < 0)
                    continue;

                // 更新
                max = Math.Max(max, curr + total - currOrigin);


                // 出
                currOrigin -= prices[left] * strategy[left];
                if(left + k / 2 < prices.Length)
                    curr -= prices[left + k / 2];
            }
            return max;
        }
        public static int MaxSatisfied(int[] customers, int[] grumpy, int minutes)
        {
            // https://leetcode.cn/problems/grumpy-bookstore-owner/description/
            int total = 0;
            for (int i = 0; i < customers.Length; i++)
            {
                if (grumpy[i] == 0)
                    total += customers[i];
            }
            if (minutes >= customers.Length)
                return customers.Sum();

            int max = 0;
            int curr = 0;
            int currTotal = 0;
            for (int i = 0; i < customers.Length; i++)
            {
                // 入
                curr += customers[i];
                if (grumpy[i] == 0)             // 记录当前滑动窗口的和  根据老板的开心度
                    currTotal += customers[i];

                int left = i - minutes + 1;
                if (left < 0)
                    continue;
                // 更新
                max = Math.Max(max, curr + total - currTotal );

                // 出
                curr -= customers[left];
                if (grumpy[left] == 0)             
                    currTotal -= customers[left];
            }
            return max;
        }
        public static int MaxScore(int[] cardPoints, int k)
        {
            
            int total = 0;
            if(k == cardPoints.Length)
                return cardPoints.Sum();
            
            int l = cardPoints.Length - k;
            int curr = 0;
            int min = int.MaxValue;

            for (int i = 0; i < cardPoints.Length; i++) 
            {
                // 入
                curr += cardPoints[i];
                total += cardPoints[i];

                int left = i - l + 1;
                if (left < 0)
                    continue;
                // 更新
                min = Math.Min(min, curr);

                // 出
                curr -= cardPoints[left];
            }
            return total - min;
        }
        public static long MaximumSubarraySum(int[] nums, int k)
        {
            // https://leetcode.cn/problems/maximum-sum-of-distinct-subarrays-with-length-k/description/
            long max = 0;
            long curr = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                // 入
                if (dict.ContainsKey(nums[i]))
                    dict[nums[i]]++;
                else
                    dict[nums[i]] = 1;
                curr += nums[i];

                // 更新
                int left = i - k + 1;
                if (left < 0)
                    continue;
                if(dict.Count == k)
                    max = Math.Max(max, curr);

                // 出
                if (dict.ContainsKey(nums[left]) && dict[nums[left]] != 1)
                    dict[nums[left]]--;
                else
                    dict.Remove(nums[left]);
                curr -= nums[left];
            }
            return max;
        }
        public static long MaxSum(IList<int> nums, int m, int k)
        {
            // https://leetcode.cn/problems/maximum-sum-of-almost-unique-subarray/description/
            // 一定要熟悉  1.入  2.更新  3.出
            long max = 0;
            long curr = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Count; i++)
            {
                // 入
                if (dict.ContainsKey(nums[i]))
                    dict[nums[i]]++;
                else
                    dict[nums[i]] = 1;
                curr += nums[i];

                int left = i - k + 1;
                if (left < 0)
                    continue;

                // 更新
                if (dict.Count >= m)
                    max = Math.Max(max, curr);

                // 出
                if (dict.ContainsKey(nums[left]) && dict[nums[left]] != 1)
                    dict[nums[left]]--;
                else
                    dict.Remove(nums[left]);
                curr -= nums[left];
            }
            return max;
        }
        public static int MinimumRecolors(string blocks, int k)
        {
            // https://leetcode.cn/problems/minimum-recolors-to-get-k-consecutive-black-blocks/description/

            int min = 0;
            int i = 0;
            int curr = 0;
            for (; i < k; i++)
            {
                if (blocks[i] == 'W')
                    curr++;
            }
            min = curr;

            while (i < blocks.Length)
            {
                if (blocks[i] == 'W')
                    curr++;
                if (blocks[i - k] == 'W')
                    curr--;
                min = Math.Min(min, curr);
                i++;
            }
            return min;

        }
        public static int[] GetAverages(int[] nums, int k)
        {
            // https://leetcode.cn/problems/k-radius-subarray-averages/description/
            int[] result = new int[nums.Length];

            if(2 * k + 1 > nums.Length)
            {
                Array.Fill(nums, -1);
                return nums;
            }
            long sum = 0;           // 一定要注意溢出的问题
            int i = 0;
            for (; i < k; i++)
            {
                result[i] = -1;            
            }

            for(int j = 0; j < i * 2 + 1; j++)      // 初始化sum
            {
                sum += nums[j];
            }

            for (; i < nums.Length - k; i++)
            {
                result[i] = (int)(sum / (2 * k + 1));
                if(i + k + 1 < nums.Length)
                    sum += nums[i + k + 1] - nums[i - k];
            }

            for(; i < nums.Length; i++)
            {
                result[i] = -1;
            }
            return result;
        }
        public static int NumOfSubarrays(int[] arr, int k, int threshold)
        {
            // https://leetcode.cn/problems/number-of-sub-arrays-of-size-k-and-average-greater-than-or-equal-to-threshold/description/
            int i = 0;
            int result = 0;
            int sum = 0;
            int thresholdSum = threshold * k;
            for (; i < k; i++)
            {
                sum += arr[i];
            }
            if (sum >= thresholdSum)
                result++;

            while (i < arr.Length)
            {
                sum += arr[i] - arr[i - k];
                if(sum >= thresholdSum)
                    result++;
                i++;
            }
            return result;
        }
        public static double FindMaxAverage(int[] nums, int k)
        {
            // https://leetcode.cn/problems/maximum-average-subarray-i/description/
            int currSum = 0;
            int max = int.MinValue;
            int i = 0;
            for (; i < k; i++)
            {
                currSum += nums[i];
            }
            max = currSum;

            i = 0;
            while (i + k < nums.Length)
            {
                currSum += nums[i + k] - nums[i];
                if (currSum > max)
                {
                    max = currSum;
                }
                i++;
            }
            return max / (double)k;
        }
        public static int Largest1BorderedSquare(int[][] grid)
        {
            // https://leetcode.cn/problems/largest-1-bordered-square/description/

            int[,] right = new int[grid.Length, grid[0].Length];
            int[,] down = new int[grid.Length, grid[0].Length];

            // 填写辅助数组
            for (int i = 0; i < grid.Length; i++)
            {
                int temp = 0;
                for (int j = grid[0].Length - 1; j >= 0; j--)
                {
                    if (grid[i][j] == 1)
                        temp++;
                    else
                        temp = 0;
                    right[i, j] = temp;
                }
            }
            for (int j = 0; j < grid[0].Length; j++)
            {
                int temp = 0;
                for (int i = grid.Length - 1; i >= 0; i--)
                {
                    if (grid[i][j] == 1)
                        temp++;
                    else
                        temp = 0;
                    down[i, j] = temp;
                }
            }

            int max = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    for (int k = 1; k - 1 < Math.Min(grid.Length - i, grid[0].Length - j); k++)
                    {
                        if (right[i, j] >= k && down[i, j] >= k && right[i + k - 1, j] >= k && down[i, j + k - 1] >= k)
                        {
                            max = Math.Max(max, k);
                        }
                    }
                }
            }
            if (max == 0)
                return 0;
            return max * max;
        }
        public static int MinPaint(string str)
        {
            // 有一些排成一行的正方形。每个正方形已经被染成红色和绿色。现在可以选择任意一个正方形然后用这两种颜色的任意一种进行染色，这个正方形的颜色将被覆盖。
            // 目标是在完成染色之后，每个红色R都比每个绿色G距离最左侧近。返回最少需要涂染多少正方形
            // 例如：s = RGRGR，我们涂染之后变成RRRGG满足要求了，涂染的个数为2，没有比这个更好的涂染方案
            // 前缀和
            int[] frontR = new int[str.Length];
            int[] frontG = new int[str.Length];
            int[] backG = new int[str.Length];
            int countR = 0;
            int countG = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == 'R')
                    countR++;
                else
                    countG++;                    
                frontR[i] = countR;
                frontG[i] = countG;
            }

            for (int i = 0;i < str.Length;i++)
            {
                backG[i] = frontG[str.Length - 1] - frontG[i];
            }

            int minIndex = 0;
            int minValue = int.MaxValue;
            for (int i = 0; i < str.Length; i++)
            {
                int temp = frontR[i] + backG[i];
                if (minValue > temp)
                {
                    minIndex = i;
                    minValue = temp;
                }
            }
            return minValue;
        }
        public static bool EatGrass(int n)
        {
            // 草一共有n的重量，两只牛轮流吃草，A牛先吃，B牛后吃
            // 每只牛在自己的回合，吃草的重量必须是4的幂，1、4、16、64....
            // 谁在自己的回合正好把草吃完谁赢，根据输入的n，返回谁赢
            return EatGrassRecur(n, n);
        }
        static bool EatGrassRecur(int rest, int n)
        {
            if(rest == 0)
                return false;
            for(int pow4 = 1;pow4 <= rest; pow4 <<= 2)
            {
                if(rest == pow4)
                    return true;
                if (!EatGrassRecur(rest - pow4, n))
                    return true;
                if(pow4 > n / 4)
                    break;
            }
            return false;
        }
        public static bool EatGrass2(int n)
        {
            // 打表法出规律后直接写
            if (n % 5 == 0 || n % 5 == 2)
                return false;
            else
                return true;
        }
        public static int AppleMinBags(int n)
        {
            // 有装下8个苹果的袋子、装下6个苹果的袋子，一定要保证买苹果时所有使用的袋子都装满
            // 对于无法装满所有袋子的方案不予考虑，给定n个苹果，返回至少要多少个袋子
            // 如果不存在每个袋子都装满的方案返回-1

            if (n % 2 != 0)
                return -1;
            int x = n / 8;
            for (int i = x; i >= 0; i--)
            {
                int rest = n - i * 8;
                if(rest >= 24)               // 大于等于24的时候就不用再判断了  因为8和6的最小公倍数是24  3个8比4个6用的袋子数更少
                    break;
                if(rest % 6 == 0)
                {
                    return i + rest / 6;
                }
            }
            return -1;
        }
        public static int CordCoverMaxPoint(int[] points, int l)
        {
            // 给定一个有序数组arr，代表数轴上个从左到右有n个点arr[0]、arr[1]...arr[n - 1]
            // 给定一个正数L，代表一根长度为L的绳子，求绳子最多能覆盖其中的几个点
            int slow = 0;
            int fast = 0;
            int res = 0;
            while(fast < points.Length)
            {
                // 只要窗口差 ≤ l 就扩张
                if (points[fast] - points[slow] <= l)
                {
                    res = Math.Max(res, fast - slow + 1);
                    fast++;
                }
                else
                {
                    slow++;
                }
            }
            return res;
        }
        public static string IntToRoman(int num)
        {
            return "";
        }
        public static int MaxArea(int[] height)
        {
            // https://leetcode.cn/problems/container-with-most-water/description/
            #region 暴力解法无法AC
            //int max = 0;
            //for (int i = 0; i < height.Length; i++)
            //{
            //    for (int j = i + 1; j < height.Length; j++)
            //    {
            //        max = Math.Max(max, (j - i) * Math.Min(height[i], height[j]));
            //    }
            //}
            //return max;
            #endregion
            #region 双指针
            int i = 0;
            int j = height.Length - 1;
            int max = 0;
            while(i != j)
            {
                int small = height[i] <= height[j] ? i : j;     // 短板 
                max = Math.Max(max, (j - i) * height[small]);
                if (height[i] <= height[j])
                {
                    i++;
                }
                else
                {
                    j--;
                }
            }
            return max;
            #endregion
        }
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
            // AI 写的代码好好看一下 优雅
            int n = nums.Length;
            ulong[] prefixSum = new ulong[n + 1];
            Stack<int> stack = new Stack<int>();
            Dictionary<int, (int left, int right)> dict = new Dictionary<int, (int left, int right)>();

            // 计算前缀和
            for (int i = 0; i < n; i++)
            {
                prefixSum[i + 1] = prefixSum[i] + (ulong)nums[i];
            }

            // 使用单调栈处理左右边界
            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0 && nums[stack.Peek()] >= nums[i])
                {
                    int top = stack.Pop();
                    int left = stack.Count > 0 ? stack.Peek() : -1;
                    dict[top] = (left, i);
                }
                stack.Push(i);
            }

            // 处理栈中剩余的元素
            while (stack.Count > 0)
            {
                int top = stack.Pop();
                int left = stack.Count > 0 ? stack.Peek() : -1;
                dict[top] = (left, n);
            }

            // 计算最大乘积
            ulong maxProduct = 0;
            foreach (var pair in dict)
            {
                int index = pair.Key;
                (int left, int right) = pair.Value;
                ulong sum = prefixSum[right] - prefixSum[left + 1];
                ulong product = sum * (ulong)nums[index];
                maxProduct = Math.Max(maxProduct, product);
            }
            return (int)(maxProduct % (1000000000 + 7));
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
