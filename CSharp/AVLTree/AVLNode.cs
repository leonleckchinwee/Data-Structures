namespace DSA.AVLTrees;

/// <summary>
/// AVL nodes used in AVLTrees.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AVLNode<T> where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Value contain in the node.
    /// </summary>
    public T Value;

    /// <summary>
    /// Left child.
    /// </summary>
    public AVLNode<T>? Left;

    /// <summary>
    /// Right child.
    /// </summary>
    public AVLNode<T>? Right;

    /// <summary>
    /// Tree the node belongs to.
    /// </summary>
    public AVLTree<T>? Tree;

    /// <summary>
    /// Height of node.
    /// </summary>
    public int Height;

    #endregion

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">Value to insert into node.</param>
    public AVLNode(T value)
    {
        Value  = value;
        Left   = null;
        Right  = null;
        Tree   = null;
        Height = -1;
    }

    /// <summary>
    /// Compares the current node value to the other node value.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="other">Other node to compare with.</param>
    /// <returns>Negative if this node is smaller; Positive is this node is bigger; 0 if equal.</returns>
    public int CompareTo(AVLNode<T> other) => Value.CompareTo(other.Value);
}