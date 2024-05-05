namespace DSA.BSTrees;

/// <summary>
/// Binary search tree nodes used in BSTrees.
/// </summary>
public class BSTreeNode<T> where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Value contain in the node.
    /// </summary>
    public T Value;

    /// <summary>
    /// Left child.
    /// </summary>
    public BSTreeNode<T>? Left;

    /// <summary>
    /// Right child.
    /// </summary>
    public BSTreeNode<T>? Right;

    /// <summary>
    /// Tree the node belongs to.
    /// </summary>
    public BSTree<T>? Tree;

    #endregion

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">Value to insert into node.</param>
    public BSTreeNode(T value)
    {
        Value = value;
        Left  = null;
        Right = null;
        Tree  = null;
    }

    public int CompareTo(BSTreeNode<T> other) => Value.CompareTo(other.Value);
}