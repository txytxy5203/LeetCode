using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class HashTable
    {

    }
    public class RandomizedSet
    {
        // https://leetcode.cn/problems/insert-delete-getrandom-o1/description/?envType=study-plan-v2&envId=top-interview-150
        int size;
        Dictionary<int, int> valIndexMap;
        Dictionary<int, int> indexValMap;
        public RandomizedSet()
        {
            size = 0;
            valIndexMap = new Dictionary<int, int>();
            indexValMap = new Dictionary<int, int>();
        }

        public bool Insert(int val)
        {
            if(valIndexMap.ContainsKey(val))
                return false;
            valIndexMap[val] = size;
            indexValMap[size] = val;
            size++;
            return true;
        }

        public bool Remove(int val)
        {
            if(!valIndexMap.ContainsKey(val))
                return false;

            int v = valIndexMap[val];
            int k = indexValMap[size - 1];
            
            valIndexMap[k] = v;                         // 把最后的元素拿去填补要remove的元素
            valIndexMap.Remove(val);
            indexValMap[v] = k;
            indexValMap.Remove(size - 1);
            size--;
            return true;
        }

        public int GetRandom()
        {
            Random r = new Random();
            int randomValue = r.Next(size);
            return indexValMap[randomValue];
        }
    }
}
