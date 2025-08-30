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
        public static void ReverseStack(Stack<int> stack)
        {
            
        }
        public static bool PredictTheWinner(int[] nums)
        {
            // 感觉可以用dict去优化一下
            int first = FirstPredictTheWinner(nums, 0, nums.Length - 1);
            int second = SecondPredictTheWinner(nums, 0, nums.Length - 1);
            return first >= second;
        }

        public static int FirstPredictTheWinner(int[] arr, int L, int R)
        {
            if(L == R)
                return arr[L];
            int left = arr[L] + SecondPredictTheWinner(arr, L + 1, R);
            int right = arr[R] + SecondPredictTheWinner(arr, L, R - 1);
            return Math.Max(left, right);       // 我先手 肯定拿max
        }
        public static int SecondPredictTheWinner(int[] arr, int L, int R)
        {
            if (L == R)
                return 0;               // 这里也要注意
            int left = FirstPredictTheWinner(arr, L + 1, R);
            int right = FirstPredictTheWinner(arr, L, R - 1);
            return Math.Min(left, right);       // 我后手  别人肯定只能让我拿最小的
        }
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
