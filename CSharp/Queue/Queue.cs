using System.Collections;

namespace DSA.Queues;

/// <summary>
/// Represents a first-in, last-out collection of items.
/// This generic queue is implemented as a circular array.
/// </summary>
public class MyQueue<T> : IEnumerable<T> where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Private property for the default capacity of the queue.
    /// </summary>
    private readonly int m_DefaultCapacity = 16;

    /// <summary>
    /// Private property containing the array holding all the items in the queue.
    /// </summary>
    private T[] m_Items;

    /// <summary>
    /// Private property containing the pointer to the start of the queue.
    /// </summary>
    private int m_Head;

    /// <summary>
    /// Private property containing the pointer to the back of the queue.
    /// </summary>
    private int m_Tail;

    /// <summary>
    /// Private property containing the max capacity of the queue.
    /// </summary>
    private int m_MaxCount;

    /// <summary>
    /// Private property containing the number of items in the queue.
    /// </summary>
    private int m_Count;

    /// <summary>
    /// Gets the number of items in the queue.
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
    /// Initializes a new empty queue with default capacity (16).
    /// </summary>
    public MyQueue()
    {
        m_MaxCount = m_DefaultCapacity;
        m_Items    = new T[m_MaxCount];
        m_Count    = 0;
        m_Head     = 0;
        m_Tail     = 0;
    }

    /// <summary>
    /// Initializes a new empty queue with specified capacity.
    /// </summary>
    /// <param name="capacity">Capacity of the queue.</param>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is negative.</exception>
    public MyQueue(int capacity)
    {
        ThrowIfNegative(capacity);
        ThrowIfTooLarge(capacity);

        m_MaxCount = capacity;
        m_Items    = new T[m_MaxCount];
        m_Count    = 0;
        m_Head     = 0;
        m_Tail     = 0;
    }

    /// <summary>
    /// Initializes a new queue with items copied from the specified collection.
    /// </summary>
    /// <param name="collection">Collection to copy from.</param>
    /// <remarks>
    /// The capacity of the queue will be the same as the number of items in the queue.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Collection is null.</exception>
    public MyQueue(IEnumerable<T> collection)
    {
        ThrowIfNull(collection);

        m_MaxCount = collection.Count();
        m_Items    = new T[m_MaxCount];
        m_Count    = m_MaxCount;
        m_Head     = 0;
        m_Tail     = m_Count;

        int index = 0;
        foreach (T item in collection)
        {
            m_Items[index] = item;
            ++index;
        }
    }

    #endregion

    #region Enqueue, Dequeue, Peek, Clear

    /// <summary>
    /// Inserts an item into the end of the queue.
    /// </summary>
    /// <param name="item">Item to insert.</param>
    /// <remarks>The capacity of the queue will double in size when needed.</remarks>
    public void Enqueue(T item)
    {
        if (IsFull())
        {
            EnsureCapacity(m_Count + 1);
        }

        m_Items[m_Tail] = item;
        
        MoveIndex(ref m_Tail);

        ++m_Count;
    }

    /// <summary>
    /// Removes and returns the item at the start of the queue.
    /// </summary>
    /// <returns>Item at the start of the queue.</returns>
    /// <exception cref="InvalidOperationException">Queue is empty.</exception>
    public T Dequeue()
    {
        ThrowIfEmpty(m_Count);

        T removed = m_Items[m_Head];
        m_Items[m_Head] = default!;

        MoveIndex(ref m_Head);

        --m_Count;
        return removed;
    }

    /// <summary>
    /// Attempts to remove and return the item at the start of the queue.
    /// </summary>
    /// <param name="item">Item at the start of the queue.</param>
    /// <returns>True if item is removed successfully; otherwise false.</returns>
    public bool TryDequeue(out T item)
    {
        if (IsEmpty())
        {
            item = default!;
            return false;
        }

        item = m_Items[m_Head];

        MoveIndex(ref m_Head);

        --m_Count;
        return true;
    }

    /// <summary>
    /// Returns the item at the start of the queue. 
    /// The queue remains unchanged.
    /// </summary>
    /// <returns>Item at the start of the queue.</returns>
    /// <exception cref="InvalidOperationException">Queue is empty.</exception>
    public T Peek()
    {
        ThrowIfEmpty(m_Count);

        return m_Items[m_Head];
    }

    /// <summary>
    /// Attempts to return the item at the start of the queue. 
    /// The queue remains unchanged.
    /// </summary>
    /// <param name="item">Item at the start of the queue.</param>
    /// <returns>True if item exists and returned successully; otherwise false.</returns>
    public bool TryPeek(out T item)
    {
        if (IsEmpty())
        {
            item = default!;
            return false;
        }

        item = m_Items[m_Head];
        return true;
    }

    /// <summary>
    /// Clears the entire queue.
    /// </summary>
    public void Clear()
    {
        if (m_Count != 0)
        {
            Array.Clear(m_Items, 0, m_Count);
            m_Count = 0;
        }
        
        m_Head  = 0;
        m_Tail  = 0;
    }

    /// <summary>
    /// Private method to move the index of the queue.
    /// </summary>
    /// <param name="index">Index to move.</param>
    /// <remarks>This method preserves the queue as a circular array.</remarks>
    private void MoveIndex(ref int index)
    {
        int tempIndex = index + 1;
        if (tempIndex == m_Items.Length)
        {
            tempIndex = 0;
        }   

        index = tempIndex;
    }

    #endregion

    #region Search

    /// <summary>
    /// Determines if the specified item is in the queue.
    /// </summary>
    /// <param name="item">Item to find.</param>
    /// <returns>True if item is found; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">Queue is empty.</exception>
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

    #endregion

    #region Get, Set, Index

    /// <summary>
    /// Returns the value at the specified index of the queue.
    /// </summary>
    /// <param name="index">Index to get value from.</param>
    /// <returns>Value at specified index.</returns>
    /// <exception cref="InvalidOperationException">Queue is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public T GetValue(int index)
    {
        ThrowIfEmpty(m_Count);
        ThrowIfIndexOutOfRange(index, m_Count - 1);

        return m_Items[index];
    }

    /// <summary>
    /// Replaces the existing value with the specified value 
    /// at the specified index of the queue.
    /// </summary>
    /// <param name="index">Index to set value at.</param>
    /// <param name="value">Value to set.</param>
    /// <exception cref="InvalidOperationException">Queue is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public void SetValue(int index, T value)
    {
        ThrowIfEmpty(m_Count);
        ThrowIfIndexOutOfRange(index, m_Count - 1);

        m_Items[index] = value;
    }

    /// <summary>
    /// Gets the first index of the value if it exists in the list.
    /// </summary>
    /// <param name="item">Value to find index of.</param>
    /// <returns>Index of the item if found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">Queue is empty.</exception>
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
    /// Gets the last index of the value if it exists in the list.
    /// </summary>
    /// <param name="item">Value to find index of.</param>
    /// <returns>Index of the item if found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">Queue is empty.</exception>
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
    /// Ensures the capacity of the queue is at least the specified capacity.
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
    /// Trims the excess spaces in the queue.
    /// </summary>
    /// <remarks>This method if attempt to reduce the size of the queue by 10%.</remarks>
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
    /// Private method to expand the current max capacity of the queue by double the size.
    /// </summary>
    /// <param name="capacity">Minimum capacity to expand to.</param>
    private void ExpandCapacity(int capacity)
    {
        int newCapacity = m_MaxCount * 2;

        if (newCapacity > Array.MaxLength)
        {
            newCapacity = Array.MaxLength;
        }

        if (newCapacity < capacity)
        {
            newCapacity = capacity;
        }

        SetCapacity(newCapacity);
    }

    /// <summary>
    /// Private method to set the capacity of the queue.
    /// </summary>
    /// <param name="capacity">Capacity of the queue.</param>
    private void SetCapacity(int capacity)
    {
        T[] copy = new T[capacity];

        if (m_Count > 0)
        {
            if (m_Head < m_Tail)
            {
                Array.Copy(m_Items, m_Head, copy, 0, m_Count);
            }   
            else
            {
                Array.Copy(m_Items, m_Head, copy, 0, m_Items.Length - m_Head);
                Array.Copy(m_Items, 0, copy, m_Items.Length - m_Head, m_Tail);
            }
        }

        m_Items    = copy;
        m_MaxCount = capacity;
        m_Head     = 0;
        m_Tail     = (m_Count == m_MaxCount) ? 0 : m_Count; 
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Determines if the queue is empty.
    /// </summary>
    /// <returns>True if list is empty; otherwise false.</returns>
    public bool IsEmpty()
    {
        return m_Count == 0;
    }

    /// <summary>
    /// Determines if the queue is full.
    /// </summary>
    /// <returns>True if list is full; otherwise false.</returns>
    public bool IsFull()
    {
        return m_Count == m_MaxCount;
    }

    /// <summary>
    /// Copies all elements from the queue into an array.
    /// </summary>
    /// <returns>Array containing all the values from the queue.</returns>
    public T[] ToArray()
    {
        if (IsEmpty())
            return [];

        T[] copy = m_Items;
        return copy;
    }

    /// <summary>
    /// Copies all elements from the queue into a list.
    /// </summary>
    /// <returns>List containing all the values from the queue.</returns>
    public List<T> ToList()
    {
        if (IsEmpty())
            return [];

        List<T> copy = m_Items.ToList();
        return copy;
    }

    #endregion

    #region Enumerator

    /// <summary>
    /// Gets the enumerator for this queue.
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
    /// Gets the enumerator for this queue.
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
        ArgumentOutOfRangeException.ThrowIfNegative(item);
    }

    /// <summary>
    /// Throws if item is too large.
    /// </summary>
    /// <param name="item">Item to check.</param>
    /// <exception cref="ArgumentOutOfRangeException">Item is too large.</exception>
    private static void ThrowIfTooLarge(int item)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(item, int.MaxValue, nameof(item));
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