namespace DSA.LinkedList;

public class LList<T> where T : notnull
{
    public int Count        { get; private set; }    // Number of elements in list
    public LLNode<T>? First { get; private set; }    // First element
    public LLNode<T>? Last  { get; private set; }    // Last element

    // Empty linked list
    public LList()
    {
        First = null;
        Last  = null;
        Count = 0;
    }

    public LList(IEnumerable<T> other)
    {
        // Copies all elements in other into a new instance of llist
    }

    // Create and add new node of specified value at front of list
    public void AddFirst(T item)
    {
        LLNode<T> newNode = new LLNode<T>(item) { List = this };

        if (First == null || Last == null)  // Empty list
        {
            First = newNode;
            Last  = First;
        }
        else
        {
            newNode.Next   = First;
            First.Previous = newNode;
            First          = newNode;

            UpdateLast();
        }

        ++Count;
    }

    // Add new node at front of list
    public void AddFirst(LLNode<T> item)
    {
        if (item.List != null)  // Check if node belongs to another list
            throw new ArgumentException("LinkedList Node belong to another list!");

        item.List = this;

        if (First == null || Last == null)  // Empty list
        {
            First = item;
            Last  = First;  
        }
        else
        {
            item.Next      = First;
            First.Previous = item;
            First          = item;

            UpdateLast();
        }

        ++Count;
    }

    // Create and add new node of specified value at end of list
    public void AddLast(T item)
    {
        LLNode<T> newNode = new LLNode<T>(item) { List = this };

        if (First == null || Last == null)  // Empty list
        {
            First = newNode;
            Last  = First;
        }
        else
        {
            Last.Next        = newNode;
            newNode.Previous = Last;

            UpdateLast();
        }

        ++Count;
    }

    // Add new node at end of list
    public void AddLast(LLNode<T> item)
    {
        if (item.List != null)  // Check if node belongs to another list
            throw new ArgumentException("LinkedList Node belong to another list!");

        item.List = this;

        if (First == null || Last == null)  // Empty list
        {
            First = item;
            Last  = First;  
        }
        else
        {
            Last.Next     = item;
            item.Previous = Last;

            UpdateLast();
        }

        ++Count;
    }

    // Updates last pointer to point at tail of list
    void UpdateLast()
    {
        if (First == null || Last == null)
            return;

        LLNode<T> tail = Last;
        while (tail.Next != null)
        {
            tail = tail.Next;
        }

        Last = tail;
    }
}
