using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        #region 位运算
        public static int Chu(int num1, int num2)
        {
            int result = 0;
            while(num1 >= 0)
            {
                num1 = Minus(num1, num2);
                result++;
            }
            return result - 1;
        }
        public static int Multi(int num1, int num2)
        {
            int res = 0;
            while(num2 != 0)
            {
                if((num2 & 1) != 0)         // num2的最右边为1  则需要相加
                {
                    res = Add(res, num1);
                }
                num1 <<= 1;
                num2 >>>= 1;                // 无符号右移
            }
            return res;
        }
        public static int Minus(int num1, int num2)
        {
            return Add(num1, RegNum(num2));
        }
        public static int RegNum(int num)
        {
            return Add(~num, 1);            //求相反数
        }
        public static int Add(int num1, int num2)
        {
            // 不使用算术运算符  实现加法
            return AddRecur(num1, num2);            // 一直半加 和 进位  直到进位为0
        }
        public static int AddRecur(int add, int and)
        {
            if (and == 0)
                return add;
            int tempAdd = add;
            int tempAnd = and;
            add = tempAdd ^ tempAnd;
            and = (tempAdd & tempAnd) << 1;
            return AddRecur(add, and);
        }
        public static bool IsPowerOfFour(int num)
        {
            // 如果是4的幂 那么唯一的1一定在基数位上
            // 直接与  01010101010101010101 与    不等于0的话就是4的幂
            return IsPowerOfTwo(num) && (num & 0x55555555) != 0;
        }
        public static bool IsPowerOfTwo(int num)
        {
            //int mostRightOne = num & -num;      // 最右边的1
            //return mostRightOne == num;         // 再判断是否和nun相等
            #region 方法二
            int temp = num - 1;
            return (temp & num) == 0;
            #endregion
        }
        public static int Max(int a, int b)
        {
            // 不使用比较运算符号  返回较大的那个数
            int cha = a - b;                // 这里的 a - b 可能会溢出
            int sign = Sign(cha);          // 这里一定要 & 1    因为复数 >> 后会变成  111111111
            int flip = Flip(sign);
            return sign * b + flip * a;
        }
        public static int Max2(int a, int b)
        {
            // 防止 a - b 溢出
            int c = a - b;
            int sa = Sign(a);
            int sb = Sign(b);
            int sc = Sign(c);
            int difSab = sa ^ sb;
            int sameSab = Flip(difSab);
            int returnA = sameSab * sc + difSab * sa;
            int returnB = Flip(returnA);
            return returnA * a + returnB * b;           // 实际上利用这个互斥式子来实现ifelse
        }
        static int Sign(int a)
        {
            return (a >> 31 & 1) ^ 1;
        }
        static int Flip(int a)
        {
            // 输入必须是1
            return a ^ 1;
        }
        #endregion
    }
}
