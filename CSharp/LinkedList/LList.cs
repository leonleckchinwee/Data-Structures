using System.Collections;

namespace DSA.LinkedLists;

/// <summary>
/// Doubly linked-list class.
/// This class accepts null value for reference types.
/// This class allows for duplicate values.
/// This class does not contain cycles as it does not contain duplicate nodes.
/// </summary>
public class LList<T> : IEnumerable<T> where T : IComparable<T>
{
    #region Properties

    /// <summary>
    /// Private property containing the first node of the list.
    /// </summary>
    private LLNode<T>? m_Head;

    /// <summary>
    /// Private property containing the last node of the list.
    /// </summary>
    private LLNode<T>? m_Tail;

    /// <summary>
    /// Private property containing the number of elements in the list.
    /// </summary>
    private int m_Count;

    /// <summary>
    /// Gets the first node of the list.
    /// </summary>
    public LLNode<T>? First => m_Head;

    /// <summary>
    /// Gets the last node of the list.
    /// </summary>
    public LLNode<T>? Last => m_Tail;

    /// <summary>
    /// Gets the number of elements in the list.
    /// </summary>
    public int Count => m_Count;

    /// <summary>
    /// Square bracket operator for accessing node at specified index.
    /// </summary>
    /// <param name="index">Index of the node.</param>
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
    /// Initializes a new empty list.
    /// </summary>
    public LList()
    {
        m_Head  = null;
        m_Tail  = null;
        m_Count = 0;
    }

    /// <summary>
    /// Initializes a new list with one node containing the specified value.
    /// </summary>
    /// <param name="value">Value to insert.</param>
    public LList(T value)
    {
        LLNode<T> node = new(value) { List = this, Previous = null, Next = null };
        m_Head         = node;
        m_Tail         = node;
        m_Count        = 1;
    }

