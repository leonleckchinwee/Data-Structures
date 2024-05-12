using System.Collections;

namespace DSA.Sets;

public class Set<T> : IEnumerable<T> where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Private property for the default capacity of the set.
    /// </summary>
    private readonly int m_DefaultCapacity = 16;

    /// <summary>
    /// Private property containing the array holding all the items in the set.
    /// </summary>
    private T[] m_Items;

    /// <summary>
    /// Private property containing the number of items in the set.
    /// </summary>
    private int m_Count;

    /// <summary>
    /// Private property containing the max capacity of the set.
    /// </summary>
    private int m_MaxCount;

    /// <summary>
    /// Gets the number of items in the set.
    /// </summary>
    public int Count => m_Count;

    /// <summary>
    /// Gets the max capacity of the set.
    /// </summary>
    public int MaxCount => m_MaxCount;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new empty set with default capacity (16).
    /// </summary>
    public Set()
    {
        m_MaxCount = m_DefaultCapacity;
        m_Items    = new T[m_MaxCount];
        m_Count    = 0;
    }

    /// <summary>
    /// Initializes a new empty set with specified capacity.
    /// </summary>
    /// <param name="capacity">Capacity of the set.</param>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is too large.</exception>
    public Set(int capacity)
    {
        ThrowIfNegative(capacity);
        ThrowIfTooLarge(capacity);

        m_MaxCount = capacity;
        m_Items    = new T[m_MaxCount];
        m_Count    = 0;
    }

    /// <summary>
    /// Initializes a new set with unique items copied from the specified collection.
    /// </summary>
    /// <param name="collection">Collection to copy from.</param>
    /// <remarks>
    /// The capacity of the set will be the same as the number of items in the set.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Collection is null.</exception>
    public Set(IEnumerable<T> collection)
    {
        ThrowIfNull(collection);

        List<T> tempList = new(collection);
        m_Items          = tempList.Distinct().ToArray();
        m_MaxCount       = m_Items.Length;
        m_Count          = m_Items.Length;
    }

    #endregion

    #region Add, Remove, Clear

    public bool Add(T item)
    {
        ThrowIfNull(item);

        if (Contains(item))
            return false;

        if (m_Count == m_MaxCount)
            ExpandCapacity(m_Count + 1);

        m_Items[m_Count] = item;
        ++m_Count;

        return true;
    }

    public bool Remove(T item)
    {
        throw new NotImplementedException();
    }

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

    public bool Contains(T item)
    {
        return Array.IndexOf(m_Items, item) >= 0;
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

    #region Enumerators

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in m_Items)
        {
            yield return item;
        }
    }

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
            throw new ArgumentNullException(nameof(item));
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
