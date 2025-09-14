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
            return CountRoutesRecur(locations, start, 0, finish, fuel);
        }
        static int CountRoutesRecur(int[] locations, int curr, int cost, int finish, int fuel)
        {    
            int res = 0;
            if (curr == finish && cost <= fuel)
                res++;                                  // 这里很关键 到了目的地还可以再走出去回来 只要fuel没超过就行
            if(cost > fuel)
                return 0;
            for (int i = 0; i < locations.Length; i++)
            {
                if(i == curr)
                    continue;
                res += CountRoutesRecur(locations, i, cost + Math.Abs(locations[i] - locations[curr]), finish, fuel);
            }
            return res;
        }
        public static int WalkWays(int start, int end, int k, int N)
        {
            Dictionary<(int, int), int> dict = new Dictionary<(int, int), int>();
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
                int res = WalkWaysRecur(2, end, k, currK + 1, N, dic);
                dic[(curr, currK)] = res;
                return res;
            }
            if (curr == N)
            {
                int res = WalkWaysRecur(N - 1, end, k, currK + 1, N, dic);
                dic[(curr, currK)] = res;
                return res;
            }
            int res1 = WalkWaysRecur(curr - 1, end, k, currK + 1, N, dic);
            int res2 = WalkWaysRecur(curr + 1, end, k, currK + 1, N, dic);
            dic[(curr, currK)] = res1 + res2;
            return res1 + res2;
        }
    }
}
