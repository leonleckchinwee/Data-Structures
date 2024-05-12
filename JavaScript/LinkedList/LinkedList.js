class Node
{
    constructor(value)
    {
        this.value = value;
        this.next  = null;
        this.prev  = null;
    }
}

/**
 * Doubly linked-list class.
 * This class accepts null value for reference types.
 * This class allows for duplicate values.
 */
class LinkedList 
{
    /**
     * Initializes a new empty linked list.
     */
    constructor()
    {
        this.head   = null;
        this.tail   = null;
        this.length = 0;
    }

    /**
     * Inserts a new item into the front of the linked list.
     * @param {*} item Item to insert.
     */
    insertFront(item)
    {
        const newNode = new Node(item);

        if (this.head == null)
        {
            this.head = newNode;
            this.tail = newNode;
        }
        else
        {
            newNode.next   = this.head;
            this.head.prev = newNode;
            this.head      = newNode;
        }

        ++this.length;
    }

    /**
     * Inserts a new item into the back of the linked list.
     * @param {*} value Item to insert.
     */
    insertBack(item)
    {
        const newNode = new Node(item);

        if (this.head == null)
        {
            this.head = newNode;
            this.tail = newNode;
        }
        else
        {
            this.tail.next = newNode;
            newNode.prev   = this.tail;
            this.tail      = newNode;
        }

        ++this.length;
    }

    /**
     * Inserts a new item before an existing item in the linked list.
     * @param {*} existingItem Existing item in the linked list.
     * @param {*} newItem New item to insert.
     * @throws If linked list is empty.
     */
    insertBefore(existingItem, newItem)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.head;
        while (current != null)
        {
            if (current.value == existingItem)
            {
                const newNode = new Node(newItem);

                // Update previous's next node if it exists
                if (current.prev != null)
                {
                    current.prev.next = newNode;
                }

                newNode.prev = current.prev;
                newNode.next = current;
                current.prev = newNode;

                // Update head pointer
                if (current == this.head)
                {
                    this.head = newNode;
                }

                ++this.length;
                return;
            }

            current = current.next;
        }
    }

    /**
     * Inserts a new item after an existing item in the linked list.
     * @param {*} existingItem Existing item in the linked list.
     * @param {*} newItem New item to insert.
     * @throws If linked list is empty.
     */
    insertAfter(existingValue, newValue)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.head;
        while (current != null)
        {
            if (current.value == existingValue)
            {
                const newNode = new Node(newValue);

                // Update next's previous node if it exists
                if (current.next != null)
                {
                    current.next.prev = newNode;
                }

                newNode.prev = current;
                newNode.next = current.next;
                current.next = newNode;

                // Update tail pointer
                if (current == this.tail)
                {
                    this.tail = newNode;
                }
                
                ++this.length;
                return;
            }

            current = current.next;
        }
    }

    /**
     * Inserts a new item at the specified index and push the rest of the item back.
     * @param {*} index Index to insert at.
     * @param {*} item Item to insert.
     * @throws If linked list is empty.
     */
    insertAt(index, item)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        if (index < 0 || index > this.length)
        {
            throw "Index out of range.";
        }

        // Inserting at head
        if (index == 0)
        {
            this.insertFront(item);
            return;
        }

        // Inserting at tail
        if (index == this.length)
        {
            this.insertBack(item);
            return;
        }

        let current   = this.head;
        let i         = 0;

        while (current != null)
        {
            if (i == index)
            {
                this.insertBefore(current.value, item);
                return;
            }

            ++i;
            current = current.next;
        }
    }

    /**
     * Removes the first item in the linked list.
     * @throws If linked list is empty.
     */
    removeFront()
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        if (this.head.next != null)
        {
            this.head.next.prev = null;
        }

        this.head = this.head.next;
        --this.length;
    }

    /**
     * Removes the last item in the linked list.
     * @throws If linked list is empty.
     */
    removeBack()
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        if (this.tail.prev != null)
        {
            this.tail.prev.next = null;
        }

        this.tail = this.tail.prev;
        --this.length;
    }

    /**
     * Removes the item at the specified index of the linked list.
     * @param {*} index Index to remove from.
     * @throws If linked list is empty or index out of range.
     */
    removeAt(index)
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
            this.removeFront();
            return;
        }

        // Removing tail
        if (index == this.length - 1)
        {
            this.removeBack();
            return;
        }

        let current   = this.head;
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
     * Removes the first item that matches the specified value.
     * @param {*} item Item to find and remove.
     * @throws If linked list is empty.
     */
    removeFirstOf(item)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.head;
        while (current != null)
        {
            if (current.value == item)
            {
                if (current == this.head)
                {   
                    this.removeFront();
                    return;
                }
                
                if (current == this.tail)
                {
                    this.removeBack();
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
     * Removes the last item that matches the specified value.
     * @param {*} item Item to find and remove.
     * @throws If linked list is empty.
     */
    removeLastOf(item)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.tail;
        while (current != null)
        {
            if (current.value == item)
            {
                if (current == this.head)
                {   
                    this.removeFront();
                    return;
                }
                
                if (current == this.tail)
                {
                    this.removeBack();
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
        this.head = null;
        this.tail = null;
        this.length = 0;
    }

    /**
     * Finds the first index of the node that matches the specified item.
     * @param {*} item Item to find index of.
     * @returns Index of item if found; otherwise -1.
     * @throws If linked list is empty.
     */
    indexOf(item)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let index = 0;
        let current = this.head;

        while (current != null)
        {
            if (current.value == item)
            {
                return index;
            }

            ++index;
            current = current.next;
        }

        return -1;
    }

    /**
     * Finds the last index of the node that matches the specified item.
     * @param {*} item Item to find index of.
     * @returns Index of item if found; otherwise -1.
     * @throws If linked list is empty.
     */
    lastIndexOf(item)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let index = this.length - 1;
        let current = this.tail;

        while (current != null)
        {
            if (current.value == item)
            {
                return index;
            }
            
            --index;
            current = current.prev;
        }

        return -1;
    }

    /**
     * Determines if the specified item exists in the linked list. 
     * @param {*} item Item to check for.
     * @returns True if item is in the linked list; otherwise false.
     * @throws If linked list is empty.
     */
    contains(item)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        var current = this.head;
        while (current != null)
        {
            if (current.value == item)
            {
                return true;
            }

            current = current.next;
        }

        return false;
    }

    hasCycle()
    {

    }

    /**
     * Determines if the linked list is empty.
     * @returns True if linked list is empty; otherwise false.
     */
    isEmpty()
    {
        return this.length == 0;
    }

    /**
     * Prints the entire linked list according to the specified order.
     * @param {*} reverse Reverse the print if true.
     */
    print(reverse = false)
    {
        if (reverse)
        {
            let current  = this.tail;
            const values = [];

            while (current != null)
            {
                values.push(current.value);
                current = current.prev;
            }

            if (!values.length != 0)
            {
                console.log(values.join(' => '));
            }
        }
        else
        {
            let current  = this.head;
            const values = [];

            while (current != null)
            {
                values.push(current.value);
                current = current.next;
            }

            if (!values.length != 0)
            {
                console.log(values.join(' => '));
            }
        }
    }
}

module.exports = LinkedList;