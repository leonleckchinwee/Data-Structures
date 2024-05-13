const LinkedList = require('../LinkedList/LinkedList');

test('constructor, empty', () => {
	const list = new LinkedList();

	expect(list.head).toBeNull;
	expect(list.tail).toBeNull;
	expect(list.length).toBe(0);
});

test('addFront, one item', () => {
	const list = new LinkedList();
	list.insertFront(1);
	
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.length).toBe(1);
});

test('addFront, two item', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	
	expect(list.head.value).toBe(2);
	expect(list.head.next.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.tail.prev.value).toBe(2);
	expect(list.length).toBe(2);
});

test('addFront, different types', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront('a');


	expect(list.head.value).toBe('a');
	expect(list.head.next.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.tail.prev.value).toBe('a');
	expect(list.length).toBe(2);
})

test('addBack, one item', () => {
	const list = new LinkedList();
	list.insertBack(1);
	
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.length).toBe(1);
});

test('addBack, two item', () => {
	const list = new LinkedList();
	list.insertBack(1);
	list.insertBack(2);
	
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(2);
	expect(list.tail.value).toBe(2);
	expect(list.tail.prev.value).toBe(1);
	expect(list.length).toBe(2);
});

test('addFront, different types', () => {
	const list = new LinkedList();
	list.insertBack(1);
	list.insertBack('a');


	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe('a');
	expect(list.tail.value).toBe('a');
	expect(list.tail.prev.value).toBe(1);
	expect(list.length).toBe(2);
})

test('add before, list is empty', () => {
	const list = new LinkedList();
	
	expect(() => list.insertBefore(1, 1)).toThrow();
});

test('add before, one item in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBefore(1, 2);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(2);
	expect(list.head.next.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.tail.prev.value).toBe(2);
})

test('add before, two item in list, add in the middle', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBack(3);
	list.insertBefore(3, 2);
	
	expect(list.length).toBe(3);
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(2);
	expect(list.head.next.next.value).toBe(3);
	expect(list.tail.value).toBe(3);
	expect(list.tail.prev.value).toBe(2);
	expect(list.tail.prev.prev.value).toBe(1);
});

test('add before, item does not exist', () => {
	const list = new LinkedList();
	list.insertBack(1);
	list.insertBefore(2, 3);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('add after, list is empty', () => {
	const list = new LinkedList();
	
	expect(() => list.insertAfter(1, 1)).toThrow();
});

test('add after, one item in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertAfter(1, 2);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(2);
	expect(list.tail.value).toBe(2);
	expect(list.tail.prev.value).toBe(1);
});

test('add after, two item in list, add in the middle', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBack(3);
	list.insertAfter(1, 2);
	
	expect(list.length).toBe(3);
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(2);
	expect(list.head.next.next.value).toBe(3);
	expect(list.tail.value).toBe(3);
	expect(list.tail.prev.value).toBe(2);
	expect(list.tail.prev.prev.value).toBe(1);
});

test('add after, item does not exist', () => {
	const list = new LinkedList();
	list.insertBack(1);
	list.insertAfter(2, 3);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('add at, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.insertAt(0, 1)).toThrow();
});

test('add at, one item in list, index out of range', () => {
	const list = new LinkedList();
	list.insertFront(1);

	expect(() => list.insertAt(2, 2)).toThrow();
});

test('add at, one item in list, add at head', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertAt(0, 2);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(2);
	expect(list.head.next.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.tail.prev.value).toBe(2);
});

test('add at, one item in list, add at tail', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertAt(1, 2);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(2);
	expect(list.tail.value).toBe(2);
	expect(list.tail.prev.value).toBe(1);
});

test('add at, two items in list, add at middle', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBack(3)
	list.insertAt(1, 2);

	expect(list.length).toBe(3);
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(2);
	expect(list.head.next.next.value).toBe(3);
	expect(list.tail.value).toBe(3);
	expect(list.tail.prev.value).toBe(2);
	expect(list.tail.prev.prev.value).toBe(1);
});

test('remove front, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.removeFront()).toThrow();
});

test('remove front, one item in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.removeFront();

	expect(list.length).toBe(0);
	expect(list.head).toBeNull;
	expect(list.tail).toBeNull;
});

test('remove front, two items in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.removeFront();

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('remove front, three items in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.insertBack(2);
	list.removeFront();

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(2);
	expect(list.tail.value).toBe(2);
	expect(list.tail.prev.value).toBe(1);
});

test('remove back, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.removeBack()).toThrow();
});

test('remove back, one item in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.removeBack();

	expect(list.length).toBe(0);
	expect(list.head).toBeNull;
	expect(list.tail).toBeNull;
});

test('remove back, two items in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.removeBack();

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(2);
	expect(list.tail.value).toBe(2);
});

test('remove back, three items in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.insertBack(2);
	list.removeBack();

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(2);
	expect(list.head.next.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.tail.prev.value).toBe(2);
});

test('remove at, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.removeBack()).toThrow();
});

test('remove at, one item in list, index out of range', () => {
	const list = new LinkedList();
	list.insertFront(1);

	expect(() => list.removeAt(2, 2)).toThrow();
});

