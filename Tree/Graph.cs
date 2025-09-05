using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{

    public class Graph
    {
        public Dictionary<int, Node> nodes;
        public HashSet<Edge> edges;
        public Graph()
        {
            nodes = new Dictionary<int, Node>();
            edges = new HashSet<Edge>();
        }
        public void RemoveNode(int id)
        {
            if (!nodes.ContainsKey(id))
            {
                Console.WriteLine("不存在该节点");
                return;
            }


            // 删去该节点的出边 和 邻居节点的入度
            foreach (var edge in nodes[id].edges)
            {
                edge.to._in--;
                edges.Remove(edge);
            }
            nodes.Remove(id);
        }
        /// <summary>
        /// 对外提供一个二维数组转Graph的方法
        /// </summary>
        /// <param name="array">[from, to ,weight]</param>
        /// <returns></returns>
        public static Graph ArrayToGraph(int[][] array)
        {
            Graph graph = new Graph();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                Node from = null;
                Node to = null;

                // 一定要先判断节点是否已经在graph里面了
                if (!graph.nodes.ContainsKey(array[i][0]))
                    from = new Node(array[i][0]);
                else
                    from = graph.nodes[array[i][0]];
                if (!graph.nodes.ContainsKey(array[i][1]))
                    to = new Node(array[i][1]);
                else
                    to = graph.nodes[array[i][1]];
                // 边直接new
                Edge edge = new Edge(array[i][2], from, to);


                from._out++;
                from.nexts.Add(to);
                from.edges.Add(edge);
                to._in++;

                if (!graph.nodes.ContainsKey(array[i][0]))
                    graph.nodes.Add(array[i][0], from);
                if (!graph.nodes.ContainsKey(array[i][1]))
                    graph.nodes.Add(array[i][1], to);
                graph.edges.Add(edge);
            }
            return graph;
        }
    }
    public class Node
    {
        public int value;
        // 和关键字重名了 只能这样了
        // 表示入度和出度
        public int _in;
        public int _out;
        /// <summary>
        /// 由当前节点出发  可以到达的节点（边是有向的）
        /// </summary>
        public List<Node> nexts;
        /// <summary>
        /// 由当前节点出发的边（边是有向的）
        /// </summary>
        public List<Edge> edges;
        public Node(int value)
        {
            this.value = value;
            _in = 0;
            _out = 0;
            nexts = new List<Node>();
            edges = new List<Edge>();
        }
    }
    public class Edge
    {
        public int weight;
        public Node from;
        public Node to;
        public Edge(int weight, Node from, Node to)
        {
            this.weight = weight;
            this.from = from;
            this.to = to;
        }
    }

    /// <summary>
    /// 左神的简单并查集
    /// </summary>
    public class MySets
    {
        public Dictionary<Node, List<Node>> setMap;
        public MySets(List<Node> nodes)
        {
            setMap = new Dictionary<Node, List<Node>>();
            foreach (var node in nodes)
            {
                List<Node> set = new List<Node>() { node };
                setMap.Add(node, set);
            }
        }
        public bool isSameSet(Node from, Node to)
        {
            if (setMap[from] == setMap[to])
            {
                return true;
            }
            return false;
        }
        public void Union(Node from, Node to)
        {
            List<Node> fromSet = setMap[from];
            List<Node> toSet = setMap[to];
            // 全部添加到 fromSet中   并且修改toSet中的节点的setMap
            foreach (var node in toSet)
            {
                fromSet.Add(node);
                setMap[node] = fromSet;
            }
        }
    }

    public class UnionFindSets<T>
    {
        class Element<T>
        {
            T value;
            public Element(T value)
            {
                this.value = value;
            }
        }
        Dictionary<T, Element<T>> elementMap;
        Dictionary<Element<T>, Element<T>> fatherMap;
        /// <summary>
        /// key：每个集合的代表元素  value：该集合的大小
        /// </summary>
        Dictionary<Element<T>, int> sizeMap;
        public UnionFindSets(List<T> list)
        {
            elementMap = new Dictionary<T, Element<T>>();
            fatherMap = new Dictionary<Element<T>, Element<T>>();
            sizeMap = new Dictionary<Element<T>, int>();
            for (int i = 0; i < list.Count; i++)
            {
                Element<T> curr = new Element<T>(list[i]);
                elementMap.Add(list[i], curr);
                fatherMap.Add(curr, curr);
                sizeMap.Add(curr, 1);
            }
        }
        Element<T> FindHead(Element<T> element)
        {
            Element<T> curr = element;
            Stack<Element<T>> stack = new Stack<Element<T>>();
            while (curr != fatherMap[curr])
            {
                curr = fatherMap[curr];
                stack.Push(curr);
            }
            while (stack.Count > 0)
            {
                fatherMap[stack.Pop()] = curr;
            }
            return curr;
        }
        public bool IsSameSet(T t1, T t2)
        {
            if(!elementMap.ContainsKey(t1) || !elementMap.ContainsKey(t2))
                return false;
            Element<T> head1 = elementMap[t1];
            Element<T> head2 = elementMap[t2];
            return FindHead(head1) == FindHead(head2);
        }
        public void Union(T t1, T t2)
        {
            if (!elementMap.ContainsKey(t1) || !elementMap.ContainsKey(t2))
                return;
            Element<T> e1 = FindHead(elementMap[t1]);
            Element<T> e2 = FindHead(elementMap[t2]);
            if(e1 != e2)
            {
                Element<T> big = sizeMap[e1] >= sizeMap[e2] ? e1 : e2;
                Element<T> small = big == e1 ? e2 : e1;
                fatherMap[small] = big;
                sizeMap[big] += sizeMap[small];
                sizeMap.Remove(small);
            }
        }
    }
}
