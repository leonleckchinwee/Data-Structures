using System.Collections;

namespace DSA.Queues;

/// <summary>
/// This is the implementation of a generic queue as a circular array.
/// (First-in, First-out)
/// </summary>
/// <remarks>This queue accepts null as a valid value for reference types 
/// and allows duplicate elements.</remarks>
public class Queue<T> : IEnumerable<T>, IReadOnlyCollection<T>, ICollection
{
    #region Properties

    /// <summary>
    /// Default queue capacity.
    /// </summary>
    private readonly int DefaultCapacity = 16;

    /// <summary>
    /// Holds all the items of the queue in an internal array.
    /// </summary>
    private T[] Items;

    /// <summary>
    /// The index of the first item.
    /// </summary>
    private int Head;

    /// <summary>
    /// The index of the next available position to enqueue.
    /// </summary>
    private int Tail;

    /// <summary>
    /// The max capacity of the queue.
    /// </summary>
    private int MaxCapacity;

    /// <summary>
    /// The number of items currently in queue.
    /// </summary>
    private int CurrentCount;

    /// <summary>
    /// The max capacity of the queue.
    /// </summary>
    public int MaxCount => MaxCapacity;

    /// <summary>
    /// The number of items currently in queue.
    /// </summary>
    public int Count    => CurrentCount;

    // Interface overrides (unused & unimplemented)
    public bool IsSynchronized => false;
    public object SyncRoot     => this;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of an empty queue with default capacity of 16.
    /// </summary>
    public Queue()
    {
        Items        = new T[DefaultCapacity];
        MaxCapacity  = DefaultCapacity;
        CurrentCount = 0;
        Head         = 0;
        Tail         = -1;
    }

    /// <summary>
    /// Initializes a new instance of an empty queue with specified capacity.
    /// </summary>
    /// <param name="capacity">The max capacity.</param>
    /// <exception cref="ArgumentOutOfRangeException">Capacity is negative.</exception>
    public Queue(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 0, nameof(capacity));

        Items        = new T[capacity];
        MaxCapacity  = capacity;
        CurrentCount = 0;
        Head         = 0;
        Tail         = -1;
    }

    /// <summary>
    /// Initializes a new instance of queue that contains elements copied from the specified collection
    /// and has the capacity equal to the number of elements copied.
    /// </summary>
    /// <param name="collection">The collection whose elements are copied to the new queue.</param>
    /// <exception cref="ArgumentNullException">Collection is null.</exception>
    public Queue(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection, nameof(collection));

        int capacity = collection.Count();
        MaxCapacity  = capacity;
        CurrentCount = capacity;

        Items        = new T[capacity];

        int index    = 0;
        foreach (T item in collection)
        {
            Items[index++] = item;
        }

        Head = 0;
        Tail = CurrentCount - 1;
    }

    #endregion

    #region Core

    /// <summary>
    /// Adds an object to the end of the queue.
    /// </summary>
    /// <param name="item">The object to add.</param>
    /// <remarks></remarks>
    public void Enqueue(T item)
    {
        if (CurrentCount == MaxCapacity)
            EnsureCapacity(MaxCapacity + 1);

        Tail        = (Tail + 1) % MaxCapacity;
        Items[Tail] = item;

        ++CurrentCount;
    }

    /// <summary>
    /// Removes and return the object at the beginning of the queue.
    /// </summary>
    /// <returns>The object that was removed.</returns>
    /// <exception cref="InvalidOperationException">The queue is empty.</exception>
    /// <remarks>The capacity remains unchanged, hence the removed object will become its default value.</remarks>
    public T Dequeue()
    {
        if (CurrentCount == 0)
            throw new InvalidOperationException("Queue is empty.");

        T item      = Items[Head];
        Items[Head] = default!;
        Head        = (Head + 1) % MaxCapacity;

        --CurrentCount;
        return item;
    }

    /// <summary>
    /// Ensures that the capacity of the queue is at least the specified capacity.
    /// </summary>
    /// <param name="capacity">The minimum capacity to ensure.</param>
    /// <returns>The new capacity of this queue.</returns>
    /// <remarks>The capacity will always be power of 2.</remarks>
    public int EnsureCapacity(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 0, nameof(capacity));

        if (MaxCapacity >= capacity)
            return MaxCapacity;

        int powerOfTwoCapacity = PowerOfTwo(capacity);
        MaxCapacity            = powerOfTwoCapacity;
        
        Array.Resize(ref Items, MaxCapacity);

        return MaxCapacity;
    }

    #endregion

    #region Interface Overrides

    /// <summary>
    /// Copies the elements of the collection to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">One-dimensional array that is the destination.</param>
    /// <param name="index">The zero-based index in array to begin copying.</param>
    /// <exception cref="ArgumentNullException">Array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Index is out of range.</exception>
    /// <exception cref="ArgumentException">Array is multi-dimensional.</exception>
    /// <exception cref="ArgumentException">Insufficient space.</exception>
    /// <exception cref="ArgumentException">Type mismatch.</exception>
    public void CopyTo(Array array, int index)
    {
        // Exceptions
        {
            ArgumentNullException.ThrowIfNull(array, "Array is null.");
            
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range.");

            if (array.Rank != 1)
                throw new ArgumentException("The array is multi-dimensional.");

            if (array.Length - index < CurrentCount)
                throw new ArgumentException("The destination array is not large enough.");

            if (array.GetType().GetElementType() != typeof(T))
                throw new ArgumentException("The array type does not match.");
        }

        Array.Copy(Items, 0, array, index, Count);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the queue.
    /// </summary>
    /// <returns>Enumerator</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return new QueueEnumerator(this);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the queue.
    /// </summary>
    /// <returns>Enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    /// <summary>
    /// Get the array underlying the queue.
    /// </summary>
    /// <returns>Array containing all the items.</returns>
    public T[] ToArray()
    {
        return Items;
    }

    #region Private

    /// <summary>
    /// Returns the next larger integer thats power of 2.
    /// </summary>
    /// <param name="number">Reference number.</param>
    /// <returns>Power of 2 result.</returns>
    private int PowerOfTwo(int number)
    {
        --number;

        number |= number >> 1;
        number |= number >> 2;
        number |= number >> 4;
        number |= number >> 8;
        number |= number >> 16;

        return number + 1;
    }

    #endregion

    /// <summary>
    /// IEnumerable<T> Interface
    /// </summary>
    private class QueueEnumerator : IEnumerator<T>
    {
        private readonly Queue<T> Queue;
        private int Index;

        object IEnumerator.Current => Current!;

        public QueueEnumerator(Queue<T> Queue)
        {
            this.Queue = Queue;
            Index      = -1;
        }

        public T Current
        {
            get
            {
                if (Index < 0 || Index >= Queue.Count)
                    throw new IndexOutOfRangeException();

                return Queue.ToArray()[(Queue.Head + Index) % Queue.MaxCapacity];
            }
        }

        public bool MoveNext()
        {
            ++Index;
            return Index < Queue.Count;
        }

        public void Reset()
        {
            Index = -1;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