    /// <summary>
    /// Initializes a new list by copying all elements from the specified collection 
    /// into the list.
    /// </summary>
    /// <param name="collection">Collection to copy from.</param>
    public LList(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        foreach (T item in collection)
        {
            LLNode<T> node = new(item);
            AddLast(node);
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Inserts a new node containing the specified value into the front of the list.
    /// </summary>
    /// <param name="value">Value to insert.</param>
    /// <returns>The new node containing the specified value.</returns>
    public LLNode<T> AddFirst(T value)
    {
        LLNode<T> newNode = new(value);

        AddFirst(newNode);

        return newNode;
    }

    /// <summary>
    /// Inserts an existing node into the front of the list.
    /// </summary>
    /// <param name="node">Node to insert.</param>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    public void AddFirst(LLNode<T> node)
    {
        ThrowIfNull(node);
        LList<T>.ThrowIfNodeAssigned(node);

        node.List = this;
        
        // Case 1: list is empty
        if (IsEmpty())
        {
            m_Head = node;
            m_Tail = node;
        }
        // Case 2: push first node back and insert front
        else
        {
            node.Previous    = null;
            node.Next        = m_Head;
            m_Head!.Previous = node;
            m_Head           = node;
        }

        ++m_Count;
        UpdateTail();   // Update last node pointer
    }

    /// <summary>
    /// Inserts a new node containing the specified value into the back of the list.
    /// </summary>
    /// <param name="value">Value to insert.</param>
    /// <returns>The new node containing the specified value.</returns>
    public LLNode<T> AddLast(T value)
    {
        LLNode<T> newNode = new(value);

        AddLast(newNode);

        return newNode;
    }

    /// <summary>
    /// Inserts an existing node into the back of the list.
    /// </summary>
    /// <param name="node">Node to insert.</param>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    public void AddLast(LLNode<T> node)
    {
        ThrowIfNull(node);
        LList<T>.ThrowIfNodeAssigned(node);

        node.List = this;

        // Case 1: list is empty.
        if (IsEmpty())
        {
            m_Head = node;
            m_Tail = node;
        }
        // Case 2: insert last and push last node back
        else
        {
            node.Previous = m_Tail!;
            node.Next     = null;
            m_Tail!.Next  = node;
            m_Tail        = node;
        }

        ++m_Count;
        UpdateTail();   // Update last node pointer
    }

    /// <summary>
    /// Inserts a new node containing the specified value before the specified existing 
    /// reference node from the list.
    /// </summary>
    /// <param name="reference">Reference node.</param>
    /// <param name="value">Value to insert.</param>
    /// <returns>The new node containing the specified value.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node does not belong to this list.</exception>
    public LLNode<T> AddBefore(LLNode<T> reference, T value)
    {
        LLNode<T> newNode = new(value);
        
        AddBefore(reference, newNode);

        return newNode;
    }

    /// <summary>
    /// Inserts an existing node before the specified existing reference node from the list.
    /// </summary>
    /// <param name="reference">Reference node.</param>
    /// <param name="node">Node to insert.</param>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node does not belong to this list.</exception>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    public void AddBefore(LLNode<T> reference, LLNode<T> node)
    {
        // Exceptions
        {
            ThrowIfEmpty(this);
            ThrowIfNull(reference);
            ThrowIfNull(node);
            LList<T>.ThrowIfNodeDoesNotBelong(this, reference);
            LList<T>.ThrowIfNodeAssigned(node);
        }

        node.List = this;

        // Case 1: add before the first node
        if (reference == m_Head)
        {
            node.Previous      = null;
            node.Next          = reference;
            reference.Previous = node;
            m_Head             = node;
        }
        // Case 2: add before any other node,
        // no need to update tail
        else
        {
            node.Previous            = reference.Previous;
            node.Next                = reference;
            reference.Previous!.Next = node;
            reference.Previous       = node;
        }

        ++m_Count;
        UpdateTail();
    }

    /// <summary>
    /// Inserts a new node containing the specified value after the specified existing 
    /// reference node from the list.
    /// </summary>
    /// <param name="reference">Reference node.</param>
    /// <param name="value">Value to insert.</param>
    /// <returns>The new node containing the specified value.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node does not belong to this list.</exception>
    public LLNode<T> AddAfter(LLNode<T> reference, T value)
    {
        LLNode<T> newNode = new(value);
        
        AddAfter(reference, newNode);

        return newNode;
    }

    /// <summary>
    /// Inserts an existing node after the specified existing reference node from the list.
    /// </summary>
    /// <param name="reference">Reference node.</param>
    /// <param name="node">Node to insert.</param>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node does not belong to this list.</exception>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    public void AddAfter(LLNode<T> reference, LLNode<T> node)
    {
        // Exceptions
        {
            ThrowIfEmpty(this);
            ThrowIfNull(reference);
            ThrowIfNull(node);
            LList<T>.ThrowIfNodeDoesNotBelong(this, reference);
            LList<T>.ThrowIfNodeAssigned(node);
        }

        node.List = this;

        // Case 1: add after the last node, updates tail pointer
        if (reference == m_Tail)
        {
            node.Previous = m_Tail!;
            node.Next     = null;
            m_Tail.Next   = node;
            m_Tail        = node;
        }
        // Case 2: after after any node,
        // no need to update head
        else
        {
            node.Previous            = reference;
            node.Next                = reference.Next;
            reference.Next!.Previous = node;
            reference.Next           = node;
        }

        ++m_Count;
    }

    /// <summary>
    /// Inserts a new node containing the specified value at the index of the list.
    /// </summary>
    /// <param name="index">Index to insert at.</param>
    /// <param name="value">Value to insert.</param>
    /// <returns>The new node containing the specified value.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public LLNode<T> AddAt(int index, T value)
    {
        LLNode<T> newNode = new(value);

        AddAt(index, newNode);

        return newNode;
    }

    /// <summary>
    /// Inserts an existing node at the specified index of the list.
    /// </summary>
    /// <param name="index">Index to insert at.</param>
    /// <param name="node">Node ti insert.</param>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    public void AddAt(int index, LLNode<T> node)
    {
        // Exceptions
        {
            ThrowIfEmpty(this);
            ThrowIfNull(node);
            ThrowIfIndexOutOfRange(this, index);
            LList<T>.ThrowIfNodeAssigned(node);
        }

        node.List = this;

        // Case 1: add at first node, push node back
        if (index == 0)
        {
            node.Previous    = null;
            node.Next        = m_Head;
            m_Head!.Previous = node;
            m_Head           = node;
        }
        // Case 2: add at any other node
        else 
        {
            LLNode<T> current = m_Head!;
            int             i = 0;

            while (current.Next != null && i < index)
            {
                ++i;
                current = current.Next;
            }

            node.Previous          = current.Previous;
            node.Next              = current;
            current.Previous!.Next = node;
            current.Previous       = node;
        }

        ++m_Count;
        UpdateTail();
    }

    #endregion

    #region Remove

    /// <summary>
    /// Removes the node at the front of the list.
    /// </summary>
    /// <returns>True if node is removed successfully; otherwise false.</returns>
    public bool RemoveFirst()
    {
        if (IsEmpty())
            return false;
        
        // Case 1: only one node in the list, 
        // only update head
        if (m_Count == 1)
        {
            m_Head = m_Head!.Next;
        }
        // Case 2: multiple nodes in the list,
        // need to update the next node
        else
        {
            m_Head!.Next!.Previous = null;
            m_Head = m_Head.Next;
        }

        --m_Count;
        return true;
    }

    /// <summary>
    /// Removes the node at the end of the list.
    /// </summary>
    /// <returns>True if node is removed successfully; otherwise false.</returns>
    public bool RemoveLast()
    {
        if (IsEmpty())
            return false;

        // Case 1: only one node in the list,
        // only update head
        if (m_Count == 1)
        {
            m_Head = m_Head!.Next;
        }
        // Case 2: multiple nodes in the list,
        // need to update tail's previous node
        else
        {
            m_Tail!.Previous!.Next = null;
            m_Tail = m_Tail!.Previous;
        }

        --m_Count;
        return true;
    }

    /// <summary>
    /// Removes the node containing the specified value from the list.
    /// </summary>
    /// <param name="value">Value to remove.</param>
    /// <returns>True if node is removed successfully; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public bool Remove(T value)
    {
        ThrowIfEmpty(this);

        LLNode<T> current = m_Head!;
        while (current != null)
        {
            if (!value.Equals(current.Value))
            {   
                current = current.Next!;
                continue;
            }

            // Case 1: removing first node
            if (current == m_Head)
            {
                if (m_Head.Next != null)
                    m_Head.Next.Previous = null;

                m_Head = m_Head.Next;
            }
            // Case 2: removing last node
            else if (current == m_Tail)
            {
                if (m_Tail.Previous != null)
                    m_Tail.Previous.Next = null;

                m_Tail = m_Tail.Previous;
            }
            // Case 3: removing middle node
            else
            {
                current.Previous!.Next = current.Next;
                current.Next!.Previous = current.Previous;
            }

            --m_Count;

            return true;
        }

        return false;
    }

    /// <summary>
    /// Removes the existing specified node from the list.
    /// </summary>
    /// <param name="node">Node to remove.</param>
    /// <returns>True if node is removed successfully; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    public bool Remove(LLNode<T> node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);
        ThrowIfNodeDoesNotBelong(this, node);

        // Case 1: removing first node
        if (node == m_Head)
        {
            if (m_Head.Next != null)
                m_Head.Next.Previous = null;

            m_Head = m_Head.Next;
        }
        // Case 2: removing last node
        else if (node == m_Tail)
        {
            if (m_Tail.Previous != null)
                m_Tail.Previous.Next = null;

            m_Tail = m_Tail.Previous;
        }
        // Case 3: removing middle node
        else
        {
            node.Previous!.Next = node.Next;
            node.Next!.Previous = node.Previous;
        }

        --m_Count;

        return true;
    }

    /// <summary>
    /// Removes the existing node at the specified index of the list.
    /// </summary>
    /// <param name="index">Index to remove.</param>
    /// <returns>True if node is removed successfully; otherwise false.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public bool RemoveAt(int index)
    {
        LLNode<T> node = GetNode(index)!;

        return Remove(node);
    }

    /// <summary>
    /// Clears all nodes in the list.
    /// </summary>
    public void Clear()
    {
        LLNode<T> current = m_Head!;
        while (current != null)
        {
            current.List = null;
            current = current.Next!;
        }

        m_Head  = null;
        m_Tail  = null;
        m_Count = 0;
    }

    #endregion

    #region Search

    /// <summary>
    /// Finds and returns the existing node containing the specified value in the list.
    /// </summary>
    /// <param name="value">Value to search for.</param>
    /// <returns>Node containing the specified value if found; otherwise null.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public LLNode<T>? Find(T value)
    {
        ThrowIfEmpty(this);

        LLNode<T> current = m_Head!;

        while (current != null)
        {
            if (value.Equals(current.Value))
            {
                return current;
            }

            current = current.Next!;
        }

        return null;
    }

    /// <summary>
    /// Determines if the specified value exists in the list.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>True if value is in the list; otherwise false.</returns>
    public bool Contains(T value)
    {
        if (IsEmpty())
            return false;

        LLNode<T> current = m_Head!;

        while (current != null)
        {
            if (value.Equals(current.Value))
                return true;

            current = current.Next!;
        }

        return false;
    }

    /// <summary>
    /// Determines if the specified node belongs to this list.
    /// </summary>
    /// <param name="node">Node to check.</param>
    /// <returns>True if node belongs to this list; otherwise false.</returns>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    public bool Contains(LLNode<T> node)
    {
        ThrowIfNull(node);
        return node.List == this;
    }

    #endregion

    #region Sort

    /// <summary>
    /// Determines if the list is sorted according to the specified order.
    /// </summary>
    /// <param name="isAscending">Order to check if sorted by.</param>
    /// <returns>True if sorted correctly; otherwise false.</returns>
    public bool IsSorted(bool isAscending = true)
    {        
        if (m_Count <= 1)
            return true;

        if (isAscending)
        {
            LLNode<T> current = m_Head!;
            T value           = current.Value;

            while (current != null)
            {
                if (value.CompareTo(current.Value) > 0)
                    return false;

                current = current.Next!;
            }
        }
        else
        {
            LLNode<T> current = m_Head!;
            T value           = current.Value;

            while (current != null)
            {
                if (value.CompareTo(current.Value) < 0)
                    return false;

                current = current.Next!;
            }
        }

        return true;
    }

    /// <summary>
    /// Apply merge sort to the list according to the specified order.
    /// </summary>
    /// <param name="isAscending">Order to sort by.</param>
    /// <remarks>Time complexity: O(n.log.n)</remarks>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public void MergeSort(bool isAscending = true)
    {
        ThrowIfEmpty(this);

        MergeSort(m_Head, isAscending);

        UpdateTail();
    }

    /// <summary>
    /// Private recursive method to split and merge sort according to the specified order.
    /// </summary>
    /// <param name="head">Starting node.</param>
    /// <param name="ascending">Order to sort by.</param>
    /// <returns>New head of the sorted list.</returns>
    private LLNode<T>? MergeSort(LLNode<T>? head, bool ascending)
    {
        // Base case
        if (head == null || head.Next == null)
            return head;

        LLNode<T>? slow = head;
        LLNode<T>? fast = head.Next;

        // Find the "middle" node
        while (fast != null && fast.Next != null)
        {
            slow = slow!.Next;
            fast = fast.Next.Next;
        }

        LLNode<T>? secondHalf = slow!.Next;
        slow.Next = null;

        // Keep splitting until only 2 nodes
        LLNode<T>? firstHalfSorted = MergeSort(head, ascending);
        LLNode<T>? secondHalfSorted = MergeSort(secondHalf, ascending);

        // Merge 2 nodes together
        return Merge(firstHalfSorted!, secondHalfSorted!, ascending);
    }

    /// <summary>
    /// Merge two nodes and sort them by order specified.
    /// </summary>
    /// <param name="node1">First node.</param>
    /// <param name="node2">Second node.</param>
    /// <returns>New head for the merged nodes.</returns>
    private LLNode<T> Merge(LLNode<T> node1, LLNode<T> node2, bool ascending)
    {
        LLNode<T> dummy;

        // Clear previous pointers since the nodes will be at the front
        node1.Previous = null;
        node2.Previous = null;

        // Use the smaller node as the first node
        if (ascending)
        {
            if (node1.CompareTo(node2) <= 0)
            {
                dummy = node1;
                node1 = node1.Next!;
            }
            else
            {
                dummy = node2;
                node2 = node2.Next!;
            }
        }
        // Use the bigger node as the first node
        else
        {
            if (node1.CompareTo(node2) > 0)
            {
                dummy = node1;
                node1 = node1.Next!;
            }
            else
            {
                dummy = node2;
                node2 = node2.Next!;
            }
        }
        
        LLNode<T> current = dummy;

        while (node1 != null && node2 != null)
        {
            int comparer = node1.CompareTo(node2);

            if (ascending)
            {
                // Node1 is smaller than node2
                if (comparer <= 0)
                {
                    current.Next = node1;
                    node1 = node1.Next!;
                }
                // Node2 is smaller than node1
                else
                {
                    current.Next = node2;
                    node2 = node2.Next!;
                }
            }
            else
            {
                // Node2 is bigger than node1
                if (comparer > 0)
                {
                    current.Next = node1;
                    node1 = node1.Next!;
                }
                // Node1 is bigger than node2
                else
                {
                    current.Next = node2;
                    node2 = node2.Next!;
                }
            }
            
            current.Next.Previous = current;
            current = current.Next;
        }

        // Append the rest of the nodes
        current.Next = node1 ?? node2;

        if (current.Next != null)
        {
            current.Next.Previous = current;
        }

        // Update head
        m_Head = dummy;
        return dummy;
    }

    #endregion

    #region List Manipulation

    /// <summary>
    /// Reverse the order of the entire list.
    /// </summary>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public void Reverse()
    {
        ThrowIfEmpty(this);
        
        if (m_Count == 1)
            return;

        LLNode<T> current = m_Head!;
        while (current != null)
        {
            (current.Next, current.Previous) = (current.Previous, current.Next);

            current = current.Previous!;
        }

        (m_Head, m_Tail) = (m_Tail, m_Head);
    }

    /// <summary>
    /// Split the list into 2. This list contains the nodes before the specified value,
    /// and the output list contains the value specified and all the nodes after it.
    /// </summary>
    /// <param name="value">Value to split at.</param>
    /// <returns>A new list with nodes at and after the specified value.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="InvalidOperationException">Value not found in the list.</exception>
    public LList<T> Split(T value)
    {
        ThrowIfEmpty(this);

        LLNode<T>? node = m_Head;
        LLNode<T>? otherNode = null;
        while (node != null)
        {
            // Find the first node containing the specified value
            if (node.Value.Equals(value))
            {
                otherNode = node;
                break;
            }

            node = node.Next!;
        }

        // Value not found in this list
        if (otherNode == null)
        {
            throw new InvalidOperationException("Value is not in the list.");
        }

        LList<T> otherList = new();
        while (otherNode != null)
        {
            Remove(otherNode);
            otherNode.List = null;

            otherList.AddLast(otherNode.Value);

            otherNode = otherNode.Next!;
        }

        return otherList;
    }

    /// <summary>
    /// Split the list into 2. This list contains the nodes before the specified node, and
    /// the output list contains the node specified and all the nodes after it.
    /// </summary>
    /// <param name="node">Node to split at.</param>
    /// <returns>A new list with nodes at and after the specified node.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="InvalidOperationException">Node does not belong to this list.</exception>
    public LList<T> Split(LLNode<T> node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);
        ThrowIfNodeDoesNotBelong(this, node);

        LList<T> otherList = new();
        while (node != null)
        {
            Remove(node);
            node.List = null;

            otherList.AddLast(node.Value);

            node = node.Next!;
        }

        return otherList;
    }

