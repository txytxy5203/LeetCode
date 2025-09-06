using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class Math_
    {
        public static int Reverse(int x)
        {
            // https://leetcode.cn/problems/reverse-integer/description/
            Queue<int> q = new Queue<int>();
            long result = 0;
            while(x != 0)
            {
                int remainder = x % 10;
                x = x / 10;
                q.Enqueue(remainder);
            }

            // 只有第一次是0的时候才做相关的操作
            bool isCheck = true;
            while(q.Count > 0)
            {
                if (q.Peek() == 0 && isCheck)
                { 
                    q.Dequeue();
                    continue;
                }
                result = result * 10 + q.Dequeue();
                isCheck = false;
                if (result > int.MaxValue || result < int.MinValue)     // 越界的判断
                    return 0;
            }
            
            return (int)result;
        }
    }
}
