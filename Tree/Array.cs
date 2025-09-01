using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class Array_
    {
        
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
