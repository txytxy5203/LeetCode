using System.Text;

namespace LeetCode
{
    public static class String_
    {
        public static IList<int> FindAnagrams(string s, string p)
        {
            // 
        }
        public static bool CheckInclusion(string s1, string s2)
        {
            // https://leetcode.cn/problems/permutation-in-string/description/
            
            if(s1.Length > s2.Length)
                return false;
            int[] s1Counts = new int[26];
            int[] s2Counts = new int[26];

            foreach (var c in s1)
            {
                s1Counts[c - 'a']++;
            }
            
            for (int i = 0; i < s2.Length; i++)
            {
                // in
                s2Counts[s2[i] - 'a']++;

                int left = i - s1.Length + 1;
                if (left < 0)
                    continue;
                // update
                if (AreCountsEqual(s1Counts, s2Counts))
                    return true;

                // out
                s2Counts[s2[left] - 'a']--;
            }
            return false;
        }
        static bool AreCountsEqual(int[] count1, int[] count2)
        {
            for (int i = 0; i < count1.Length; i++) {
                if (count1[i] != count2[i])
                    return false;   
            }
            return true;
        }
        public static int MaxFreq(string s, int maxLetters, int minSize, int maxSize)
        {
            // https://leetcode.cn/problems/maximum-number-of-occurrences-of-a-substring/description/ 
            Dictionary<char, int> dict = new Dictionary<char, int>();
            Dictionary<string, int> result = new Dictionary<string, int> ();
            for (int i = 0; i < s.Length; i++)
            {
                // 入
                if (dict.ContainsKey(s[i]))
                    dict[s[i]]++;
                else
                    dict[s[i]] = 1;

                int left = i - minSize + 1;
                if(left < 0)
                    continue;
                // update
                if (dict.Count <= maxLetters)
                {
                    string str = s.Substring(left, minSize);
                    if (result.ContainsKey(str))
                        result[str]++;
                    else
                        result[str] = 1;
                }
                // out
                if (dict[s[left]] == 1)
                    dict.Remove(s[left]);
                else
                    dict[s[left]]--;
            }
            return result.Count == 0 ? 0 : result.Values.Max();
        }
        public static int MaxVowels(string s, int k)
        {
            // https://leetcode.cn/problems/maximum-number-of-vowels-in-a-substring-of-given-length/description/
            Queue<char> queue = new Queue<char>();
            int i = 0;
            int curr = 0;
            int max = 0;
            for (; i < k; i++)
            {
                if ("aeiou".Contains(s[i]))
                    curr++;
                queue.Enqueue(s[i]);
            }
            max = Math.Max(max, curr);

            while(i < s.Length)
            {
                char outChar = queue.Dequeue();
                if("aeiou".Contains(outChar))
                    curr--;

                queue.Enqueue(s[i]);
                if ("aeiou".Contains(s[i]))
                {
                    curr++;
                }
                max = Math.Max(max, curr);
                if(max == k)
                    return max;
                i++;
            }
            return max;
        }
        public static int MyAtoi(string s)
        {
            // https://leetcode.cn/problems/string-to-integer-atoi/description/
            int len = s.Length;
            int index = 0;
            // 1 去除 ' '
            while (index < len && s[index] == ' ')
            {
                index++;
            }

            // 2 index == len ?
            if (index == len)
                return 0;

            // 3 
            int sign = 1;
            if (s[index] == '-')
            {
                index++;
                sign = -1;
            }
            else if (s[index] == '+')
            {
                index++;
            }

            // 4 
            int res = 0;
            while(index < len)
            {
                char curr = s[index];
                if (s[index] < '0' || s[index] > '9')
                    break;

                // 题目中说：环境只能存储 32 位大小的有符号整数，因此，需要提前判断乘以 10 以后是否越界
                if (res > int.MaxValue / 10 || (res == int.MaxValue / 10 && (curr - '0') > int.MaxValue % 10))
                {
                    return int.MaxValue;
                }
                if (res < int.MinValue / 10 || (res == int.MinValue / 10 && (curr - '0') > -(int.MinValue % 10)))
                {
                    return int.MinValue;
                }
                res = res * 10 + sign * (curr - '0');
                index++;
            }
            return res;
            
        }
        public static string Convert(string s, int numRows)
        {
            // https://leetcode.cn/problems/zigzag-conversion/description/

            if (numRows == 1)
                return s;

            // 先确定二维数组的大小
            char[,] chars;
            int oneGroup = 2 * (numRows - 1);                   // 每一组的数量
            int x = s.Length / oneGroup * (numRows - 1);
            int y = s.Length % oneGroup;
            if (y < numRows)
            {
                chars = new char[numRows, x + 1];
            }
            else
            {
                chars = new char[numRows, x + y - numRows + 2];
            }

            // 向数组填值            就是确定在二维数组中的位置  细心一点就行了
            for (int i = 0; i < s.Length; i++)
            {
                int col;
                int row;

                int groupIndex = i % oneGroup;            // 在当前组所处的位置
                if (groupIndex < numRows)
                {
                    col = i / oneGroup * (numRows - 1);
                    row = groupIndex;
                }
                else
                {
                    col = i / oneGroup * (numRows - 1) + groupIndex - numRows + 1;
                    row = oneGroup - groupIndex;
                }
                chars[row, col] = s[i];
            }

            // 拼接
            StringBuilder sb = new StringBuilder();
            foreach (char c in chars)
            {
                if (c == '\0')
                    continue;
                sb.Append(c);
            }
            return sb.ToString();
        }
        public static string Convert2(string s, int numRows)
        {
            // https://leetcode.cn/problems/zigzag-conversion/solutions/21610/zzi-xing-bian-huan-by-jyd/
            // 6666666666
            if (numRows == 1)
                return s;
            List<StringBuilder> list = new List<StringBuilder>();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < numRows; i++)
            {
                list.Add(new StringBuilder());
            }

