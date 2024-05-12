class Node
{
    constructor(value)
    {
        this.value = value;
        this.next  = null;
    }
}

/**
 * Represents a last-in, first-out collection of items.
 * This class accepts null value for reference types.
 * This class allows for duplicate values.
 * This stack class is implemented using a linked-list.
 */
class Stack
{
    /**
     * Initializes an empty stack.
     */
    constructor()
    {
        this.top  = null;
        this.size = 0;
    }

    /**
     * Push a new item to the top of the stack.
     * @param {*} item Item to insert.
     */
    push(item)
    {
        const node = new Node(item);

        node.next = this.top;
        this.top  = node;

        ++this.size;
    }

    /**
     * Removes and returns the item at the top of the stack.
     * @returns Item at the top of the stack.
     * @throws If stack is empty.
     */
    pop()
    {
        if (this.isEmpty())
        {
            throw "Stack is empty.";
        }

        const node = this.top;
        this.top   = this.top.next;

        --this.size;
        return node.value;
    }

    /**
     * Returns the item at the top of the stack without changing the stack.
     * @returns Item at the top of the stack.
     * @throws If stack is empty.
     */
    peek()
    {
        if (this.isEmpty())
        {
            throw "Stack is empty.";
        }

        const node = this.top;
        return node.value;
    }

    /**
     * Determines if the specified item exists in the stack.
     * @param {*} item Item to check for.
     * @returns True if item is in the stack; otherwise false.
     * @throws If stack is empty.
     */
    contains(item)
    {
        if (this.isEmpty())
        {
            throw "Stack is empty.";
        }

        const node = this.top;
        while (node != null)
        {
            if (node.value == item)
            {
                return true;
            }

            node = node.next;
        }

        return false;
    }

    /**
     * Clears the entire stack.
     */
    clear()
    {
        this.top  = null;
        this.size = 0;
    }

    /**
     * Determines if the stack is empty.
     * @returns True if stack is empty; otherwise false.
     */
    isEmpty()
    {
        return this.size == 0;
    }
}

module.exports = Stack;