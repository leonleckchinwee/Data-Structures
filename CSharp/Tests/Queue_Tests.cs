namespace DSA.Queues.Tests;

public class Queue_Tests
{
    [TestFixture]
    public class QueueTests_Constructor
    {
        [Test]
        public void Constructor_EmptyQueue()
        {
            MyQueue<int> queue = new();

            Assert.That(queue, !Is.Null);
            Assert.That(queue.Count, Is.EqualTo(0));
            Assert.That(queue.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void Constructor_SpecifiedCapacity()
        {
            MyQueue<int> queue = new(20);

            Assert.That(queue, !Is.Null);
            Assert.That(queue.Count, Is.EqualTo(0));
            Assert.That(queue.MaxCount, Is.EqualTo(20));
        }

        [Test]
        public void Constructor_SpecifiedCapacity_NegativeCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MyQueue<int>(-1));
        }

        [Test]
        public void Constructor_EnumeratorNull()
        {
            Assert.Throws<InvalidOperationException>(() => new MyQueue<int>(null!));
        }

        [Test]
        public void Constructor_Enumerator_Array()
        {
            MyQueue<int> queue = new([1, 2, 3]);

            Assert.That(queue, !Is.Null);
            Assert.That(queue.Count, Is.EqualTo(3));
            Assert.That(queue.MaxCount, Is.EqualTo(3));
            Assert.That(queue.ToArray(), Is.EquivalentTo(new int[] {1, 2, 3}));
        }

        [Test]
        public void Constructor_Enumerator_List()
        {
            MyQueue<int> queue = new(new List<int>() {1, 2, 3});

            Assert.That(queue, !Is.Null);
            Assert.That(queue.Count, Is.EqualTo(3));
            Assert.That(queue.MaxCount, Is.EqualTo(3));
            Assert.That(queue.ToArray(), Is.EquivalentTo(new int[] {1, 2, 3}));
        }
    }

    [TestFixture]
    public class QueueTests_Enqueue
    {
        [Test]
        public void Enqueue_Empty_EnqueueOne()
        {
            MyQueue<int> q= new(2);
            q.Enqueue(1);

            Assert.That(q.ToArray(), Is.EquivalentTo(new int[] { 1, 0 }));
        }

        [Test]
        public void Enqueue_NeedToResize()
        {
            MyQueue<int> q = new(2);
            q.Enqueue(1);
            q.Enqueue(2);

            int capacity1 = q.MaxCount;
            int[] array1 = q.ToArray()!;

            q.Enqueue(3);
            int capacity2 = q.MaxCount;
            int[] array2 = q.ToArray()!;

            Assert.That(capacity1, Is.EqualTo(2));
            Assert.That(capacity2, Is.EqualTo(4));
            Assert.That(array1, Is.EquivalentTo(new int[] { 1, 2 }));
            Assert.That(array2, Is.EquivalentTo(new int[] { 1, 2, 3, 0 }));
        }
    }

