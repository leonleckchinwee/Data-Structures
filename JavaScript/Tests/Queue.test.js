const { experiments } = require('webpack');
const Queue = require('../Queue/Queue');

test('constructor', () => {
    const queue = new Queue();

    expect(queue.size).toBe(0);
    expect(queue.first).toBeNull();
    expect(queue.last).toBeNull();
});

test('enqueue, queue is empty', () => {
    const queue = new Queue();
    queue.enqueue(1);

    expect(queue.size).toBe(1);
    expect(queue.first.value).toBe(1);
    expect(queue.first).toBe(queue.last);
});

test('enqueue, different types', () => {
    const queue = new Queue();
    queue.enqueue(1);
    queue.enqueue('a');
    queue.enqueue(null);

    expect(queue.size).toBe(3);
    expect(queue.first.value).toBe(1);
    expect(queue.first.next.value).toBe('a');
    expect(queue.first.next.next.value).toBeNull();
    expect(queue.last.value).toBeNull();
});

test('dequeue, list is empty', () => {
    const queue = new Queue();
    
    expect(() => queue.dequeue()).toThrow();
});

test('dequeue, one item in list', () => {
    const queue = new Queue();
    queue.enqueue(1);
    const item = queue.dequeue();

    expect(queue.size).toBe(0);
    expect(item).toBe(1);
});

test('dequeue, two items in list', () => {
    const queue = new Queue();
    queue.enqueue(1);
    queue.enqueue(null);

    const item = queue.dequeue();
    
    expect(queue.size).toBe(1);
    expect(item).toBe(1);
    expect(queue.first.value).toBeNull();
    expect(queue.first).toBe(queue.last);
});

test('front, list is empty', () => {
    const queue = new Queue();

    expect(() => queue.front()).toThrow();
});

test('front, one item in list', () => {
    const queue = new Queue();
    queue.enqueue(1);
    const item = queue.front();

    expect(queue.size).toBe(1);
    expect(item).toBe(1);
    expect(queue.first.value).toBe(1);
    expect(queue.first).toBe(queue.last);
});

test('front, correct item after enqueue', () => {
    const queue = new Queue();
    queue.enqueue(1);
    const item1 = queue.front();
    const size1 = queue.size;

    queue.enqueue('a');
    const item2 = queue.front();
    const size2 = queue.size;

    expect(item1).toBe(1);
    expect(item1).toBe(item2);
    expect(size1).toBe(1);
    expect(size2).toBe(2);
})

test('front, correct item after dequeue', () => {
    const queue = new Queue();
    queue.enqueue(1);
    queue.enqueue(null);

    const item1 = queue.front();
    queue.dequeue();

    const item2 = queue.front();

    expect(queue.size).toBe(1);
    expect(item1).toBe(1);
    expect(item2).toBeNull();
    expect(queue.first.value).toBeNull();
    expect(queue.first).toBe(queue.last);
});