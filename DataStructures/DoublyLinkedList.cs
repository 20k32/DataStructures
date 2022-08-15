using System.Collections;

namespace DoublyLinkedList
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
        public Node(T data)
        {
            Data = data;
        }
        public override string ToString()
        {
            return Data.ToString();
        }
    }
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private Node<T> Head;
        private Node<T> Tail;
        public int Size { get; private set; }
        public DoublyLinkedList() { }
        public DoublyLinkedList(T data)
        {
            var item = new Node<T>(data);
            Head = item;
            Tail = item;
            Size = 1;
        }
        public void AddToBeg(T data)
        {
            var node = new Node<T>(data);
            if (Size == 0)
            {
                Head = node;
                Tail = node;
                Size = 1;
                return;
            }


            Tail.Next = node;
            node.Previous = Tail;
            Tail = node;
            Size++;
        }
        public void Delete(T data)
        {
            var current = Head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }
                current = current.Next;
            }
            if (current != null)
            {
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    Tail = current.Previous;
                }
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    Head = current.Next;
                }
                Size--;
            }
        }
        public DoublyLinkedList<T> Reverse()
        {
            var res = new DoublyLinkedList<T>();
            var current = Tail;
            while (current != null)
            {
                res.AddToBeg(current.Data);
                current = current.Previous;
            }
            return res;
        }
        public IEnumerator GetEnumerator()
        {
            Node<T> node = Head;
            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }
    }
}
