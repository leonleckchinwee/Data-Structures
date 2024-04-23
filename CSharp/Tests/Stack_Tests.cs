namespace DSA.Stacks.Tests;

public class Stack_Tests
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
}