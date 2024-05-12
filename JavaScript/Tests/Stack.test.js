const Stack = require('../Stack/Stack');

test('constructor', () => {
    const stack = new Stack();

    expect(stack.top).toBeNull;
    expect(stack.size).toBe(0);
});

test('push, number, empty stack', () => {
    const stack = new Stack();
    stack.push(1);

    expect(stack.top.value).toBe(1);
    expect(stack.size).toBe(1);
});

test('push, null, empty stack', () => {
    const stack = new Stack();
    stack.push(null);

    expect(stack.top.value).toBe(null);
    expect(stack.size).toBe(1);
})

test('push, multiple values', () => {
    const stack = new Stack();
    stack.push(1);

    expect(stack.top.value).toBe(1);
    expect(stack.size).toBe(1);

    stack.push('a');

    expect(stack.top.value).toBe('a');
    expect(stack.size).toBe(2);
});

test('pop, empty stack', () => {
    const stack = new Stack();
    
    expect(() => stack.pop()).toThrow();
});

test('pop, one item in stack', () => {
    const stack = new Stack();
    stack.push(1);
    const item = stack.pop();

    expect(item).toBe(1);
    expect(stack.size).toBe(0);
});

test('pop, multiple items in stack', () => {
    const stack = new Stack();
    stack.push(1);
    stack.push(null);

    const item = stack.pop();

    expect(item).toBeNull();
    expect(stack.top.value).toBe(1);
    expect(stack.size).toBe(1);
});

test('peek, empty stack', () => {
    const stack = new Stack();

    expect(() => stack.peek()).toThrow();
});

test('peek, one item in stack', () => {
    const stack = new Stack();
    stack.push(1);
    const item = stack.peek();

    expect(item).toBe(1);
    expect(stack.top.value).toBe(1);
    expect(stack.size).toBe(1);
});

test('peek, multiple items in stack', () => {
    const stack = new Stack();
    stack.push(1);
    stack.push('a')
    const item = stack.peek();

    expect(item).toBe('a');
    expect(stack.top.value).toBe('a');
    expect(stack.size).toBe(2);
});