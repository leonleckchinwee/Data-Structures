namespace DSA.BSTrees;

/// <summary>
/// Binary search tree.
/// The tree does not allow duplicates.
/// Null value is allowed for reference types.
/// </summary>
public class BSTree<T> where T : IComparable<T>
{
    #region Properties 

    /// <summary>
    /// Private property that storing the root node.
    /// </summary>
    private BSTreeNode<T>? m_Root;

    /// <summary>
    /// Private property storing the number of nodes present in the tree.
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

    /// <summary>
    /// Removes the node containing the specified value from the tree.
    /// </summary>
    /// <param name="value">Value to remove.</param>
    /// <returns>True if value is removed; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    public bool Remove(T value)
    {
        ThrowIfEmpty();
        ThrowIfNull(value);

        bool removed = false;
        m_Root = RemoveValue(m_Root, value, ref removed);

        if (removed)
            --m_Count;

        return removed;
    }

    /// <summary>
    /// Removes the specified existing node from the tree.
    /// </summary>
    /// <param name="node">Node to remove.</param>
    /// <returns>True if node is removed; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="ArgumentException">Node belongs to another tree.</exception>
    public bool Remove(BSTreeNode<T>? node)
    {
        ThrowIfEmpty();
        ThrowIfNull(node);
        ThrowIfNodeDoesNotBelong(node!);

        bool removed = false;
        m_Root = RemoveNode(m_Root, node!, ref removed);

        if (removed)
            --m_Count;

        return removed;
    }

    /// <summary>
    /// Attempts to remove the node containing the specified value without any exceptions thrown.
    /// </summary>
    /// <param name="value">Value to remove.</param>
    /// <returns>True if value is successfully removed; otherwise false.</returns>
    public bool TryRemove(T value)
    {
        if (IsEmpty() || value == null)
        {
            return false;
        }

        bool removed = false;
        m_Root = RemoveValue(m_Root, value, ref removed);

        if (removed)
            --m_Count;

        return removed;
    }

    /// <summary>
    /// Attempts to remove an existing specified node without any exceptions thrown.
    /// </summary>
    /// <param name="node">Node to remove.</param>
    /// <returns>True if node is successfully removed; otherwise false.</returns>
    public bool TryRemove(BSTreeNode<T>? node)
    {
        if (IsEmpty() || node == null || node.Tree != this)
        {
            return false;
        }

        bool removed = false;
        m_Root = RemoveNode(m_Root, node!, ref removed);

        if (removed)
            --m_Count;

        return removed;
    }

    /// <summary>
    /// Private recursive method to remove a value from the tree.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="value">Value to remove.</param>
    /// <param name="removed">Boolean to decrease count.</param>
    /// <returns>Removed node.</returns>
    private BSTreeNode<T>? RemoveValue(BSTreeNode<T>? node, T value, ref bool removed)
    {
        if (node == null)   // Value not found
        {
            removed = false;
            return null;
        }

        int comparer = value.CompareTo(node.Value);

        if (comparer < 0)
        {
            node.Left = RemoveValue(node.Left, value, ref removed);
        }
        else if (comparer > 0)
        {
            node.Right = RemoveValue(node.Right, value, ref removed);
        }
        else
        {
            removed = true;

            // Node has no children
            if (node.Left == null && node.Right == null)
                return null;

            // Node has one child
            if (node.Left == null)
            {
                BSTreeNode<T>? temp = node.Right;
                node.Right = null;
                return temp;
            }
            else if (node.Right == null)
            {
                BSTreeNode<T>? temp = node.Left;
                node.Left = null;
                return temp;
            }

            // Node has both childrens
            BSTreeNode<T> successor = Predecessor(node)!;
            node.Value = successor!.Value;
            node.Left = RemoveValue(node.Left, successor.Value, ref removed);
        }

        return node;
    }

    /// <summary>
    /// Private recursive method to remove a node from the tree.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="target">Target node to remove.</param>
    /// <param name="removed">Boolean to decrease count.</param>
    /// <returns>Removed node.</returns>
    private BSTreeNode<T>? RemoveNode(BSTreeNode<T>? node, BSTreeNode<T> target, ref bool removed)
    {
        if (node == null)   // Value not found
        {
            removed = false;
            return null;
        }

        int comparer = target.CompareTo(node);

        if (comparer < 0)
        {
            node.Left = RemoveNode(node.Left, target, ref removed);
        }
        else if (comparer > 0)
        {
            node.Right = RemoveNode(node.Right, target, ref removed);
        }
        else
        {
            removed = true;

            // Node has no children
            if (node.Left == null && node.Right == null)
                return null;

            // Node has one child
            if (node.Left == null)
            {
                BSTreeNode<T>? temp = node.Right;
                node.Right = null;
                return temp;
            }
            else if (node.Right == null)
            {
                BSTreeNode<T>? temp = node.Left;
                node.Left = null;
                return temp;
            }

            // Node has both childrens
            BSTreeNode<T> successor = Predecessor(node)!;
            node.Value = successor!.Value;
            node.Left = RemoveNode(node.Left, successor, ref removed);
        }

        return node;
    }

