"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LinkedList = exports.Node = void 0;
var Node = /** @class */ (function () {
    function Node(value) {
        this.value = value;
        this.next = null;
        this.prev = null;
    }
    Node.prototype.equals = function (value) {
        return this.value == value;
    };
    return Node;
}());
exports.Node = Node;
var LinkedList = /** @class */ (function () {
    function LinkedList() {
        this.first = null;
        this.last = null;
        this.length = 0;
    }
    LinkedList.prototype.insertFirst = function (value) {
        var node = new Node(value);
        if (this.isEmpty()) {
            this.first = node;
            this.last = node;
        }
        else {
            node.next = this.first;
            this.first.prev = node;
            this.first = node;
        }
        ++this.length;
    };
    LinkedList.prototype.insertLast = function (value) {
        var node = new Node(value);
        if (this.isEmpty()) {
            this.first = node;
            this.last = node;
        }
        else {
            node.prev = this.last;
            this.last.next = node;
            this.last = node;
        }
        ++this.length;
    };
    LinkedList.prototype.insertBefore = function (existingValue, newValue) {
        if (this.isEmpty()) {
            throw "List is empty.";
        }
        var current = this.first;
        while (current != null) {
            if (current.value == existingValue) {
                var node = new Node(newValue);
                if (current == this.first) {
                    this.first.prev = node;
                    node.next = this.first;
                    this.first = node;
                    ++this.length;
                    return;
                }
                if (current.prev != null) {
                    current.prev.next = node;
                    current.prev = node;
                }
                node.prev = current.prev;
                node.next = current;
                ++this.length;
                return;
            }
            current = current.next;
        }
    };
    LinkedList.prototype.isEmpty = function () {
        return this.first == null;
    };
    return LinkedList;
}());
exports.LinkedList = LinkedList;
