namespace DSA.Stacks.Tests;

public class Stack_Tests_NonGeneric
{
    [TestFixture]
    public class StackTests_Constructor
    {
        [Test]
        public void Constructor_Empty()
        {
            MyStack<int> stack = new();

            Assert.That(stack, Has.Count.EqualTo(0));
            Assert.That(stack.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void Constructor_NegativeCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MyStack<int>(-1));
        }

        [Test]
        public void Constructor_Capacity()
        {
            MyStack<int> stack = new(10);

            Assert.That(stack, Has.Count.EqualTo(0));
            Assert.That(stack.MaxCount, Is.EqualTo(10));
        }

        [Test]
        public void Constructor_NullEnumerable()
        {
            Assert.Throws<InvalidOperationException>(() => new MyStack<int>(null!));
        }

        [Test]
        public void Constructor_EmptyEnumerator()
        {
            MyStack<int> stack = new([]);

            Assert.That(stack, Has.Count.EqualTo(0));
            Assert.That(stack.MaxCount, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_Enumerator()
        {
            MyStack<int> stack = new([1, 2, 3]);

            Assert.That(stack, Has.Count.EqualTo(3));
            Assert.That(stack.MaxCount, Is.EqualTo(3));
            Assert.That(stack.ToArray(), Is.EquivalentTo(new[] { 1, 2, 3}));
        }
    }

    [TestFixture]
    public class StackTests_Push
    {
        [Test]
        public void Push_Empty()
        {
            MyStack<int> stack = new(4);
            stack.Push(1);

            Assert.That(stack, Has.Count.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.ToArray(), Is.EquivalentTo(new[] { 1, 0, 0, 0 }));
        }

        [Test]
        public void Push_NeedResize()
        {
            MyStack<int> stack = new([ 1, 2, 3 ]);
            int previousCap = stack.MaxCount;
            int previousCount = stack.Count;

            stack.Push(4);

            Assert.That(previousCap, Is.EqualTo(3));
            Assert.That(previousCount, Is.EqualTo(previousCap));
            Assert.That(stack.MaxCount, Is.EqualTo(previousCap * 2));
            Assert.That(stack.Count, Is.EqualTo(previousCount + 1));
            Assert.That(stack.ToArray(), Is.EquivalentTo(new [] { 1, 2, 3, 4, 0, 0 }));
        }
    }

    [TestFixture]
    public class StackTests_Pop
    {
        [Test]
        public void Empty()
        {
            MyStack<int> stack = new();

            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Test]
        public void OneItem()
        {
            MyStack<int> stack = new(4);
            stack.Push(1);
            
            int count = stack.Count;
            int item = stack.Pop();

            Assert.That(count, Is.EqualTo(1));
            Assert.That(stack, Has.Count.EqualTo(0));
            Assert.That(item, Is.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.ToArray(), Is.Empty);
        }

        [Test]
        public void TwoItem()
        {
            MyStack<int> stack = new(4);
            stack.Push(1);
            stack.Push(2);

            int count1 = stack.Count;
            int item1 = stack.Pop();
            int count2 = stack.Count;
            int item2 = stack.Pop();

            Assert.That(count1, Is.EqualTo(2));
            Assert.That(item1, Is.EqualTo(2));
            Assert.That(count2, Is.EqualTo(1));
            Assert.That(item2, Is.EqualTo(1));
            Assert.That(stack, Has.Count.EqualTo(0));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.ToArray(), Is.Empty);
        }

        [Test]
        public void TwoItem_OneItemLeft()
        {
            MyStack<int> stack = new(4);
            stack.Push(1);
            stack.Push(2);

            int count = stack.Count;
            int item = stack.Pop();

            Assert.That(count, Is.EqualTo(2));
            Assert.That(item, Is.EqualTo(2));
            Assert.That(stack, Has.Count.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.ToArray(), Is.EquivalentTo(new [] { 1, 0, 0, 0 }));
        }

        [Test]
        public void TryPop_Empty()
        {
            MyStack<int> stack = new();
            bool removed = stack.TryPop(out var item);
            
            Assert.That(removed, Is.False);
            Assert.That(item, Is.EqualTo(0));
        }

        [Test]
        public void TryPop_OneItem()
        {
            MyStack<int> stack = new(4);
            stack.Push(1);
            
            int count = stack.Count;
            bool removed = stack.TryPop(out var item);

            Assert.That(count, Is.EqualTo(1));
            Assert.That(stack, Has.Count.EqualTo(0));
            Assert.That(removed, Is.True);
            Assert.That(item, Is.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.ToArray(), Is.Empty);
        }

        [Test]
        public void TryPop_TwoItem()
        {
            MyStack<int> stack = new(4);
            stack.Push(1);
            stack.Push(2);

            int count1 = stack.Count;
            bool removed1 = stack.TryPop(out var item1);
            int count2 = stack.Count;
            bool removed2 = stack.TryPop(out var item2);

            Assert.That(count1, Is.EqualTo(2));
            Assert.That(item1, Is.EqualTo(2));
            Assert.That(count2, Is.EqualTo(1));
            Assert.That(item2, Is.EqualTo(1));
            Assert.That(removed1, Is.True);
            Assert.That(removed2, Is.True);
            Assert.That(stack, Has.Count.EqualTo(0));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.ToArray(), Is.Empty);
        }

