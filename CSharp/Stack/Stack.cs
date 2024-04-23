using System.Collections;

namespace DSA.Stacks;

public class Stack : ICloneable, ICollection, IEnumerable
{
    #region Properties

    /// <summary>
    /// Default stack capacity.
    /// </summary>
    private const int DefaultCapacity = 100;
    /// <summary>
    /// Holds all the items in the stack in an internal array.
    /// </summary>
    private object[] Items;

    /// <summary>
    /// Points to the top element.
    /// </summary>
    private int Top;

    /// <summary>
    /// Capacity of the stack.
    /// </summary>
    public int MaxCount { get; private set; }

    /// <summary>
    /// Number of elements in the stack.
    /// </summary>
    public int Count => Top + 1;

    /// <summary>
    /// ICollection overrides (not used).
    /// </summary>
    public bool IsSynchronized => false;
    public object SyncRoot => this;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of stack that is empty and has the default initial capacity (100).
    /// </summary>
    public Stack()
    {
        Items    = new object[DefaultCapacity];
        Top      = -1;
        MaxCount = DefaultCapacity;
    }

    /// <summary>
    /// Initializes a new instance of stack that is empty and has the specified initial capacity.
    /// </summary>
    /// <param name="capacity">The initial capacity of the stack.</param>
    /// <exception cref="ArgumentOutOfRangeException">capacity is less than zero.</exception>
    public Stack(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 0, nameof(capacity));

        Items = new object[capacity];
        Top   = -1;
        MaxCount = capacity;
    }

    #endregion

    #region Add

    #endregion

    /// <summary>
    /// Inserts an item at the top of the stack.
    /// </summary>
    /// <param name="item">The item to push onto the stack. The item can be null.</param>
    /// <remarks>The internal array will resize by twice the size to accomadate more items when needed.</remarks>
    public void Push(object? item)
    {
        CheckForResize();

        ++Top;
        Items[Top] = item!;
    }

    public void InsertAt(int index, object? item)
    {

    }

    public void Concat(Stack other)
    {

    }

    #region Remove

    /// <summary>
    /// Removes and returns the item at the top of the stack.
    /// </summary>
    /// <returns>The item at the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    public object? Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        object item = Items[Top];
        --Top;

        TrimExcess();

        return item;
    }   

    public bool RemoveAt(int index, object? item)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes all items from the stack.
    /// </summary>
    public void Clear()
    {
        Array.Clear(Items, 0, Top + 1);
        Top = -1;
    }

    #endregion

    #region Getters / Setters

    /// <summary>
    /// Returns the item at the top of the stack without removing it.
    /// </summary>
    /// <returns>The item at the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    public object? Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        return Items[Top];
    }

    public object? Peek(int index)
    {
        throw new NotImplementedException();
    }

    public int IndexOf(object? item)
    {
        throw new NotImplementedException();
    }

    public int LastIndexOf(object? item)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Determines whether the stack is empty.
    /// </summary>
    /// <returns>True if stack is empty; otherwise false.</returns>
    public bool IsEmpty()
    {
        return Top == -1;
    }

    public bool Contains(object? item)
    {
        throw new NotImplementedException();
    }

    public void Reverse()
    {

    }

    /// <summary>
    /// Copies the stack to a new array.
    /// </summary>
    /// <returns>A new array containing all the items in the stack.</returns>
    public object?[] ToArray()
    {
        return Items.ToArray();
    }

    // TODO: check this...
    public List<object?> ToList()
    {
        return [.. Items];
    }

    /// <summary>
    /// Creates a shallow copy of the stack.
    /// </summary>
    /// <returns>A shallow copy of the stack.</returns>
    public object Clone()
    {
        Stack clone = new Stack(Items.Length) { Top = this.Top };

        Array.Copy(Items, clone.Items, Items.Length);
        return clone;
    }

    /// <summary>
    /// Copies the stack to an existing one-dimenstional array, starting at the specified array index.
    /// </summary>
    /// <param name="array">The one-dimensional array to copy to. Array must have zero-based indexing.</param>
    /// <param name="index">The zero-based index in array at which copying begins.</param>
    /// <exception cref="ArgumentNullException">array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">index is less than zero.</exception>
    /// <exception cref="ArgumentException">array is multi-dimensional.</exception>
    /// <exception cref="ArgumentException">index is invalid.</exception>
    /// <exception cref="ArgumentException">array does not have enough space.</exception>
    /// <exception cref="InvalidCastException">unable to cast automatically from stack to array.</exception>
    public void CopyTo(Array array, int index)
    {
        ArgumentNullException.ThrowIfNull(array, nameof(array));
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0, nameof(index));
        
        if (array.Rank != 1 || index >= array.Length)
            throw new ArgumentException("Array must be one-dimensional!");

        int availableSpace = array.Length - index;
        if (availableSpace >= Count)
            throw new ArgumentException("Array does not have enough space to copy to!");

        if (array.GetType().GetElementType() != typeof(Stack))
            throw new InvalidCastException("Type of stack cannot be cast automatically to the type of the destination array.");

        Array.Copy(Items, 0, array, index, Count);
    }

    /// <summary>
    /// Returns an IEnumerator for the stack.
    /// </summary>
    /// <returns>IEnumerator</returns>
    public IEnumerator GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    #endregion

    #region Private

    /// <summary>
    /// Performs a resizing of the internal array if needed.
    /// </summary>
    private void CheckForResize()
    {
        if (Top == Items.Length - 1)
        {
            MaxCount = Items.Length * 2;
            Array.Resize(ref Items, MaxCount);
        }
    }

    private void TrimExcess()
    {
        int halfSize = MaxCount / 2;
        MaxCount = (Count <= halfSize) ? halfSize : MaxCount;
    }

    #endregion
}
