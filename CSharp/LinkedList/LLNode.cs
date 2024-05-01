namespace DSA.LinkedLists;

public class LLNode<T> where T : notnull, IComparable<T>
{
    #region Properties

    /// <summary>
    /// Gets the value contained in this node.
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// Gets the next node in the list.
    /// </summary>
    public LLNode<T>? Next { get; set; }

    /// <summary>
    /// Gets the previous node in the list.
    /// </summary>
    public LLNode<T>? Previous { get; set; }

    /// <summary>
    /// Gets the list that this node belongs to.
    /// </summary>
    public LList<T>? List { get; internal set; }

    #endregion

    #region Constructor
    
    /// <summary>
    /// Initializes a new instance containing the specified value.
    /// </summary>
    public LLNode(T value)
    {
        Value    = value;
        Next     = null;
        Previous = null;
        List     = null;
    }

    #endregion

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

    #region Operators

    /// <summary>
    /// Determines whether left node's value is smaller or equal to right node's value.
    /// </summary>
    public static bool operator <= (LLNode<T>? left, LLNode<T>? right)
    {
        if (left == null || right == null)
            return false;

        return left.Value.CompareTo(right.Value) <= 0;
    }

    /// <summary>
    /// Determines whether left node's value is greater or equal to right node's value.
    /// </summary>
    public static bool operator >= (LLNode<T>? left, LLNode<T>? right)
    {
        if (left == null || right == null)
            return false;

        return left.Value.CompareTo(right.Value) >= 0;
    }

    /// <summary>
    /// Determines whether left node's value is smaller than right node's value.
    /// </summary>
    public static bool operator < (LLNode<T>? left, LLNode<T>? right)
    {
        if (left == null || right == null)
            return false;

        return left.Value.CompareTo(right.Value) < 0;
    }

    /// <summary>
    /// Determines whether left node's value is greater than right node's value.
    /// </summary>
    public static bool operator > (LLNode<T>? left, LLNode<T>? right)
    {
        if (left == null || right == null)
            return false;

        return left.Value.CompareTo(right.Value) > 0;
    }

    #endregion
}