    [TestFixture]
    public class QueueTests_Dequeue
    {
        [Test]
        public void Dequeue_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q.Dequeue());
        }

        [Test]
        public void Dequeue_One()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            int item = q.Dequeue();

            Assert.That(item, Is.EqualTo(1));
            Assert.That(q.Count, Is.EqualTo(0));
            Assert.That(q.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void Dequeue_Two()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);

            int item = q.Dequeue();

            Assert.That(item, Is.EqualTo(1));
            Assert.That(q.Count, Is.EqualTo(1));
            Assert.That(q.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void Dequeue_Three()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);

            int item = q.Dequeue();
            q.Enqueue(3);

            int item2 = q.Dequeue();

            Assert.That(item, Is.EqualTo(1));
            Assert.That(item2, Is.EqualTo(2));
            Assert.That(q.Count, Is.EqualTo(1));
            Assert.That(q.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void TryDequeue_Empty()
        {
            MyQueue<int> q = new();
            bool removed = q.TryDequeue(out var item);

            Assert.That(removed, Is.False);
            Assert.That(item, Is.EqualTo(0));
        }

        [Test]
        public void TryDequeue_One()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            bool removed = q.TryDequeue(out var item);

            Assert.That(removed, Is.True);
            Assert.That(item, Is.EqualTo(1));
            Assert.That(q.Count, Is.EqualTo(0));
            Assert.That(q.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void TryDequeue_Two()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);

            bool removed = q.TryDequeue(out var item);

            Assert.That(removed, Is.True);
            Assert.That(item, Is.EqualTo(1));
            Assert.That(q.Count, Is.EqualTo(1));
            Assert.That(q.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void TryDequeue_Three()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);

            bool removed1 = q.TryDequeue(out var item);
            q.Enqueue(3);

            bool removed2 = q.TryDequeue(out var item2);

            Assert.That(removed1, Is.True);
            Assert.That(removed2, Is.True);
            Assert.That(item, Is.EqualTo(1));
            Assert.That(item2, Is.EqualTo(2));
            Assert.That(q.Count, Is.EqualTo(1));
            Assert.That(q.MaxCount, Is.EqualTo(16));
        }
    }

    [TestFixture]
    public class QueueTests_Peek
    {
        [Test]
        public void Peek_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q.Peek());
        }

        [Test]
        public void Peek_One()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            int item = q.Peek();

            Assert.That(q, Has.Count.EqualTo(1));
            Assert.That(item, Is.EqualTo(1));
        }

        [Test]
        public void Peek_Two()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);

            int item = q.Peek();

            Assert.That(q, Has.Count.EqualTo(2));
            Assert.That(item, Is.EqualTo(1));
        }

        [Test]
        public void TryPeek_Empty()
        {
            MyQueue<int> q = new();
            bool removed = q.TryPeek(out var item);
            
            Assert.That(removed, Is.False);
            Assert.That(item, Is.EqualTo(0));
        }

        [Test]
        public void TryPeek_One()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            bool removed = q.TryPeek(out var item);

            Assert.That(q, Has.Count.EqualTo(1));
            Assert.That(removed, Is.True);
            Assert.That(item, Is.EqualTo(1));
        }

        [Test]
        public void TryPeek_Two()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);

            bool removed = q.TryPeek(out var item);

            Assert.That(q, Has.Count.EqualTo(2));
            Assert.That(removed, Is.True);
            Assert.That(item, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class QueueTests_EnsureCapacity
    {
        [Test]
        public void EnsureCapacity_Negative()
        {
            MyQueue<int> q = new();

            Assert.Throws<ArgumentOutOfRangeException>(() => q.EnsureCapacity(-1));
        }
        
        [Test]
        public void EnsureCapacity_Zero()
        {
            MyQueue<int> q = new();
            q.EnsureCapacity(0);

            Assert.That(q, Has.Count.EqualTo(0));
            Assert.That(q.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void EnsureCapacity_InitialSmaller()
        {
            MyQueue<int> q = new(0);
            int initial = q.MaxCount;

            q.EnsureCapacity(1);
            int capacity = q.MaxCount;

            Assert.That(initial, Is.EqualTo(0));
            Assert.That(capacity, Is.EqualTo(1));
        }

        [Test]
        public void EnsureCapacity_CapacityTooLarge()
        {
            MyQueue<int> q = new();
            q.EnsureCapacity(Array.MaxLength);

            Assert.That(q.MaxCount, Is.EqualTo(Array.MaxLength));
        }

        [Test]
        public void EnsureCapacity_DoubleCapacity()
        {
            MyQueue<int> q = new();
            q.EnsureCapacity(20);

            Assert.That(q.MaxCount, Is.EqualTo(32));
        }
    }

    [TestFixture]
    public class QueueTests_TrimExcess
    {
        [Test]
        public void TrimExcess_Empty()
        {
            MyQueue<int> q = new(10);
            q.TrimExcess();

            Assert.That(q.MaxCount, Is.EqualTo(9));
        }

        [Test]
        public void TrimExcess_FullToOneFreeSpace()
        {
            MyQueue<int> q = new(2);
            q.Enqueue(1);
            q.Enqueue(2);

            int capacity1 = q.MaxCount;

            q.Dequeue();
            q.TrimExcess();

            int capacity2 = q.MaxCount;

            Assert.That(capacity1, Is.EqualTo(2));
            Assert.That(capacity2, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class QueueTests_GetSet
    {
        [Test]
        public void GetValue_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q.GetValue(1));
        }

        [Test]
        public void GetValue_IndexOutOfRange()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.Throws<IndexOutOfRangeException>(() => q.GetValue(1));
        }

        [Test]
        public void GetValue_CorrectIndex()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.That(q.GetValue(0), Is.EqualTo(1));
        }

        [Test]
        public void SetValue_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q.SetValue(1, 1));
        }

        [Test]
        public void SetValue_IndexOutOfRange()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.Throws<IndexOutOfRangeException>(() => q.SetValue(1, 1));
        }

        [Test]
        public void SetValue_CorrectIndex()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            q.SetValue(0, 2);

            Assert.That(q.Peek(), Is.EqualTo(2));
            Assert.That(q, Has.Count.EqualTo(1));
        }

        [Test]
        public void SquareBracket_GetValue_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => Console.Write(q[1]));
        }

        [Test]
        public void SquareBracket_GetValue_IndexOutOfRange()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.Throws<IndexOutOfRangeException>(() => Console.Write(q[1]));
        }

        [Test]
        public void SquareBracket_GetValue_CorrectIndex()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.That(q[0], Is.EqualTo(1));
        }

        [Test]
        public void SquareBracket_SetValue_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q[1] = 1);
        }

        [Test]
        public void SquareBracket_SetValue_IndexOutOfRange()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.Throws<IndexOutOfRangeException>(() => q[1] = 1);
        }

        [Test]
        public void SquareBracket_SetValue_CorrectIndex()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q[0] = 2;

            Assert.That(q.Peek(), Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class QueueTests_Contains
    {
        [Test]
        public void Contains_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q.Contains(1));
        }

        [Test]
        public void Contains_ReturnsFalse()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.That(q.Contains(2), Is.False);
        }

        [Test]
        public void Contains_ReturnsTrue()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.That(q.Contains(1), Is.True);
        }
    }

    [TestFixture]
    public class QueueTests_IndexOf
    {
        [Test]
        public void IndexOf_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q.IndexOf(1));
        }

        [Test]
        public void IndexOf_ValueNotFound()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.That(q.IndexOf(2), Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_ValueFound()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(1);

            Assert.That(q.IndexOf(1), Is.EqualTo(0));
        }

        [Test]
        public void LastIndexOf_Empty()
        {
            MyQueue<int> q = new();

            Assert.Throws<InvalidOperationException>(() => q.LastIndexOf(1));
        }

        [Test]
        public void LastIndexOf_ValueNotFound()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);

            Assert.That(q.LastIndexOf(2), Is.EqualTo(-1));
        }

        [Test]
        public void LastIndexOf_ValueFound()
        {
            MyQueue<int> q = new();
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(1);

            Assert.That(q.LastIndexOf(1), Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class QueueTests_Circular
    {
        [Test]
        public void Circular_One()
        {
            MyQueue<int> q = new(4);
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(4);

            int first = q.Dequeue();
            q.Enqueue(1);

            Assert.That(first, Is.EqualTo(1));
        }
    }
}