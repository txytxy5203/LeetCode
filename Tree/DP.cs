using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LeetCode
{
    public static class DP
    {
        public static int HorseProbability(int x, int y, int k)
        {
            // (x,y)是目标位置  k为必须走几步
            if (k < 1)
                return 0;
            return HorseProbabilityRecur(x, y, k, 0, 0);
        }
        static int HorseProbabilityRecur(int x, int y, int rest, int currX, int currY)
        {
            if(rest < 0 || currX < 0  || currX > 8 || currY < 0 || currY > 9)
                return 0;
            if(rest == 0)
            {
                if (currX == x && currY == y)
                    return 1;
                else
                    return 0;
            }
            int way1 = HorseProbabilityRecur(x, y, rest - 1, currX + 2, currY + 1);
            int way2 = HorseProbabilityRecur(x, y, rest - 1, currX + 2, currY - 1);
            int way3 = HorseProbabilityRecur(x, y, rest - 1, currX + 1, currY + 2);
            int way4 = HorseProbabilityRecur(x, y, rest - 1, currX + 1, currY - 2);
            int way5 = HorseProbabilityRecur(x, y, rest - 1, currX - 2, currY + 1);
            int way6 = HorseProbabilityRecur(x, y, rest - 1, currX - 2, currY - 1);
            int way7 = HorseProbabilityRecur(x, y, rest - 1, currX - 1, currY + 2);
            int way8 = HorseProbabilityRecur(x, y, rest - 1, currX - 1, currY - 2);
            int total = way1 + way2 + way3 + way4 + way5 + way6 + way7 + way8;
            return total;
        }
        public static int HorseProbability2(int x, int y, int k)
        {
            if (k < 1)
                return 0;
            int[,,] dp = new int[9, 10, k + 1];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int e = 0; e < k + 1; e++)
                    {
                        dp[i, j, e] = -1;
                    }
                }
            }
            dp[x, y, 0] = 1;

            for (int i = 0; i < 9; i++)
            {
                for(int j = 0;j < 10; j++)
                {
                    if (i == x && j == y)
                        continue;
                    dp[i, j, 0] = 0;
                }
            }

            for (int i = 0;i < 9;i++)
            {
                for (int j = 0; j < 10;j++)
                {
                    for(int e = 1; e < k + 1; e++)
                    {
                        //int way1 = HorseProbabilityRecur(x, y, rest - 1, currX + 2, currY + 1);
                        //int way2 = HorseProbabilityRecur(x, y, rest - 1, currX + 2, currY - 1);
                        //int way3 = HorseProbabilityRecur(x, y, rest - 1, currX + 1, currY + 2);
                        //int way4 = HorseProbabilityRecur(x, y, rest - 1, currX + 1, currY - 2);
                        //int way5 = HorseProbabilityRecur(x, y, rest - 1, currX - 2, currY + 1);
                        //int way6 = HorseProbabilityRecur(x, y, rest - 1, currX - 2, currY - 1);
                        //int way7 = HorseProbabilityRecur(x, y, rest - 1, currX - 1, currY + 2);
                        //int way8 = HorseProbabilityRecur(x, y, rest - 1, currX - 1, currY - 2);
                        int way1 = i + 2 >= 9 || j + 1 >= 10 ? 0 : dp[i + 2, j + 1, e - 1];
                        int way2 = i + 2 >= 9 || j - 1 < 0 ? 0 : dp[i + 2, j - 1, e - 1];
                        int way3 = i + 1 >= 9 || j + 2 >= 10 ? 0 : dp[i + 1, j + 2, e - 1];
                        int way4 = i + 1 >= 9 || j - 2 < 0 ? 0 : dp[i + 1, j - 2, e - 1];
                        int way5 = i - 2 < 0 || j + 1 >= 10 ? 0 : dp[i - 2, j + 1, e - 1];
                        int way6 = i - 2 < 0 || j - 1 < 0 ? 0 : dp[i - 2, j - 1, e - 1];
                        int way7 = i - 1 < 0 || j + 2 >= 10 ? 0 : dp[i - 1, j + 2, e - 1];
                        int way8 = i - 1 < 0 || j - 2 < 0 ? 0 : dp[i - 1, j - 2, e - 1];
                        int total = way1 + way2 + way3 + way4 + way5 + way6 + way7 + way8;
                        dp[i, j, e] = total;
                    }
                }
            }

            return dp[x, y, k];
        }
        public static int Rob(int[] nums)
        {
            // https://leetcode.cn/problems/house-robber/description/
            if(nums.Length == 0 || nums == null)
                return 0;
            int[] dp = new int[nums.Length];
            Array.Fill(dp, -1);
            return RobRecur(nums, 0, dp);
        }
        static int RobRecur(int[] nums, int index, int[] dp)
        {
            if (index >= nums.Length)
                return 0;
            if (dp[index] != -1)
                return dp[index];
            int yes = nums[index] + RobRecur(nums, index + 2, dp);
            int no = RobRecur(nums, index + 1, dp);
            int max = Math.Max(yes, no);
            dp[index] = max;
            return max;
        }
        public static int Rob2(int[] nums)
        {
            if (nums.Length == 0 || nums == null)
                return 0;
            int[] dp = new int[nums.Length];
            
            for(int i = dp.Length - 1; i >= 0; i--)
            {
                int yes = i + 2 >= dp.Length ? nums[i] : nums[i] + dp[i + 2];
                int no = i + 1 >= dp.Length ? 0 : dp[i + 1];
                dp[i] = Math.Max(yes, no);
            }
            return dp[0];
        }
        public static int ClimbStairs(int n)
        {
            if(n < 1)
                return 0; 
            int[] dp = new int[n + 1];
            Array.Fill(dp, 0);
            return ClimbStairsRecur(n, dp);
        }
        static int ClimbStairsRecur(int rest, int[] dp)
        {
            if (rest < 0)                   // 一定记住越界的情况一定要放在前面
                return 0;
            if (rest == 0)
                return 1;
            if (dp[rest] != 0)
                return dp[rest];

            int one = ClimbStairsRecur(rest - 1, dp);
            int two = ClimbStairsRecur(rest - 2, dp);
            dp[rest] = one + two;
            return dp[rest];               
        }
        public static int ClimbStairs2(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 1;
            for (int i = 2; i < dp.Length; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
            return dp[n];
        }
        public static int Fib(int n)
        {
            // https://leetcode.cn/problems/fibonacci-number/description/
            if (n < 2)
                return 1;
            int[] dp = new int[n];
            Array.Fill(dp, 0);
            dp[0] = 1;
            dp[1] = 1;
            return FibRecur(n, dp);
        }
        static int FibRecur(int n, int[] dp)
        {
            if (dp[n - 1] != 0)
                return dp[n - 1];
            dp[n - 1] = FibRecur(n - 1, dp) + FibRecur(n - 2, dp);
            return dp[n - 1];
        }
        public static int CoinChange(int[] coins, int amount)
        {
            // https://leetcode.cn/problems/coin-change/description/
            // 超出了时间限制  没有AC
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int cost = CoinChangeRecur(coins, amount, 0, dict);
            return cost != int.MaxValue ? cost : -1;
        }
        static int CoinChangeRecur(int[] coins, int rest, int curr, Dictionary<int, int> dict)
        {
            if (rest < 0)
                return int.MaxValue;
            if (dict.ContainsKey(rest))
                return dict[rest];
            if (rest == 0)
            {
                dict[rest] = curr;
                return curr;
            }
            int res = int.MaxValue;
            for (int i = 0; i < coins.Length; i++)
            {
                res = Math.Min(res, CoinChangeRecur(coins, rest - coins[i], curr + 1, dict));
            }
            dict[rest] = res;
            return res;
        }
        public static int CoinChange2(int[] coins, int amount)
        {
            // DP矩阵递推
            // 但是会超时
            int maxTimes = 100;
            int[,] dp = new int[amount + 1, maxTimes];
            for (int i = 0;i < maxTimes;i++)
            {
                dp[0, i] = i;
            }
            for (int i = 1; i < dp.GetLength(0);i++)
            {
                for (int j = 0; j < dp.GetLength(1);j++)
                {
                    int min = int.MaxValue;
                    for(int k = 0; k < coins.Length;k++)
                    {
                        if (i - coins[k] >= 0 && j +1 <= maxTimes - 1)
                            min = Math.Min(min, dp[i - coins[k], j + 1]);
                    }
                    dp[i, j] = min;
                }
            }
            return dp[amount, 0] != int.MaxValue ? dp[amount,0] : -1;
        }
        public static int CoinChange3(int[] coins, int amount)
        {
            return 0;
        }
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
