using System.Collections;

namespace DSA.AVLTrees;

/// <summary>
/// Self-balancing binary search tree.
/// The tree does not allow duplicates.
/// Null value is not allowed.
/// </summary>
public class AVLTree<T> : IEnumerable<T> where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Private property that storing the root node.
    /// </summary>
    private AVLNode<T>? m_Root;

    /// <summary>
    /// Private property storing the number of nodes present in the tree.
    /// </summary>
    private int m_Count;

    /// <summary>
    /// Root node of the tree.
    /// </summary>
    public AVLNode<T>? Root => m_Root;

    /// <summary>
    /// Number of nodes present in the tree.
    /// </summary>
    public int Count => m_Count;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes an new empty tree.
    /// </summary>
    public AVLTree()
    {
        m_Root  = null;
        m_Count = 0;
    }

    /// <summary>
    /// Initializes a new tree with root node containing the specificied value.
    /// </summary>
    /// <param name="value">Value to insert as the root node of this tree.</param>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    public AVLTree(T value)
    {
        ThrowIfNull(value);

        m_Root  = new AVLNode<T>(value) { Tree = this };
        m_Count = 1;
    }

    /// <summary>
    /// Initializes a new tree with all the items in the collection inserted properly
    /// into this tree.
    /// </summary>
    /// <param name="collection">Collection to insert from.</param>
    /// <exception cref="ArgumentNullException">Collection is null.</exception>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    public AVLTree(IEnumerable<T> collection)
    {
        ThrowIfNull(collection);

        foreach (var item in collection)
        {
            ThrowIfNull(item);
            Insert(item);
        }
    }

    #endregion

    #region Insert

    /// <summary>
    /// Inserts a new node containing the specified value into this tree.
    /// </summary>
    /// <param name="value">Value to insert.</param>
    /// <returns>A new node containing the specified value.</returns>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    /// <exception cref="InvalidOperationException">Duplicate values not allowed.</exception>
    public AVLNode<T> Insert(T value)
    {
        ThrowIfNull(value);

        AVLNode<T> node = new(value) { Tree = this };

        m_Root = Insert(m_Root, node);

        ++m_Count;

        return node;
    }

    /// <summary>
    /// Inserts an existing node into this tree.
    /// </summary>
    /// <param name="node">Existing node to insert.</param>
    /// <remarks>Note: This method will insert by node's value and does not care if
    /// specified node is already assigned to another tree.</remarks>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="ArgumentNullException">Node's value is null.</exception>
    /// <exception cref="InvalidOperationException">Duplicate values not allowed.</exception>
    public void Insert(AVLNode<T> node)
    {
        ThrowIfNull(node);
        ThrowIfNull(node.Value);

        node.Tree = this;
        m_Root    = Insert(m_Root, node);

        ++m_Count;
    }

    /// <summary>
    /// Attempts to insert a new node containing the specified value into this tree.
    /// </summary>
    /// <param name="value">Value to insert.</param>
    /// <param name="node">A new node containing the specified value.</param>
    /// <returns>True if value is inserted successfully; otherwise false.</returns>
    /// <remarks>Safe version of insert, no exceptions will be thrown.</remarks>
    public bool TryInsert(T value, out AVLNode<T> node)
    {
        if (value == null)
        {
            node = null!;
            return false;
        }

        AVLNode<T> newNode = new(value) { Tree = this };

        m_Root = TryInsert(m_Root, newNode);

        if (m_Root == null)
        {
            node = null!;
            return false;
        }

        node = newNode;
        ++m_Count;

        return true;
    }

    /// <summary>
    /// Attempts to insert an existing node into this tree.
    /// </summary>
    /// <param name="node">Existing node to insert.</param>
    /// <returns>True if node is inserted successfully; otherwise false.</returns>
    /// <remarks>Safe version of insert, no exceptions will be thrown.
    /// Note: This method will insert by node's value and does not care if
    /// specified node is already assigned to another tree.</remarks>
    public bool TryInsert(AVLNode<T> node)
    {
        if (node == null || node.Value == null)
        {
            return false;
        }

        node.Tree = this;

        m_Root = TryInsert(m_Root, node);

        ++m_Count;

        return true;
    }

    /// <summary>
    /// Private recursive method to insert a new node into this tree.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="newNode">New node to insert.</param>
    /// <returns>New root node of this tree.</returns>
    /// <remarks>This method will automatically balance the tree.</remarks>
    /// <exception cref="InvalidOperationException">Duplicate values not allowed.</exception>
    private AVLNode<T> Insert(AVLNode<T>? node, AVLNode<T> newNode)
    {
        // Tree is empty
        if (node == null)
        {
            return newNode;
        }

        int comparer = newNode.CompareTo(node);

        if (comparer < 0)
        {
            node.Left = Insert(node.Left, newNode);
        }
        else if (comparer > 0)
        {
            node.Right = Insert(node.Right, newNode);
        }
        else
        {
            // Duplicates not allowed
            ThrowIfDuplicate();
        }

        // Update node's height
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        // Balance the tree
        return BalanceTree(node);
    }

    /// <summary>
    /// Private recursive method to attempt to insert a new node into this tree without any exceptions.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="newNode">New node to insert.</param>
    /// <returns>New root node of this tree.</returns>
    /// <remarks>Safe version of insert, no exceptions will be thrown.
    /// This method will automatically balance the tree.</remarks>
    private AVLNode<T>? TryInsert(AVLNode<T>? node, AVLNode<T> newNode)
    {
        // Tree is empty
        if (node == null)
        {
            return newNode;
        }

        int comparer = newNode.CompareTo(node);

        if (comparer < 0)
        {
            node.Left = Insert(node.Left, newNode);
        }
        else if (comparer > 0)
        {
            node.Right = Insert(node.Right, newNode);
        }
        else
        {
            // Duplicates
            return null;
        }

        // Update node's height
        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        // Balance the tree
        return BalanceTree(node);
    }

    #endregion

    #region Delete

    /// <summary>
    /// Deletes the node containing the specified value from this tree.
    /// </summary>
    /// <param name="value">Value to delete.</param>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Value is null.</exception>
    public void Delete(T value)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(value);

        bool removed = false;
        m_Root       = Delete(m_Root, value, ref removed);
        
        if (removed)
        {
            --m_Count;
        }
    }

    /// <summary>
    /// Deletes the specified node from this tree.
    /// </summary>
    /// <param name="node">Node to delete.</param>
    /// <remarks>Note: The specified node does not need to belong to this tree.
    /// Deletion will occur by matching node's value.</remarks>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="ArgumentNullException">Node's value is null.</exception>
    public void Delete(AVLNode<T>? node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);
        ThrowIfNull(node!.Value);

        bool removed = false;
        m_Root       = Delete(m_Root, node!.Value, ref removed);

        if (removed)
        {
            --m_Count;
        }
    }

    /// <summary>
    /// Attempts to delete the node containing the specified value from this tree.
    /// </summary>
    /// <param name="value">Value to delete.</param>
    /// <returns>True if deletion is successful; otherwise false.</returns>
    /// <remarks>Safe version of delete, no exceptions will be thrown.</remarks>
    public bool TryDelete(T value)
    {
        if (IsEmpty() || value == null)
        {
            return false;
        }

        bool removed = false;
        m_Root       = Delete(m_Root, value, ref removed);

        if (removed)
        {
            --m_Count;
        }
        
        return removed;
    }

    /// <summary>
    /// Attempts to delete the specified node from this tree.
    /// </summary>
    /// <param name="node">Node to delete.</param>
    /// <returns>True if deletion is successful; otherwise false.</returns>
    /// <remarks>Safe version of delete, no exceptions will be thrown. 
    /// Deletion will occur by matching node's value.</remarks>
    public bool TryDelete(AVLNode<T>? node)
    {
        if (IsEmpty() || node == null || node.Value == null)
        {
            return false;
        }

        bool removed = false;
        m_Root       = Delete(m_Root, node.Value, ref removed);

        if (removed)
        {
            --m_Count;
        }

        return removed;
    }

    /// <summary>
    /// Private recursive method to delete a value from this tree.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="value">Value to remove.</param>
    /// <returns>New root node.</returns>
    private AVLNode<T> Delete(AVLNode<T>? node, T value, ref bool removed)
    {
        if (node == null)
        {
            return null!;
        }

        int comparer = value.CompareTo(node.Value);

        if (comparer < 0)
        {
            node.Left = Delete(node.Left, value, ref removed);
        }
        else if (comparer > 0)
        {
            node.Right = Delete(node.Right, value, ref removed);
        }
        else
        {
            // Only one child or no child
            if (node.Left == null || node.Right == null)
            {
                AVLNode<T>? temp = node.Left ?? node.Right;

                // No child
                if (temp == null)
                {
                    node.Tree = null;
                    node = null;
                }
                // One child
                else
                {
                    node = temp;
                }
            }
            else
            {
                AVLNode<T> temp = Successor(node)!;
                node.Value      = temp.Value;
                node.Right      = Delete(node.Right, temp.Value, ref removed);
            }

            removed = true;
        }

        if (node == null)
        {
            return null!;
        }

        node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

        return BalanceTree(node);
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
    private void ClearNodes(AVLNode<T>? node)
    {
        if (node == null)
        {
            return;
        }

        ClearNodes(node.Left);
        ClearNodes(node.Right);

        node.Left  = null;
        node.Right = null;
        node.Tree  = null;
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
    public AVLNode<T>? Find(T value)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(value);
        
        return GetNode(m_Root, value);
    }

    /// <summary>
    /// Attempts to find and return the node containing the specified value.
    /// </summary>
    /// <param name="value">Value to search for.</param>
    /// <param name="node">Node containing the value.</param>
    /// <returns>True if value is found; otherwise false.</returns>
    /// <remarks>Safe version of find, no exceptions will be thrown.</remarks>
    public bool TryFind(T value, out AVLNode<T>? node)
    {
        if (IsEmpty() || value == null)
        {
            node = null;
            return false;
        }

        node = GetNode(m_Root, value);
        return true;
    }

    /// <summary>
    /// Private recursive method to get a node with the specified value.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <param name="value">Value to search for.</param>
    /// <returns>Node containing the value.</returns>
    private AVLNode<T>? GetNode(AVLNode<T>? node, T value)
    {
        if (node == null)
        {
            return null;
        }

        int comparer = value.CompareTo(node.Value);

        if (comparer < 0)
        {
            return GetNode(node.Left, value);
        }

        if (comparer > 0)
        {
            return GetNode(node.Right, value);
        }

        return node;
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
    public bool Contains(AVLNode<T>? node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);

        return node!.Tree == this;
    }

    #endregion

    #region Min, Max

    /// <summary>
    /// Finds and returns the minimum value in this tree.
    /// </summary>
    /// <returns>Min value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public T MinValue()
    {
        ThrowIfEmpty(this);

        AVLNode<T> node = m_Root!;
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node.Value;
    }

    /// <summary>
    /// Finds and returns the maximum value in this tree.
    /// </summary>
    /// <returns>Max value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public T MaxValue()
    {
        ThrowIfEmpty(this);

        AVLNode<T> node = m_Root!;
        while (node.Right != null)
        {
            node = node.Right;
        }

        return node.Value;
    }

    /// <summary>
    /// Finds and returns the node containing the minimum value in this tree.
    /// </summary>
    /// <returns>Node containing the min value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public AVLNode<T>? MinNode()
    {
        ThrowIfEmpty(this);

        AVLNode<T> node = m_Root!;
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node;
    }

    /// <summary>
    /// Finds and returns the node containing the maximum value in this tree.
    /// </summary>
    /// <returns>Node containing the max value.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    public AVLNode<T>? MaxNode()
    {
        ThrowIfEmpty(this);

        AVLNode<T> node = m_Root!;
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
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="ArgumentException">Node belongs to another tree.</exception>
    public AVLNode<T>? Predecessor(AVLNode<T>? node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);
        ThrowIfNodeDoesNotBelong(this, node!);

        // If node has left child, the predecessor is the largest node in left subtree
        if (node!.Left != null)
        {   
            AVLNode<T> temp = node.Left;
            while (temp.Right != null)
            {
                temp = temp.Right;
            }

            return temp;
        }
        // Predecessor is above this node
        else
        {
            AVLNode<T>? predecessor = null;
            AVLNode<T>? current     = m_Root;

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
                    current     = current.Right;
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
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="ArgumentException">Node belongs to another tree.</exception>
    public AVLNode<T>? Successor(AVLNode<T>? node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);
        ThrowIfNodeDoesNotBelong(this, node!);

        // If node has right child, the successor is the smallest node in the right subtree
        if (node!.Right != null)
        {
            AVLNode<T> temp = node.Right;
            while (temp.Left != null)
            {
                temp = temp.Left;
            }

            return temp;
        }
        // Successor is above this node
        else
        {
            AVLNode<T>? successor = null;
            AVLNode<T>? current   = m_Root;

            while (current != null)
            {
                int comparer = node.CompareTo(current);

                if (comparer < 0)
                {
                    // Successor must be in left subtree,
                    // since curret node is bigger than node
                    successor = current;
                    current   = current.Left;
                }
                else if (comparer > 0)
                {
                    // Successor must be in right subtree,
                    // since it must be bigger than current node
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
    public static void PrintInorder(AVLNode<T>? node)
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
    public static void PrintPreorder(AVLNode<T>? node)
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
    public static void PrintPostorder(AVLNode<T>? node)
    {
        if (node == null)
            return;

        PrintPostorder(node.Left);
        PrintPostorder(node.Right);
        Console.Write(node.Value.ToString() + ", ");
    }

    #endregion

    #region Rotation

    /// <summary>
    /// Perform a left rotation for the specified node.
    /// </summary>
    /// <param name="a">Node to perform rotation on.</param>
    /// <returns>New source node after rotation.</returns>
    private AVLNode<T> RotateLeft(AVLNode<T> a)
    {
        AVLNode<T>? b = a.Right;
        AVLNode<T>? c = b!.Left;

        b.Left  = a;
        a.Right = c;

        a.Height = 1 + Math.Max(Height(a.Left), Height(a.Right));
        b.Height = 1 + Math.Max(Height(b.Left), Height(b.Right));

        return b;
    }

    /// <summary>
    /// Perform a right rotation for the specified node.
    /// </summary>
    /// <param name="a">Node to perform rotation on.</param>
    /// <returns>New source node after rotation.</returns>
    private AVLNode<T> RotateRight(AVLNode<T> a)
    {
        AVLNode<T>? b = a.Left;
        AVLNode<T>? c = b!.Right;

        b.Right = a;
        a.Left  = c;

        a.Height = 1 + Math.Max(Height(a.Left), Height(a.Right));
        b.Height = 1 + Math.Max(Height(b.Left), Height(b.Right));

        return b;
    }

    /// <summary>
    /// Balance tree from the specified node.
    /// </summary>
    /// <param name="node">Source node.</param>
    /// <returns>New source node after balancing.</returns>
    private AVLNode<T> BalanceTree(AVLNode<T> node)
    {
        int balanceFactor = GetBalanceFactor(node);

        if (balanceFactor > 1)
        {
            // Left Right Case
            if (GetBalanceFactor(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left!);
            }

            // Left Left Case
            return RotateRight(node);
        }

        if (balanceFactor < -1)
        {
            // Right Left Case
            if (GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right!);
            }

            // Right Right Case
            return RotateLeft(node);
        }

        return node;
    }

    #endregion

    #region Height, Depth, Balance Factor

    /// <summary>
    /// Gets the height of the specified node.
    /// </summary>
    /// <param name="node">Node to get height of.</param>
    /// <returns>Height of the node.</returns>
    public int Height(AVLNode<T>? node)
    {
        return (node == null) ? 0 : node.Height;
    }

    /// <summary>
    /// Get the depth  from source node to target node.
    /// </summary>
    /// <param name="source">Source node.</param>
    /// <param name="target">Target node.</param>
    /// <returns>Depth  from source to target if valid; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="ArgumentException">Node belongs to another tree.</exception>
    public int Depth(AVLNode<T>? source, AVLNode<T>? target)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(target);

        if (source == null)
            return -1;

        ThrowIfNodeDoesNotBelong(this, source!);
        ThrowIfNodeDoesNotBelong(this, target!);

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
    private int GetBalanceFactor(AVLNode<T>? root)
    {
        return (root == null) ? 0 : Height(root.Left) - Height(root.Right);
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Determines if this tree is empty by checking if the root node exists.
    /// </summary>
    /// <returns>True if empty; otherwise false.</returns>
    public bool IsEmpty()
    {
        return m_Root == null;
    }

    /// <summary>
    /// Determines if this tree is balanced. The tree is balanced if the balance factor is within
    /// the range (-1, 0, 1).
    /// </summary>
    /// <returns>True if balanced; otherwise false;</returns>
    public bool IsBalanced()
    {
        return Math.Abs(GetBalanceFactor(m_Root)) <= 1;
    }

    #endregion

    #region Enumerators

    /// <summary>
    /// Gets the enumerator for this tree.
    /// </summary>
    /// <returns>Enumerator.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(this);
    }

    /// <summary>
    /// Gets the enumerator for this tree.
    /// </summary>
    /// <returns>Enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Enumerator for AVL Tree.
    /// </summary>
    private class Enumerator : IEnumerator<T>
    {
        /// <summary>
        /// Private property containing the stack containing nodes from the tree.
        /// </summary>
        private Stack<AVLNode<T>> stack;

        /// <summary>
        /// Private property containing the current node.
        /// </summary>
        private AVLNode<T>? current;

        /// <summary>
        /// Private property containing the AVL Tree.
        /// </summary>
        private AVLTree<T> tree;

        /// <summary>
        /// The current node.
        /// </summary>
        public T Current
        {
            get
            {
                if (current == null)
                {
                    throw new InvalidOperationException();
                }

                return current.Value;
            }
        }

        /// <summary>
        /// IEnumerator current node.
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// Initializes the properties.
        /// </summary>
        /// <param name="tree">AVL Tree.</param>
        public Enumerator(AVLTree<T> tree)
        {
            this.tree = tree;
            stack = new();
            current = null;

            PushLeft(tree.Root!);
        }

        /// <summary>
        /// Push the left most nodes into the stack.
        /// </summary>
        /// <param name="node">Nodes to push.</param>
        private void PushLeft(AVLNode<T> node)
        {
            while (node != null)
            {
                stack.Push(node);
                node = node.Left!;
            } 
        }

        /// <summary>
        /// Move method for IEnumerator.
        /// </summary>
        /// <returns>True if there are nodes in the stack; otherwise false.</returns>
        public bool MoveNext()
        {
            if (stack.Count == 0)
            {
                return false;
            }

            current = stack.Pop();
            PushLeft(current.Right!);

            return true;
        }

        /// <summary>
        /// Reset method for IEnumerator
        /// </summary>
        public void Reset()
        {
            stack.Clear();
            current = null;
            PushLeft(tree.Root!);
        }

        /// <summary>
        /// Dispose method for IEnumerator
        /// </summary>
        public void Dispose()
        {
            
        }
    }

    #endregion

    #region Exceptions

    /// <summary>
    /// Throws if the given tree is empty.
    /// </summary>
    /// <exception cref="InvalidOperationException">Tree is empty.</exception>
    private static void ThrowIfEmpty(AVLTree<T> tree)
    {
        if (tree.IsEmpty())
        {
            throw new InvalidOperationException("Tree is empty.");
        }
    }

    /// <summary>
    /// Throws if the given item is null.
    /// </summary>
    /// <param name="item">Item to check.</param>
    /// <exception cref="ArgumentNullException">Item is null.</exception>
    private static void ThrowIfNull(object? item)
    {
        ArgumentNullException.ThrowIfNull(item, "Node is null.");
    }

    /// <summary>
    /// Throws if the given node is already assigned to another tree.
    /// </summary>
    /// <param name="node">Node to check.</param>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    private static void ThrowIfNodeAssigned(AVLNode<T> node)
    {
        if (node.Tree != null)
        {
            throw new InvalidOperationException("Node can only be assigned to one list.");
        }
    }

    /// <summary>
    /// Throws if the given node is not assigned to any tree.
    /// </summary>
    /// <param name="node">Node to check.</param>
    /// <exception cref="InvalidOperationException">Node is not assigned.</exception>
    private static void ThrowIfNodeNotAssigned(AVLNode<T> node)
    {
        if (node.Tree == null)
        {
            throw new InvalidOperationException("Node is not assigned.");
        }
    }

    /// <summary>
    /// Throws if the given node does not belong to the tree.
    /// </summary>
    /// <param name="node">Node to check</param>
    /// <exception cref="ArgumentException">Node belongs to another tree.</exception>
    private static void ThrowIfNodeDoesNotBelong(AVLTree<T> tree, AVLNode<T> node)
    {
        if (node.Tree != tree)
        {
            throw new ArgumentException("Node belongs to another tree.");
        }
    }

    /// <summary>
    /// Throws if the given index is out of range of the tree.
    /// </summary>
    /// <param name="index">Index to check.</param>
    /// <exception cref="ArgumentOutOfRangeException">Index out of range.</exception>
    private static void ThrowIfOutOfRange(AVLTree<T> tree, int index)
    {
        if (index <= 0 || index > tree.m_Count)
        {
            throw new ArgumentOutOfRangeException("Index is out of range.");
        }
    }

    /// <summary>
    /// Throws if inserting duplicate value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Duplicate values not allowed.</exception>
    private static void ThrowIfDuplicate()
    {
        throw new InvalidOperationException("Duplicate values not allowed.");
    }

    #endregion
}
