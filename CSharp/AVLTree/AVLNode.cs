namespace DSA.AVLTrees;

/// <summary>
/// AVL nodes used in AVLTrees.
/// </summary>
public class AVLNode<T>(T value) where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Value contain in the node.
    /// </summary>
    public T Value = value;

    /// <summary>
    /// Left child.
    /// </summary>
    public AVLNode<T>? Left = null;

    /// <summary>
    /// Right child.
    /// </summary>
    public AVLNode<T>? Right = null;

    /// <summary>
    /// Tree the node belongs to.
    /// </summary>
    public AVLTree<T>? Tree = null;

    /// <summary>
    /// Height of node.
    /// </summary>
    public int Height = 1;

    #endregion

    /// <summary>
    /// Compares the current node value to the other node value.
    /// </summary>
    /// <remarks></remarks>
    /// <param name="other">Other node to compare with.</param>
    /// <returns>Negative if this node is smaller; Positive is this node is bigger; 0 if equal.</returns>
    public int CompareTo(AVLNode<T> other) => Value.CompareTo(other.Value);
}