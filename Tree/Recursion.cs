using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class Recursion
    {
        static int result = 0;

        public static int TranslateToStr(string str)
        {
            TranslateToStrRecur(str, 0, "");
            return result;
        }
        public static void TranslateToStrRecur(string str, int index, string former)
        {
            if(index == str.Length)
            {
                result++;
                return;
            }
            if (str[0] == '0')
                return;
            // 这里还可以再优化一下  如果是3到9就不可能选两位了
            if (index + 1 == str.Length || str[index + 1] != '0')               // 取一位
            {
                char offset = (char)('A' + str[index] - '0' - 1);
                string a = former + offset;         // 这里要注意  "1"使用(int）强行转换会变成asc码
                TranslateToStrRecur(str, index + 1, a);
            }

            if (index + 1 == str.Length)
                return;        

            if (index + 2 < str.Length && str[index + 2] != '0')                // 取两位
            {
                char b = (char)('A' + int.Parse(str.Substring(index, index + 1)) - 1);
                if (b >= 'A' && b <= 'Z')
                {
                    TranslateToStrRecur(str, index + 2, former + b);
                }
            }
            else if(index + 2 == str.Length)
            {
                char b = (char)('A' + int.Parse(str.Substring(index, index + 1)) - 1);
                if (b >= 'A' && b <= 'Z')
                {
                    TranslateToStrRecur(str, index + 2, former + b);
                }
            }
            
        }
        public static int TranslateToStr2(string str)
        {
            // 左神优化版
            TranslateToStrRecur2(str, 0);
            return result;
        }
        public static int TranslateToStrRecur2(string str, int i)
        {
            if (i == str.Length)
                return 1;
            if (str[i] == '0')                      // 左神的思想是到了为0的才“修剪”
                return 0;

            if (str[i] ==  '1')
            {
                int res = TranslateToStrRecur2(str, i + 1);
                if (i + 1 < str.Length)
                    res += TranslateToStrRecur2(str, i + 2);
                return res;
            }

            if (str[i] == '2')
            {
                int res = TranslateToStrRecur2(str, i + 1);
                if (i + 1 < str.Length && str[i + 1] > '1' && str[i + 1] < '6')
                    res += TranslateToStrRecur2(str, i + 2);
                return res;
            }
            return TranslateToStrRecur2(str, i + 1);
        }
        public static void ReverseStack(Stack<int> stack)
        {
            // 这种递归的思路又和之前的不一样了
            // 逆序一个栈 不使用额外的数据结构
            if(stack.Count == 0)
                return;
            int i = GetStackBottom(stack);
            ReverseStack(stack);
            stack.Push(i);
        }
        public static int GetStackBottom(Stack<int> stack)
        {
            int result = stack.Pop();
            if (stack.Count == 0)
                return result;
            else
            {
                int last = GetStackBottom(stack);
                stack.Push(result);
                return last;
            }
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
