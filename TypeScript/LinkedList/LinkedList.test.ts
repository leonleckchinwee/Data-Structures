import { LinkedList, Node } from './LinkedList';

describe('constructor', () => {
    test('node', () => {
        const node = new Node<number>(1);

        expect(node.value).toEqual(1);
        expect(node.next).toBeNull();
        expect(node.prev).toBeNull();
    });

    test('linked list', () => {
        const list = new LinkedList<number>();

        expect(list.first).toBeNull();
        expect(list.last).toBeNull();
        expect(list.length).toEqual(0);
    });
});

describe('insert', () => {
    test('first, list is empty, unknown types', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst('a');

        expect(list.first!.value).toEqual('a');
        expect(list.first!.next!.value).toEqual(1);
        expect(list.last!.value).toEqual(1);
        expect(list.last!.prev!.value).toEqual('a');
        expect(list.length).toEqual(2);
    });

    test('first, list is empty, one type', () => {
        const list = new LinkedList<number>();
        list.insertFirst(1);
        list.insertFirst(2);

        expect(list.first!.value).toEqual(2);
        expect(list.first!.next!.value).toEqual(1);
        expect(list.last!.value).toEqual(1);
        expect(list.last!.prev!.value).toEqual(2);
        expect(list.length).toEqual(2);
    });

    test('last, list is empty, unknown types', () => {
        const list = new LinkedList();
        list.insertLast(1);
        list.insertLast('a');

        expect(list.first!.value).toEqual(1);
        expect(list.first!.next!.value).toEqual('a');
        expect(list.last!.value).toEqual('a');
        expect(list.last!.prev!.value).toEqual(1);
        expect(list.length).toEqual(2);
    });

    test('last, list is empty, one type', () => {
        const list = new LinkedList<number>();
        list.insertLast(1);
        list.insertLast(2);

        expect(list.first!.value).toEqual(1);
        expect(list.first!.next!.value).toEqual(2);
        expect(list.last!.value).toEqual(2);
        expect(list.last!.prev!.value).toEqual(1);
        expect(list.length).toEqual(2);
    });

    test('before, list is empty', () => {
        const list = new LinkedList();

        expect(() => list.insertBefore(1, 1)).toThrow();
    });

    test('before, value does not exist', () => {
        const list = new LinkedList<number>();
        list.insertFirst(1);
        list.insertBefore(2, 3);

        expect(list.first!.value).toEqual(1);
        expect(list.first).toBe(list.last);
        expect(list.length).toEqual(1);
    });

    test('before, value is last', () => {
        const list = new LinkedList<number>();
        list.insertFirst(1);
        list.insertBefore(1, 2);

        expect(list.first!.value).toEqual(2);
        expect(list.first!.next!.value).toBe(1);
        expect(list.length).toEqual(2);
    });

    test('before, value is middle', () => {
        const list = new LinkedList<number>();
        list.insertFirst(1);
        list.insertLast(3);
        list.insertBefore(3, 2);

        expect(list.first!.value).toEqual(1);
        expect(list.first!.next!.value).toBe(2);
        expect(list.first!.next!.next!.value).toBe(3);
        expect(list.last!.value).toEqual(3);
        expect(list.last!.prev!.value).toBe(2);
        expect(list.last!.prev!.prev!.value).toBe(1);
        expect(list.length).toEqual(3);
    });

    test('after, list is empty', () => {
        const list = new LinkedList();

        expect(() => list.insertAfter(1, 1)).toThrow();
    });

    test('after, value does not exist', () => {
        const list = new LinkedList<number>();
        list.insertFirst(1);
        list.insertAfter(2, 3);

        expect(list.first!.value).toEqual(1);
        expect(list.first).toBe(list.last);
        expect(list.length).toEqual(1);
    });

    test('after, value is last', () => {
        const list = new LinkedList<number>();
        list.insertFirst(1);
        list.insertAfter(1, 2);

        expect(list.first!.value).toEqual(1);
        expect(list.first!.next!.value).toBe(2);
        expect(list.last!.value).toEqual(2);
        expect(list.last!.prev!.value).toEqual(1);
        expect(list.length).toEqual(2);
    });

    test('after, value is middle', () => {
        const list = new LinkedList<number>();
        list.insertFirst(1);
        list.insertLast(3);
        list.insertAfter(1, 2);

        expect(list.first!.value).toEqual(1);
        expect(list.first!.next!.value).toBe(2);
        expect(list.first!.next!.next!.value).toBe(3);
        expect(list.last!.value).toEqual(3);
        expect(list.last!.prev!.value).toBe(2);
        expect(list.last!.prev!.prev!.value).toBe(1);
        expect(list.length).toEqual(3);
    });

    test('add at, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.insertAt(0, 1)).toThrow();
    });
    
    test('add at, one item in list, index out of range', () => {
        const list = new LinkedList();
        list.insertFirst(1);
    
        expect(() => list.insertAt(2, 2)).toThrow();
    });
    
    test('add at, one item in list, add at first', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertAt(0, 2);
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(2);
        expect(list.first!.next!.value).toBe(1);
        expect(list.last!.value).toBe(1);
        expect(list.last!.prev!.value).toBe(2);
    });
    
    test('add at, one item in list, add at last', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertAt(1, 2);
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(1);
        expect(list.first!.next!.value).toBe(2);
        expect(list.last!.value).toBe(2);
        expect(list.last!.prev!.value).toBe(1);
    });
    
    test('add at, two items in list, add at middle', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertLast(3)
        list.insertAt(1, 2);
    
        expect(list.length).toBe(3);
        expect(list.first!.value).toBe(1);
        expect(list.first!.next!.value).toBe(2);
        expect(list.first!.next!.next!.value).toBe(3);
        expect(list.last!.value).toBe(3);
        expect(list.last!.prev!.value).toBe(2);
        expect(list.last!.prev!.prev!.value).toBe(1);
    });
});

