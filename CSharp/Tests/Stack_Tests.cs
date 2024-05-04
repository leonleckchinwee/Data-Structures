namespace DSA.Stacks.Tests;

public class Stack_Tests_NonGeneric
{
    [TestFixture]
    public class StackTests_Ctors
    {
        [Test]
        public void DefaultConstructor_StackSizeShouldBeZero()
        {
            // Arrange
            Stack stack = new Stack();

            // Act
            int count = stack.Count;

            // Assert
            Assert.Zero(count);
        }

        [Test]
        public void CapacityConstructor_StackSizeShouldBeZero()
        {
            // Arrange
            int capacity = 10;
            Stack stack = new Stack(capacity);

            // Act
            int count = stack.Count;

            // Assert
            Assert.Zero(count);
        }

        [Test]
        public void CapacityConstructorWithNegativeCapacity_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            int negativeCapacity = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Stack(negativeCapacity));
        }

        [Test]
        public void CapacityConstructorWithZeroCapacity_ShouldCreateEmptyStack()
        {
            // Arrange & Act
            Stack stack = new Stack(0);

            // Assert
            Assert.Zero(stack.Count);
        }

        [Test]
        public void CapacityConstructorWithLargeCapacity_ShouldCreateEmptyStack()
        {
            // Arrange
            int largeCapacity = 1000000;

            // Act
            Stack stack = new Stack(largeCapacity);

            // Assert
            Assert.Zero(stack.Count);
        }

        [Test]
        public void DefaultConstructor_ShouldInitializeEmptyStack()
        {
            // Arrange
            Stack stack;

            // Act
            stack = new Stack();

            // Assert
            Assert.That(stack.MaxCount, Is.EqualTo(100)); // Default capacity
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void ParameterizedConstructor_WithValidCapacity_ShouldInitializeStack()
        {
            // Arrange
            int capacity = 50;
            Stack stack;

            // Act
            stack = new Stack(capacity);

            // Assert
            Assert.That(stack.MaxCount, Is.EqualTo(capacity));
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void ParameterizedConstructor_WithNegativeCapacity_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            int capacity = -10;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Stack(capacity));
        }
    }

    [TestFixture]
    public class StackTests_Push
    {
        [Test]
        public void Push_WhenStackIsEmpty_ShouldAddItemToStack()
        {
            // Arrange
            Stack stack = new Stack();
            object item = 42;

            // Act
            stack.Push(item);

            // Assert
            Assert.That(stack.MaxCount, Is.EqualTo(100));
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(item));
        }

        [Test]
        public void Push_WhenStackIsNotEmpty_ShouldAddItemToTop()
        {
            // Arrange
            Stack stack = new Stack();
            object item1 = 10;
            object item2 = 20;

            // Act
            stack.Push(item1);
            stack.Push(item2);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(item2));
        }

        [Test]
        public void Push_WithNullItem_ShouldAddNullToStack()
        {
            // Arrange
            Stack stack = new Stack();
            object? nullItem = null;

            // Act
            stack.Push(nullItem);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.Null);
        }

        [Test]
        public void Push_WhenStackReachesCapacity_ShouldResizeInternalArray()
        {
            // Arrange
            Stack stack = new Stack(2);
            object item1 = 1;
            object item2 = 2;
            object item3 = 3;

            // Act
            stack.Push(item1);
            stack.Push(item2);
            stack.Push(item3); // This should trigger the internal array resize

            // Assert
            Assert.That(stack.Count, Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(item3));
        }
    }

    [TestFixture]
    public class StackTests_Pop
    {
         [Test]
        public void Pop_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Stack stack = new Stack();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Test]
        public void Pop_WhenStackIsNotEmpty_ShouldRemoveAndReturnTopItem()
        {
            // Arrange
            Stack stack = new Stack();
            object item1 = 10;
            object item2 = 20;
            stack.Push(item1);
            stack.Push(item2);

            // Act
            object? poppedItem = stack.Pop();

            // Assert
            Assert.That(poppedItem, Is.EqualTo(item2));
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(item1));
        }
    }

    [TestFixture]
    public class StackTest_Peek
    {
        [Test]
        public void Peek_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Stack stack = new Stack();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Test]
        public void Peek_WhenStackIsNotEmpty_ShouldReturnTopItemWithoutRemoving()
        {
            // Arrange
            Stack stack = new Stack();
            object item1 = 10;
            object item2 = 20;
            stack.Push(item1);
            stack.Push(item2);

            // Act
            object? peekedItem = stack.Peek();

            // Assert
            Assert.That(peekedItem, Is.EqualTo(item2));
            Assert.That(stack.Count, Is.EqualTo(2)); // Count should not change
        }
    }

    [TestFixture]
    public class StackTests_PeekAtIndex
    {
        [Test]
        public void PeekWithIndex_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Stack stack = new Stack();
            int index = 0;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.Peek(index));
        }

        [Test]
        public void PeekWithIndex_WithNegativeIndex_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            int invalidIndex = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => stack.Peek(invalidIndex));
        }

        [Test]
        public void PeekWithIndex_WithIndexGreaterThanOrEqualToCount_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            int invalidIndex = 2;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => stack.Peek(invalidIndex));
        }

        [Test]
        public void PeekWithIndex_WithValidIndex_ShouldReturnItemAtIndex()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            int index = 1;

            // Act
            object peekedItem = stack.Peek(index)!;

            // Assert
            Assert.That(peekedItem, Is.EqualTo(20));
            Assert.That(stack.Count, Is.EqualTo(3)); // Count should not change
        }

        [Test]
        public void PeekWithIndex_WithNullItem_ShouldReturnNullIfPresent()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(null);
            stack.Push(20);
            int index = 1;

            // Act
            object peekedItem = stack.Peek(index)!;

            // Assert
            Assert.That(peekedItem, Is.Null);
            Assert.That(stack.Count, Is.EqualTo(3)); // Count should not change
        }
    }

    [TestFixture]
    public class StackTests_Clear
    {
        [Test]
        public void Clear_WhenStackIsEmpty_ShouldNotThrowException()
        {
            // Arrange
            Stack stack = new Stack();

            // Act
            stack.Clear();

            // Assert
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Clear_WhenStackIsNotEmpty_ShouldRemoveAllItems()
        {
            // Arrange
            Stack stack = new Stack();
            object item1 = 10;
            object item2 = 20;
            stack.Push(item1);
            stack.Push(item2);

            // Act
            stack.Clear();

            // Assert
            Assert.That(stack.Count, Is.EqualTo(0));
            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Test]
        public void Clear_ShouldNotAffectStackCapacity()
        {
            // Arrange
            Stack stack = new Stack(10);
            object item = 42;
            stack.Push(item);

            // Act
            stack.Clear();

            // Assert
            Assert.That(stack.Count, Is.EqualTo(0));
            stack.Push(item); // Should not cause a resize
            Assert.That(stack.MaxCount, Is.EqualTo(10)); // Original capacity
        }
    }

    [TestFixture]
    public class StackTests_Resize
    {
        [Test]
        public void Push_WhenStackReachesCapacity_ShouldResizeInternalArray()
        {
            // Arrange
            int initialCapacity = 2;
            Stack stack = new Stack(initialCapacity);
            object item1 = 1;
            object item2 = 2;
            object item3 = 3;

            // Act
            stack.Push(item1);
            stack.Push(item2);
            stack.Push(item3); // This should trigger the internal array resize

            // Assert
            Assert.That(stack.Count, Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(item3));
            Assert.That(stack.MaxCount, Is.GreaterThan(initialCapacity));
        }

        [Test]
        public void Pop_TrimExcess()
        {
            // Arrange
            int initialCapacity = 2;
            Stack stack = new Stack(initialCapacity);
            object item1 = 1;
            object item2 = 2;
            object item3 = 3;

            stack.Push(item1);
            stack.Push(item2);
            stack.Push(item3); // Trigger resize

            // Act
            stack.Pop(); // Pop an item after resize

            // Assert
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.MaxCount, Is.EqualTo(initialCapacity)); // Capacity should shrink
        }

        [Test]
        public void Clear_AfterResize_ShouldNotAffectCapacity()
        {
            // Arrange
            int initialCapacity = 2;
            Stack stack = new Stack(initialCapacity);
            object item1 = 1;
            object item2 = 2;
            object item3 = 3;

            stack.Push(item1);
            stack.Push(item2);
            stack.Push(item3); // Trigger resize

            // Act
            stack.Clear();

            // Assert
            Assert.That(stack.Count, Is.EqualTo(0));
            Assert.That(stack.MaxCount, Is.GreaterThan(initialCapacity)); // Capacity should not shrink
        }
    }

    [TestFixture]
    public class StackTests_ToArray
    {
        [Test]
        public void ToArray_WhenStackIsEmpty_ShouldReturnEmptyArray()
        {
            // Arrange
            Stack stack = new Stack();

            // Act
            object[] array = stack.ToArray()!;

            // Assert
            Assert.That(array.Count, Is.EqualTo(100));
            for (int i = 0; i < 100; ++i)
            {
                Assert.That(array[i], Is.Null);
            }
        }

        [Test]
        public void ToArray_WhenStackIsNotEmpty_ShouldReturnArrayWithCorrectElements()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            // Act
            object[] array = stack.ToArray()!;
            object[] array2 = new object[100];

            array2[0] = 10;
            array2[1] = 20;
            array2[2] = 30;

            for (int i = 3; i < 100; ++i)
            {
                array2[i] = null!;
            }

            // Assert
            Assert.That(array.Length, Is.EqualTo(100));
            Assert.That(array, Is.EqualTo(array2));
        }
    }

    [TestFixture]
    public class StackTests_ToList
    {
        [Test]
        public void ToList_WhenStackIsEmpty_ShouldReturnEmptyList()
        {
            // Arrange
            Stack stack = new Stack();

            // Act
            List<object> list = stack.ToList()!;

            // Assert
            Assert.That(list.Count, Is.EqualTo(100));
            for (int i = 0; i < 100; ++i)
            {
                Assert.That(list[i], Is.Null);
            }
        }

        [Test]
        public void ToList_WhenStackIsNotEmpty_ShouldReturnListWithCorrectElements()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            // Act
            List<object> list = stack.ToList()!;
            List<object> list2 = new List<object>() { 10, 20, 30 };

            for (int i = 3; i < 100; ++i)
            {
                list2.Add(null!);
            }

            // Assert
            Assert.That(list.Count, Is.EqualTo(100));
            Assert.That(list, Is.EqualTo(list2));
        }
    }

    [TestFixture]
    public class StackTests_InsertAt
    {
        [TestFixture]
        public class StackInsertAtTests
        {
            [Test]
            public void InsertAt_WithNegativeIndex_ShouldThrowArgumentOutOfRangeException()
            {
                // Arrange
                Stack stack = new Stack();
                int invalidIndex = -1;
                object item = 42;

                // Act & Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => stack.InsertAt(invalidIndex, item));
            }

            [Test]
            public void InsertAt_WithIndexGreaterThanCount_ShouldThrowArgumentOutOfRangeException()
            {
                // Arrange
                Stack stack = new Stack();
                stack.Push(10);
                stack.Push(20);
                int invalidIndex = 3;
                object item = 30;

                // Act & Assert
                Assert.Throws<ArgumentOutOfRangeException>(() => stack.InsertAt(invalidIndex, item));
            }

            [Test]
            public void InsertAt_AtStart_ShouldInsertItemAtIndex0()
            {
                // Arrange
                Stack stack = new Stack();
                stack.Push(10);
                stack.Push(20);
                int index = 0;
                object item = 0;

                // Act
                stack.InsertAt(index, item);

                // Assert
                Assert.That(stack.Count, Is.EqualTo(3));
                Assert.That(stack.Peek(), Is.EqualTo(20));
                Assert.That(stack.ToArray()[0], Is.EqualTo(item));
            }

            [Test]
            public void InsertAt_InMiddle_ShouldInsertItemAtSpecifiedIndex()
            {
                // Arrange
                Stack stack = new Stack();
                stack.Push(10);
                stack.Push(20);
                stack.Push(30);
                int index = 1;
                object item = 15;

                // Act
                stack.InsertAt(index, item);

                // Assert
                Assert.That(stack.Count, Is.EqualTo(4));
                Assert.That(stack.Peek(), Is.EqualTo(30));
                Assert.That(stack.ToList()[1], Is.EqualTo(item));
                Assert.That(stack.ToArray()[2], Is.EqualTo(20));
            }

            [Test]
            public void InsertAt_AtEnd_ShouldInsertItemAtTopOfStack()
            {
                // Arrange
                Stack stack = new Stack();
                stack.Push(10);
                stack.Push(20);
                int index = 2;
                object item = 30;

                // Act
                stack.InsertAt(index, item);

                // Assert
                Assert.That(stack.Count, Is.EqualTo(3));
                Assert.That(stack.Peek(), Is.EqualTo(item));
                Assert.That(stack.ToArray()[1], Is.EqualTo(20));
                Assert.That(stack.ToArray()[0], Is.EqualTo(10));
            }
        }


    }

    [TestFixture]
    public class StackTests_Concat
    {
        [Test]
        public void Concat_WithEmptyStack_ShouldNotModifySourceStack()
        {
            // Arrange
            Stack sourceStack = new Stack();
            sourceStack.Push(10);
            sourceStack.Push(20);

            Stack emptyStack = new Stack();

            // Act
            sourceStack.Concat(emptyStack);

            // Assert
            Assert.That(sourceStack.Count, Is.EqualTo(2));
            Assert.That(sourceStack.Peek(), Is.EqualTo(20));
        }

        [Test]
        public void Concat_WithNonEmptyStack_ShouldConcatenateCorrectly()
        {
            // Arrange
            Stack sourceStack = new Stack(2);
            sourceStack.Push(10);
            sourceStack.Push(20);

            Stack otherStack = new Stack(2);
            otherStack.Push(30);
            otherStack.Push(40);

            // Act
            sourceStack.Concat(otherStack);

            // Assert
            Assert.That(sourceStack.Count, Is.EqualTo(4));
            Assert.That(sourceStack.Peek(), Is.EqualTo(40));
            Assert.That(sourceStack.ToArray(), Is.EqualTo(new object[] { 10, 20, 30, 40 }));
        }

        [Test]
        public void Concat_WithLargerStack_ShouldResizeInternalArray()
        {
            // Arrange
            Stack sourceStack = new Stack(2);
            sourceStack.Push(10);
            sourceStack.Push(20);

            Stack largerStack = new Stack(3);
            largerStack.Push(30);
            largerStack.Push(40);
            largerStack.Push(50);

            // Act
            sourceStack.Concat(largerStack);

            // Assert
            Assert.That(sourceStack.Count, Is.EqualTo(5));
            Assert.That(sourceStack.Peek(), Is.EqualTo(50));
            Assert.That(sourceStack.ToArray(), Is.EqualTo(new object[] { 10, 20, 30, 40, 50 }));
        }
    }

    [TestFixture]
    public class StackTests_RemoveAtIndex
    {
        [Test]
        public void RemoveAt_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Stack stack = new Stack();
            int index = 0;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.RemoveAt(index));
        }

        [Test]
        public void RemoveAt_WithNegativeIndex_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            int invalidIndex = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => stack.RemoveAt(invalidIndex));
        }

        [Test]
        public void RemoveAt_WithIndexGreaterThanCount_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            int invalidIndex = 2;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => stack.RemoveAt(invalidIndex));
        }

        [Test]
        public void RemoveAt_FromStart_ShouldRemoveItemAtIndex0()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            int index = 0;

            // Act
            object removedItem = stack.RemoveAt(index)!;

            // Assert
            Assert.That(removedItem, Is.EqualTo(10));
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(30));
        }

        [Test]
        public void RemoveAt_FromMiddle_ShouldRemoveItemAtSpecifiedIndex()
        {
            // Arrange
            Stack stack = new Stack(3);
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            int index = 1;

            // Act
            object removedItem = stack.RemoveAt(index)!;

            // Assert
            Assert.That(removedItem, Is.EqualTo(20));
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(30));
            Assert.That(stack.ToArray(), Is.EqualTo(new object[] { 10, 30, null! }));
        }

        [Test]
        public void RemoveAt_FromEnd_ShouldRemoveTopItem()
        {
            // Arrange
            Stack stack = new Stack(4);
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            int index = 2;

            // Act
            object removedItem = stack.RemoveAt(index)!;

            // Assert
            Assert.That(removedItem, Is.EqualTo(30));
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(20));
            Assert.That(stack.ToArray(), Is.EqualTo(new object[] { 10, 20 }));
        }
    }

    [TestFixture]
    public class StackTests_RemoveItem
    {
        [Test]
        public void Remove_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Stack stack = new Stack();
            object item = 42;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.Remove(item));
        }

        [Test]
        public void Remove_WithItemInStack_ShouldRemoveItem()
        {
            // Arrange
            Stack stack = new Stack(3);
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            object itemToRemove = 20;

            // Act
            stack.Remove(itemToRemove);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(30));
            Assert.That(stack.ToArray(), Is.EqualTo(new object[] { 10, 30, null! }));
        }

        [Test]
        public void Remove_WithItemNotInStack_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            object itemToRemove = 30;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => stack.Remove(itemToRemove));
        }

        [Test]
        public void Remove_WithNullItem_ShouldRemoveNullIfPresent()
        {
            // Arrange
            Stack stack = new Stack(3);
            stack.Push(10);
            stack.Push(null);
            stack.Push(20);

            // Act
            stack.Remove(null);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(20));
            Assert.That(stack.ToArray(), Is.EqualTo(new object[] { 10, 20, null! }));
        }

        [Test]
        public void Remove_WithDuplicateItems_ShouldRemoveFirstOccurrence()
        {
            // Arrange
            Stack stack = new Stack(3);
            stack.Push(10);
            stack.Push(20);
            stack.Push(10);
            object itemToRemove = 10;

            // Act
            stack.Remove(itemToRemove);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(10));
            Assert.That(stack.ToArray(), Is.EqualTo(new object[] { 20, 10, null! }));
        }
    }

    [TestFixture]
    public class StackTests_IndexOf
    {
        [Test]
        public void IndexOf_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Stack stack = new Stack();
            object item = 42;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.IndexOf(item));
        }

        [Test]
        public void IndexOf_WithItemInStack_ShouldReturnIndex()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            object itemToFind = 20;

            // Act
            int index = stack.IndexOf(itemToFind);

            // Assert
            Assert.That(index, Is.EqualTo(1));
        }

        [Test]
        public void IndexOf_WithItemNotInStack_ShouldReturnNegativeOne()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            object itemToFind = 30;

            // Act
            int index = stack.IndexOf(itemToFind);

            // Assert
            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_WithNullItem_ShouldReturnIndexIfNullExistsInStack()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(null);
            stack.Push(20);

            // Act
            int indexOfNull = stack.IndexOf(null);

            // Assert
            Assert.That(indexOfNull, Is.EqualTo(1));
        }

        [Test]
        public void IndexOf_WithDuplicateItems_ShouldReturnFirstIndex()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(10);
            object itemToFind = 10;

            // Act
            int index = stack.IndexOf(itemToFind);

            // Assert
            Assert.That(index, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class StackTests_LastIndexOf
    {
        [Test]
        public void LastIndexOf_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            Stack stack = new Stack();
            object item = 42;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => stack.LastIndexOf(item));
        }

        [Test]
        public void LastIndexOf_WithItemInStack_ShouldReturnLastIndex()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(10);
            object itemToFind = 10;

            // Act
            int lastIndex = stack.LastIndexOf(itemToFind);

            // Assert
            Assert.That(lastIndex, Is.EqualTo(2));
        }

        [Test]
        public void LastIndexOf_WithItemNotInStack_ShouldReturnNegativeOne()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            object itemToFind = 30;

            // Act
            int lastIndex = stack.LastIndexOf(itemToFind);

            // Assert
            Assert.That(lastIndex, Is.EqualTo(-1));
        }

        [Test]
        public void LastIndexOf_WithNullItem_ShouldReturnLastIndexIfNullExistsInStack()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(null);
            stack.Push(20);
            stack.Push(null);

            // Act
            int lastIndexOfNull = stack.LastIndexOf(null);

            // Assert
            Assert.That(lastIndexOfNull, Is.EqualTo(3));
        }

        [Test]
        public void LastIndexOf_WithDuplicateItems_ShouldReturnLastIndex()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(10);
            stack.Push(20);
            object itemToFind = 20;

            // Act
            int lastIndex = stack.LastIndexOf(itemToFind);

            // Assert
            Assert.That(lastIndex, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class StackTests_Contains
    {
        [Test]
        public void Contains_WhenStackIsEmpty_ShouldReturnFalse()
        {
            // Arrange
            Stack stack = new Stack();
            object item = 42;

            // Act
            bool contains = stack.Contains(item);

            // Assert
            Assert.That(contains, Is.False);
        }

        [Test]
        public void Contains_WithItemInStack_ShouldReturnTrue()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            object itemToFind = 20;

            // Act
            bool contains = stack.Contains(itemToFind);

            // Assert
            Assert.That(contains, Is.True);
        }

        [Test]
        public void Contains_WithItemNotInStack_ShouldReturnFalse()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            object itemToFind = 30;

            // Act
            bool contains = stack.Contains(itemToFind);

            // Assert
            Assert.That(contains, Is.False);
        }

        [Test]
        public void Contains_WithNullItem_ShouldReturnTrueIfNullExistsInStack()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(null);
            stack.Push(20);

            // Act
            bool containsNull = stack.Contains(null);

            // Assert
            Assert.That(containsNull, Is.True);
        }

        [Test]
        public void Contains_WithDuplicateItems_ShouldReturnTrue()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(10);
            object itemToFind = 10;

            // Act
            bool contains = stack.Contains(itemToFind);

            // Assert
            Assert.That(contains, Is.True);
        }

        [Test]
        public void Contains_WithManyItems_ShouldReturnTrue()
        {
            // Arrange
            Stack stack = new Stack();

            for (int i = 1; i <= 10; ++i)
            {
                stack.Push(i);
            }
            object itemToFind = 10;

            // Act
            bool contains = stack.Contains(itemToFind);

            // Assert
            Assert.That(contains, Is.True);
        }

    }

    [TestFixture]
    public class StackTests_Reverse
    {
        [Test]
        public void Reverse_WhenStackIsEmpty_ShouldThrowException()
        {
            // Arrange
            Stack stack = new Stack();

            // Assert
            Assert.Throws<InvalidOperationException>(() => stack.Reverse());
        }

        [Test]
        public void Reverse_WithSingleItemStack_ShouldNotChangeOrder()
        {
            // Arrange
            Stack stack = new Stack();
            stack.Push(10);

            // Act
            stack.Reverse();

            // Assert
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(10));
        }
        
        [Test]
        public void Reverse_WithMultipleItems_ShouldReverseOrder()
        {
            // Arrange
            Stack stack = new Stack(4);
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Push(40);

            // Act
            stack.Reverse();

            // Assert
            Assert.That(stack.Count, Is.EqualTo(4));
            Assert.That(stack.Peek(), Is.EqualTo(10));
            Assert.That(stack.ToArray(), Is.EqualTo(new object[] { 40, 30, 20, 10 }));
        }

        [Test]
        public void Reverse_WithNullItems_ShouldReverseOrderIncludingNulls()
        {
            // Arrange
            Stack stack = new Stack(4);
            stack.Push(10);
            stack.Push(null);
            stack.Push(20);
            stack.Push(null);

            // Act
            stack.Reverse();

            // Assert
            Assert.That(stack.Count, Is.EqualTo(4));
            Assert.That(stack.Peek(), Is.EqualTo(10));
            Assert.That(stack.ToArray(), Is.EqualTo(new object[] { null!, 20, null!, 10 }));
        }
    }
}

