namespace DataStructures
{
    public class Deque<T>
    {
        private List<T> Data = new List<T>();
        private T Head => Data.Last();
        private T Tail => Data.First();
        public int Count => Data.Count;
        public Deque() { }
        public Deque(T data)
        {
            Data.Add(data);
        }
        public void PushBack(T el)
        {
            Data.Insert(0, el);
        }
        public void PushAhead(T el)
        {
            Data.Add(el);
        }
        public T PopBack()
        {
            var item = Tail;
            Data.Remove(item);
            return item;
        }
        public T PopAhead()
        {
            var item = Head;
            Data.Remove(item);
            return item;
        }
        public T PeekBack()
        {
            return Tail;
        }
        public T PeekAhead()
        {
            return Head;
        }
    }
}