    /// <summary>
    /// Split list into 2. This list contains the nodes before the specified index, and
    /// the output list contains nodes at and after the specified index.
    /// </summary>
    /// <param name="index">Index to split at.</param>
    /// <returns>A new list with nodes at and after the specified index.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public LList<T> SplitAt(int index)
    {
        ThrowIfEmpty(this);
        ThrowIfIndexOutOfRange(this, index);

        LLNode<T> node = GetNode(index)!;
        LList<T> otherList = new();
        while (node != null)
        {
            Remove(node);
            node.List = null;

            otherList.AddLast(node.Value);

            node = node.Next!;
        }

        return otherList;
    }

    /// <summary>
    /// Append a non-null collection to the back of the list.
    /// </summary>
    /// <param name="collection">Collection to append.</param>
    /// <exception cref="ArgumentNullException">Collection is null.</exception>
    /// <exception cref="InvalidOperationException">Collection is empty.</exception>
    public void Append(IEnumerable<T> collection)
    {
        ThrowIfNull(collection);
        ThrowIfEmpty(collection);

        foreach (T item in collection)
        {
            AddLast(item);
        }
    }

    /// <summary>
    /// Prepend a non-null collection to the front of the list.
    /// </summary>
    /// <param name="collection"></param>
    /// <exception cref="ArgumentNullException">Collection is null.</exception>
    /// <exception cref="InvalidOperationException">Collection is empty.</exception>
    public void Prepend(IEnumerable<T> collection)
    {
        ThrowIfNull(collection);
        ThrowIfEmpty(collection);

        int index = 0;
        LLNode<T> node = null!;
        foreach (T item in collection)
        {
            if (index == 0)
            {
                node = AddFirst(item);
                ++index;
            }
            else
            {
                node = AddAfter(node, item);
            }
        }
    }