test('remove at, one item in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.removeAt(0);

	expect(list.length).toBe(0);
	expect(list.head).toBeNull;
	expect(list.tail).toBeNull;
});

test('remove at, two items in list, removing head', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBack(2)
	list.removeAt(0);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(2);
	expect(list.tail.value).toBe(2);
});

test('remove at, two items in list, removing tail', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBack(2)
	list.removeAt(1);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('remove at, three items in list, removing middle', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBack(2)
	list.insertBack(3)
	list.removeAt(1);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(1);
	expect(list.head.next.value).toBe(3);
	expect(list.tail.value).toBe(3);
	expect(list.tail.prev.value).toBe(1);
});

test('remove first of, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.removeFirstOf(1)).toThrow;
});	

test('remove first of, value not in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.removeFirstOf(2);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('remove first of, one item in list, value is in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.removeFirstOf(1);

	expect(list.length).toBe(0);
	expect(list.head).toBeNull;
	expect(list.tail).toBeNull;
});

test('remove first of, two item in list, removing head', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.removeFirstOf(2);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('remove first of, two item in list, removing tail', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.removeFirstOf(1);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(2);
	expect(list.tail.value).toBe(2);
});

test('remove first of, three item in list, removing middle', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.insertFront(3);
	list.removeFirstOf(2);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(3);
	expect(list.head.next.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.tail.prev.value).toBe(3);
});

test('remove first of, three item in list, duplicate values', () => {
	const list = new LinkedList();
	list.insertFront(3);
	list.insertFront(2);
	list.insertFront(3);
	list.removeFirstOf(3);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(2);
	expect(list.head.next.value).toBe(3);
	expect(list.tail.value).toBe(3);
	expect(list.tail.prev.value).toBe(2);
});

test('remove last of, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.removeLastOf(1)).toThrow;
});	

test('remove last of, value not in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.removeLastOf(2);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('remove last of, one item in list, value is in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.removeLastOf(1);

	expect(list.length).toBe(0);
	expect(list.head).toBeNull;
	expect(list.tail).toBeNull;
});

test('remove last of, two item in list, removing head', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.removeLastOf(2);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(1);
	expect(list.tail.value).toBe(1);
});

test('remove last of, two item in list, removing tail', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.removeLastOf(1);

	expect(list.length).toBe(1);
	expect(list.head.value).toBe(2);
	expect(list.tail.value).toBe(2);
});

test('remove last of, three item in list, removing middle', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.insertFront(3);
	list.removeLastOf(2);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(3);
	expect(list.head.next.value).toBe(1);
	expect(list.tail.value).toBe(1);
	expect(list.tail.prev.value).toBe(3);
});

test('remove last of, three item in list, duplicate values', () => {
	const list = new LinkedList();
	list.insertFront(3);
	list.insertFront(2);
	list.insertFront(3);
	list.removeLastOf(3);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(3);
	expect(list.head.next.value).toBe(2);
	expect(list.tail.value).toBe(2);
	expect(list.tail.prev.value).toBe(3);
});

test('index of, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.indexOf(1)).toThrow;
});

test('index of, value not in list', () => {
	const list = new LinkedList();
	list.insertFront(2);
	const index = list.indexOf(1);

	expect(index).toBe(-1);
});

test('index of, value is in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	const index = list.indexOf(1);

	expect(index).toBe(0);
});

test('index of, value is in list, duplicate values', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.insertFront(1);
	list.insertFront(2);
	const index = list.indexOf(1);

	expect(index).toBe(1);
});

test('last index of, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.lastIndexOf(1)).toThrow;
});

test('last index of, value not in list', () => {
	const list = new LinkedList();
	list.insertFront(2);
	const index = list.lastIndexOf(1);

	expect(index).toBe(-1);
});

test('last index of, value is in list', () => {
	const list = new LinkedList();
	list.insertFront(1);
	const index = list.lastIndexOf(1);

	expect(index).toBe(0);
});

test('last index of, value is in list, duplicate values', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertFront(2);
	list.insertFront(1);
	list.insertFront(2);
	const index = list.lastIndexOf(1);

	expect(index).toBe(3);
});

test('contains, list is empty', () => {
	const list = new LinkedList();

	expect(() => list.contains(1)).toThrow();
});

test('contains, value not in list', () => {
	const list = new LinkedList();
	list.insertBack(1);

	expect(list.contains(2)).toBe(false);
});

test('contains, value is in list', () => {
	const list = new LinkedList();
	list.insertBack(1);

	expect(list.contains(1)).toBe(true);
});

test('is empty, list is empty', () => {
	const list = new LinkedList();

	expect(list.isEmpty()).toBe(true);
});

test('is empty, list is not empty', () => {
	const list = new LinkedList();
	list.insertFront(1);

	expect(list.isEmpty()).toBe(false);
});

test('duplicate nodes', () => {
	const list = new LinkedList();
	list.insertFront(1);
	list.insertBack(list.head);

	expect(list.length).toBe(2);
	expect(list.head.value).toBe(1);
	expect(list.head.next.prev.value).toBe(1);
	expect(list.head.next.value).toBe(list.head);
	expect(list.head.next.value.value).toBe(1);
	expect(list.tail.value).toBe(list.head);
});	
