using System.Collections;

namespace DSA.Stacks;

public class Stack<T> : IEnumerable<T>, IReadOnlyCollection<T>, ICollection
{
    #region Properties

    private const int DefaultCapacity = 16;

    private T[] Items;

    private int Top;

    public int MaxCount { get; private set; }

    public int Count => Top + 1;

    public bool IsSynchronized => false;

    public object SyncRoot => this;

    #endregion   

    #region Constructors 

    public Stack()
    {
        Items    = new T[DefaultCapacity];
        Top      = -1;
        MaxCount = DefaultCapacity;
    }

    public Stack(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 0, nameof(capacity));

        Items    = new T[capacity];
        Top      = -1;
        MaxCount = capacity;
    }

    public Stack(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection, nameof(collection));

        int capacity = collection.Count();
        MaxCount     = capacity;
        Top          = capacity - 1;

        int index    = 0;
        Items        = new T[capacity];

        foreach (T item in collection)
        {
            Items[index++] = item;
        }
    }

    #endregion

    #region Push

    public void Push(T item)
    {
        if (Top < Items.Length - 1)
        {
            ++Top;
            Items[Top] = item;
        }
        else
        {
            PushAndResize(item);
        }
    }

    private void PushAndResize(T item)
    {
        if (Top == Items.Length)
            throw new ArgumentOutOfRangeException(nameof(Top), "Stack is out of range.");

        Resize(Count + 1);

        ++Top;
        Items[Top] = item;
    }

    private void Resize(int currentCapacity)
    {
        int newCapacity = DefaultCapacity;
        if (Count != 0) // Double the capacity
            newCapacity = Count * 2; 

        if (newCapacity > Array.MaxLength) // Limit max capacity
            newCapacity = Array.MaxLength;

        if (newCapacity < currentCapacity)  
            newCapacity = currentCapacity;

        MaxCount = newCapacity;
        Array.Resize(ref Items, newCapacity);
    }

    #endregion

    #region Pop

    public T Pop()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        throw new NotImplementedException();
    }

    public bool TryPop(out T item)
    {
        if (Count == 0)
        {
            item = default!;
            return false;
        }

        throw new NotImplementedException();
    }

    #endregion

    #region Peek

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        return Items[Top];
    }

    public bool TryPeek(out T item)
    {
        if (Count == 0)
        {
            item = default!;
            return false;
        }

        item = Items[Top];
        return true;
    }

    #endregion

    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}