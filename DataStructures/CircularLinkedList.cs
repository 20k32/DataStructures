using System.Collections;

namespace CircularLinkedList
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
    public class CircularLinkedList<T> : IEnumerable<T>
    {
        private Node<T> Head;
        public int Size { get; private set; }
        public CircularLinkedList() { }
        public CircularLinkedList(T data)
        {
            SetHeadItem(data);
        }
        private void SetHeadItem(T data)
        {
            Head = new Node<T>(data);
            Head.Next = Head;
            Head.Previous = Head;
            Size = 1;
        }

        public void AddToBeg(T data)
        {
            if (Size == 0)
            {
                SetHeadItem(data);
                return;
            }
            var node = new Node<T>(data);
            node.Next = Head;
            node.Previous = Head.Previous;
            Head.Previous.Next = node;
            Head.Previous = node;
            Size++;
        }
        public void Delete(T data)
        {
            var current = Head;

            if (Head.Data.Equals(data))
            {
                Head.Next.Previous = Head.Previous;
                Head.Previous.Next = Head.Next;
                Size--;
                Head = Head.Next;
                return;
            }

            for (int i = Size; i > 0; i--)
            {
                if (current != null && current.Data.Equals(data))
                {
                    current.Next.Previous = current.Previous;
                    current.Previous.Next = current.Next;
                    Size--;
                }
                current = current.Next;
            }
        }
        public IEnumerator GetEnumerator()
        {
            Node<T> node = Head;
            for (int i = 0; i < Size; i++)
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
