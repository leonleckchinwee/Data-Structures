
class Node
{
    constructor(value)
    {
        this.value = value;
        this.next  = null;
        this.prev  = null;
    }
}

class LinkedList 
{
    constructor()
    {
        this.head   = null;
        this.tail   = null;
        this.length = 0;
    }

    insertFront(value)
    {
        const newNode = new Node(value);

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

    insertBack(value)
    {
        const newNode = new Node(value);

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

    insertBefore(existingValue, newValue)
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

    insertAt(index, value)
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
            this.insertFront(value);
            return;
        }

        // Inserting at tail
        if (index == this.length)
        {
            this.insertBack(value);
            return;
        }

        let current   = this.head;
        let i         = 0;

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

    removeFirstOf(value)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.head;
        while (current != null)
        {
            if (current.value == value)
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

    removeLastOf(value)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let current = this.tail;
        while (current != null)
        {
            if (current.value == value)
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

    clear()
    {
        this.head = null;
        this.tail = null;
        this.length = 0;
    }

    indexOf(value)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let index = 0;
        let current = this.head;

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

    lastIndexOf(value)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        let index = this.length - 1;
        let current = this.tail;

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

    contains(value)
    {
        if (this.isEmpty())
        {
            throw "List is empty.";
        }

        var current = this.head;
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

    isEmpty()
    {
        return this.length == 0;
    }

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