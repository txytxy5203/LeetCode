using LeetCode;
using System.Text;



Recursion.AllRank("abcd");

#region Greedy Algorithm
int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
{
    // k 能做几个项目   w 启动资金    
    PriorityQueue<(int, int), int> locked = new PriorityQueue<(int, int), int>();
    PriorityQueue<int, int> unLocked = new PriorityQueue<int, int>();
    for (int i = 0; i < capital.Length; i++)
    {
        locked.Enqueue((capital[i], profits[i]), capital[i]);
    }
    while (k > 0)
    {
        while (locked.Count > 0 && locked.Peek().Item1 < w)
        {
            (int cap, int pro) = locked.Dequeue();
            unLocked.Enqueue(pro, -pro);
        }
        if(unLocked.Count == 0)
            break;
        w += unLocked.Dequeue();
        k--;
    }
    return w;

}
int lessMoney(int[] arr)
{
    // 哈夫曼编码
    // https://leetcode.cn/problems/minimum-cost-to-connect-sticks/description/
    // https://blog.csdn.net/u014299052/article/details/121807068
    PriorityQueue<int, int> pQ = new PriorityQueue<int, int>();
    for (int i = 0; i < arr.Length; i++)
    {
        pQ.Enqueue(arr[i], arr[i]);
    }

    int cost = 0;
    while (pQ.Count > 1)    // 因为是一次弹出两个所有要 大于1
    {
        int a = pQ.Dequeue();
        int b = pQ.Dequeue();
        cost += a + b;
        pQ.Enqueue(cost, cost);
    }
    return cost;
}
string LowestString(string[] strs)
{
    // 拼接生成字典序最小的字符串
    // https://www.nowcoder.com/practice/f1f6a1a1b6f6409b944f869dc8fd3381
    Array.Sort(strs, (a, b) => {
        string first = a + b;
        string last = b + a;       
        for (int i = 0; i < first.Length; i++)
        {
            if (first[i] < last[i])
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        return 0;
        // 也可以直接 first.compareto(last) 默认也是返回字典序
    });
    var result = new StringBuilder();
    foreach (string str in strs)
    {
        result.Append(str);
    }
    return result.ToString();
}
int FindLongestChain(int[][] pairs)
{
    // 自己的解法
    // https://leetcode.cn/problems/maximum-length-of-pair-chain/description/?envType=problem-list-v2&envId=greedy
    bool[] canUse = new bool[pairs.GetLength(0)];
    Array.Fill(canUse, true);
    int result = 0;

    int earliest = int.MaxValue;
    int index = 0;
    while (!IsAllFalse(canUse))
    {
        earliest = int.MaxValue;

        for (int i = 0; i < pairs.GetLength(0); i++)
        {
            if (!canUse[i])
                continue;
            if (earliest > pairs[i][1])
            {
                index = i;
                earliest = pairs[i][1];
            }
        }
        canUse[index] = false;
        result++;
        for (int i = 0; i < pairs.GetLength(0); i++)
        {
            if (!canUse[i])
                continue;
            if (pairs[index][1] >= pairs[i][0])     // 注意这里是大于等于
            {
                canUse[i] = false;
            }
        }
    }
    return result;
}
bool IsAllFalse(bool[] canUse)
{
    foreach (bool b in canUse)
    {
        if(b)
            return false;
    }
    return true;
}
int FindLongestChain2(int[][] pairs)
{
    // 左神的解法
    // 按照结束时间排序
    Array.Sort(pairs, (a, b) => a[1].CompareTo(b[1]));
    int result = 0;
    int timePoint = int.MinValue;           // 当前的时间节点
    for (int i = 0; i < pairs.GetLength(0); i++)
    { 
        if(timePoint < pairs[i][0])         // 一定注意这里的条件是什么
        {
            result++;
            timePoint = pairs[i][1];
        }
    }
    return result;
}
#endregion
#region Graph
static int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
{
    // 这个思路不行
    // https://leetcode.cn/problems/cheapest-flights-within-k-stops/description/?envType=problem-list-v2&envId=vzsxaVPG
    Graph graph = Graph.ArrayToGraph(flights);
    Dictionary<Node, int> dic = FindCheapestPriceDijkstra(graph.nodes[src], k);

    if (dic.ContainsKey(graph.nodes[dst]))
    {
        return dic[graph.nodes[dst]];
    }
    return -1;
}
static Dictionary<Node, int> FindCheapestPriceDijkstra(Node head, int k)
{
    Dictionary<Node, int> distanceDic = new Dictionary<Node, int>();        // 从head到其他节点的距离
    distanceDic.Add(head, 0);

    HashSet<Node> selectedNodes = new HashSet<Node>();      // 已经被利用完的节点
    Node cur = GetMinDistanceAndUnselectedNode(distanceDic, selectedNodes);     // 在distanceDic中选一个value最小的  且不在selectedNodes中
    while (cur != null)
    {
        foreach (var edge in cur.edges)
        {
            if (!distanceDic.ContainsKey(edge.to))
            {
                distanceDic.Add(edge.to, int.MaxValue);      // dic中没有这个节点那就直接赋一个无穷大
            }

            if ((edge.weight + distanceDic[edge.from] < distanceDic[edge.to]) && (selectedNodes.Count < k + 1))    // 看看能否更新距离
            {
                distanceDic[edge.to] = edge.weight + distanceDic[edge.from];
            }
        }
        selectedNodes.Add(cur);         // 利用完cur节点的所有边后   就加入set中
        cur = GetMinDistanceAndUnselectedNode(distanceDic, selectedNodes);
    }
    return distanceDic;
}
static int NetworkDelayTime(int[][] times, int n, int k)
{
    // https://leetcode.cn/problems/network-delay-time/description/?envType=problem-list-v2&envId=vzsxaVPG
    // 图的题目我都是用左神的框架写的  下次记得练一下二维数组框架下的Coding
    // 先生成一个图
    Graph graph = Graph.ArrayToGraph(times);

    Dictionary<Node, int> dic = Dijkstra(graph.nodes[k]);

    if (dic.Count != n)
        return -1;
    return dic.Values.Max();
}
static Dictionary<Node, int> Dijkstra(Node head)
{
    // 左神还有一个堆优化的版本在高级班
    Dictionary<Node, int> distanceDic = new Dictionary<Node, int>();            // 从head到其他节点的距离
    distanceDic.Add(head, 0);

    HashSet<Node> selectedNodes = new HashSet<Node>();                          // 已经被利用完的节点
    Node cur = GetMinDistanceAndUnselectedNode(distanceDic, selectedNodes);     // 在distanceDic中选一个value最小的  且不在selectedNodes中
    while (cur != null)
    {
        foreach (var edge in cur.edges)
        {
            if (!distanceDic.ContainsKey(edge.to))
            {
                distanceDic.Add(edge.to, int.MaxValue);      // dic中没有这个节点那就直接赋一个无穷大
            }

            if (edge.weight + distanceDic[edge.from] < distanceDic[edge.to])    // 看看能否更新距离
            {
                distanceDic[edge.to] = edge.weight + distanceDic[edge.from];
            }
        }
        selectedNodes.Add(cur);         // 利用完cur节点的所有边后   就加入set中
        cur = GetMinDistanceAndUnselectedNode(distanceDic, selectedNodes);
    }
    return distanceDic;
}
static Dictionary<Node, int> Dijkstra2(Node head, int size)
{
    // 左神堆优化版的Dijkstra算法
    NodeHeap nodeHeap = new NodeHeap(size);
    nodeHeap.AddOrUpdateOrIgnore(head, 0);
    Dictionary<Node, int> result = new Dictionary<Node, int>();
    while(!nodeHeap.IsEmpty())
    {
        NodeRecord nodeRecord = nodeHeap.Pop();
        Node node = nodeRecord.node;
        int dis = nodeRecord.distance;
        foreach (var edge in node.edges)
        {
            nodeHeap.AddOrUpdateOrIgnore(edge.to, edge.weight + dis);
        }
        result.Add(node, dis);
    }
    return result;
}
static Node GetMinDistanceAndUnselectedNode(Dictionary<Node, int> dict, HashSet<Node> set)
{
    Node result = null;
    int min = int.MaxValue;
    foreach (var node in dict.Keys)
    {
        if (set.Contains(node))
            continue;
        if (dict[node] < min)
        {
            result = node;
            min = dict[node];
        }
    }
    return result;
}
static int NetworkDelayTime2(int[][] times, int n, int k)
{
    // 使用Floyd算法  直接使用邻接矩阵
    int[,] w = new int[n, n];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            w[i, j] = i == j ? 0 : int.MaxValue >> 1;
        }
    }
    for (int i = 0; i < times.GetLength(0); i++)     // 题目给的路径全部写进去
    {
        w[times[i][0] - 1, times[i][1] - 1] = times[i][2];
    }
    Floyd(w);

    int result = 0;
    for (int i = 0; i < n; i++)
    {
        result = Math.Max(w[k - 1, i], result);
    }
    return result >= int.MaxValue >> 1 ? -1 : result;
}
static void Floyd(int[,] w)
{
    // Floyd算法  邻接矩阵实现
    // 三层循环嵌套   中间-起始-终点
    int n = w.GetLength(0);
    for (int p = 0; p < n; p++)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                w[i, j] = Math.Min(w[i, j], w[i, p] + w[p, j]);
            }
        }
    }
}
static int NetworkDelayTime3(int[][] times, int n, int k)
{
    // 邻接矩阵实现Dijkstra算法
    int[,] w = new int[n, n];
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            w[i, j] = i == j ? 0 : int.MaxValue >> 1;
        }
    }
    for (int i = 0; i < times.GetLength(0); i++)     // 题目给的路径全部写进去
    {
        w[times[i][0] - 1, times[i][1] - 1] = times[i][2];
    }


    int[] result = new int[n];
    Array.Fill(result, int.MaxValue >> 1);
    result[k - 1] = 0;
    bool[] isLocked = new bool[n];
    Array.Fill(isLocked, false);

    int curr = GetMinDistanceAndUnselectedInt(result, isLocked);
    while (curr != -1)
    {
        for (int i = 0; i < n; i++)
        {
            int distance = w[curr, i] + result[curr];
            if (distance < result[i])
            {
                result[i] = distance;
            }
        }
        isLocked[curr] = true;
        curr = GetMinDistanceAndUnselectedInt(result, isLocked);
    }

    int max = result.Max();
    return max >= int.MaxValue >> 1 ? -1 : max;

}
static int GetMinDistanceAndUnselectedInt(int[] result, bool[] isLocked)
{
    int curr = int.MaxValue;
    int index = -1;
    for (int i = 0; i < result.Length; i++)
    {
        if (isLocked[i]) continue;
        if (curr > result[i])
        {
            index = i;
            curr = result[i];
        }
    }
    return index;
}
static HashSet<Edge> PrimMST(Graph graph)
{
    // 最小生成树   prim算法

    HashSet<Edge> result = new HashSet<Edge>();
    HashSet<Node> visited = new HashSet<Node>();                                //已经经过的节点
    PriorityQueue<Edge, int> priorityQueue = new PriorityQueue<Edge, int>();    // 待选择的边

    foreach (var node in graph.nodes.Values)        // 这里的for循环是因为图可能不连通  那么每个子图都有一个最小生成树
                                                    // 如果说了连通的那么就不用循环
    {
        if (!visited.Contains(node))
        {
            visited.Add(node);
            foreach (var edge in node.edges)
            {
                priorityQueue.Enqueue(edge, edge.weight);
            }
            while (priorityQueue.Count != 0)
            {
                Edge edge = priorityQueue.Dequeue();
                Node toNode = edge.to;
                if (!visited.Contains(toNode))
                {
                    visited.Add(toNode);
                    result.Add(edge);
                    foreach (var nextEdge in toNode.edges)              // 这里会重复地把一些边加入到队列中 但是不影响结果
                                                                        // 因为节点重复了还是会不要的
                    {
                        priorityQueue.Enqueue(nextEdge, nextEdge.weight);
                    }
                }
            }
        }
    }
    return result;
}
static HashSet<Edge> KruskalMST(Graph graph)
{
    // 最小生成树   kruskal算法
    HashSet<Edge> result = new HashSet<Edge>();
    MySets mySets = new MySets(graph.nodes.Values.ToList<Node>());

    // 这里用优先级队列应该会更好
    List<Edge> edges = graph.edges.ToList<Edge>();
    // 排序
    edges.Sort((a, b) =>
    {
        if (a.weight < b.weight)
            return -1;
        return 1;
    });

    foreach (var edge in edges)
    {
        if (!mySets.isSameSet(edge.from, edge.to))
        {
            result.Add(edge);
            mySets.Union(edge.from, edge.to);
        }
    }
    return result;
}
static List<Node> SortedTopology(Graph graph)
{
    List<Node> result = new List<Node>();
    Node node = null;
    int id = 0;

    while (graph.nodes.Count != 0)
    {
        foreach (var i in graph.nodes.Keys)
        {
            if (graph.nodes[i]._in == 0)
            {
                id = i;
                node = graph.nodes[i];
                break;
            }
        }
        // 判断node是否为空
        result.Add(node);
        graph.RemoveNode(id);
    }
    return result;

}
static List<Node> SortedTopology2(Graph graph)
{
    // 左神的写法
    Dictionary<Node, int> inDic = new Dictionary<Node, int>();
    Queue<Node> zeroInQueue = new Queue<Node>();

    foreach (var node in graph.nodes.Values)
    {
        inDic.Add(node, node._in);
        if (node._in == 0)
            zeroInQueue.Enqueue(node);
    }

    List<Node> result = new List<Node>();
    while (zeroInQueue.Count != 0)
    {
        Node cur = zeroInQueue.Dequeue();
        result.Add(cur);
        foreach (var node in cur.nexts)
        {
            inDic[node]--;
            if (inDic[node] == 0)
            {
                zeroInQueue.Enqueue(node);
            }
        }
    }
    return result;
}
static void BFS(Node node)
{
    if (node == null)
        return;
    Queue<Node> queue = new Queue<Node>();
    // 如果题目中明确说了是有一个范围的  那么就可以直接用数组存  更快
    HashSet<Node> visited = new HashSet<Node>();

    queue.Enqueue(node);
    visited.Add(node);
    while (queue.Count > 0)
    {
        Node curr = queue.Dequeue();
        // 在出队列的时候执行逻辑
        foreach (var neighbor in curr.nexts)
        {
            if (!visited.Contains(neighbor))
            {
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
            }
        }
    }
}
static void DFS(Node node)
{
    // DFS的逻辑和我想的有一点不一样  仔细看
    if (node == null)
        return;

    Stack<Node> stack = new Stack<Node>();
    HashSet<Node> visited = new HashSet<Node>();

    stack.Push(node);
    visited.Add(node);
    // 在Push的时候就执行逻辑
    Console.WriteLine(node.value);
    while (stack.Count > 0)
    {
        Node curr = stack.Pop();
        foreach (var neighbor in curr.nexts)
        {
            if (!visited.Contains(neighbor))
            {
                // 这里把curr重新入栈  是因为要保持栈中存在的Node就是你遍历的路径！！！
                stack.Push(curr);
                stack.Push(neighbor);
                visited.Add(neighbor);
                Console.WriteLine(neighbor.value);
                break;
            }
        }
    }
}
#endregion