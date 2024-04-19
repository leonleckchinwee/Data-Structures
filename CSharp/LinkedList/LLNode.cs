namespace DSA.LinkedList;

public class LLNode<T> where T : notnull
{
    public T Value               { get; set; }  // Value

    public LLNode<T>? Next       { get; set; }  // Next pointer
    public LLNode<T>? Previous   { get; set; }  // Previous pointer

    public LList<T>? List        { get; internal set; }  // List this node belongs to

    public LLNode(T value)
    {
        Value    = value;
        Next     = null;
        Previous = null;
        List     = null;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not LLNode<T>)
            return false;

        LLNode<T> node = (LLNode<T>)obj;

        return Value.Equals(node.Value);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}