    #endregion

    #region Get, Set, Index

    /// <summary>
    /// Returns the value at the specified index of the list.
    /// </summary>
    /// <param name="index">Index to get value from.</param>
    /// <returns>Value at specified index.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public T GetValue(int index)
    {
        ThrowIfEmpty(this);
        ThrowIfIndexOutOfRange(this, index);

        int i             = 0;
        LLNode<T> current = m_Head!;
        T value           = default!;

        while (current != null)
        {
            if (index.Equals(i))
            {
                value = current.Value;
                break;
            }

            ++i;
            current = current.Next!;
        }

        return value;
    }

    /// <summary>
    /// Returns the node at the specified index of the list.
    /// </summary>
    /// <param name="index">Index to get node from.</param>
    /// <returns>Node at specified index.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public LLNode<T>? GetNode(int index)
    {
        ThrowIfEmpty(this);
        ThrowIfIndexOutOfRange(this, index);

        int i             = 0;
        LLNode<T> current = m_Head!;
        LLNode<T> temp    = null!;

        while (current != null)
        {
            if (index.Equals(i))
            {
                temp = current;
                break;
            }

            ++i;
            current = current.Next!;
        }

        return temp;
    }

    /// <summary>
    /// Replaces the existing node's value with the specified value 
    /// at the specified index of the list.
    /// </summary>
    /// <param name="index">Index to set value at.</param>
    /// <param name="value">Value to set.</param>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public void SetValue(int index, T value)
    {
        ThrowIfEmpty(this);
        ThrowIfIndexOutOfRange(this, index);

        int i             = 0;
        LLNode<T> current = m_Head!;

        while (current != null)
        {
            if (i == index)
            {
                current.Value = value;
                break;
            }

            ++i;
            current = current.Next!;
        }
    }

