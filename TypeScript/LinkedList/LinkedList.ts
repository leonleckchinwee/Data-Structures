/**
 * Node for the doubly linked list class below.
 * This class accepts null value for reference types.
 */
export class Node<T>
{
    /**
     * Value contained in this node.
     */
    value: T;

    /**
     * Reference of the next node.
     */
    next : Node<T> | null;

    /**
     * Reference of the previous node.
     */
    prev : Node<T> | null;
    
    /**
     * Initializes a new node containing the specified value.
     * @param value Value to insert into the new node.
     */
    constructor(value: T)
    {
        this.value = value;
        this.next  = null;
        this.prev  = null;
    }

    /**
     * Determines if the specified node has the same value as this node.
     * @param node Other node to check with.
     * @returns True if both node's value are equal; otherwise false.
     */
    equals(node: Node<T>)
    {
        return this.value == node.value;
    }
}

/**
 * Doubly linked list class.
 * This class accepts null value for reference types.
 * This class allows for duplicate values.
 */
export class LinkedList<T>
{
    /**
     * Reference to the first node in this linked list.
     */
    first : Node<T> | null;

    /**
     * Reference to the last node in this linked list.
     */
    last  : Node<T> | null;

    /**
     * Number of nodes in this linked list.
     */
    length: number;

    /**
     * Initializes a new empty linked list.
     */
    constructor()
    {
        this.first  = null;
        this.last   = null;
        this.length = 0;
    }

    /**
     * Inserts a new node containing the specified value at the front of this linked list.
     * @param value Value to insert.
     */
    insertFirst(value: T)
    {
        const node = new Node<T>(value);

        if (this.isEmpty())
        {
            this.first = node;
            this.last  = node;
        }
        else
        {
            node.next        = this.first;
            this.first!.prev = node;
            this.first       = node;
        }

        ++this.length;
    }

    /**
     * Inserts a new node containing the specified value at the back of this linked list.
     * @param value Value to insert.
     */
    insertLast(value: T)
    {
        const node = new Node<T>(value);

        if (this.isEmpty())
        {
            this.first = node;
            this.last  = node;
        }
        else
        {
            node.prev       = this.last;
            this.last!.next = node;
            this.last       = node;
        }

        ++this.length;
    }

    /**
     * Inserts a new node containing the specified value before the first existing specific node of this linked list.
     * Nothing happens if the specified existing value is not in this linked list.
     * @param existingValue Existing value in the linked list.
     * @param newValue New value to insert.
     * @throws If linked list is empty. 
     */
    insertBefore(existingValue: T, newValue: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.first;
        while (current != null)
        {
            if (current.value == existingValue)
            {
                const node = new Node<T>(newValue);

                if (current.prev != null)
                {
                    current.prev.next = node;
                }

                node.prev    = current.prev;
                node.next    = current;
                current.prev = node;

                if (current == this.first)
                {
                    this.first = node;
                }

                ++this.length;

                return;
                
            }

            current = current.next;
        }
    }

    /**
     * Inserts a new node containing the specified value after the first existing specific node of this linked list.
     * Nothing happens if the specified existing value is not in this linked list.
     * @param existingValue Existing value in the linked list.
     * @param newValue New value to insert.
     * @throws If linked list is empty. 
     */
    insertAfter(existingValue: T, newValue: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.first;
        while (current != null)
        {
            if (current.value == existingValue)
            {
                const node = new Node<T>(newValue);

                if (current.next != null)
                {
                    current.next.prev = node;
                }

                node.prev    = current;
                node.next    = current.next;
                current.next = node;

                if (current == this.last)
                {
                    this.last = node;
                }

                ++this.length;

                return;
            }

            current = current.next;
        }
    }

    /**
     * Inserts a new node containing the specified value at the specified index and push the rest of the nodes back.
     * @param index Index to insert at.
     * @param value Value to insert.
     * @throws If linked list is empty.
     * @throws If index is out of range.
     */
    insertAt(index: number, value: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        if (index < 0 || index > this.length)
        {
            throw "Index out of range.";
        }

        if (index == 0)
        {
            this.insertFirst(value);
            return;
        }

        if (index == this.length)
        {
            this.insertLast(value);
            return;
        }

        let current = this.first;
        let i       = 0;

        while (current != null)
        {
            if (i == index)
            {
                this.insertBefore(current.value, value);
                return;
            }

            ++i;
            current = current.next;
        }
    }

