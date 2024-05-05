namespace DSA.BSTrees;

/// <summary>
/// Binary search tree that only supports integers (for now).
/// The tree does not allow duplicates.
/// </summary>
public class BSTree<T> where T : IComparable<T>
{
    #region Properties 

    /// <summary>
    /// Private member that storing the root node.
    /// </summary>
    private BSTreeNode<T>? m_Root;

    /// <summary>
    /// Private member storing the number of nodes present in the tree.
    /// </summary>
    private int m_Count;

    /// <summary>
    /// Root node of the tree.
    /// </summary>
    public BSTreeNode<T>? Root => m_Root;

    /// <summary>
    /// Number of nodes present in the tree.
    /// </summary>
    public int Count => m_Count;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes an new empty tree.
    /// </summary>
    public BSTree()
    {
        m_Root  = null;
        m_Count = 0;
    }

    /// <summary>
    /// Initializes a new tree with root node containing the specificied value.
    /// </summary>
    /// <param name="value">Value to insert as the root node of the tree.</param>
    public BSTree(T value)
    {
        m_Root  = new BSTreeNode<T>(value) { Tree = this };
        m_Count = 1;
    }

    #endregion

    #region Insert

    /// <summary>
    /// Inserts a new value into the tree.
    /// </summary>
    /// <param name="value">Value to insert.</param>
    /// <returns>A new node containing the specified value.</returns>
    /// <exception cref="InvalidOperationException">Duplicate values are not allowed.</exception>
    public BSTreeNode<T> Insert(T value)
    {
        BSTreeNode<T> newNode = new BSTreeNode<T>(value) { Tree = this };
        m_Root = Insert(m_Root, newNode);

        return newNode;
    }

    /// <summary>
    /// Inserts an exisiting node into the tree.
    /// </summary>
    /// <param name="newNode">Node to insert into the tree.</param>
    /// <exception cref="InvalidOperationException">Duplicate values are not allowed.</exception>
    /// <exception cref="InvalidOperationException">Node belongs to another tree.</exception>
    public void Insert(BSTreeNode<T> newNode)
    {
        if (newNode.Tree != null && newNode.Tree != this)   // Check that node belongs to only one tree
            throw new InvalidOperationException("Node belongs to another tree.");

        newNode.Tree = this;
        m_Root = Insert(m_Root, newNode);
    }

    /// <summary>
    /// Private recursive method to insert a new node.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="newNode">New node to insert.</param>
    /// <returns>New node inserted.</returns>
    /// <exception cref="InvalidOperationException">Duplicate values are not allowed.</exception>
    private BSTreeNode<T> Insert(BSTreeNode<T>? node, BSTreeNode<T> newNode)
    {
        if (node == null)   // Insert new node
        {
            ++m_Count;
            return newNode;
        }

        int comparer = newNode.CompareTo(node);

        if (comparer < 0)   // New node is smaller than current node
        {
            node.Left = Insert(node.Left, newNode);
        }
        else if (comparer > 0)  // New node is greater than current node
        {
            node.Right = Insert(node.Right, newNode);
        }
        else    // Duplicate values
        {
            throw new InvalidOperationException("Duplicate values not allowed.");
        }

        return node;
    }

    #endregion

    #region Remove

    #endregion

    #region Min, Max

    /// <summary>
    /// Finds and returns the minimum value in the tree.
    /// </summary>
    /// <returns>Min value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public T MinValue()
    {
        ThrowIfEmpty();

        BSTreeNode<T> node = m_Root!;
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node.Value;
    }

    /// <summary>
    /// Finds and returns the maximum value in the tree.
    /// </summary>
    /// <returns>Max value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public T MaxValue()
    {
        ThrowIfEmpty();

        BSTreeNode<T> node = m_Root!;
        while (node.Right != null)
        {
            node = node.Right;
        }

        return node.Value;
    }

    /// <summary>
    /// Finds and returns the node containing the minimum value in the tree.
    /// </summary>
    /// <returns>Node containing the min value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public BSTreeNode<T>? MinNode()
    {
        ThrowIfEmpty();

        BSTreeNode<T> node = m_Root!;
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node;
    }

    /// <summary>
    /// Finds and returns the node containing the maximum value in the tree.
    /// </summary>
    /// <returns>Node containing the max value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public BSTreeNode<T>? MaxNode()
    {
        ThrowIfEmpty();

        BSTreeNode<T> node = m_Root!;
        while (node.Right != null)
        {
            node = node.Right;
        }

        return node;
    }

    #endregion

    #region Height, Depth, Balance Factor

    /// <summary>
    /// Get the height from the root node to its deepest child node.
    /// </summary>
    /// <returns>Height of tree.</returns>
    public int Height()
    {
        return GetHeight(m_Root);
    }

    /// <summary>
    /// Private recursive method to find the max height of tree.
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    private int GetHeight(BSTreeNode<T>? root)
    {
        if (root == null)
            return -1;

        int left  = GetHeight(root.Left);
        int right = GetHeight(root.Right);

        return Math.Max(left, right) + 1;
    }

    /// <summary>
    /// Get the depth of tree from source node to target node.
    /// </summary>
    /// <param name="source">Source node.</param>
    /// <param name="target">Target node.</param>
    /// <returns>Depth of tree from source to target.</returns>
    public int GetDepth(BSTreeNode<T>? source, BSTreeNode<T>? target)
    {
        if (source == null)
            return -1;

        if (source == target)
            return 0;

        int left  = GetDepth(source.Left, target);
        int right = GetDepth(source.Right, target);

        if (left != -1)
            return left + 1;
        else if (right != -1)
            return right + 1;

        return -1;   
    }

    /// <summary>
    /// The difference between the heights of left subtree and right subtree.
    /// </summary>
    /// <returns>The difference in heights.</returns>
    public int BalanceFactor()
    {
        return GetBalanceFactor(m_Root);
    }

    /// <summary>
    /// Private method to find the difference between the heights of left and right subtrees.
    /// </summary>
    /// <param name="root">Root node.</param>
    /// <returns>The balance factor.</returns>
    private int GetBalanceFactor(BSTreeNode<T>? root)
    {
        if (root == null)
            return 0;

        int left  = GetHeight(root.Left);
        int right = GetHeight(root.Right);

        return left - right;
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Determines if the tree is empty by checking if the root node exists.
    /// </summary>
    /// <returns>True if empty; otherwise false.</returns>
    public bool IsEmpty()
    {
        return Root == null;
    }

    /// <summary>
    /// Determines if the tree is balanced. The tree is balanced if the balance factor is within
    /// the range (-1, 0, 1).
    /// </summary>
    /// <returns>True if balanced; otherwise false;</returns>
    public bool IsBalanced()
    {
        return Math.Abs(BalanceFactor()) <= 1;
    }

    #endregion

    #region Exceptions Handling

    /// <summary>
    /// Throws an exception if the tree is empty.
    /// </summary>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    private void ThrowIfEmpty()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Tree is empty.");
    }

    #endregion

}