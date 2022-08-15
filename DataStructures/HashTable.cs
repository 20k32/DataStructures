namespace HashTable
{
    public class Node<K, V>
    {
        public K Key { get; private set; }
        public V Value { get; set; }
        public Node(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }
    public class HashTable<K, V>
    {
        private LinkedList<Node<K, V>>[] Nodes;
        private int Capacity { get; set; }
        private int Size { get; set; }
        private const float LOAD_FACTOR = 0.75f;
        public HashTable(int capacity)
        {
            Capacity = capacity;
            Nodes = new LinkedList<Node<K, V>>[capacity];
        }
        private int GetHash(K key)
        {
            return Math.Abs(key.GetHashCode()) % Capacity;
        }
        private float GetLoadFactor()
        {
            return LOAD_FACTOR / Capacity;
        }
        public void Add(K key, V value)
        {
            if (GetLoadFactor() >= LOAD_FACTOR)
            {
                Resize();
            }

            int index = GetHash(key);
            if (Nodes[index] == null)
            {
                Nodes[index] = new LinkedList<Node<K, V>>();
            }
            var Node = new Node<K, V>(key, value);
            var LNode = new LinkedListNode<Node<K, V>>(Node);
            Nodes[index].AddFirst(LNode);
            Size++;
        }
        private void Resize()
        {
            Capacity = Capacity * 2;
            var temp = Nodes;
            Size = 0;
            Nodes = new LinkedList<Node<K, V>>[Capacity];
            foreach (var item in temp)
            {
                if (item != null)
                {
                    foreach (var pair in item)
                    {
                        if (pair != null)
                        {
                            Add(pair.Key, pair.Value);
                        }
                    }
                }
            }
        }
        public bool Remove(K key)
        {
            int index = GetHash(key);
            if (Nodes[index] == null) return false;
            foreach (var item in Nodes[index])
            {
                if (item.Key.Equals(key))
                {
                    Nodes[index].Remove(item);
                    break;
                }
            }
            return true;
        }
        public V GetValue(K key)
        {
            int index = GetHash(key);

            if (Nodes[index] == null)
                return default(V);
            foreach (var item in Nodes[index])
            {
                if (item.Key.Equals(key))
                {
                    return item.Value;
                }
            }
            return default(V);
        }
        public void Clear()
        {
            Size = 0;
            Nodes = new LinkedList<Node<K, V>>[Capacity];
        }
        public void Show()
        {
            foreach (var item in Nodes)
                if (item != null)
                    foreach (var pair in item)
                        Console.WriteLine($"{pair.Key} {pair.Value}");
        }
    }
}
