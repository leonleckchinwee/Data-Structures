using DSA.Stacks;

namespace DSA.BSTrees;

/// <summary>
/// Binary search tree that only supports integers (for now).
/// The tree does not allow duplicates.
/// </summary>
public class BSTree
{
    /// <summary>
    /// Root of the tree.
    /// </summary>
    public BSTreeNode? Root { get; private set; }

    /// <summary>
    /// Number of nodes in the tree.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Initializes an empty tree.
    /// </summary>
    public BSTree()
    {
        Root  = null;
        Count = 0;
    }

    /// <summary>
    /// Initializes a tree with root node containing the specified value.
    /// </summary>
    /// <param name="value">Value to insert as the root of the tree.</param>
    public BSTree(int value)
    {
        Root  = new BSTreeNode(value);
        Count = 1;
    }

    /// <summary>
    /// Inserts a new node into the tree with the specified value.
    /// </summary>
    /// <param name="value">Value to insert into tree.</param>
    /// <exception cref="InvalidOperationException">Duplicate entries not allowed.</exception>
    public void Insert(int value)
    {
        Root = Insert(Root, value);
    }

    private BSTreeNode Insert(BSTreeNode? node, int value)
    {
        if (node == null)   // New node to insert
        {
            ++Count;
            node = new BSTreeNode(value);
            return node;
        }

        int comparer = value.CompareTo(node.Value);

        if (comparer < 0)       // Value smaller than current node
        {
            node.Left = Insert(node.Left, value);
        }
        else if (comparer > 0)  // Value greater than current node
        {
            node.Right = Insert(node.Right, value);
        }
        else                    // Duplicate values
        {
            throw new InvalidOperationException("Duplicate values not allowed.");
        }

        return node;
    }

    /// <summary>
    /// Removes an existing node from the tree with the specified value.
    /// </summary>
    /// <param name="value">Value to remove from tree.</param>
    public bool Remove(int value)
    {
        if (Count == 0)
            return false;

        bool removed = false;
        Root = Remove(Root, value, ref removed);

        if (removed)
            --Count;

        return removed;
    }

    private BSTreeNode? Remove(BSTreeNode? node, int value, ref bool removed)
    {
        if (node == null)
        {
            removed = false;
            return node;
        }

        int comparer = value.CompareTo(node.Value);

        if (comparer < 0)       // Value smaller than current node
        {
            node.Left = Remove(node.Left, value, ref removed);
        }
        else if (comparer > 0)  // Value greater than current node
        {
            node.Right = Remove(node.Right, value, ref removed);
        }
        else                    // Node to remove
        {
            removed = true;

            if (node.Left == null)
                return node!.Right;
            else if (node.Right == null)
                return node!.Left;
                
            node.Value = Minimum(node.Left);
            node.Left = Remove(node.Left, node.Value, ref removed); 
        }

        return node;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(int value)
    {
        throw new NotImplementedException();
    }

    public string? PrintInorder()
    {
        if (Root == null)
            return null;

        return Inorder(Root);
    }

    private string? Inorder(BSTreeNode? node)
    {
        if (node == null)
            return null;

        string nodeString = "";

        nodeString += Inorder(node!.Left);

        nodeString += node.Value.ToString() + ", ";

        nodeString += Inorder(node!.Right);

        return nodeString;
    }

    public string? PrintPreorder()
    {
        if (Root == null)
            return null;

        return Preorder(Root);
    }

    private string? Preorder(BSTreeNode? node)
    {
        if (node == null)
            return null;

        string nodeString = "";

        nodeString += node.Value.ToString() + ", ";

        nodeString += Preorder(node!.Left);
        nodeString += Preorder(node!.Right);

        return nodeString;
    }

    public string? PostorderTraversal()
    {
        if (Root == null)
            return null;

        return Postorder(Root);
    }

    private string? Postorder(BSTreeNode? node)
    {
        if (node == null)
            return null;

        string nodeString = "";

        nodeString += Postorder(node!.Left);
        nodeString += Postorder(node!.Right);

        nodeString += node.Value.ToString() + ", ";

        return nodeString;
    }

    public int Height()
    {
        throw new NotImplementedException();
    }

    public int Minimum(BSTreeNode? node)
    {
        if (Root == null || node == null)
            throw new ArgumentNullException("Node is null.");

        int min = node.Value;

        while (node.Left != null)
        {
            min  = node.Left.Value;
            node = node.Left;
        }

        return min;
    }

    public int Maximum()
    {
        throw new NotImplementedException();
    }

    public int Predecessor(int value)
    {
        throw new NotImplementedException();
    }

    public BSTreeNode? InorderSuccessor(BSTreeNode? node)
    {
        Stacks.Stack<BSTreeNode> a = new Stacks.Stack<BSTreeNode>();

        throw new NotImplementedException();
    }
}