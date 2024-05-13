class Node
{
    constructor(value)
    {
        this.value = value;
        this.next  = null;
    }
}

class Queue
{
    constructor()
    {
        this.first = null;
        this.last  = null;
        this.size  = 0;
    }

    enqueue(item)
    {
        const node = new Node(item);

        if (this.isEmpty())
        {
            this.first = node;
            this.last  = node;
        }
        else
        {
            this.last.next = node;
            this.last      = node;
        }

        ++this.size;
    }

    dequeue()
    {
        if (this.isEmpty())
        {
            throw "Queue is empty.";
        }

        const node = this.first;
        this.first = this.first.next;

        --this.size;
        return node.value;
    }

    front()
    {
        if (this.isEmpty())
        {
            throw "Queue is empty.";
        }

        const copy = this.first.value;
        return copy;
    }

    contains(item)
    {
        if (this.isEmpty())
        {
            throw "Queue is empty.";
        }

        let current = this.first;
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

    clear()
    {
        this.first = null;
        this.last  = null;
        this.size  = 0;
    }

    isEmpty()
    {
        return this.size == 0;
    }
}

module.exports = Queue;