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
    public class NodeHeap
    {
        Node[] nodes;
        Dictionary<Node, int> heapIndexMap;
        Dictionary<Node, int> distanceMap;
        int size;
        public NodeHeap(int size) 
        { 
            nodes = new Node[size];
            heapIndexMap = new Dictionary<Node, int>();
            distanceMap = new Dictionary<Node, int>();
            this.size = 0;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public void AddOrUpdateOrIgnore(Node node, int distance)
        {
            if(InHeap(node))
            {
                distanceMap[node] = Math.Min(distanceMap[node], distance);
                HeapInsert(heapIndexMap[node]);
            }
            if(!IsEntered(node))
            {
                nodes[size] = node;
                heapIndexMap[node] = size;
                distanceMap[node] = distance;
                HeapInsert(size);
                size++;
            }
        }
        public NodeRecord Pop()
        {
            NodeRecord nodeRecord = new NodeRecord(nodes[0], distanceMap[nodes[0]]);
            Swap(0, size - 1);
            heapIndexMap[nodes[size - 1]] = -1;
            distanceMap.Remove(nodes[size - 1]);
            nodes[size - 1] = null;
            size--;
            Heapify(0);
            return nodeRecord;
        }
        bool IsEntered(Node node)
        {
            return heapIndexMap.ContainsKey(node);
        }
        bool InHeap(Node node)
        {
            return IsEntered(node) && heapIndexMap[node] != -1;
        }
        void HeapInsert(int index)
        {
            while (distanceMap[nodes[index]] < distanceMap[nodes[(index - 1) / 2]])
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }
        void Heapify(int index)
        {
            while (true)
            {
                int left = index * 2 + 1;
                int right = index * 2 + 2;
                int largest = index;

                if(left < size && heapIndexMap[nodes[left]] < heapIndexMap[nodes[index]]) largest = left;
                if(right < size && heapIndexMap[nodes[right]] < heapIndexMap[nodes[index]]) largest = right;

                if(largest == index) break;
                Swap(index, largest);
                index = largest;
            }
        }
        void Swap(int index1, int index2)
        {
            heapIndexMap[nodes[index1]] = index2;
            heapIndexMap[nodes[index2]] = index1;
            Node temp = nodes[index1];
            nodes[index1] = nodes[index2];
            nodes[index2] = temp;
        }
    }
    public class NodeRecord
    {
        public Node node;
        public int distance;
        public NodeRecord(Node node, int distance) 
        { 
            this.node = node;
            this.distance = distance;
        }
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
