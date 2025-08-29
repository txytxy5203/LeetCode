using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{

    public static class Recursion
    {
        public static void AllRank(string str)
        {
            // 再好好理解一下  我觉得写的比左神更好理解一些
            bool[] isChoosed = new bool[str.Length]; 
            AllRankRecur(isChoosed, "", str);
        }
        public static void AllRankRecur(bool[] isChoosed, string strFormer, string str)
        { 
            if(IsAllTrue(isChoosed))
            {
                Console.WriteLine(strFormer);
                return;
            }      
            for (int i = 0;i < str.Length;i++)
            {
                if (isChoosed[i])
                    continue;
                isChoosed[i] = true;
                string b = strFormer + str[i];
                AllRankRecur(isChoosed, b, str);
                isChoosed[i] = false;                   // 回溯
            }
        }
        public static bool IsAllTrue(bool[] isChoosed)
        {
            foreach(bool isTrue in isChoosed) 
            {
                if (!isTrue)
                    return false;
            }
            return true;
        }
        public static void AllSubseries(string str)
        {         
            // 所有的子序列
            AllSubseriesRecur(0, "", str);
        }
        public static void AllSubseriesRecur(int i, string strFormer, string str)
        {
            if(i == str.Length)
            {
                Console.WriteLine(strFormer);
                return;
            }

            // 当前的字符 要么就要 要么就不要
            string a = strFormer + str[i];
            AllSubseriesRecur(i + 1, a, str);
            string b = strFormer;
            AllSubseriesRecur(i + 1, b, str);
        }
        public static int Hnt(int n)
        {
            if (n == 1)
                return 1;
            return 2 * Hnt(n - 1) + 1;
        }
    }
}
