using System.Collections;

namespace SinglyDirectedList_2
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node(T data)
        {
            Data = data;
        }
    }

    public class LinkedList<T> : IEnumerable
    {
        private Node<T>? Head;
        private Node<T>? Tail;
        public int Size { get; private set; }

        public void AddToEnd(T data)
        {
            var node = new Node<T>(data);
            if (Head == null)
                Head = node;
            else
                Tail.Next = node;
            Tail = node;
            Size++;
        }
        public void AddToBeg(T data)
        {
            Node<T> node = new Node<T>(data);
            node.Next = Head;
            Head = node;
            if (Size == 0)
                Tail = Head;
            Size++;
        }
        public bool Delete(T data)
        {
            Node<T> current = Head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            Tail = previous;
                    }
                    else
                    {
                        Head = Head.Next;
                        if (Head == null)
                            Tail = null;
                    }
                    Size--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }
        public void Reverse()
        {
            Node<T> current = Head;
            Node<T> previous = null;
            Node<T> next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            Head = previous;
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
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
