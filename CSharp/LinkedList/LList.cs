using System.Collections;

namespace DSA.LinkedList;

public class LList<T> : IEnumerable<T> where T : notnull, IComparable<T>
{
    #region Properties

    /// <summary>
    /// Gets the number of nodes actually contained in this list.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Gets the first node of this list.
    /// </summary>
    public LLNode<T>? First { get; private set; }

    /// <summary>
    /// Gets the last node of this list.
    /// </summary>
    public LLNode<T>? Last { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of list that is empty.
    /// </summary>
    public LList()
    {
        First = null;
        Last  = null;
        Count = 0;
    }

    /// <summary>
    /// Initializes a new instance of list that contains elements copied from the 
    /// specified IEnumerable.
    /// </summary>
    public LList(IEnumerable<T> other)
    {
        if (other == null)
            return;

        foreach (T item in other)
        {
            LLNode<T> node = new LLNode<T>(item);

            AddLast(node);
        }
    }

    #endregion

    #region Add

    /// <summary>
    /// Adds a new node containing the specified value at the start of this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    /// <returns>The new node containing the value.</returns>
    public LLNode<T> AddFirst(T item)
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

        return newNode;
    }

    /// <summary>
    /// Adds the specified new node at that start of this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    public void AddFirst(LLNode<T> item)
    {
        if (item.List != null)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node belongs to another list!");
        if (item == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(item), "LinkedList node is null!");

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

    /// <summary>
    /// Adds a new node containing the specified value at the end of this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    /// <returns>The new node containing the value.</returns>
    public LLNode<T> AddLast(T item)
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

        return newNode;
    }