describe('remove', () => {
    test('first, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.removeFirst()).toThrow();
    });
    
    test('first, one item in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.removeFirst();
    
        expect(list.length).toBe(0);
        expect(list.first).toBeNull;
        expect(list.first).toBeNull;
    });
    
    test('first, two items in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.removeFirst();
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(1);
        expect(list.last!.value).toBe(1);
    });
    
    test('first, three items in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.insertLast(2);
        list.removeFirst();
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(1);
        expect(list.first!.next!.value).toBe(2);
        expect(list.last!.value).toBe(2);
        expect(list.last!.prev!.value).toBe(1);
    });

    test('remove back, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.removeLast()).toThrow();
    });
    
    test('last, one item in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.removeLast();
    
        expect(list.length).toBe(0);
        expect(list.first).toBeNull;
        expect(list.last).toBeNull;
    });
    
    test('last, two items in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.removeLast();
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(2);
        expect(list.last!.value).toBe(2);
    });
    
    test('last, three items in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.insertLast(2);
        list.removeLast();
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(2);
        expect(list.first!.next!.value).toBe(1);
        expect(list.last!.value).toBe(1);
        expect(list.last!.prev!.value).toBe(2);
    });

    test('remove at, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.removeLast()).toThrow();
    });
    
    test('remove at, one item in list, index out of range', () => {
        const list = new LinkedList();
        list.insertFirst(1);
    
        expect(() => list.removeAt(2)).toThrow();
    });
    
    test('remove at, one item in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.removeAt(0);
    
        expect(list.length).toBe(0);
        expect(list.first).toBeNull;
        expect(list.last).toBeNull;
    });
    
    test('remove at, two items in list, removing first', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertLast(2)
        list.removeAt(0);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(2);
        expect(list.last!.value).toBe(2);
    });
    
    test('remove at, two items in list, removing last', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertLast(2)
        list.removeAt(1);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(1);
        expect(list.last!.value).toBe(1);
    });
    
    test('remove at, three items in list, removing middle', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertLast(2)
        list.insertLast(3)
        list.removeAt(1);
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(1);
        expect(list.first!.next!.value).toBe(3);
        expect(list.last!.value).toBe(3);
        expect(list.last!.prev!.value).toBe(1);
    });

    test('remove first of, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.removeFirstOf(1)).toThrow;
    });	
    
    test('remove first of, value not in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.removeFirstOf(2);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(1);
        expect(list.last!.value).toBe(1);
    });
    
    test('remove first of, one item in list, value is in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.removeFirstOf(1);
    
        expect(list.length).toBe(0);
        expect(list.first!).toBeNull;
        expect(list.last!).toBeNull;
    });
    
    test('remove first of, two item in list, removing first!', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.removeFirstOf(2);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(1);
        expect(list.last!.value).toBe(1);
    });
    
    test('remove first of, two item in list, removing last!', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.removeFirstOf(1);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(2);
        expect(list.last!.value).toBe(2);
    });
    
    test('remove first of, three item in list, removing middle', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.insertFirst(3);
        list.removeFirstOf(2);
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(3);
        expect(list.first!.next!.value).toBe(1);
        expect(list.last!.value).toBe(1);
        expect(list.last!.prev!.value).toBe(3);
    });
    
    test('remove first of, three item in list, duplicate values', () => {
        const list = new LinkedList();
        list.insertFirst(3);
        list.insertFirst(2);
        list.insertFirst(3);
        list.removeFirstOf(3);
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(2);
        expect(list.first!.next!.value).toBe(3);
        expect(list.last!.value).toBe(3);
        expect(list.last!.prev!.value).toBe(2);
    });
    
    test('remove last of, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.removeLastOf(1)).toThrow;
    });	
    
    test('remove last of, value not in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.removeLastOf(2);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(1);
        expect(list.last!.value).toBe(1);
    });
    
    test('remove last of, one item in list, value is in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.removeLastOf(1);
    
        expect(list.length).toBe(0);
        expect(list.first!).toBeNull;
        expect(list.last!).toBeNull;
    });
    
    test('remove last of, two item in list, removing first!', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.removeLastOf(2);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(1);
        expect(list.last!.value).toBe(1);
    });
    
    test('remove last of, two item in list, removing last!', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.removeLastOf(1);
    
        expect(list.length).toBe(1);
        expect(list.first!.value).toBe(2);
        expect(list.last!.value).toBe(2);
    });
    
    test('remove last of, three item in list, removing middle', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.insertFirst(3);
        list.removeLastOf(2);
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(3);
        expect(list.first!.next!.value).toBe(1);
        expect(list.last!.value).toBe(1);
        expect(list.last!.prev!.value).toBe(3);
    });
    
    test('remove last of, three item in list, duplicate values', () => {
        const list = new LinkedList();
        list.insertFirst(3);
        list.insertFirst(2);
        list.insertFirst(3);
        list.removeLastOf(3);
    
        expect(list.length).toBe(2);
        expect(list.first!.value).toBe(3);
        expect(list.first!.next!.value).toBe(2);
        expect(list.last!.value).toBe(2);
        expect(list.last!.prev!.value).toBe(3);
    });
});

