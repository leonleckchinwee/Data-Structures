namespace DSA.BSTrees;

public class BSTree<T> where T : IComparable<T>
{
    public class BSTreeNode<T2> where T2 : IComparable<T2>
    {
        public T2              Value;

        public BSTreeNode<T2>? Left;
        public BSTreeNode<T2>? Right;

        public BSTreeNode(T2 value)
        {
            Value = value;
            Left  = null;
            Right = null;
        }
    }

    public BSTreeNode<T>? Root { get; private set; }

    public int Count { get; private set; }

    public BSTree()
    {
        Root  = null;
        Count = 0;
    }

    public BSTree(T value)
    {
        Root  = new BSTreeNode<T>(value);
        Count = 1;
    }

    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    private BSTreeNode<T> Insert(BSTreeNode<T>? node, T value)
    {
        if (node == null)   // New node
        {
            node = new BSTreeNode<T>(value);

            ++Count;
            return node;
        }

        int comparer = value.CompareTo(node.Value);

        if (comparer == 0)  // Duplicate values
        {
            throw new InvalidOperationException("Duplicate values not allowed.");
        }

        if (comparer < 0)   // Value smaller than current node
        {
            node.Left = Insert(node.Left, value);
        }
        else if (comparer > 0)  // Value greater than current node
        {
            node.Right = Insert(node.Right, value);
        }

        return Root!;
    }

    public void Remove(T value)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(T value)
    {
        throw new NotImplementedException();
    }

    public void PrintInorder(int indent = 0)
    {
        if (Root == null)
            return;

        Inorder(Root, indent);
    }

    private void Inorder(BSTreeNode<T>? node, int indent = 0)
    {
        if (node == null)
            return;

        Inorder(node!.Left, indent + 1);

        Console.Write(new string(' ', indent * 4));
        Console.Write(node!.Value);

        Inorder(node!.Right, indent + 1);
    }

    public void PreorderTraversal(Action<T> action)
    {
        throw new NotImplementedException();
    }

    public void PostorderTraversal(Action<T> action)
    {
        throw new NotImplementedException();
    }

    public int Height()
    {
        throw new NotImplementedException();
    }

    public T Minimum()
    {
        throw new NotImplementedException();
    }

    public T Maximum()
    {
        throw new NotImplementedException();
    }

    public T Predecessor(T value)
    {
        throw new NotImplementedException();
    }

    public T Successor(T value)
    {
        throw new NotImplementedException();
    }
}