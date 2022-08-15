namespace Graph
{
    public class SimpleGraph
    {
        private double[,] List { get; set; }
        private int Size { get; set; }
        public SimpleGraph(int size)
        {
            Size = size;
            List = new double[size, size];
        }
        public SimpleGraph(double[,] list)
        {
            if (list.GetLength(0) != list.GetLength(1)) return;
            List = list;
            Size = list.GetLength(0);
        }
        public void add(int from, int to, double weight)
        {
            List[from, to] = weight;
        }
        public List<int> BFS(int from)
        {
            bool[] visited = new bool[Size];
            var queue = new Queue<int>();
            var list = new List<int>();
            queue.Enqueue(from);
            visited[from] = true;
            while (queue.Count != 0)
            {
                int index = queue.Dequeue();
                list.Add(index);
                for (int i = 0; i < Size; i++)
                {
                    if (List[index, i] != 0 && !visited[i])
                    {
                        visited[i] = true;
                        queue.Enqueue(i);
                    }
                }
            }
            return list;
        }
        private void _DFS(int from, bool[] visited, ref List<int> list)
        {
            visited[from] = true;
            list.Add(from);
            for (int i = 0; i < Size; i++)
            {
                if (List[from, i] != 0 && !visited[i])
                    _DFS(i, visited, ref list);
            }
        }
        public void DFS(int from, List<int> list)
        {
            _DFS(from, new bool[Size], ref list);
        }
        public List<int> NonRecDFS(int from)
        {
            bool[] visited = new bool[Size];
            var stack = new Stack<int>();
            var list = new List<int>();
            stack.Push(from);

            while (stack.Count != 0)
            {
                int index = stack.Pop();
                if (!visited[index]) list.Add(index);
                visited[index] = true;
                for (int i = Size - 1; i >= 0; i--)
                {
                    if (!visited[i] && List[index, i] != 0)
                    {
                        stack.Push(i);
                    }
                }
            }
            return list;
        }
        public int VertexDegree(int vertex)
        {
            int degree = 0;
            for (int i = 0; i < Size; i++)
            {
                if (List[vertex, i] != 0)
                {
                    degree++;
                }
            }
            return degree;
        }
        #region Wave (Lee) Algorithm
        private static bool IsValid(int i, int j, int n, int m)
        {
            return (i >= 0 && i < n && j >= 0 && j < m);
        }
        private static void _Wave(double[,] arr, int i, int j, int coeff, int n, int m, Queue<KeyValuePair<int, int>> pair)
        {
            if (IsValid(i - 1, j, n, m) && arr[i - 1, j] == 1)
            {
                arr[i - 1, j] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i - 1, j));
            }
            if (IsValid(i - 1, j + 1, n, m) && arr[i - 1, j + 1] == 1)
            {
                arr[i - 1, j + 1] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i - 1, j + 1));
            }
            if (IsValid(i, j + 1, n, m) && arr[i, j + 1] == 1)
            {
                arr[i, j + 1] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i, j + 1));
            }
            if (IsValid(i + 1, j + 1, n, m) && arr[i + 1, j + 1] == 1)
            {
                arr[i + 1, j + 1] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i + 1, j + 1));
            }
            if (IsValid(i + 1, j, n, m) && arr[i + 1, j] == 1)
            {
                arr[i + 1, j] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i + 1, j));
            }
            if (IsValid(i + 1, j - 1, n, m) && arr[i + 1, j - 1] == 1)
            {
                arr[i + 1, j - 1] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i + 1, j - 1));
            }
            if (IsValid(i, j - 1, n, m) && arr[i, j - 1] == 1)
            {
                arr[i, j - 1] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i, j - 1));
            }
            if (IsValid(i - 1, j - 1, n, m) && arr[i - 1, j - 1] == 1)
            {
                arr[i - 1, j - 1] = coeff;
                pair.Enqueue(new KeyValuePair<int, int>(i - 1, j - 1));
            }
        }
        public static void Wave(double[,] arr, int x, int y, int rows, int cols)
        {
            if (arr[x, y] == 0) return;
            int coeff = 2;
            arr[x, y] = coeff;
            var queue = new Queue<KeyValuePair<int, int>>();
            queue.Enqueue(new KeyValuePair<int, int>(x, y));
            while (queue.Count > 0)
            {
                int count = queue.Count;
                coeff++;
                while (count > 0)
                {
                    KeyValuePair<int, int> point = queue.Dequeue();
                    int i = point.Key;
                    int j = point.Value;
                    _Wave(arr, i, j, coeff, rows, cols, queue);
                    count--;
                }
            }
        }
        #endregion
        #region Pathfinding Algorithms
        public IList<double> GetShortestPath(int start)
        {
            List<double> distance = new List<double>();
            bool[] visited = new bool[Size];
            for (int i = 0; i < Size; i++)
            {
                distance.Add(double.MaxValue);
                visited[i] = false;
            }

            distance[start] = 0;
            int index = -1;
            for (int i = 0; i < Size; i++)
            {
                double min = double.MaxValue;
                for (int j = 0; j < Size; j++)
                {
                    if (!visited[j] && distance[j] <= min)
                    {
                        min = distance[j];
                        index = j;
                    }
                }
                visited[index] = true;
                for (int j = 0; j < Size; j++)
                {
                    if (!visited[j] && List[index, j] > -1
                        && distance[index] != double.MaxValue
                        && distance[index] + List[index, j] < distance[j])
                        distance[j] = distance[index] + List[index, j];
                }
            }
            return distance;
        }
        public double[,] GetAllShortestPath()
        {
            double[,] distances = new double[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    distances[i, j] = List[i, j];
                }
            }
            for (int k = 0; k < Size; k++)
            {
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {


                        if (distances[i, k] != 0 && distances[k, j] != 0 && i != j)
                        {
                            //distances[i, j] = Math.Min(distances[i, j], distances[i, k] + distances[k, j]);
                            if (distances[i, k] + distances[k, j] < distances[i, j] || distances[i, j] == 0)
                                distances[i, j] = distances[i, k] + distances[k, j];
                        }
                    }
                }
            }
            return distances;
        }
        #endregion
        #region Topological Sort (Tarjan's Algorithm)
        enum Colors { WHITE, GREY, BLACK };
        public IList<int> TopologicalSort()
        {
            Stack<int> result = new Stack<int>();
            Colors[] colors = new Colors[Size];
            Stack<int> path = new Stack<int>();
            for (int i = 0; i < Size; i++)
            {
                int vertex = i;
                int saved = i;
                if (path.Count > 0)
                {
                    i--;
                    vertex = path.Pop();
                    saved = vertex;
                }

                if (colors[vertex] == Colors.BLACK)
                    continue;

                colors[vertex] = Colors.GREY;
                path.Push(vertex);
                for (int j = 0; j < Size; j++)
                {
                    if (List[vertex, j] != 0 && colors[j] != Colors.BLACK)
                    {
                        if (colors[j] == Colors.GREY)
                            continue;

                        vertex = j;
                        path.Push(vertex);
                        break;
                    }
                }
                if (vertex == saved)
                {
                    colors[vertex] = Colors.BLACK;
                    result.Push(vertex);
                    path.Pop();
                }
            }
            return result.ToList();
        }
        #endregion
        public IDictionary<int, IList<int>> GetComponents()
        {
            int color = 1;
            var table = new Dictionary<int, IList<int>>();
            int count = Size;
            while (count > 0)
            {
                int vertex = 0;
                bool[] visited = new bool[Size];
                int[] colors = new int[Size];
                while (VertexDegree(vertex) == 0 && visited[vertex]) vertex++;
                var vertices = NonRecDFS(vertex);
                foreach (int item in vertices)
                {
                    visited[item] = true;
                    colors[item] = color;
                }
                count -= vertices.Count;
                table.Add(color, vertices);
                color++;
            }
            return table;
        }
        public static void Expamle()
        {
            double[,] arr = new double[,]{
                {0,1,0,1},
                {1,0,1,0},
                {0,1,0,0},
                {1,0,0,1}
            };

            double[,] tmp = new double[arr.GetLength(0), arr.GetLength(1)];
            Array.Copy(arr, tmp, arr.GetLength(0) * arr.GetLength(1));

            Console.WriteLine("Given array:\n");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n---\n");
            var graph = new SimpleGraph(arr);
            Console.WriteLine("BFS");
            foreach (var item in graph.BFS(0))
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n\nDFS");
            foreach (var item in graph.NonRecDFS(0))
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n---\n");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.WriteLine($"Vertex {i} , Degree {graph.VertexDegree(i)} .");
            }
           
            Console.WriteLine("\n---\n");
            Console.WriteLine("Wave algorithm:\n");
            Wave(tmp, 0, 1, arr.GetLength(0), arr.GetLength(1));
            for (int i = 0; i < tmp.GetLength(0); i++)
            {
                for (int j = 0; j < tmp.GetLength(1); j++)
                {
                    Console.Write(tmp[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n---\n");
            tmp = graph.GetAllShortestPath();
            Console.WriteLine("Path from vertex 0 to vertex 2: " + tmp[0, 2]);
            Console.WriteLine("\n---\n");
            Console.WriteLine("All components in graph:");
            foreach (var item in graph.GetComponents())
            {
                Console.Write(item.Key + ": ");
                foreach (var component in item.Value)
                {
                    Console.Write(component + ", ");
                }
            }
            Console.WriteLine("\n---\n");
            Console.WriteLine("Topologial sort:");
            var list = graph.TopologicalSort();
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
        }
    }
}