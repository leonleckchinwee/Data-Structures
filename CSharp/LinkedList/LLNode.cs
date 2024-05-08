namespace DSA.LinkedLists;

/// <summary>
/// Node for linked-lists.
/// This node accepts null value for reference types.
/// </summary>
public class LLNode<T>(T value) where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Gets the value contained in this node.
    /// </summary>
    public T Value = value;

    /// <summary>
    /// Gets the next node in the list.
    /// </summary>
    public LLNode<T>? Next = null;

    /// <summary>
    /// Gets the previous node in the list.
    /// </summary>
    public LLNode<T>? Previous = null;

    /// <summary>
    /// Gets the list that this node belongs to.
    /// </summary>
    public LList<T>? List = null;

    #endregion

    public int CompareTo(LLNode<T> other) => Value.CompareTo(other.Value);

    #region Overrides

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>Hash value.</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>String representing this node.</returns>
    public override string ToString()
    {
        return $"{Value}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <returns>True if equal; otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not LLNode<T>)
            return false;

        LLNode<T> node = (LLNode<T>)obj;

        return Value.Equals(node.Value);
    }

    #endregion

    public static LLNode<T> Copy(LLNode<T> node)
    {
        ArgumentNullException.ThrowIfNull(node, "Cannot copy null node.");

        LLNode<T> newNode = new(node.Value) 
        {
            Next     = node.Next,
            Previous = node.Previous,
            List     = node.List
        };

        return newNode;
    }
}