    /// <summary>
    /// Adds the specified new node at that end of this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    public void AddLast(LLNode<T> item)
    {
        if (item.List != null)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node belongs to another list!");
        if (item == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(item), "LinkedList node is null!");

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

    /// <summary>
    /// Adds a new node containing the specified value before the specified existing node in this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    /// <returns>The new node containing the value.</returns>
    public LLNode<T> AddBefore(LLNode<T> node, T item)
    {
        if (node.List != this)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node is not in current list!");
        if (node == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(item), "LinkedList node is null!");

        LLNode<T> newNode = new LLNode<T>(item) { List = this };

        if (First == null || Last == null)  // Empty list
        {
            First = newNode;
            Last  = First;
        }
        else
        {
            if (node.Previous == null)  // Front of list
            {
                node.Previous = newNode;
                newNode.Next  = node;
                First         = newNode;
            }
            else
            {
                newNode.Previous   = node.Previous;
                node.Previous.Next = newNode;
                newNode.Next       = node;
                node.Previous      = newNode;
            }

            UpdateLast();
        }

        ++Count;

        return newNode;
    }

    /// <summary>
    /// Adds the specified new node before the specified existing node in this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    public void AddBefore(LLNode<T> node, LLNode<T> newNode)
    {
        if (node.List != this)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node is not in current list!");
        if (node == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(node), "LinkedList node is null!");

        if (newNode.List != this)
            throw new InvalidOperationException("LinkedList node belongs to another list!");
        if (newNode == null)
            throw new ArgumentNullException(nameof(newNode), "LinkedList node is null!");

         if (First == null || Last == null)  // Empty list
        {
            First = newNode;
            Last  = First;
        }
        else
        {
            if (node.Previous == null)  // Front of list
            {
                node.Previous = newNode;
                newNode.Next  = node;
                First         = newNode;
            }
            else
            {
                newNode.Previous   = node.Previous;
                node.Previous.Next = newNode;
                newNode.Next       = node;
                node.Previous      = newNode;
            }

            UpdateLast();
        }

        ++Count;
    }

    /// <summary>
    /// Adds a new node containing the specified value after the specified existing node in this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    /// <returns>The new node containing the value.</returns>
    /// 
    public LLNode<T> AddAfter(LLNode<T> node, T item)
    {
        if (node.List != this)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node is not in current list!");
        if (node == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(item), "LinkedList node is null!");

        LLNode<T> newNode = new LLNode<T>(item) { List = this };

        if (First == null || Last == null)  // Empty list
        {
            First = newNode;
            Last  = First;
        }
        else
        {
            if (node.Next == null)  // End of list
            {
                node.Next        = newNode;
                newNode.Previous = node;
            }
            else
            {
                newNode.Previous   = node;
                node.Next.Previous = newNode;
                newNode.Next       = node.Next;
                node.Next          = newNode;
            }

            UpdateLast();
        }

        ++Count;

        return newNode;
    }

    /// <summary>
    /// Adds the specified new node after the specified existing node in this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    public void AddAfter(LLNode<T> node, LLNode<T> newNode)
    {
        if (node.List != this)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node is not in current list!");
        if (node == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(node), "LinkedList node is null!");

        if (newNode.List != this)
            throw new InvalidOperationException("LinkedList node belongs to another list!");
        if (newNode == null)
            throw new ArgumentNullException(nameof(newNode), "LinkedList node is null!");

        if (First == null || Last == null)  // Empty list
        {
            First = newNode;
            Last  = First;
        }
        else
        {
            if (node.Next == null)  // End of list
            {
                node.Next        = newNode;
                newNode.Previous = node;
            }
            else
            {
                newNode.Previous   = node;
                node.Next.Previous = newNode;
                newNode.Next       = node.Next;
                node.Next          = newNode;
            }

            UpdateLast();
        }

        ++Count;
    }

    /// <summary>
    /// Appends another list to this list, adding all nodes from second list to the end of the first list.
    /// </summary>
    /// <remarks>Time complexity: O(m + n) where m is the Count in the other list.</remarks>
    public void Append(LList<T>? otherList)
    {
        if (otherList == null)
            throw new ArgumentNullException(nameof(otherList), "Linkedlist is empty!");
        if (otherList.Count == 0 || Count == 0)
            throw new InvalidOperationException("Linkedlist is empty!");

        LLNode<T> otherNode = otherList.First!; // TODO: Edge case where first list is empty!
        while (otherNode != null)
        {
            otherNode.List = this;

            otherNode.Previous = Last;
            Last = otherNode; 

            otherNode = otherNode.Next!;
        }

        UpdateLast();
        Count += otherList.Count;
    }

    #endregion

    #region Remove

    /// <summary>
    /// Removes the node at the start of this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    public void RemoveFirst()
    {
        if (First == null || Last == null)
            throw new InvalidOperationException("LinkedList is empty!");

        if (First == Last) // Only one element
        {
            First = Last = null;
        }
        else
        {
            First = First.Next;

            UpdateLast();
        }

        --Count;
    }

    /// <summary>
    /// Removes the node at the end of this list.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    public void RemoveLast()
    {
        if (First == null || Last == null)
            throw new InvalidOperationException("LinkedList is empty!");

        if (First == Last) // Only one element
        {
            First = Last = null;
        }
        else
        {
            Last = Last.Previous;
        }

        --Count;
    }

    /// <summary>
    /// Removes the first/last occurence of the specified value from this list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    public bool Remove(T value, bool removeFirst = true)
    {
        if (Count == 0)     // Empty list
            return false;

        if (First == Last)  // Only one element in list
        {
            if (First!.Value.Equals(value))
            {
                First = Last = null;
                --Count;

                return true;
            }

            return false;
        }

        LLNode<T>? node = LinearSearch(value, findFirst: removeFirst);

        if (node == null)   // Element not in list
            return false;

        if (node == First)  // First element
        {
            First = First.Next;
        }
        else if (node == Last)  // Last element
        {
            Last = Last.Previous;
        }
        else
        {
            node.Previous!.Next = node.Next;
            node.Next!.Previous = node.Previous;
        }

        UpdateLast();

        --Count;

        return true;
    }

    /// <summary>
    /// Removes the specified node from this list.
    /// </summary>
    public void Remove(LLNode<T> node)
    {
        if (node.List != this)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node is not in current list!");
        if (node == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(node), "LinkedList node is null!");

        if (First == Last)  // Only one element in list
        {
            First = Last = null;
            --Count;

            return;
        }

        if (node == First)  // First element
        {
            First = First.Next;
        }
        else if (node == Last)  // Last element
        {
            Last = Last.Previous;
        }
        else
        {
            node.Previous!.Next = node.Next;
            node.Next!.Previous = node.Previous;
        }

        UpdateLast();
        
        --Count;
    }

    /// <summary>
    /// Removes all nodes from this list.
    /// </summary>
    public void Clear()
    {
        First = Last = null;
        Count = 0;
    }

    #endregion

    #region Search

    /// <summary>
    /// Determines whether a value is in this list.
    /// </summary>
    /// <remarks>This method performs a linear search: O(n) where n is Count.</remarks>
    /// <returns>True if value is found; otherwise false.</returns>
    public bool Contains(T value)
    {
        LLNode<T>? node = LinearSearch(value);

        return node != null;
    }

    // TODO: Contains(LLNode<T> node)

    /// <summary>
    /// Determines whether there is a cycle in this list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>True if cycle is found; otherwise false.</returns>
    public bool HasCycle(LLNode<T> node)
    {
        if (node.List != this)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node is not in current list!");
        if (node == null)       // Check if new node is null
            throw new ArgumentNullException(nameof(node), "LinkedList node is null!");

        HashSet<LLNode<T>> nodes = new HashSet<LLNode<T>>();
        while (node != null)
        {
            if (nodes.Contains(node))
                return true;

            nodes.Add(node);

            node = node.Next!;
        }

        return false;
    }

    /// <summary>
    /// Finds the node with the largest value in this list.
    /// </summary>
    /// <remarks>Time complexity: O(n log n).</remarks>
    /// <returns>Node with largest value if found; otherwise null.</returns>
    public LLNode<T>? Max()
    {
        return MergeSort(First, ascendingOrder: false);
    }

    /// <summary>
    /// Finds the node with the smallest value in this list.
    /// </summary>
    /// <remarks>Time complexity: O(n log n).</remarks>
    /// <returns>Node with smallest value if found; otherwise null.</returns>
    public LLNode<T>? Min()
    {
        return MergeSort(First, ascendingOrder: true);
    }

    /// <summary>
    /// Performs a linear search to find the first/last node that contains the specified value.
    /// </summary>
    /// <remarks>If findFirst is true, it will search from start to end; otherwise it will search from
    /// end to start. Time complexity: O(n).</remarks>
    /// <returns>Node with specified value.</returns>
    public LLNode<T>? LinearSearch(T value, bool findFirst = true)
    {
        if (First == null || Last == null)
            return null;

        if (findFirst)  // Finds from first to last
        {
            LLNode<T> head = First;
            while (head != null)
            {
                if (head.Value.Equals(value))
                    return head;

                head = head.Next!;
            }
        }
        else    // Finds from last to first
        {
            LLNode<T> tail = Last;
            while (tail != null)
            {
                if (tail.Value.Equals(value))
                    return tail;

                tail = tail.Previous!;
            }
        }
        
        return null;
    }

    /// <summary>
    /// Performs a binary search to find the first node that contains the specified value.
    /// </summary>
    /// <remarks>Note: List will be sorted if its not sorted in ascending order.
    /// Time complexity: O(n), because of finding the middle node.</remarks>
    /// <returns>Node with specified value.</returns>
    public LLNode<T>? BinarySearch(T value)
    {
        if (First == null)
            return null;

        if (!IsSorted(ascendingOrder: true))    // Sort the list if not sorted
            First = MergeSort(First, ascendingOrder: true);

        LLNode<T> start  = First!;
        LLNode<T>? end   = null;
        LLNode<T> target = new LLNode<T>(value);
        
        do
        {
            LLNode<T>? middle = GetMiddleNode(start, end);

            if (middle == null)
                return null;
            
            if (middle.Value.Equals(value))
                return middle;

            if (target < middle)    // Target value lesser than middle node
                end = middle;
            else                    // Target value greater than middle node
                start = middle.Next!;
        }
        while (end == null || end != start);

        return null;
    }

    #endregion

    #region Sort

    /// <summary>
    /// Swaps the positions of two nodes without changing their values.
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    public void Swap(LLNode<T> first, LLNode<T> second)
    {
        if (first.List != this || second.List != this)  // Check if node belongs to another list
            throw new InvalidOperationException("LinkedList node is not in current list!");
        if (first == null || second == null)            // Check if node is null
            throw new ArgumentNullException("LinkedList node is null!");
        if (first == second)                            // Check if nodes are the same
            throw new InvalidOperationException("Cannot swap the same node!");

        if (first == First)
            First = second;
        else if (second == First)
            First = first;

        if (first.Previous != null)
            first.Previous.Next = second;
        if (second.Previous != null)
            second.Previous.Next = first;

        if (first.Next != null)
            first.Next.Previous = second;
        if (second.Next != null)
            second.Next.Previous = first;

        (first.Next, second.Next) = (second.Next, first.Next);

        if (first == second.Previous)
            first.Previous = second;
        else if (second == first.Previous)
            second.Previous = first;
        else
            (first.Previous, second.Previous) = (second.Previous, first.Previous);

        UpdateLast();
    }

    public void SwapData(LLNode<T> first, LLNode<T> second)
    {
        if (first != null && second != null && first != second)
            (first.Value, second.Value) = (second.Value, first.Value);
    }

    /// <summary>
    /// Reverse the positions of all nodes in this list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>The new head of this list.</returns>
    public LLNode<T> Reverse()
    {
        if (Count == 0 || First == null || Last == null)
            throw new InvalidOperationException("Linkedlist is empty!");

        if (First == Last)  // Only one element
            return First;

        LLNode<T> current   = First;
        LLNode<T>? previous = null;
        
        while (current != null)
        {
            LLNode<T>? next  = current.Next;
            current.Next     = previous;
            current.Previous = next;

            previous = current;
            current  = next!;
        }

        First = Last;
        UpdateLast();

        return First;
    }

    /// <summary>
    /// Performs merge sort to sort the list in specified order
    /// </summary>
    /// <remarks>Time complexity: O(n log n).</remarks>
    /// <returns>The new head of the ordered list.</returns>
    public LLNode<T>? MergeSort(LLNode<T>? head, bool ascendingOrder = true)
    {
        if (head == null || head.Next == null)
            return head;

        LLNode<T> middle = GetMiddleNode(head)!;
        LLNode<T>  left  = head;
        LLNode<T> right  = middle.Next!; 

        middle!.Next = null;    // Splits the list into halves

        if (ascendingOrder)
            return SortAndMerge(MergeSort(left,  ascendingOrder: true), 
                                MergeSort(right, ascendingOrder: true), ascendingOrder: true);
        else
            return SortAndMerge(MergeSort(right, ascendingOrder: false),
                                MergeSort(left,  ascendingOrder: false), ascendingOrder: false);
    }

    public LLNode<T>? QuickSort()
    {
        throw new NotImplementedException();
    }

    public LLNode<T> InsertionSort()
    {
        throw new NotImplementedException();
    }

    public LLNode<T> SelectionSort()
    {
        throw new NotImplementedException();
    }

    public LLNode<T> BubbleSort()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Utilities

    /// <summary>
    /// An enumerator for this list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>List enumerator.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        LLNode<T>? current = First;
        while (current != null)
        {
            yield return current.Value;
            
            current = current.Next!;
        }
    }

