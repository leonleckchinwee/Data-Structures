using System.Collections;

namespace DSA.Stacks;

/// <summary>
/// Represents a last-in, first-out collection of items.
/// This class accepts null value for reference types.
/// This class allows for duplicate values.
/// </summary>
public class MyStack<T> : IEnumerable<T> where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Private property for the default capacity of the stack.
    /// </summary>
    private readonly int m_DefaultCapacity = 16;

    /// <summary>
    /// Private property containing the array holding all the items in the stack.
    /// </summary>
    private T[] m_Items;

    /// <summary>
    /// Private property containing the number of items in the stack.
    /// </summary>
    private int m_Count;

    /// <summary>
    /// Private property containing the max capacity of the stack.
    /// </summary>
    private int m_MaxCount;

    /// <summary>
    /// Gets the number of items in the stack.
    /// </summary>
    public int Count => m_Count;

    /// <summary>
    /// Gets the max capacity of the queue.
    /// </summary>
    public int MaxCount => m_MaxCount;

    /// <summary>
    /// Square bracket operator for accessing value at specified index.
    /// </summary>
    /// <param name="index">Index of value.</param>
    /// <returns>Value at the specified index.</returns>
    /// <remarks>This list is zero-index.</remarks>
    public T this[int index]
    {
        get => GetValue(index);
        set => SetValue(index, value);
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new empty stack with default capacity (16).
    /// </summary>
    public MyStack()
    {
        m_MaxCount = m_DefaultCapacity;
        m_Items    = new T[m_MaxCount];
        m_Count    = 0;
    }

    /// <summary>
    /// Initializes a new empty stack with specified capacity.
    /// </summary>
    /// <param name="capacity">Capacity of the stack.</param>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is too large.</exception>
    public MyStack(int capacity)
    {
        ThrowIfNegative(capacity);
        ThrowIfTooLarge(capacity);

        m_MaxCount = capacity;
        m_Items    = new T[m_MaxCount];
        m_Count    = 0;
    }

    /// <summary>
    /// Initializes a new stack with items copied from the specified collection.
    /// </summary>
    /// <param name="collection">Collection to copy from.</param>
    /// <remarks>
    /// The capacity of the stack will be the same as the number of items in the stack.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Collection is null.</exception>
    public MyStack(IEnumerable<T> collection)
    {
        ThrowIfNull(collection);

        m_MaxCount = collection.Count();
        m_Items    = new T[m_MaxCount];
        m_Count    = m_MaxCount;

        int index = 0;
        foreach (T item in collection)
        {
            m_Items[index] = item;
            ++index;
        }
    }

    #endregion

    #region Push, Pop, Peek, Clear

    /// <summary>
    /// Push an item to the top of the stack.
    /// </summary>
    /// <param name="item">Item to push.</param>
    public void Push(T item)
    {
        if (m_Count < m_Items.Length)
        {
            m_Items[m_Count] = item;
            ++m_Count;
        }
        else
        {
            EnsureCapacity(m_Count + 1);
            m_Items[m_Count] = item;
            ++m_Count;
        }
    }

    /// <summary>
    /// Remove and return the item at the top of the stack.
    /// </summary>
    /// <returns>Item at the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public T Pop()
    {
        ThrowIfEmpty(m_Count);

        int tempCount = m_Count - 1;

        T[] copy = m_Items;
        T item   = copy[tempCount];

        copy[tempCount] = default!;

        m_Count = tempCount;

        return item;
    }

    /// <summary>
    /// Attempts to remove and return the item at the top of the stack.
    /// </summary>
    /// <param name="item">Item at the top of the stack.</param>
    /// <returns>True if item is removed successfully; otherwise false.</returns>
    public bool TryPop(out T item)
    {
        if (IsEmpty())
        {
            item = default!;
            return false;
        }

        int tempCount = m_Count - 1;

        T[] copy = m_Items;
        item   = copy[tempCount];

        copy[tempCount] = default!;

        m_Count = tempCount;

        return true;
    }

    /// <summary>
    /// Returns the item at the start of the stack. 
    /// The stack remains unchanged.
    /// </summary>
    /// <returns>Item at the start of the stack.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public T Peek()
    {
        ThrowIfEmpty(m_Count);

        T[] copy = m_Items;

        return copy[m_Count - 1];
    }

    /// <summary>
    /// Attempts to return the item at the start of the stack. 
    /// The stack remains unchanged.
    /// </summary>
    /// <param name="item">Item at the start of the stack.</param>
    /// <returns>True if item exists and returned successully; otherwise false.</returns>
    public bool TryPeek(out T item)
    {
        if (IsEmpty())
        {
            item = default!;
            return false;
        }

        T[] copy = m_Items;
        item = copy[m_Count - 1];

        return true;
    }

    /// <summary>
    /// Clears the entire stack..
    /// </summary>
    public void Clear()
    {
        if (m_Count != 0)
        {
            Array.Clear(m_Items, 0, m_Count);
            m_Count = 0;
        }
    }

    #endregion

    #region Search

    /// <summary>
    /// Determines if the specified item is in the stack.
    /// </summary>
    /// <param name="item">Item to find.</param>
    /// <returns>True if item is found; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public bool Contains(T item)
    {
        ThrowIfEmpty(m_Count);

        for (int i = 0; i < m_Count; ++i)
        {
            if (m_Items[i].Equals(item))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Finds and returns the minimum value in the stack.
    /// </summary>
    /// <returns>Minimum value in the stack.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public T MinValue()
    {
        ThrowIfEmpty(m_Count);

        T min = m_Items[0];
        foreach (T item in m_Items)
        {
            if (item.CompareTo(min) < 0)
                min = item;
        }

        return min;
    }

    /// <summary>
    /// Finds and returns the maximum value in the stack.
    /// </summary>
    /// <returns>Maximum value in the stack.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public T MaxValue()
    {
        ThrowIfEmpty(m_Count);

        T max = m_Items[0];
        foreach (T item in m_Items)
        {
            if (item.CompareTo(max) > 0)
                max = item;
        }

        return max;
    }

    #endregion

    #region Get, Set, Index

    /// <summary>
    /// Returns the value at the specified index of the stack.
    /// </summary>
    /// <param name="index">Index to get value from.</param>
    /// <returns>Value at specified index.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public T GetValue(int index)
    {
        ThrowIfEmpty(m_Count);
        ThrowIfIndexOutOfRange(index, m_Count - 1);

        return m_Items[index];  
    }

    /// <summary>
    /// Replaces the existing value with the specified value 
    /// at the specified index of the stack.
    /// </summary>
    /// <param name="index">Index to set value at.</param>
    /// <param name="value">Value to set.</param>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public void SetValue(int index, T value)
    {
        ThrowIfEmpty(m_Count);
        ThrowIfIndexOutOfRange(index, m_Count - 1);

        m_Items[index] = value;
    }

    /// <summary>
    /// Gets the first index of the value if it exists in the stack.
    /// </summary>
    /// <param name="item">Value to find index of.</param>
    /// <returns>Index of the item if found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public int IndexOf(T item)
    {
        ThrowIfEmpty(m_Count);

        for (int i = 0; i < m_Count; ++i)
        {
            if (m_Items[i].Equals(item))
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Gets the last index of the value if it exists in the stack.
    /// </summary>
    /// <param name="item">Value to find index of.</param>
    /// <returns>Index of the item if found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public int LastIndexOf(T item)
    {
        ThrowIfEmpty(m_Count);

        for (int i = m_Count - 1; i >= 0; --i)
        {
            if (m_Items[i].Equals(item))
                return i;
        }

        return -1;
    }

    #endregion

    #region Capacity

    /// <summary>
    /// Ensures the capacity of the stack is at least the specified capacity.
    /// </summary>
    /// <param name="capacity">The minimum capacity to ensure.</param>
    /// <returns>New capacity of the queue.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is too large.</exception>
    public int EnsureCapacity(int capacity)
    {
        ThrowIfNegative(capacity);
        ThrowIfTooLarge(capacity);

        // Only resize if specified capacity is larger than current capacity
        if (m_MaxCount < capacity)
        {
            ExpandCapacity(capacity);
        }

        return m_MaxCount;
    }

    /// <summary>
    /// Trims the excess spaces in the stack.
    /// </summary>
    /// <remarks>This method if attempt to reduce the size of the stack by 10%.</remarks>
    public void TrimExcess()
    {
        int halfCapacity = (int)(m_MaxCount * 0.9f);

        if (m_Count <= halfCapacity)
        {
            m_MaxCount = halfCapacity;
            Array.Resize(ref m_Items, m_MaxCount);
        }
    }

    /// <summary>
    /// Private method to expand the current max capacity of the stack by double the size.
    /// </summary>
    /// <param name="capacity">Minimum capacity to expand to.</param>
    private void ExpandCapacity(int capacity)
    {
        int newCapacity = (m_MaxCount == 0) ? m_DefaultCapacity : m_MaxCount * 2;

        if (newCapacity > Array.MaxLength)
        {
            newCapacity = Array.MaxLength;
        }

        if (newCapacity < capacity)
        {
            newCapacity = capacity;
        }

        m_MaxCount = newCapacity;
        Array.Resize(ref m_Items, newCapacity);
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Determines if the stack is empty.
    /// </summary>
    /// <returns>True if stack is empty; otherwise false.</returns>
    public bool IsEmpty()
    {
        return m_Count == 0;
    }

    /// <summary>
    /// Determines if the stack is full.
    /// </summary>
    /// <returns>True if stack is full; otherwise false.</returns>
    public bool IsFull()
    {
        return m_Count == m_MaxCount;
    }

    /// <summary>
    /// Copies all elements from the stack into an array.
    /// </summary>
    /// <returns>Array containing all the values from the stack.</returns>
    public T[] ToArray()
    {
        if (IsEmpty())
            return [];

        T[] copy = m_Items;
        return copy;
    }

    /// <summary>
    /// Copies all elements from the stack into a list.
    /// </summary>
    /// <returns>List containing all the values from the stack.</returns>
    public List<T> ToList()
    {
        if (IsEmpty())
            return [];

        List<T> copy = [.. m_Items];
        return copy;
    }


    #endregion

    #region Enumerators

    /// <summary>
    /// Gets the enumerator for this stack.
    /// </summary>
    /// <returns>Enumerator.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in m_Items)
        {
            yield return item;
        }
    }

    /// <summary>
    /// Gets the enumerator for this stack.
    /// </summary>
    /// <returns>Enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region Exceptions

    /// <summary>
    /// Throws if object is null.
    /// </summary>
    /// <param name="item">Item to check.</param>
    /// <exception cref="InvalidOperationException">Object is null.</exception>
    private static void ThrowIfNull(object? item)
    {
        if (item == null)
            throw new InvalidOperationException(nameof(item));
    }

    /// <summary>
    /// Throws if item is negative.
    /// </summary>
    /// <param name="item">Item to check.</param>
    /// <exception cref="ArgumentOutOfRangeException">Item is negative.</exception>
    private static void ThrowIfNegative(int item)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(item, nameof(item));
    }

    /// <summary>
    /// Throws if item is too large.
    /// </summary>
    /// <param name="item">Item to check.</param>
    /// <exception cref="ArgumentOutOfRangeException">Item is too large.</exception>
    private static void ThrowIfTooLarge(int item)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(item, Array.MaxLength, nameof(item));
    }

    /// <summary>
    /// Throws if item is zero.
    /// </summary>
    /// <param name="item">Item to check.</param>
    /// <exception cref="InvalidOperationException">Item is empty.</exception>
    private static void ThrowIfEmpty(int item)
    {
        if (item == 0)
            throw new InvalidOperationException(nameof(item));
    }

    /// <summary>
    /// Throws if index is out of range.
    /// </summary>
    /// <param name="index">Index to check.</param>
    /// <param name="count">Count to check.</param>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    private static void ThrowIfIndexOutOfRange(int index, int count)
    {
        if (index < 0 || index > count)
            throw new IndexOutOfRangeException(nameof(index));
    }

    #endregion
}
