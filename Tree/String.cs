using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class String_
    {
        public static string LongestPalindrome(string s)
        {
            // https://leetcode.cn/problems/longest-palindromic-substring/description/
            for (int i = s.Length; i > 0; i--)
            {
                for (int index = 0; index + i <= s.Length; index++)
                {
                    string str = s.Substring(index, i);
                    if (IsPalindrome(str))
                    {
                        return str;
                    }
                }
            }
            return "";
        }
        public static bool IsPalindrome(string str)
        {
            // 判断是否是回文串
            char[] chars = str.ToCharArray();
            Stack<char> stack = new Stack<char>();
            foreach (char c in chars)
            {
                stack.Push(c);
            }
            StringBuilder sb = new StringBuilder();
            while (stack.Count > 0)
            {
                sb.Append(stack.Pop());
            }
            if (sb.ToString() == str)
            {
                return true;
            }
            return false;
        }
        public static String LongestPalindrome2(string s)
        {
            if (s == null || s.Length == 0)
                return "";
            int left = 0;
            int right = 0;
            int len = 1;
            int maxStart = 0;
            int maxLen = 0;
            for (int i = 0; i < s.Length; i++)
            {
                left = i - 1;
                right = i + 1;
                while(left >= 0 && s[left] == s[i])
                {
                    left --;
                    len++;
                }
                while(right < s.Length && s[right] == s[i])
                {
                    right ++; 
                    len++;
                }
                while(left >= 0 && right < s.Length && s[left] == s[right])
                {
                    len += 2;
                    left --;
                    right ++;
                }
                if (len > maxLen)
                {
                    maxLen = len; 
                    maxStart = left;
                }
                len = 1;

            }
            return s.Substring(maxStart, len);
        }
        public static int LengthOfLongestSubstring(string s)
        {
            // https://leetcode.cn/problems/longest-substring-without-repeating-characters/description/
            if (s == null) return 0;

            HashSet<char> record = new HashSet<char>();
            bool isBreak = false;
            int i = 1;
            for (; i <= s.Length; i++)
            {
                isBreak = false;
                int index = 0;
                for (; index + i - 1 < s.Length; index++)
                {
                    string sub = s.Substring(index, i);
                    char[] chars = sub.ToCharArray();
                    record.Clear();
                    foreach (char c in chars)
                    {
                        if (record.Contains(c))
                        {
                            break;
                        }
                        else
                        {
                            record.Add(c);
                            if(record.Count == chars.Length)
                                isBreak = true;
                        }
                    }
                    if(isBreak)
                        break;
                }

                // 扫了一遍都没有就退出了
                if(index + i - 1 == s.Length)
                    break;
            }
            return i - 1;
        }
        public static int LengthOfLongestSubstring2(string s)
        {
            // 滑动窗口的思想   抽象的  好好体会一下
            if (s == null || s.Length == 0)
                return 0;
            int left = 0;
            int max = 0;
            Dictionary<char, int> record = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++) 
            {
                if (record.ContainsKey(s[i]))
                    left = Math.Max(left, record[s[i]] + 1);
                record[s[i]] = i;
                max = Math.Max(max, i - left + 1);
            }
            return max;
        }
    }
}
