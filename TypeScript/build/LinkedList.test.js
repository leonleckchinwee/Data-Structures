"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var LinkedList_1 = require("./LinkedList");
describe('constructor', function () {
    test('node', function () {
        var node = new LinkedList_1.Node(1);
        expect(node.value).toEqual(1);
        expect(node.next).toBeNull();
        expect(node.prev).toBeNull();
    });
    test('linked list', function () {
        var list = new LinkedList_1.LinkedList();
        expect(list.first).toBeNull();
        expect(list.last).toBeNull();
        expect(list.length).toEqual(0);
    });
});
describe('insert', function () {
    test('first, list is empty, unknown types', function () {
        var list = new LinkedList_1.LinkedList();
        list.insertFirst(1);
        list.insertFirst('a');
        expect(list.first.value).toEqual('a');
        expect(list.first.next.value).toEqual(1);
        expect(list.last.value).toEqual(1);
        expect(list.last.prev.value).toEqual('a');
        expect(list.length).toEqual(2);
    });
    test('first, list is empty, one type', function () {
        var list = new LinkedList_1.LinkedList();
        list.insertFirst(1);
        list.insertFirst(2);
        expect(list.first.value).toEqual(2);
        expect(list.first.next.value).toEqual(1);
        expect(list.last.value).toEqual(1);
        expect(list.last.prev.value).toEqual(2);
        expect(list.length).toEqual(2);
    });
    test('last, list is empty, unknown types', function () {
        var list = new LinkedList_1.LinkedList();
        list.insertLast(1);
        list.insertLast('a');
        expect(list.first.value).toEqual(1);
        expect(list.first.next.value).toEqual('a');
        expect(list.last.value).toEqual('a');
        expect(list.last.prev.value).toEqual(1);
        expect(list.length).toEqual(2);
    });
    test('last, list is empty, one type', function () {
        var list = new LinkedList_1.LinkedList();
        list.insertLast(1);
        list.insertLast(2);
        expect(list.first.value).toEqual(1);
        expect(list.first.next.value).toEqual(2);
        expect(list.last.value).toEqual(2);
        expect(list.last.prev.value).toEqual(1);
        expect(list.length).toEqual(2);
    });
    test('before, list is empty', function () {
        var list = new LinkedList_1.LinkedList();
        expect(function () { return list.insertBefore(1, 1); }).toThrow();
    });
    test('before, value does not exist', function () {
        var list = new LinkedList_1.LinkedList();
        list.insertFirst(1);
        list.insertBefore(2, 3);
        expect(list.first.value).toEqual(1);
        expect(list.first).toBe(list.last);
        expect(list.length).toEqual(1);
    });
    test('before, value is last', function () {
        var list = new LinkedList_1.LinkedList();
        list.insertFirst(1);
        list.insertBefore(1, 2);
        expect(list.first.value).toEqual(2);
        expect(list.first.next.value).toBe(1);
        expect(list.length).toEqual(2);
    });
    test('before, value is middle', function () {
        var list = new LinkedList_1.LinkedList();
        list.insertFirst(1);
        list.insertLast(3);
        list.insertBefore(3, 2);
        expect(list.first.value).toEqual(1);
        expect(list.first.next.value).toBe(2);
        expect(list.first.next.next.value).toBe(3);
        expect(list.last.value).toEqual(3);
        expect(list.last.prev.value).toBe(2);
        expect(list.last.prev.prev.value).toBe(1);
        expect(list.length).toEqual(3);
    });
});