    /// <summary>
    /// Clears the entire tree.
    /// </summary>
    public void Clear()
    {
        ClearNodes(m_Root);

        m_Root  = null;
        m_Count = 0;
    }

    /// <summary>
    /// Private recursive method to clear all nodes and their references.
    /// </summary>
    /// <param name="node">Node to clear.</param>
    private void ClearNodes(BSTreeNode<T>? node)
    {
        if (node == null)
            return;

        ClearNodes(node.Left);
        ClearNodes(node.Right);

        node.Left  = null;
        node.Right = null;
    }

    #endregion

    #region Search

    /// <summary>
    /// Finds and returns the node containing the specified value.
    /// </summary>
    /// <param name="value">Value to search for.</param>
    /// <returns>Node containing the value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    public BSTreeNode<T>? Find(T value)
    {
        ThrowIfEmpty();
        ThrowIfNull(value);

        return GetNode(m_Root!, value);
    }

    /// <summary>
    /// Private recursive method to get a node with the specified value.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="value">Value to search for.</param>
    /// <returns>Node containing the value.</returns>
    private BSTreeNode<T>? GetNode(BSTreeNode<T> node, T value)
    {
        if (node == null)
            return null;

        int comparer = value.CompareTo(node.Value);

        if (comparer < 0)
        {
            return GetNode(node.Left!, value);
        }
        else if (comparer > 0)
        {
            return GetNode(node.Right!, value);
        }
        else
        {
            return node;
        }
    }

    /// <summary>
    /// Determines if value is present in the tree.
    /// </summary>
    /// <param name="value">Value to search for.</param>
    /// <returns>True if value is in the tree; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    public bool Contains(T value)
    {
        return Find(value) != null;
    }

    /// <summary>
    /// Determines if node is present in the tree.
    /// </summary>
    /// <param name="node">Node to search for.</param>
    /// <returns>True if node belongs to the tree; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    public bool Contains(BSTreeNode<T>? node)
    {
        ThrowIfEmpty();
        ThrowIfNull(node);

        return node!.Tree == this;
    }

    /// <summary>
    /// Finds the node containing the Kth largest value in the tree.
    /// </summary>
    /// <param name="k">K index.</param>
    /// <returns>Returns the node if found; otherwise null.</returns>
    /// <exception cref="ArgumentException">Index out of range.</exception>
    public BSTreeNode<T>? KthLargest(int k)
    {
        ThrowIfEmpty();

        if (k < 1)
            throw new ArgumentException("Invalid k-index.");

        int count = 0;
        BSTreeNode<T>? node = GetKthLargest(m_Root, k, ref count) ?? 
                                throw new ArgumentException("Invalid k-index.");
        
        return node;
    }

    /// <summary>
    /// Finds the node containing the Kth smallest value in the tree.
    /// </summary>
    /// <param name="k">K index.</param>
    /// <returns>Returns the node if found; otherwise null.</returns>
    /// <exception cref="ArgumentException">Index out of range.</exception>
    public BSTreeNode<T>? KthSmallest(int k)
    {
        ThrowIfEmpty();

        if (k < 1)
            throw new ArgumentException("Invalid k-index.");

        int count = 0;
        BSTreeNode<T>? node = GetKthSmallest(m_Root, k, ref count) ?? 
                                throw new ArgumentException("Invalid k-index.");
        
        return node;
    }

    /// <summary>
    /// Private recursive method to find the Kth largest value in the tree.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="k">K index.</param>
    /// <param name="count">Counter for k.</param>
    /// <returns>Returns the node if found; otherwise false.</returns>
    private BSTreeNode<T>? GetKthLargest(BSTreeNode<T>? node, int k, ref int count)
    {
        if (node == null)
            return default!;

        // First look at right subtree
        BSTreeNode<T>? result = GetKthLargest(node.Right, k, ref count);
        if (result != null)
            return result;

        // If right subtree does not exist
        ++count;
        if (count == k)
            return node;

        return GetKthLargest(node.Left, k, ref count);
    }

    /// <summary>
    /// Private recursive method to find the Kth smallest value in the tree.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="k">K index.</param>
    /// <param name="count">Counter for k.</param>
    /// <returns>Returns the node if found; otherwise false.</returns>
    private BSTreeNode<T>? GetKthSmallest(BSTreeNode<T>? node, int k, ref int count)
    {
        if (node == null)
            return null;

        // First look at left subtree
        BSTreeNode<T>? result = GetKthSmallest(node.Left, k, ref count);
        if (result != null)
            return result;

        // If left subtree does not exist
        ++count;
        if (count == k)
            return node;

        return GetKthSmallest(node.Right, k, ref count);
    }

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

    #region Traversal

