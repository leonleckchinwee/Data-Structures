using System.Collections;

namespace DSA.Array;

public class SArray<T> : IEnumerable<T>
{
    #region Properties

    /// <summary>
    /// Array containing all values.
    /// </summary>
    public T[] Data { get; set; }

    /// <summary>
    /// Number of elements in the array.
    /// </summary>
    public int Length => Data.Length;

    /// <summary>
    /// Operator [] for accessing and changing values.
    /// </summary>
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException(nameof(index));

            return Data[index];
        }
        set
        {
            if (index < 0 || index >= Length)
                throw new IndexOutOfRangeException(nameof(index));

            Data[index] = value;
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor to initialize a new instance of array with specified size.
    /// </summary>
    /// <remarks>Size must be non-negative.</remarks>
    public SArray(int size)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(size);
        Data = new T[size];
    }

    /// <summary>
    /// Constructor to initialize a new instance of array with elements from a non-null list.
    /// </summary>
    public SArray(List<T> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        Data = list.ToArray();
    }

    /// <summary>
    /// Constructor to initialize a new instance of array with elements from a non-null IEnumerable.
    /// </summary>
    public SArray(IEnumerable<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        Data = items.ToArray();
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds an item into the array.
    /// </summary>
    /// <remarks>Exception will be thrown if array is full.</remarks>
    public void Add(T item)
    {
        for (int i = 0; i < Data.Length; ++i)
        {
            if (Data[i] == null)    // Handles nullable values
            {
                Data[i] = item;
                return;
            }
            else if (Data[i]!.Equals(0))
            {
                Data[i] = item;
                return;
            }
        }

        throw new InvalidOperationException("Array is full!");
    }

    #endregion

    #region Remove

    /// <summary>
    /// Clears all values to default and size of array remins the same.
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < Data.Length; ++i)
        {
            Data[i] = default!;
        }
    }

    #endregion

    #region Search

    // TODO...

    #endregion

    #region Sort

    // TODO...

    #endregion

    #region Utilities

    /// <summary>
    /// An enumerator for this array.
    /// </summary>
    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)Data).GetEnumerator();
    }

    /// <summary>
    /// An enumerator for this array.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
}