using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class Array_
    {
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
