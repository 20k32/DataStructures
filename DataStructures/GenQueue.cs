namespace Queue
{
    public class GenQueueList<T>
    {
        private List<T> Data = new List<T>();
        private T Head => Data.Last();
        private T Tail => Data.First();
        public int Count { get; private set; }
        public GenQueueList() { }
        public GenQueueList(T data)
        {
            Data.Add(data);
        }
        public void Push(T el)
        {
            Data.Insert(0, el);
        }
        public T Pop()
        {
            var head = Head;
            Data.Remove(head);
            return head;
        }
        public T Peek()
        {
            return Head;
        }
        public override string ToString()
        {
            string s = "";
            foreach (var item in Data)
            {
                s += item + "\n";
            }
            return s;
        }
    }

    public class GenQueueArr<T>
    {
        private T[] Data;
        private T Head => Data[Count > 0 ? Count - 1 : 0];
        private T Tail => Data[0];
        private int MAXCOUNT => Data.Length;
        public int Count { get; private set; }
        public GenQueueArr(T data, int size)
        {
            Data = new T[size];
            Data[0] = data;
            Count = 1;
        }
        public GenQueueArr(int size)
        {
            Data = new T[size];
            Count = 0;
        }
        public void Push(T el)
        {
            if (Count < MAXCOUNT)
            {
                var result = (new T[] { el }).Concat(Data);
                Data = result.ToArray();
                Count++;
            }
        }
        public T Pop()
        {
            var head = Head;
            Count--;
            return head;
        }
        public T Peek()
        {
            return Head;
        }
        public override string ToString()
        {
            string s = "";
            foreach (var item in Data)
            {
                s += item + "\n";
            }
            return s;
        }
    }
}
