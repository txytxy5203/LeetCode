using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class DP
    {
        public static int CountRoutes(int[] locations, int start, int finish, int fuel)
        {
            // https://leetcode.cn/problems/count-all-possible-routes/description/
            int[,] dp = new int[locations.Length, fuel + 1];
            for (int i = 0; i < locations.Length; i++)
            {
                for (int j = 0;j < fuel + 1; j++)
                {
                    dp[i, j] = -1;
                }
            }
            return CountRoutesRecur(locations, start, 0, finish, fuel, dp);
        }
        static int CountRoutesRecur(int[] locations, int curr, int cost, int finish, int fuel, int[,] dp)
        {
            if (cost > fuel)
                return 0;
            if (dp[curr, cost] != -1)
                return dp[curr, cost];
            int res = 0;
            if (curr == finish && cost <= fuel)
            { 
                res++;                                  // 这里很关键 到了目的地还可以再走出去回来 只要fuel没超过就行
                res %= 1_000_000_007;
            }

            for (int i = 0; i < locations.Length; i++)
            {
                if(i == curr)
                    continue;
                res += CountRoutesRecur(locations, i, cost + Math.Abs(locations[i] - locations[curr]), finish, fuel, dp);
                res %= 1_000_000_007;
            }
            dp[curr, cost] = res;
            return res;
        }
        public static int WalkWays(int start, int end, int k, int N)
        {
            Dictionary<(int, int), int> dict = new Dictionary<(int, int), int>();       // 记忆化搜索   加缓存就行
            return WalkWaysRecur(start, end, k , 0, N, dict);
        }
        static int WalkWaysRecur(int curr, int end, int k, int currK, int N, Dictionary<(int, int), int> dic)
        {
            if (curr == end && currK == k)
                return 1;
            if (currK > k)
                return 0;

            if (dic.ContainsKey((curr, currK)))
            {
                return dic[(curr, currK)];
            }
            if (curr == 1)
            {
                dic[(curr, currK)] = WalkWaysRecur(2, end, k, currK + 1, N, dic);
            }
            if (curr == N)
            {
                dic[(curr, currK)] = WalkWaysRecur(N - 1, end, k, currK + 1, N, dic);
            }
            dic[(curr, currK)] = WalkWaysRecur(curr - 1, end, k, currK + 1, N, dic) 
                               + WalkWaysRecur(curr + 1, end, k, currK + 1, N, dic);
            return dic[(curr, currK)];
        }
    }
}