public class Stack_Tests_Generic
{
    [TestFixture]
    public class StackTests_Ctors
    {
        [Test]
        public void DefaultConstructor_StackSizeShouldBeZero()
        {
            // Arrange
            Stack<int> stack = new();

            // Act
            int count = stack.Count;

            // Assert
            Assert.Zero(count);
        }

        [Test]
        public void CapacityConstructor_StackSizeShouldBeZero()
        {
            // Arrange
            int capacity = 10;
            Stack<int> stack = new(capacity);

            // Act
            int count = stack.Count;

            // Assert
            Assert.Zero(count);
        }

        [Test]
        public void CapacityConstructorWithNegativeCapacity_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            int negativeCapacity = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Stack<int>(negativeCapacity));
        }

        [Test]
        public void CapacityConstructorWithZeroCapacity_ShouldCreateEmptyStack()
        {
            // Arrange & Act
            Stack<int> stack = new(0);

            // Assert
            Assert.Zero(stack.Count);
        }

        [Test]
        public void CapacityConstructorWithLargeCapacity_ShouldCreateEmptyStack()
        {
            // Arrange
            int largeCapacity = 1000000;

            // Act
            Stack<int> stack = new(largeCapacity);

            // Assert
            Assert.Zero(stack.Count);
        }

        [Test]
        public void DefaultConstructor_ShouldInitializeEmptyStack()
        {
            // Arrange
            Stack<int> stack;

            // Act
            stack = new();

            // Assert
            Assert.That(stack.MaxCount, Is.EqualTo(16)); // Default capacity
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void ParameterizedConstructor_WithValidCapacity_ShouldInitializeStack()
        {
            // Arrange
            int capacity = 50;
            Stack<int> stack;

            // Act
            stack = new(capacity);

            // Assert
            Assert.That(stack.MaxCount, Is.EqualTo(capacity));
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void ParameterizedConstructor_WithNegativeCapacity_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            int capacity = -10;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Stack<int>(capacity));
        }
    }