    /// <summary>
    /// Replaces the existing node's value with the specified node's value
    /// at the specified index of the list.
    /// </summary>
    /// <param name="index">Index to set value at.</param>
    /// <param name="node">Node to set.</param>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    public void SetNode(int index, LLNode<T> node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);
        ThrowIfIndexOutOfRange(this, index);

        int i             = 0;
        LLNode<T> current = m_Head!;

        while (current != null)
        {
            if (index.Equals(i))
            {
                current.Value = node.Value;
                break;
            }

            ++i;
            current = current.Next!;
        }
    }

    /// <summary>
    /// Returns the first index of node containing the specified value in the list.
    /// </summary>
    /// <param name="value">Value to find index of.</param>
    /// <returns>Index if value is found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public int IndexOf(T value)
    {
        ThrowIfEmpty(this);

        int i             = 0;
        LLNode<T> current = m_Head!;

        while (current != null)
        {
            if (value.Equals(current.Value))
                return i;

            ++i;
            current = current.Next!;
        }

        return -1;
    }

    /// <summary>
    /// Returns the first index of node containing the same value from the specified node.
    /// </summary>
    /// <param name="node">Node's value to find index of.</param>
    /// <returns>Index if node's value is found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <remarks>The specified node need not belong to the same list, this method will find
    /// the first index by matching the values.</remarks>
    public int IndexOf(LLNode<T>? node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);

        int i             = 0;
        LLNode<T> current = m_Head!;

        while (current != null)
        {
            if (node!.Equals(current))
                return i;

            ++i;
            current = current.Next!;
        }

        return -1;
    }

    /// <summary>
    /// Returns the last index of node containing the specified value in the list.
    /// </summary>
    /// <param name="value">Value to find last index of.</param>
    /// <returns>Index if value is found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public int LastIndexOf(T value)
    {
        ThrowIfEmpty(this);

        int i             = m_Count - 1;
        LLNode<T> current = m_Tail!;

        while (current != null)
        {
            if (value.Equals(current.Value))
                return i;

            --i;
            current = current.Previous!;
        }

        return -1;
    }

    /// <summary>
    /// Returns the last index of node containing the same value from the specified node.
    /// </summary>
    /// <param name="value">Value to find last index of.</param>
    /// <returns>Index if value is found; otherwise -1.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    /// <exception cref="ArgumentNullException">Node is null.</exception>
    /// <remarks>The specified node need not belong to the same list, this method will find
    /// the last index by matching the values.</remarks>
    public int LastIndexOf(LLNode<T>? node)
    {
        ThrowIfEmpty(this);
        ThrowIfNull(node);

        int i             = m_Count - 1;
        LLNode<T> current = m_Tail!;

        while (current != null)
        {
            if (node!.Equals(current))
                return i;

            --i;
            current = current.Previous!;
        }

        return -1;
    }

    #endregion

    #region Min, Max

    /// <summary>
    /// Returns the smallest value in the list.
    /// </summary>
    /// <returns>Smallest value in the list.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public T MinValue()
    {
        ThrowIfEmpty(this);
        
        LLNode<T> current = m_Head!;
        T min = current.Value;

        while (current != null)
        {
            if (current.Value.CompareTo(min) < 0)
                min = current.Value;

            current = current.Next!;
        }

        return min;
    }

    /// <summary>
    /// Returns the largest value in the list.
    /// </summary>
    /// <returns>Largest value in the list.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public T MaxValue()
    {
        ThrowIfEmpty(this);
        
        LLNode<T> current = m_Head!;
        T max = current.Value;

        while (current != null)
        {
            if (current.Value.CompareTo(max) > 0)
                max = current.Value;

            current = current.Next!;
        }

        return max;
    }

    /// <summary>
    /// Returns the node containing the smallest value in the list.
    /// </summary>
    /// <returns>Node containing the smallest value in the list.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public LLNode<T> MinNode()
    {
        ThrowIfEmpty(this);

        LLNode<T> current = m_Head!;
        LLNode<T> min = current;

        while (current != null)
        {
            if (current.Value.CompareTo(min.Value) < 0)
                min = current;

            current = current.Next!;
        }

        return min;
    }

    /// <summary>
    /// Returns the node containing the largest value in the list.
    /// </summary>
    /// <returns>Node containing the largest value in the list.</returns>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    public LLNode<T> MaxNode()
    {
        ThrowIfEmpty(this);

        LLNode<T> current = m_Head!;
        LLNode<T> max = current;

        while (current != null)
        {
            if (current.Value.CompareTo(max.Value) > 0)
                max = current;

            current = current.Next!;
        }

        return max;
    }

    #endregion

    #region Utilities

    /// <summary>
    /// Determines if the list is empty by checking if the first node exists.
    /// </summary>
    /// <returns>True if the list is empty; otherwise false.</returns>
    public bool IsEmpty()
    {
        return m_Head == null;
    }

    /// <summary>
    /// Copies all elements from the linked-list into an array.
    /// </summary>
    /// <returns>Array containing all the values from the list.</returns>
    public T[]? ToArray()
    {
        if (IsEmpty())
            return null;

        T[] array         = new T[m_Count];
        int index         = 0;
        LLNode<T> current = m_Head!;

        while (current != null)
        {
            array[index] = current.Value;

            ++index;
            current = current.Next!;
        }

        return array;
    }

    /// <summary>
    /// Copies all elements from the linked-list into a list.
    /// </summary>
    /// <returns>List containing all the values from the list.</returns>
    public List<T>? ToList()
    {
        if (IsEmpty())
            return null;

        List<T> list      = [];
        LLNode<T> current = m_Head!;

        while (current != null)
        {
            list.Add(current.Value);
            current = current.Next!;
        }
        return list;
    }

    /// <summary>
    /// Updates the pointer to point at the last node of the list.
    /// </summary>
    private void UpdateTail()
    {
        if (IsEmpty())
            return;

        LLNode<T> current = m_Head!;
        while (current != null)
        {
            m_Tail          = current;
            m_Tail.Previous = current.Previous;
            m_Tail.Next     = current.Next;
            current         = current.Next!;
        }
    }

    #endregion

    #region Enumerators

    /// <summary>
    /// Gets the enumerator for this list.
    /// </summary>
    /// <returns>Enumerator.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        LLNode<T>? current = m_Head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    /// <summary>
    /// Gets the enumerator for this list.
    /// </summary>
    /// <returns>Enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region Overrides

    /// <summary>
    /// Determines if two lists have the same values.
    /// </summary>
    /// <param name="obj">Other list to check with.</param>
    /// <returns>True if both list's values are the same; otherwise false.</returns>
    /// <remarks>This method checks the nodes' values.</remarks>
    public override bool Equals(object? obj)
    {
        if (obj is not LList<T>)
            return false;

        LList<T> other = (LList<T>)obj;
        if (other.Count != m_Count)
            return false;

        LLNode<T> node = m_Head!;
        LLNode<T> otherNode = other.m_Head!;
        while (node != null && m_Head != null)
        {
            if (!node.Value.Equals(otherNode.Value))
                return false;

            node = node.Next!;
            otherNode = otherNode.Next!;
        }

        if (node != null || otherNode != null)
            return false;

        return true;
    }

    /// <summary>
    /// Default hash code.
    /// </summary>
    /// <returns>Hash code.</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Gets the linked list as string.
    /// </summary>
    /// <returns>String representative of the linked list.</returns>
    public override string ToString()
    {
        if (IsEmpty())
            return "Empty list.";

        string list = "";
        LLNode<T>? current = m_Head;

        while (current != null)
        {
            list += current.Value + ", ";
            current = current.Next!;
        }

        return list;
    }

    #endregion

    #region Static Methods

    /// <summary>
    /// Merge 2 list into 1.
    /// </summary>
    /// <param name="list1">First list.</param>
    /// <param name="list2">Second list.</param>
    /// <param name="firstListInFront">Determines which list is at front of list.</param>
    /// <returns>A new list with nodes from both specified lists.</returns>
    /// <exception cref="ArgumentNullException">List is null.</exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static LList<T> Merge(LList<T> list1, LList<T> list2, bool firstListInFront = true)
    {
        ThrowIfNull(list1);
        ThrowIfNull(list2);
        ThrowIfBothEmpty(list1, list2);

        LList<T> newList = new();

        // First specified list at front
        if (firstListInFront)
        {
            LLNode<T> node = list1.First!;
            while (node != null)
            {
                newList.AddLast(node.Value);

                node = node.Next!;
            }

            node = list2.First!;
            while (node != null)
            {
                newList.AddLast(node.Value);

                node = node.Next!;
            }
        }
        // Second specified list at front
        else
        {
            LLNode<T> node = list2.First!;
            while (node != null)
            {
                newList.AddLast(node.Value);

                node = node.Next!;
            }

            node = list1.First!;
            while (node != null)
            {
                newList.AddLast(node.Value);

                node = node.Next!;
            }
        }

        return newList;
    }

    #endregion

    #region Exceptions

    /// <summary>
    /// Throws if object specified is null.
    /// </summary>
    /// <param name="value">Value to check</param>
    /// <exception cref="ArgumentNullException">Object is null.</exception>
    private static void ThrowIfNull(object? value)
    {
        ArgumentNullException.ThrowIfNull(value, "Argument is null.");
    }

    /// <summary>
    /// Throws if list is empty.
    /// </summary>
    /// <param name="list">List to check.</param>
    /// <exception cref="InvalidOperationException">List is empty.</exception>
    private static void ThrowIfEmpty(IEnumerable<T> list)
    {
        if (!list.Any())
            throw new InvalidOperationException(nameof(list));
    }

    /// <summary>
    /// Throws if both lists are empty.
    /// </summary>
    /// <param name="list1">First list to check.</param>
    /// <param name="list2">Second list to check.</param>
    /// <exception cref="InvalidOperationException">Both lists are empty.</exception>
    private static void ThrowIfBothEmpty(LList<T> list1, LList<T> list2)
    {
        if (list1.IsEmpty() && list2.IsEmpty())
        throw new InvalidOperationException("Both list is empty.");
    }

    /// <summary>
    /// Throws if node belongs to another list.
    /// </summary>
    /// <param name="node">Node to check.</param>
    /// <exception cref="InvalidOperationException">Node belongs to another list.</exception>
    private static void ThrowIfNodeAssigned(LLNode<T> node)
    {
        if (node.List != null)
            throw new InvalidOperationException("Node can only be assigned to one list.");
    }

    /// <summary>
    /// Throws if node that must be belonging to this list does not.
    /// </summary>
    /// <param name="list">List which node must belong to.</param>
    /// <param name="node">Node to check.</param>
    /// <exception cref="InvalidOperationException">Node does not belong to this list.</exception>
    private static void ThrowIfNodeDoesNotBelong(LList<T> list, LLNode<T> node)
    {
        if (node.List != list)
            throw new InvalidOperationException("Node does not belong to this list.");
    }

    /// <summary>
    /// Throws if index is out of range.
    /// </summary>
    /// <param name="list">List to check.</param>
    /// <param name="index">Index to check.</param>
    /// <exception cref="IndexOutOfRangeException">Index out of range.</exception>
    private static void ThrowIfIndexOutOfRange(LList<T> list, int index)
    {
        if (index < 0 || index >= list.Count)
            throw new IndexOutOfRangeException("Index out of range.");
    }

    #endregion
}