    /**
     * Removes the first node in this linked list.
     * @throws If linked list is empty.
     */
    removeFirst()
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        if (this.first!.next != null)
        {
            this.first!.next.prev = null;
        }

        this.first = this.first!.next;
        --this.length;
    }

    /**
     * Removes the last node in this linked list.
     * @throws If linked list is empty.
     */
    removeLast()
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        if (this.last!.prev != null)
        {
            this.last!.prev!.next = null;
        }

        this.last = this.last!.prev;
        --this.length;
    }

    /**
     * Removes the node at the specified index of this linked list.
     * @param index Index to remove from.
     * @throws If linked list is empty.
     * @throws If index is out of range.
     */
    removeAt(index: number)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        if (index < 0 || index >= this.length)
        {
            throw "Index out of range.";
        }

        // Removing head
        if (index == 0)
        {
            this.removeFirst();
            return;
        }

        // Removing tail
        if (index == this.length - 1)
        {
            this.removeLast();
            return;
        }

        let current   = this.first;
        let i         = 0;

        while (current != null)
        {
            if (i == index)
            {
                if (current.prev != null)
                {
                    current.prev.next = current.next;
                }

                if (current.next != null)
                {
                    current.next.prev = current.prev;
                }

                current = null;

                --this.length;
                return;
            }

            ++i;
            current = current.next;
        }
    }

    /**
     * Removes the first node that matches with the specified value in this linked list.
     * Nothing happens if the specified existing value is not in this linked list.
     * @param value Value to match and remove.
     * @throws If linked list is empty.
     */
    removeFirstOf(value: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.first;
        while (current != null)
        {
            if (current.value == value)
            {
                if (current == this.first)
                {   
                    this.removeFirst();
                    return;
                }
                
                if (current == this.last)
                {
                    this.removeLast();
                    return;
                }

                if (current.prev != null)
                {
                    current.prev.next = current.next;
                }

                if (current.next != null)
                {
                    current.next.prev = current.prev;
                }

                current = null;

                --this.length;
                return;
            }

            current = current.next;
        }
    }

    /**
     * Removes the last node that matches with the specified value in this linked list.
     * Nothing happens if the specified existing value is not in this linked list.
     * @param value Value to match and remove.
     * @throws If linked list is empty.
     */
    removeLastOf(value: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.last;
        while (current != null)
        {
            if (current.value == value)
            {
                if (current == this.first)
                {   
                    this.removeFirst();
                    return;
                }
                
                if (current == this.last)
                {
                    this.removeLast();
                    return;
                }

                if (current.prev != null)
                {
                    current.prev.next = current.next;
                }

                if (current.next != null)
                {
                    current.next.prev = current.prev;
                }

                current = null;

                --this.length;
                return;
            }

            current = current.prev;
        }
    }

    /**
     * Clears the entire linked list.
     */
    clear()
    {
        this.first  = null;
        this.last   = null;
        this.length = 0;
    }

    /**
     * Finds the first index of the node that matches with the specified value.
     * @param value Value to find index of.
     * @returns Index of node if found; otherwise -1.
     * @throws If linked list is empty.
     */
    indexOf(value: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let index = 0;
        let current = this.first;

        while (current != null)
        {
            if (current.value == value)
            {
                return index;
            }

            ++index;
            current = current.next;
        }

        return -1;
    }

    /**
     * Finds the last index of the node that matches with the specified value.
     * @param value Value to find index of.
     * @returns Index of node if found; otherwise -1.
     * @throws If linked list is empty.
     */
    lastIndexOf(value: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let index = this.length - 1;
        let current = this.last;

        while (current != null)
        {
            if (current.value == value)
            {
                return index;
            }
            
            --index;
            current = current.prev;
        }

        return -1;
    }

    /**
     * Determines if the specified value exists in this linked list. 
     * @param value Value to check for.
     * @returns True if value is in this linked list; otherwise false.
     * @throws If linked list is empty.
     */
    contains(value: T)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        var current = this.first;
        while (current != null)
        {
            if (current.value == value)
            {
                return true;
            }

            current = current.next;
        }

        return false;
    }

    /**
     * Determines if this linked list is empty.
     * @returns True if linked list is empty; otherwise false.
     */
    isEmpty()
    {
        return this.first == null;
    }
}