describe('index of', () => {
    test('index of, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.indexOf(1)).toThrow;
    });
    
    test('index of, value not in list', () => {
        const list = new LinkedList();
        list.insertFirst(2);
        const index = list.indexOf(1);
    
        expect(index).toBe(-1);
    });
    
    test('index of, value is in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        const index = list.indexOf(1);
    
        expect(index).toBe(0);
    });
    
    test('index of, value is in list, duplicate values', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.insertFirst(1);
        list.insertFirst(2);
        const index = list.indexOf(1);
    
        expect(index).toBe(1);
    });
    
    test('last index of, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.lastIndexOf(1)).toThrow;
    });
    
    test('last index of, value not in list', () => {
        const list = new LinkedList();
        list.insertFirst(2);
        const index = list.lastIndexOf(1);
    
        expect(index).toBe(-1);
    });
    
    test('last index of, value is in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        const index = list.lastIndexOf(1);
    
        expect(index).toBe(0);
    });
    
    test('last index of, value is in list, duplicate values', () => {
        const list = new LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        list.insertFirst(1);
        list.insertFirst(2);
        const index = list.lastIndexOf(1);
    
        expect(index).toBe(3);
    });
});

describe('contains', () => {
    test('contains, list is empty', () => {
        const list = new LinkedList();
    
        expect(() => list.contains(1)).toThrow();
    });
    
    test('contains, value not in list', () => {
        const list = new LinkedList();
        list.insertFirst(1);
    
        expect(list.contains(2)).toBe(false);
    });
    
    test('contains, value is in list', () => {
        const list = new LinkedList();
        list.insertLast(1);
    
        expect(list.contains(1)).toBe(true);
    });
});