    /// <summary>
    /// An enumerator for this list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>List enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator()!;
    }

    /// <summary>
    /// Copies the entire list to a compatible one-dimensional array, starting at the 
    /// specified index of the target array.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    public void CopyTo(T[] array, int index)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array), "Array is null!");
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative!");

        if (Count > array.Length - index)
            throw new ArgumentException("Array capacity is insufficient!");

        LLNode<T>? current = First;
        int i = index;
        while (current != null)
        {
            array[i] = current.Value;
            current  = current.Next!;
            ++i;
        }
    }

     /// <summary>
    /// Gets the middle node of this list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>Middle node of this list.</returns>
    public LLNode<T> GetMiddleNode()
    {
        if (Count == 0 || First == null || Last == null)
            throw new InvalidOperationException("Linkedlist is empty!");

        LLNode<T>  slow = First;
        LLNode<T>? fast = First.Next;

        while (fast != null)
        {
            fast = fast.Next;

            if (fast != null)
            {
                slow = slow.Next!;
                fast = fast.Next;
            }
        }

        return slow;
    }

    /// <summary>
    /// Gets the middle node between specified node and end of list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>Middle node.</returns>
    public LLNode<T>? GetMiddleNode(LLNode<T> node)
    {
        if (node == null)
            return null;

        if (node.List != this)
            return null;

        LLNode<T>  slow = node;
        LLNode<T>? fast = node.Next;

        while (fast != null)
        {
            fast = fast.Next;

            if (fast != null)
            {
                slow = slow.Next!;
                fast = fast.Next;
            }
        }

        return slow;
    }

    /// <summary>
    /// Gets the middle node between specified start and end node.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>Middle node.</returns>
    public LLNode<T>? GetMiddleNode(LLNode<T>? start, LLNode<T>? end)
    {
        bool IsBefore(LLNode<T> start, LLNode<T> end)
        {
            LLNode<T> current = start;
            while (current != null)
            {
                if (current == end)
                    return true;

                current = current.Next!;
            }
            return false;
        }

        if (start == null)
            return null;

        if (end == null)
            return GetMiddleNode(start);

        if (start.List != this || end.List != this)
            throw new InvalidOperationException("Linkedlist node is not in current list!");

        // Check if start is before end
        if (!IsBefore(start, end))
            (start, end) = (end, start);

        LLNode<T>  slow = start;
        LLNode<T>? fast = start;

        while (fast != end && fast?.Next != null)
        {
            slow = slow.Next!;
            fast = fast!.Next!.Next;
        }

        return slow;
    }

    /// <summary>
    /// Sort and merge lists according to the order specified.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>First node of new sorted list.</returns>
    public LLNode<T>? SortAndMerge(LLNode<T>? a, LLNode<T>? b, bool ascendingOrder = true)
    {
        if (a == null)
            return b;

        if (b == null)
            return a;

        LLNode<T>? result;

        if (ascendingOrder) // Smallest to greatest
        {
            if (a <= b)
            {
                result          = a;
                result.Next     = SortAndMerge(a.Next, b, ascendingOrder: true);
            }
            else
            {
                result          = b;
                result.Next     = SortAndMerge(a, b.Next, ascendingOrder: true);
            }
        }
        else    // Greatest to smallest
        {
            if (a >= b)
            {
                result      = a;
                result.Next = SortAndMerge(a.Next, b, ascendingOrder: false);
            }
            else
            {
                result      = b;
                result.Next = SortAndMerge(a, b.Next, ascendingOrder: false);
            }
        }

        // Handles previous pointers
        LLNode<T> head = result;
        while (result.Next != null)
        {
            LLNode<T> temp  = result;
            result          = result.Next!;
            result.Previous = temp;
        }

        return head;
    }

    /// <summary>
    /// Gets the string representing the entire list according to specified order.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>String representing the list.</returns>
    public string ListAsString(bool frontToBack = true)
    {
        string list = "";

        if (frontToBack)
        {
            LLNode<T>? current = First;

            list += "[Front to back]: ";
            while (current != null)
            {
                list += current.Value.ToString() + " -> ";
                current = current.Next!;
            }
            list += '\n';
        }
        else
        {
            LLNode<T>? current = Last;

            list += "[Back to front]: ";
            while (current != null)
            {
                list += current.Value.ToString() + " -> ";
                current = current.Previous!;
            }
            list += '\n';
        }

        return list;
    }

    #endregion

    #region Private    

    /// <summary>
    /// Updates the last pointer to point at the correct end of this list.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    void UpdateLast()
    {
        if (First == null && Last == null)
            return;

        if (Last == null)   // Last is null, so need to traverse the entire list to find last again
        {
            LLNode<T> current = First!;

            while (current.Next != null)
            {
                current = current.Next;
            }

            Last = current;
        }
        else    // Travserse from last if last is not null
        {
            LLNode<T> current = Last;

            while (current.Next != null)
            {
                current = current.Next;
            }

            Last = current;
        }
    }

    /// <summary>
    /// Checks if list is sorted according to specified order.
    /// </summary>
    /// <returns>True is sorted correctly; otherwise false.</returns>
    bool IsSorted(bool ascendingOrder = true)
    {
        if (First == null)
            return false;

        LLNode<T> current = First;
        while (current != null)
        {
            if (current.Next != null)
            {
                if (ascendingOrder && current.Next < current)
                    return false;
                
                if (!ascendingOrder && current.Next > current)
                    return false;
            }

            current = current.Next!;
        }

        return true;
    }

    #endregion
}
