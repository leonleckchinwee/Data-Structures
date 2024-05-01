using System.Collections;

namespace DSA.Queues.Tests;

public class Queue_Tests
{
    [TestFixture]
    public class QueueTests_Constructors
    {
        [Test]
        public void DefaultConstructor_InitializesQueueWithDefaultCapacity()
        {
            // Arrange
            const int expectedDefaultCapacity = 16;

            // Act
            var queue = new Queue<int>();

            // Assert
            Assert.That(queue.MaxCount, Is.EqualTo(expectedDefaultCapacity));
            Assert.That(queue.Count, Is.EqualTo(0));
        }

        [TestCase(0)]
        [TestCase(10)]
        [TestCase(100)]
        public void CapacityConstructor_InitializesQueueWithGivenCapacity(int capacity)
        {
            // Act
            var queue = new Queue<int>(capacity);

            // Assert
            Assert.That(queue.MaxCount, Is.EqualTo(capacity));
            Assert.That(queue.Count, Is.EqualTo(0));
        }

        [Test]
        public void CapacityConstructor_ThrowsArgumentOutOfRangeException_WhenCapacityIsNegative()
        {
            // Arrange
            int capacity = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Queue<int>(capacity));
        }

        [Test]
        public void CollectionConstructor_InitializesQueueWithCollectionItems()
        {
            // Arrange
            List<int> collection = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            var queue = new Queue<int>(collection);

            // Assert
            Assert.That(queue.Count, Is.EqualTo(collection.Count));
            Assert.That(queue.MaxCount, Is.EqualTo(collection.Count));
            CollectionAssert.AreEqual(collection, queue.ToArray());
        }

