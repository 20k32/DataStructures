namespace SinglyDirectedList_1
{
    public class Item<T>
    {
        public T Data { get; set; }
        public Item<T> Next { get; set; }
        public Item(T data)
        {
            Data = data;
        }
        public override string ToString()
        {
            return Data.ToString();
        }
    }

    public class LinkedList<T>
    {
        private Item<T> Head;
        private Item<T> Tail;
        public int Count { get; private set; }
        public LinkedList() { }
        public LinkedList(T data)
        {
            SetHeadItem(data);
        }
        public void Push(T el)
        {
            if (Count == 0)
            {
                SetHeadItem(el);
                return;
            }
            var item = new Item<T>(el)
            {
                Next = Tail
            };
            Tail = item;
            Count++;
        }
        public T Pop()
        {
            var data = Head.Data;
            var current = Tail.Next;
            var previous = Tail;
            while (current != null && current.Next != null)
            {
                previous = current;
                current = current.Next;
            }
            Head = previous;
            Head.Next = null;
            Count--;
            return data;
        }
        public T Peek()
        {
            return Head.Data;
        }
        private void SetHeadItem(T data)
        {
            var item = new Item<T>(data);
            Head = item;
            Tail = item;
            Count = 1;
        }
    }
}
