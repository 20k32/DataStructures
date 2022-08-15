namespace BinaryTree
{
    public class Node<T> : IComparable<T>
        where T : IComparable<T>
    {
        public T Value { get; private set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node(T value)
        {
            Value = value;
        }
        public int CompareTo(T? other)
        {
            return Value.CompareTo(other);
        }
        public int CompareTo(Node<T> node)
        {
            return Value.CompareTo(node.Value);
        }
    }

    public class BinaryTree<T>
        where T : IComparable<T>
    {
        private Node<T> Root { get; set; }
        public int Count { get; private set; }
        private void AddTo(Node<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                    node.Left = new Node<T>(value);
                else
                    AddTo(node.Left, value);
            }
            else // value >= node.Value
            {
                if (node.Right == null)
                    node.Right = new Node<T>(value);
                else
                    AddTo(node.Right, value);
            }
        }
        public void Add(T value)
        {
            if (Root == null)
                Root = new Node<T>(value);
            else
                AddTo(Root, value);
            Count++;
        }
        public Node<T> FindWithParent(out Node<T> root, T value)
        {
            Node<T> current = Root;
            root = null;
            while(current != null)
            {
                int compare = current.CompareTo(value);
                if (compare > 0)
                {
                    root = current;
                    current = current.Left;
                }
                else if (compare < 0)
                {
                    root = current;
                    current = current.Right;
                }
                else break;
            }
            return current;
        }
        public bool Contains(T value)
        {
            return FindWithParent(out Node<T> parent, value) != null;
        }
        public bool Remove(T value)
        {   
            Node<T> parent;
            Node<T> current = FindWithParent(out parent, value);

            if (current == null) return false;
            
            if(current.Right == null)
            {
                if (parent == null)
                {
                    Root = current.Left;
                }
                else
                {
                    int compare = parent.CompareTo(current.Value);
                    if(compare > 0)
                    {
                        parent.Left = current.Left;
                    }
                    if(compare < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                    Root = current.Right;
                else
                {
                    int compare = parent.CompareTo(current.Value);
                    if (compare > 0)
                    {
                        parent.Left = current.Right;
                    }
                    if (compare < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            else
            {
                Node<T> LastLeft = current.Right.Left;
                Node<T> LastLeftParent = current.Right;

                while(LastLeft.Left != null)
                {
                    LastLeftParent = LastLeft;
                    LastLeft = LastLeft.Left;
                }
                LastLeftParent.Left = LastLeft.Right;
                LastLeft.Left = current.Left;
                LastLeft.Right = current.Right;
                if (parent == null)
                    Root = LastLeft;
                else
                {
                    int compare = parent.CompareTo(current.Value);
                    if (compare > 0)
                    {
                        parent.Left = LastLeft;
                    }
                    if (compare < 0)
                    {
                        parent.Right = LastLeft;
                    }
                }
            }
            return true;
        }
        #region Inorder
        private void _Inorder(Node<T> root)
        {
            if(root.Left != null)
                _Inorder(root.Left);
            Console.WriteLine(root.Value);
            if (root.Right != null)
                _Inorder(root.Right);
        }
        public void Inorder()
        {
            _Inorder(Root);
        }
        #endregion
        #region Postorder
        private void _Postorder(Node<T> root)
        {
            if (root.Left != null)
                _Postorder(root.Left);
            if (root.Right != null)
                _Postorder(root.Right);
            Console.WriteLine(root.Value);
        }
        public void Postorder()
        {
            _Postorder(Root);
        }
        #endregion
        #region Preorder
        private void _Preorder(Node<T> root)
        {
            Console.WriteLine(root.Value);
            if (root.Left != null)
                _Preorder(root.Left);
            if (root.Right != null)
                _Preorder(root.Right);
        }
        public void Preorder()
        {
            _Preorder(Root);
        }
        #endregion
        public static void Expamle()
        {
            var tree = new BinaryTree<int>();
            tree.Add(10);
            tree.Add(5);
            tree.Add(124);
            tree.Add(3);
            tree.Add(4);
            tree.Add(1);
            tree.Add(2);
            tree.Inorder();
            Console.WriteLine("---");
            Console.WriteLine(tree.Remove(124));
            Console.WriteLine(tree.Remove(33));
            Console.WriteLine(tree.Remove(3));
            Console.WriteLine("-In-");
            tree.Inorder();
            Console.WriteLine("-Pre-");
            tree.Preorder();
            Console.WriteLine("-Post-");
            tree.Postorder();
        }
    }
}