[TestFixture]
    public class StackTests_Push
    {
        [Test]
        public void Push_WhenStackIsEmpty_ShouldAddItemToStack()
        {
            // Arrange
            Stack<int> stack = new();
            int item = 42;

            // Act
            stack.Push(item);

            // Assert
            Assert.That(stack.MaxCount, Is.EqualTo(16));
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.EqualTo(item));
        }

        [Test]
        public void Push_WhenStackIsNotEmpty_ShouldAddItemToTop()
        {
            // Arrange
            Stack<int> stack = new();
            int item1 = 10;
            int item2 = 20;

            // Act
            stack.Push(item1);
            stack.Push(item2);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(2));
            Assert.That(stack.Peek(), Is.EqualTo(item2));
        }

        [Test]
        public void Push_WithNullItem_ShouldAddNullToStack()
        {
            // Arrange
            Stack<string> stack = new();
            string? nullItem = null;

            // Act
            stack.Push(nullItem!);

            // Assert
            Assert.That(stack.Count, Is.EqualTo(1));
            Assert.That(stack.Peek(), Is.Null);
        }

        [Test]
        public void Push_WhenStackReachesCapacity_ShouldResizeInternalArray()
        {
            // Arrange
            Stack<int> stack = new(2);
            int item1 = 1;
            int item2 = 2;
            int item3 = 3;

            // Act
            stack.Push(item1);
            stack.Push(item2);
            stack.Push(item3); // This should trigger the internal array resize

            // Assert
            Assert.That(stack.Count, Is.EqualTo(3));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.Peek(), Is.EqualTo(item3));
        }
    }




}