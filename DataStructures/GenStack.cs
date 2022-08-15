namespace Stack
{
    public class GenStack<T>
    {
        public event Action<string> print;
        public int Size { get; private set; }
        private T[] Data { get; set; }
        public GenStack()
        {
            Data = new T[0];
        }
        public bool IsEmpty()
        {
            return Size == 0;
        }
        public void Push(T el)
        {
            RebuildArr(1);
            Data[Size] = el;
            Size++;
        }
        public void Pop()
        {
            if (IsEmpty()) throw new Exception("Stack is empty.");
            RebuildArr(-1);
            Size--;
        }
        public T Peek()
        {
            if (IsEmpty()) throw new Exception("Stack is empty.");
            return Data[Size - 1];
        }
        public void Print()
        {
            foreach (var item in Data)
            {
                print?.Invoke(item?.ToString() ?? "");
            }
        }
        public void Clear()
        {
            Size = 0;
            Data = new T[Size];
        }
        private void RebuildArr(int step)
        {
            T[] tmp = Data;
            Data = new T[Size + step];
            for (int i = 0; i < (tmp.Length > Data.Length ? Data.Length : tmp.Length); i++)
                Data[i] = tmp[i];
        }
    }
}