        [Test]
        public void TryPop_TwoItem_OneItemLeft()
        {
            MyStack<int> stack = new(4);
            stack.Push(1);
            stack.Push(2);

            int count = stack.Count;
            bool removed = stack.TryPop(out var item);

            Assert.That(count, Is.EqualTo(2));
            Assert.That(item, Is.EqualTo(2));
            Assert.That(removed, Is.True);
            Assert.That(stack, Has.Count.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(4));
            Assert.That(stack.ToArray(), Is.EquivalentTo(new [] { 1, 0, 0, 0 }));
        }
    }

    [TestFixture]
    public class StackTests_Peek
    {
        [Test]
        public void Empty()
        {
            MyStack<int> stack = new();

            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Test]
        public void OneItem()
        {
            MyStack<int> stack = new();
            stack.Push(1);

            Assert.That(stack, Has.Count.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(16));
            Assert.That(stack.Peek(), Is.EqualTo(1));
        }

        [Test]
        public void TwoItem_RemovedOne()
        {
            MyStack<int> stack = new();
            stack.Push(1);
            int item1 = stack.Peek();
            stack.Push(2);
            int item2 = stack.Peek();
            stack.Pop();
            int item3 = stack.Peek();

            Assert.That(stack, Has.Count.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(16));
            Assert.That(item1, Is.EqualTo(1));
            Assert.That(item2, Is.EqualTo(2));
            Assert.That(item3, Is.EqualTo(1));
        }

        [Test]
        public void Enumerables()
        {
            MyStack<int> stack = new([1, 2, 3]);

            Assert.That(stack, Has.Count.EqualTo(3));
            Assert.That(stack.MaxCount, Is.EqualTo(3));
            Assert.That(stack.Peek(), Is.EqualTo(3));
        }

        [Test]
        public void TryPeek_Empty()
        {
            MyStack<int> stack = new();
            bool removed = stack.TryPeek(out var item);
            
            Assert.That(removed, Is.False); 
            Assert.That(item, Is.EqualTo(0));
        }

        [Test]
        public void TryPeek_OneItem()
        {
            MyStack<int> stack = new();
            stack.Push(1);

            bool removed = stack.TryPeek(out var item);

            Assert.That(stack, Has.Count.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(16));
            Assert.That(removed, Is.True);
            Assert.That(item, Is.EqualTo(1));
        }

        [Test]
        public void TryPeek_TwoItem_RemovedOne()
        {
            MyStack<int> stack = new();
            stack.Push(1);
            bool removed1 = stack.TryPeek(out var item1);
            stack.Push(2);
            bool removed2 = stack.TryPeek(out var item2);
            stack.Pop();
            bool removed3 = stack.TryPeek(out var item3);

            Assert.That(stack, Has.Count.EqualTo(1));
            Assert.That(stack.MaxCount, Is.EqualTo(16));
            Assert.That(item1, Is.EqualTo(1));
            Assert.That(item2, Is.EqualTo(2));
            Assert.That(item3, Is.EqualTo(1));
            Assert.That(removed1, Is.True);
            Assert.That(removed2, Is.True);
            Assert.That(removed3, Is.True);
        }

        [Test]
        public void TryPeek_Enumerables()
        {
            MyStack<int> stack = new([1, 2, 3]);
            bool removed = stack.TryPeek(out var item);

            Assert.That(stack, Has.Count.EqualTo(3));
            Assert.That(stack.MaxCount, Is.EqualTo(3));
            Assert.That(removed, Is.True);
            Assert.That(item, Is.EqualTo(3));
        }
    }
}