    /// <summary>
    /// Finds the in-order predecessor of the given node.
    /// </summary>
    /// <param name="node">The source node.</param>
    /// <returns>The predecessor node.</returns>
    public BSTreeNode<T>? Predecessor(BSTreeNode<T>? node)
    {
        ThrowIfEmpty();
        ThrowIfNull(node);
        ThrowIfNodeDoesNotBelong(node!);

         // If node has left child, the predecessor is the largest node in left subtree
        if (node!.Left != null)
        {
            BSTreeNode<T> temp = node.Left;
            while (temp.Right != null)
            {
                temp = temp.Right;
            }

            return temp;
        }
        // Predecessor is above this node
        else
        {
            BSTreeNode<T>? predecessor = null;
            BSTreeNode<T>? current = m_Root!;

            while (current != null)
            {
                int comparer = node.CompareTo(current);

                if (comparer < 0)
                {
                    // Predecessor must be in left subtree,
                    // since it must be smaller than current node
                    current = current.Left;
                }
                else if (comparer > 0)
                {
                    // Predecessor must be in right subtree,
                    // since it must be the largest value in the right subtree
                    predecessor = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return predecessor;
        }
    }

    /// <summary>
    /// Finds the in-order successor of the given node.
    /// </summary>
    /// <param name="node">The source node.</param>
    /// <returns>The successor node.</returns>
    public BSTreeNode<T>? Successor(BSTreeNode<T>? node)
    {
        ThrowIfEmpty();
        ThrowIfNull(node);
        ThrowIfNodeDoesNotBelong(node!);

        // If node has right child, the successor is the smallest node in the right subtree
        if (node!.Right != null)
        {
            BSTreeNode<T> temp = node.Right;
            while (temp.Left != null)
            {
                temp = temp.Left;
            }

            return temp;
        }
        // Successor is above this node
        else
        {
            BSTreeNode<T>? successor = null;
            BSTreeNode<T>? current = m_Root;

            while (current != null)
            {
                int comparer = node.CompareTo(current);

                if (comparer < 0)
                {
                    successor = current;
                    current = current.Left;
                }
                else if (comparer > 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                } 
            }

            return successor;
        }
    }

    /// <summary>
    /// Prints the tree in-order from the specified node.
    /// </summary>
    /// <param name="node">Node to start from.</param>
    public void PrintInorder(BSTreeNode<T>? node)
    {
        if (node == null)
            return;

        PrintInorder(node.Left);
        Console.Write(node.Value.ToString() + ", ");
        PrintInorder(node.Right);
    }

    /// <summary>
    /// Prints the tree pre-order from the specified node.
    /// </summary>
    /// <param name="node">Node to start from.</param>
    public void PrintPreorder(BSTreeNode<T>? node)
    {
        if (node == null)
            return;

        Console.Write(node.Value.ToString() + ", ");
        PrintPreorder(node.Left);
        PrintPreorder(node.Right);
    }

    /// <summary>
    /// Prints the tree post-order from the specified node.
    /// </summary>
    /// <param name="node">Node to start from.</param>
    public void PrintPostorder(BSTreeNode<T>? node)
    {
        if (node == null)
            return;

        PrintPostorder(node.Left);
        PrintPostorder(node.Right);
        Console.Write(node.Value.ToString() + ", ");
    }

    #endregion

    #region Merge, Split

    // TODO: Merge
    // TODO: Split

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
    public int Depth(BSTreeNode<T>? source, BSTreeNode<T>? target)
    {
        ThrowIfEmpty();
        ThrowIfNull(target);

        if (source == null)
            return -1;

        ThrowIfNodeDoesNotBelong(source!);
        ThrowIfNodeDoesNotBelong(target!);

        if (source == target)
            return 0;

        int left  = Depth(source!.Left, target);
        int right = Depth(source!.Right, target);

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

    #region Serialization

    // TODO: file serialization?

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

    /// <summary>
    /// Throws an exception if given node is null.
    /// </summary>
    /// <param name="node">Node to check.</param>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    private void ThrowIfNull(BSTreeNode<T>? node)
    {
        ArgumentNullException.ThrowIfNull(node, "Node is null.");
    }

    /// <summary>
    /// Throws an exception if given value is null.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    public void ThrowIfNull(T value)
    {
        ArgumentNullException.ThrowIfNull(value, "Value is null.");
    }

    /// <summary>
    /// Throws an exception if given node belongs to another tree.
    /// </summary>
    /// <param name="node">Node to check</param>
    /// <exception cref="ArgumentException">Node belongs to another tree.</exception>
    public void ThrowIfNodeDoesNotBelong(BSTreeNode<T> node)
    {
        if (node.Tree != this)
            throw new ArgumentException("Node belongs to another tree.");
    }

    /// <summary>
    /// Throws an exception if given index is out of range of the tree.
    /// </summary>
    /// <param name="index">Index to check.</param>
    /// <exception cref="ArgumentOutOfRangeException">Index out of range.</exception>
    public void ThrowIfOutOfRange(int index)
    {
        if (index <= 0 || index > m_Count)
            throw new ArgumentOutOfRangeException("Index is out of range.");
    }

    #endregion
}