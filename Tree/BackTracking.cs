using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class BackTracking
    {
        public static int TotalNQueens(int n)
        {
            // 返回多少种
            // https://leetcode.cn/problems/n-queens-ii/
            // 依旧是递归的套路  每一层都管下一层要信息
            int[] record = new int[n];
            return TotalNQueensRecur(0, record, n);
        }
        public static int TotalNQueensRecur(int i, int[] record, int n)
        {
            if (i == n)
                return 1;
            int res = 0;
            for (int j = 0; j < n; j++)
            {
                if (IsValid(record, i, j))
                {
                    record[i] = j;
                    res += TotalNQueensRecur(i + 1, record, n);
                }
            }
            return res;
        }
        static bool IsValid(int[] record, int i, int j)
        {
            for (int k = 0; k < i; k++)
            {
                if (record[k] == j)     // 不能共列
                    return false;
                if (Math.Abs(record[k] - j) == Math.Abs(k - i))     // 看斜率
                    return false;
                #region 自己的写法
                //// 往两个斜角方向延申
                //int x = record[k];
                //int y = k;
                //while (x <= j && y <= i)
                //{
                //    if (x == j && y == i)
                //        return false;
                //    x++;
                //    y++;
                //}
                //x = record[k];
                //y = k;
                //while (x >= j && y <= i)
                //{
                //    if (x == j && y == i)
                //        return false;
                //    x--;
                //    y++;
                //}
                #endregion
            }
            return true;
        }
        /// <summary>
        /// 请不要超过32皇后问题
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int TotalNQueens2(int n)
        {
            // 利用位运算加速
            // 左神视频暴力递归的最后面
            if(n < 1 || n > 32)
                return 0;   
            int limit = n == 32 ? -1 : (1 << n) - 1;
            return TotalNQueensRecur2(limit, 0, 0, 0);
        }
        public static int TotalNQueensRecur2(int limit, int colLim, int leftDiaLim, int rightDiaLim)
        {
            if(colLim == limit)
                return 1;
            int mostRightOne = 0;
            int pos = limit & (~(colLim | leftDiaLim | rightDiaLim));
            int res = 0;
            while (pos != 0)
            {
                mostRightOne = pos & (~pos + 1);
                pos -= mostRightOne;
                res += TotalNQueensRecur2(limit, colLim | mostRightOne,
                                          (leftDiaLim | mostRightOne) << 1,
                                          (rightDiaLim | mostRightOne) >> 1);
            }
            return res;
        }
    }
}
