﻿using System.Collections;

namespace DSA.LinkedList;

public class LList<T> : IEnumerable<T> where T : notnull, IComparable<T> // List does not accept null values
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

        LLNode<T> otherNode = otherList.First!;
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
    /// Performs a linear search to find the first/last node that contains the specified value.
    /// </summary>
    /// <remarks>Time complexity: O(n).</remarks>
    /// <returns>True if value is found; otherwise false.</returns>
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

    #endregion

    #region Sort

    /// <summary>
    /// Swap the positions of two nodes without changing their value
    /// </summary>
    /// <remarks>Time complexity: O(1).</remarks>
    /// <returns>True if value is found; otherwise false.</returns>
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

        LLNode<T> middle  = GetMiddleNode(head)!;
        LLNode<T>  left   = head;
        LLNode<T> right   = middle.Next!; 

        middle!.Next = null;    // Splits the list into halves

        if (ascendingOrder)
            return SortAndMerge(MergeSort(left, ascendingOrder: true), 
                                MergeSort(right, ascendingOrder: true));
        else
            return SortAndMerge(MergeSort(right, ascendingOrder: false),
                                MergeSort(left, ascendingOrder: false), ascendingOrder: false);
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

        LLNode<T>? fast = First.Next;
        LLNode<T>  slow = First;

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

        LLNode<T>? fast = node.Next;
        LLNode<T>  slow = node;

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

    #endregion
}
