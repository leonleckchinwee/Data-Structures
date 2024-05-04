namespace DSA.BSTrees;

/// <summary>
/// Binary search tree nodes used in BSTrees.
/// This only supports integers (for now).
/// </summary>
public class BSTreeNode
{
    /// <summary>
    /// Value contain in the node.
    /// </summary>
    public int Value;

    /// <summary>
    /// Left child.
    /// </summary>
    public BSTreeNode? Left;

    /// <summary>
    /// Right child.
    /// </summary>
    public BSTreeNode? Right;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">Value to insert into node.</param>
    public BSTreeNode(int value)
    {
        Value = value;
        Left  = null;
        Right = null;
    }
}