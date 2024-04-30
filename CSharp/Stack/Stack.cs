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

        Items    = new object[capacity];
        Top      = -1;
        MaxCount = capacity;
    }

    #endregion

    #region Add

    /// <summary>
    /// Inserts an item at the top of the stack.
    /// </summary>
    /// <param name="item">The item to push onto the stack. The item can be null.</param>
    /// <remarks>The internal array will resize by twice the size to accomodate more items when needed.</remarks>
    public void Push(object? item)
    {
        CheckForResize();

        ++Top;
        Items[Top] = item!;
    }

    /// <summary>
    /// Inserts an item into the stack at the specified index.
    /// </summary>
    /// <param name="index">Index to insert item.</param>
    /// <param name="item">Item to insert.</param>
    /// <exception cref="ArgumentOutOfRangeException">index out of range.</exception>
    public void InsertAt(int index, object? item)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range!");

        CheckForResize();

        // Move items after index to the right
        for (int i = Count; i > index; --i)
        {
            Items[i] = Items[i - 1];
        }

        ++Top;
        Items[index] = item!;
    }

    /// <summary>
    /// Concatenates another stack to the current stack.
    /// </summary>
    /// <param name="other">The other stack to concat.</param>
    /// <remarks>The new capacity will be the sum of both stacks.</remarks>
    public void Concat(Stack other)
    {
        if (other.Count == 0)
            return;

        ForceResize(this.MaxCount + other.MaxCount);

        Array.Copy(other.Items, 0, this.Items, Count, other.Count);
        Top += other.Count;
    }

    #endregion

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

    /// <summary>
    /// Removes and returns the item at the specified index of the stack.
    /// </summary>
    /// <param name="index">Item's index.</param>
    /// <returns>The item at the specified index of the stack.</returns>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">index out of range.</exception>
    public object? RemoveAt(int index)
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range!");

        object? item = Items[index];

        for (int i = index; i < Count - 1; ++i)
        {
            Items[i] = Items[i + 1];
        }

        Items[Count - 1] = null!;

        --Top;
        TrimExcess();

        return item;
    }

    /// <summary>
    /// Removes specified item from the stack.
    /// </summary>
    /// <param name="item">Item to remove.</param>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">index out of range.</exception>
    public void Remove(object? item)
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        int index = IndexOf(item);

        ArgumentOutOfRangeException.ThrowIfEqual(index, -1, nameof(item));

        for (int i = index; i < Count - 1; ++i)
        {
            Items[i] = Items[i + 1];
        }

        Items[Count - 1] = null!;

        --Top;
        TrimExcess();
    }

    /// <summary>
    /// Removes all items from the stack. The capacity remains unchanged.
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

    /// <summary>
    /// Returns the item at the specified index of the stack without removing it.
    /// </summary>
    /// <param name="index">Index to peek at.</param>
    /// <returns>The item at the specified index of the stack.</returns>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">index out of range.</exception>
    public object? Peek(int index)
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index out of range!");

        return Items[index];
    }

    /// <summary>
    /// Finds the first index of specified item in the stack.
    /// </summary>
    /// <param name="item">Item to find index of.</param>
    /// <returns>Index of item found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    public int IndexOf(object? item)
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        bool nullCheck = item == null;

        for (int i = 0; i < Count; ++i)
        {
            if (nullCheck)
            {
                if (Items[i] == item)
                    return i;
            }
            else
            {
                if (Items[i].Equals(item))
                    return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Finds the last index of specified item in the stack.
    /// </summary>
    /// <param name="item">Item to find index of.</param>
    /// <returns>Index of item found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    public int LastIndexOf(object? item)
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        bool nullCheck = item == null;

        for (int i = Count - 1; i >= 0; --i)
        {
            if (nullCheck)
            {
                if (Items[i] == item)
                    return i;
            }
            else
            {
                if (Items[i].Equals(item))
                    return i;
            }
        }

        return -1;
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

    /// <summary>
    /// Determines if specified item is in the stack.
    /// </summary>
    /// <param name="item">Item to check for.</param>
    /// <returns>True if stack contains the item; otherwise false.</returns>
    /// <remarks>This function uses loop unrolling. It is a linear search, and will find the first item it encounters.</remarks>
    public bool Contains(object? item)
    {
        if (IsEmpty())
            return false;

        int remaining = Count % 4;
        int i = 0;

        bool nullCheck = item == null;

        // Loop unrolling
        for (; i < Count - remaining; i += 4)
        {
            if (nullCheck)
            {
                if (Items[i] == item || Items[i + 1] == item || Items[i + 2] == item || Items[i + 3] == item)
                    return true;
            }
            else
            {
                if (Items[i].Equals(item) || Items[i + 1].Equals(item) || Items[i + 2].Equals(item) || Items[i + 3].Equals(item))
                    return true;     
            }
        }

        for (; i < Count; ++i)
        {
            if (nullCheck)
            {
                if (Items[i] == item)
                    return true;
            }    
            else
            {
                if (Items[i].Equals(item))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Reverses the order of all the items in the stack.
    /// </summary>
    /// <exception cref="InvalidOperationException">stack is empty.</exception>
    public void Reverse()
    {
        if (IsEmpty())
            throw new InvalidOperationException("The stack is empty!");

        Array.Reverse(Items, 0, Count);

        Top = Count - 1;
    }

    /// <summary>
    /// Copies the stack to a new array.
    /// </summary>
    /// <returns>A new array containing all the items in the stack.</returns>
    public object?[] ToArray()
    {
        return Items.ToArray();
    }

    /// <summary>
    /// Copies the stack to a new list.
    /// </summary>
    /// <returns>A new list containing all the items in the stack.</returns>
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

    /// <summary>
    /// Force a max capacity resize of the stack.
    /// </summary>
    /// <param name="newCapacity">capacity is negative.</param>
    private void ForceResize(int newCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(newCapacity, 0, nameof(newCapacity));

        Array.Resize(ref Items, newCapacity);
    }

    /// <summary>
    /// Trims the excess spaces for memory efficiency. 
    /// </summary>
    /// <remarks>Checks if max count can be reduced by half.</remarks>
    private void TrimExcess()
    {
        int halfSize = MaxCount / 2;
        MaxCount = (Count <= halfSize) ? halfSize : MaxCount;

        Array.Resize(ref Items, MaxCount);
    }

    #endregion
}