            int index = 0;
            bool flag = false;
            for (int i = 0; i < s.Length; i++)
            {

                list[index].Append(s[i]);
                if ((i % (numRows - 1) == 0))
                    flag = !flag;
                if (flag)
                    index++;
                else
                    index--;
            }
            foreach (var sb in list)
            {
                result.Append(sb);
            }
            return result.ToString();
        }
        public static int KMP(string s, string m)
        {
            // 再好好体会一下KMP算法的过程
            // O(n)
            if (s == null || s.Length == 0 || m == null || m.Length == 0)
                return -1;
            int[] next = GetNextArray(m);
            int i1 = 0;
            int i2 = 0;
            while (i1 < s.Length && i2 < m.Length)
            {
                if (s[i1] == m[i2])         // 对应位置上的字符相等 ++
                {
                    i1++;
                    i2++;
                }
                else if (i2 == 0)           // i2回到了0位置
                {
                    i1++;
                }
                else
                {
                    i2 = next[i2];
                }
            }
            return i2 == m.Length ? i1 - i2 : -1;
        }
        static int[] GetNextArray(string m)
        {
            if (m.Length == 1)
                return new int[] { -1 };
            int[] next = new int[m.Length];
            next[0] = -1;
            next[1] = 0;
            int i = 2;
            int cn = 0;
            while (i < m.Length)
            {
                if (m[i - 1] == m[cn])
                {
                    cn++;
                    next[i] = cn;
                    i++;
                }
                else if (cn > 0)
                {
                    cn = next[cn];
                }
                else
                {
                    next[i] = 0;
                    i++;
                }
            }
            return next;
        }
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
                while (left >= 0 && s[left] == s[i])
                {
                    left--;
                    len++;
                }
                while (right < s.Length && s[right] == s[i])
                {
                    right++;
                    len++;
                }
                while (left >= 0 && right < s.Length && s[left] == s[right])
                {
                    len += 2;
                    left--;
                    right++;
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
        public static String LongestPalindrome3(string s)
        {
            // 马拉车算法  Manacher
            #region 左神的写法
            //if (s == null || s.Length == 0)
            //    return 0;
            //string str = "#" + string.Join("#", "abc".ToCharArray()) + "#";         // 把s处理成中间加上#的字符串
            //int[] pArr = new int[s.Length];         // 回文半径数组
            //int c = -1;         // 中心
            //int r = -1;         // 回文右边界的再往右一个位置   最右的有效区是R-1位置
            //int curr = int.MinValue;
            //for (int i = 0; i != str.Length; i++)
            //{
            //    // i至少的回文区域  先给pArr[i]
            //    pArr[i] = r > i ? Math.Min(pArr[2 * c - i], r - i) : 1;
            //    while (i + pArr[i] < str.Length && i - pArr[i] > -1)
            //    {
            //        if (str[i + pArr[i]] == str[i - pArr[i]])
            //            pArr[i]++;
            //        else
            //            break;
            //    }
            //    if (i + pArr[i] > r)
            //    {
            //        r = i + pArr[i];
            //        c = i;
            //    }
            //    curr = Math.Max(curr, pArr[i]);
            //}
            //return curr - 1;
            #endregion

            #region 自己写的
            //if (s == null || s.Length == 0)
            //    return "";
            //string str = "#" + string.Join("#", s.ToCharArray()) + "#";         // 把s处理成中间加上#的字符串
            //int[] pArr = new int[str.Length];         // 回文半径数组       "aba" 回文半径为1  我这里和左神的定义有点区别
            //int c = -1;         // 中心
            //int r = -1;         // 回文最大右边界
            //string curr = "";

            //for (int i = 0; i < str.Length; i++)
            //{
            //    if(i >= r)
            //    {
            //        int index = 1;
            //        while (i - index >=0 && i + index < str.Length && str[i - index] == str[i + index])     // 向外扩展
            //        {
            //            index++;
            //        }
            //        pArr[i] = index - 1;
            //        if(r < i + index - 1)   // 更新右边界
            //        {
            //            r = i + index - 1;                       
            //            c = i;
            //        }
            //        curr = 2 * index - 1 > curr.Length ? str.Substring(i  - index + 1, 2 * index - 1) : curr;
            //    }
            //    else
            //    {
            //        if (pArr[2 * c - i] < r - i)
            //        {
            //            pArr[i] = pArr[2 * c - i];
            //        }
            //        else if(pArr[2 * c - i] > r - i)
            //        {
            //            pArr[i] = r - i;
            //        }
            //        else
            //        {
            //            int index = r - i;          // 这里可以直接从r开始扩展
            //            while (i - index >= 0 && i + index < str.Length && str[i - index] == str[i + index])     // 向外扩展
            //            {
            //                index++;
            //            }
            //            pArr[i] = index - 1;
            //            if (r < i + index - 1)   // 更新右边界
            //            {
            //                r = i + index - 1;
            //                c = i;
            //            }
            //            curr = 2 * index - 1 > curr.Length ? str.Substring(i - index + 1, 2 * index - 1) : curr;
            //        }
            //    }
            //}
            //return curr.Replace("#", "");
            #endregion


            #region AI优化的
            if (string.IsNullOrEmpty(s))
                return "";

            // 预处理字符串，插入特殊字符
            string str = "#" + string.Join("#", s.ToCharArray()) + "#";
            int[] pArr = new int[str.Length];
            int c = -1; // 中心
            int r = -1; // 回文最大右边界
            int maxLen = 0;
            int centerIndex = -1;

            for (int i = 0; i < str.Length; i++)
            {
                int mirror = 2 * c - i; // 镜像位置
                if (i < r)
                {
                    pArr[i] = Math.Min(r - i, pArr[mirror]);
                }

                // 尝试扩展回文       统一往外扩展   省去了if else的逻辑
                while (i + (pArr[i] + 1) < str.Length && i - (pArr[i] + 1) >= 0 && str[i + (pArr[i] + 1)] == str[i - (pArr[i] + 1)])
                {
                    pArr[i]++;
                }

                // 更新中心和右边界
                if (i + pArr[i] > r)
                {
                    c = i;
                    r = i + pArr[i];
                }

                // 更新最长回文子串
                if (pArr[i] > maxLen)
                {
                    maxLen = pArr[i];
                    centerIndex = i;
                }
            }

            // 提取最长回文子串
            int start = (centerIndex - maxLen) / 2;
            return s.Substring(start, maxLen);
            #endregion

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
                            if (record.Count == chars.Length)
                                isBreak = true;
                        }
                    }
                    if (isBreak)
                        break;
                }

                // 扫了一遍都没有就退出了
                if (index + i - 1 == s.Length)
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

