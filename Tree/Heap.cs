using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Heap
    {
    }
    /// <summary>
    /// 找数据流中的中位数
    /// </summary>
    public class MedianFinder
    {
        PriorityQueue<int, int> bigHeap;
        PriorityQueue<int, int> smallHeap;
        public MedianFinder()
        {
            bigHeap = new PriorityQueue<int, int>();
            smallHeap = new PriorityQueue<int, int>();
        }

        public void AddNum(int num)
        {
            if (bigHeap.Count == 0)
            {
                bigHeap.Enqueue(num, -num);
                return;
            }

            if(num <= bigHeap.Peek())
            {
                bigHeap.Enqueue(num, -num);
            }
            else
            {
                smallHeap.Enqueue(num, num);
            }
            if((bigHeap.Count - smallHeap.Count) >= 2)
            {
                int a = bigHeap.Dequeue();
                smallHeap.Enqueue(a, a);
            }
            else if((smallHeap.Count - bigHeap.Count) >= 2)
            {
                int a = smallHeap.Dequeue();
                bigHeap.Enqueue(a, -a);
            }
        }
        public double FindMedian()
        {
            if(bigHeap.Count > smallHeap.Count)
            {
                return bigHeap.Peek();
            }
            else if(bigHeap.Count < smallHeap.Count)
            {
                return smallHeap.Peek();
            }
            else 
            { 
                return (bigHeap.Peek() + smallHeap.Peek()) / 2d;
            }
        }
    }
}
