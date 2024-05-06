namespace DSA.AVLTrees;

/// <summary>
/// Self-balancing binary search tree.
/// The tree does not allow duplicates.
/// Null value is allowed for reference types.
/// </summary>
public class AVLTree<T> where T : IComparable<T>
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
    /// <param name="value">Value to insert as the root node of the tree.</param>
    public AVLTree(T value)
    {
        m_Root  = new AVLNode<T>(value);
        m_Count = 1;
    }

    #endregion

}