        [Test]
        public void CollectionConstructor_ThrowsArgumentNullException_WhenCollectionIsNull()
        {
            // Arrange
            IEnumerable<int> collection = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Queue<int>(collection));
        }
    }

    [TestFixture]
    public class QueueTests_EnsureCapacity
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void EnsureCapacity_SetsCapacityToPowerOfTwoWhenRequestedCapacityIsLessThanCurrentCapacity(int requestedCapacity)
        {
            // Arrange
            var queue = new Queue<int>(2);
            int expectedCapacity = requestedCapacity == 3 ? 4 : Math.Max(requestedCapacity, 2);

            // Act
            queue.EnsureCapacity(requestedCapacity);

            // Assert
            Assert.That(queue.MaxCount, Is.EqualTo(expectedCapacity));
        }

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void EnsureCapacity_SetsCapacityToPowerOfTwoWhenRequestedCapacityIsMoreThanCurrentCapacity(int requestedCapacity)
        {
            // Arrange
            var queue = new Queue<int>(2);
            int expectedCapacity = 8;

            // Act
            queue.EnsureCapacity(requestedCapacity);

            // Assert
            Assert.That(queue.MaxCount, Is.EqualTo(expectedCapacity));
        }

        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        public void EnsureCapacity_SetsCapacityToNextPowerOfTwoWhenRequestedCapacityIsGreaterThanCurrentCapacity(int requestedCapacity)
        {
            // Arrange
            var queue = new Queue<int>(2);
            int expectedCapacity = 16;

            // Act
            queue.EnsureCapacity(requestedCapacity);

            // Assert
            Assert.That(queue.MaxCount, Is.EqualTo(expectedCapacity));
        }

        [Test]
        public void EnsureCapacity_ThrowsArgumentOutOfRangeException_WhenRequestedCapacityIsNegative()
        {
            // Arrange
            var queue = new Queue<int>();
            int requestedCapacity = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => queue.EnsureCapacity(requestedCapacity));
        }
    }

    [TestFixture]
    public class QueueTests_Enqueue
    {
        [Test]
        public void Enqueue_AddsItemToQueueWhenQueueIsEmpty()
        {
            // Arrange
            var queue = new Queue<int>();
            int item = 42;

            // Act
            queue.Enqueue(item);

            // Assert
            Assert.That(queue.Count, Is.EqualTo(1));
            Assert.That(queue.ToArray()[0], Is.EqualTo(item));
        }

        [Test]
        public void Enqueue_AddsItemToQueueWhenQueueIsNotFull()
        {
            // Arrange
            var queue = new Queue<int>(4);
            queue.Enqueue(1);
            queue.Enqueue(2);

            int item = 3;

            // Act
            queue.Enqueue(item);

            // Assert
            Assert.That(queue.Count, Is.EqualTo(3));
            Assert.That(queue.ToArray()[2], Is.EqualTo(item));
        }

        [Test]
        public void Enqueue_DoublesSizeWhenQueueIsFull()
        {
            // Arrange
            var queue = new Queue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);

            int item = 3;
            int expectedCapacity = 4;

            // Act
            queue.Enqueue(item);

            // Assert
            Assert.That(queue.Count, Is.EqualTo(3));
            Assert.That(queue.MaxCount, Is.EqualTo(expectedCapacity));
            Assert.That(queue.ToArray(), Is.EqualTo(new[] { 1, 2, 3, 0 }));
        }

        [Test]
        public void Enqueue_Strings()
        {
            var queue = new Queue<string>(4);
            queue.Enqueue("One");
            queue.Enqueue("Two");

            Assert.That(queue.Count, Is.EqualTo(2));
            Assert.That(queue.MaxCount, Is.EqualTo(4));
            Assert.That(queue.ToArray(), Is.EqualTo(new[] { "One", "Two", null, null }));
        }

        [Test]
        public void Enqueue_LargeSize()
        {
            var queue = new Queue<int>(100);

            for (int i = 0; i < 100; ++i)
            {
                queue.Enqueue(i);
            }
            queue.Enqueue(100);

            Assert.That(queue.Count, Is.EqualTo(101));
            Assert.That(queue.MaxCount, Is.EqualTo(128));
            Assert.That(queue.ToArray()[100], Is.EqualTo(100));
        }
    }

    [TestFixture]
    public class QueueTests_Dequeue
    {
        [Test]
        public void Dequeue_RemovesItemFromQueueWhenQueueIsNotEmpty()
        {
            // Arrange
            var queue = new Queue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act
            int dequeuedItem = queue.Dequeue();

            // Assert
            Assert.That(dequeuedItem, Is.EqualTo(1));
            Assert.That(queue.Count, Is.EqualTo(2));
            Assert.That(queue.ToArray(), Is.EqualTo(new[] { 0, 2, 3 }));
        }

        [Test]
        public void Dequeue_WrapsAroundWhenHeadReachesEnd()
        {
            // Arrange
            var queue = new Queue<int>(4);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Dequeue();
            queue.Dequeue();

            // Act
            int dequeuedItem = queue.Dequeue();

            // Assert
            Assert.That(dequeuedItem, Is.EqualTo(3));
            Assert.That(queue.Count, Is.EqualTo(1));
            Assert.That(queue.ToArray(), Is.EqualTo(new[] { 0, 0, 0, 4 }));
        }  

        [Test]
        public void Dequeue_WrapsAroundWhenHeadReachesEnd_Empty()
        {
            // Arrange
            var queue = new Queue<int>(4);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Dequeue();
            queue.Dequeue();

            // Act
            int dequeuedItem1 = queue.Dequeue();
            var dequeuedItem2 = queue.Dequeue();

            // Assert
            Assert.That(dequeuedItem1, Is.EqualTo(3));
            Assert.That(dequeuedItem2, Is.EqualTo(4));
            Assert.That(queue.Count, Is.EqualTo(0));
            Assert.That(queue.ToArray(), Is.EqualTo(new[] { 0, 0, 0, 0 }));
        } 

        [Test]
        public void Dequeue_ThrowsInvalidOperationException_WhenQueueIsEmpty()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
    }

    [TestFixture]
    public class QueueTests_CopyTo
    {
        [Test]
        public void CopyTo_CopiesQueueItemsToArray()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            int[] array = new int[3];

            // Act
            queue.CopyTo(array, 0);

            // Assert
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, array);
        }

        [Test]
        public void CopyTo_CopiesQueueItemsToArrayAtSpecifiedIndex()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            int[] array = new int[5];

            // Act
            queue.CopyTo(array, 2);

            // Assert
            CollectionAssert.AreEqual(new int[] { 0, 0, 1, 2, 3 }, array);
        }

        [Test]
        public void CopyTo_ThrowsArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Array array = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => queue.CopyTo(array, 0));
        }

        [Test]
        public void CopyTo_ThrowsArgumentOutOfRangeException_WhenIndexIsNegative()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            int[] array = new int[3];
            int index = -1;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => queue.CopyTo(array, index));
        }

        [Test]
        public void CopyTo_ThrowsArgumentOutOfRangeException_WhenIndexIsBeyondArrayLength()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            int[] array = new int[3];
            int index = 4;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => queue.CopyTo(array, index));
        }

        [Test]
        public void CopyTo_ThrowsArgumentException_WhenArrayIsNotLargeEnough()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            int[] array = new int[2];
            int index = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => queue.CopyTo(array, index));
        }

    }

    [TestFixture]
    public class QueueTests_Enumerators
    {
        [Test]
        public void GetEnumerator_ReturnsEnumeratorForQueue()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act
            IEnumerator<int> enumerator = queue.GetEnumerator();

            // Assert
            int[] expectedItems = new[] { 1, 2, 3 };
            int index = 0;
            while (enumerator.MoveNext())
            {
                Assert.That(enumerator.Current, Is.EqualTo(expectedItems[index++]));
            }
            Assert.That(index, Is.EqualTo(expectedItems.Length));
        }

        [Test]
        public void NonGenericGetEnumerator_ReturnsEnumeratorForQueue()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act
            IEnumerator enumerator = ((IEnumerable)queue).GetEnumerator();

            // Assert
            int[] expectedItems = new[] { 1, 2, 3 };
            int index = 0;
            while (enumerator.MoveNext())
            {
                Assert.That(enumerator.Current, Is.EqualTo(expectedItems[index++]));
            }
            Assert.That(index, Is.EqualTo(expectedItems.Length));
        }

        [Test]
        public void GetEnumerator_ReturnsDistinctEnumerators()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            // Act
            IEnumerator<int> enumerator1 = queue.GetEnumerator();
            IEnumerator<int> enumerator2 = queue.GetEnumerator();

            // Assert
            Assert.That(enumerator1, Is.Not.EqualTo(enumerator2));
        }

        [Test]
        public void GetEnumerator_EnumeratesQueueInCorrectOrder()
        {
            // Arrange
            var queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();
            queue.Enqueue(3);

            // Act
            IEnumerator<int> enumerator = queue.GetEnumerator();

            // Assert
            int[] expectedItems = new[] { 2, 3 };
            int index = 0;
            while (enumerator.MoveNext())
            {
                Assert.That(enumerator.Current, Is.EqualTo(expectedItems[index++]));
            }
            Assert.That(index, Is.EqualTo(expectedItems.Length));
        }
    }






}