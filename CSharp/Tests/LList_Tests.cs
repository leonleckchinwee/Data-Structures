namespace DSA.LinkedLists.Tests;

public class LinkedList_Tests
{
    [TestFixture]
    public class LListTests_Node
    {
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(-100, ExpectedResult = -100)]
        [TestCase(int.MaxValue, ExpectedResult = int.MaxValue)]
        public int CreateNode_ContainingCorrectValue(int value)
        {
            // Arrange
            LLNode<int> node = new LLNode<int>(value);

            // Act & Assert
            Assert.That(node.Next, Is.Null);
            Assert.That(node.Previous, Is.Null);
            Assert.That(node.List, Is.Null);

            return node.Value;
        }
    }

    [TestFixture]
    public class LListTests_Constructor
    {
        [Test]
        public void Constructor_Empty()
        {
            LList<int> list = new();

            Assert.That(list, !Is.Null);
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
            Assert.That(list, Is.Empty);
        }

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(-100, ExpectedResult = -100)]
        [TestCase(int.MaxValue, ExpectedResult = int.MaxValue)]
        public int Constructor_FirstNode(int value)
        {
            LList<int> list = new(value);

            Assert.That(list, !Is.Null);
            Assert.That(list.First, !Is.Null);
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.First.Value, Is.EqualTo(value));
            Assert.That(list.Last.Value, Is.EqualTo(value));
            Assert.That(list, Has.Count.EqualTo(1));

            return list.First.Value;
        }

        [Test]
        public void Constructor_Enumerable_List()
        {
            List<int> list = [1, 2, 3, 4, 5];
            LList<int> list2 = new(list);
            
            Assert.That(list2, Has.Count.EqualTo(5));
            for (int i = 0; i < list2.Count; ++i)
            {
                Assert.That(list2[i], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void Constructor_Enumerable_Array()
        {
            int[] list = [1, 2, 3, 4, 5];
            LList<int> list2 = new(list);

            Assert.That(list2, Has.Count.EqualTo(5));
            Assert.That(list2, Has.Count.EqualTo(5));
            for (int i = 0; i < list2.Count; ++i)
            {
                Assert.That(list2[i], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void Constructor_Enumerable_Enumerable()
        {
            IEnumerable<int> list = [1, 2, 3, 4, 5];
            LList<int> list2 = new(list);

            Assert.That(list2, Has.Count.EqualTo(5));
            Assert.That(list2, Has.Count.EqualTo(5));
            for (int i = 0; i < list2.Count; ++i)
            {
                Assert.That(list2[i], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void Constructor_Enumerable_Null()
        {
            Assert.That(() => new LList<int>(null!), Throws.ArgumentNullException);
        }
    }

    [TestFixture]
    public class LListTests_AddFirst
    {
        [Test]
        public void AddFirst_EmptyList()
        {
            LList<int> list = new();
            list.AddFirst(1);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddFirst_WithFirstNode()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(1));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddFirst_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddFirst(2);
            list.AddFirst(3);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(3));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void AddFirst_Node_EmptyList()
        {
            LList<int> list = new();
            LLNode<int> node = new(1);
            list.AddFirst(node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddFirst_NodeAlreadyAssigned_ShouldThrowInvalidOperation()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddFirst(list.First!), Throws.InvalidOperationException);
        }

        [Test]
        public void AddFirst_Node_WithFirstNode()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);
            list.AddFirst(node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(1));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddFirst_Node_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddFirst(2);
            LLNode<int> node = new(2);
            list.AddFirst(node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }
    }

    [TestFixture]
    public class LListTest_AddLast
    {
         [Test]
        public void AddLast_EmptyList()
        {
            LList<int> list = new();
            list.AddLast(1);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddLast_WithFirstNode()
        {
            LList<int> list = new(1);
            list.AddLast(2);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddLast_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(3));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void AddLast_Node_EmptyList()
        {
            LList<int> list = new();
            LLNode<int> node = new(1);
            list.AddLast(node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list, Has.Count.EqualTo(1));
        }

        [Test]
        public void AddLast_NodeAlreadyAssigned_ShouldThrowInvalidOperation()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddLast(list.First!), Throws.InvalidOperationException);
        }

        [Test]
        public void AddLast_Node_WithFirstNode()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);
            list.AddLast(node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddLast_Node_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            LLNode<int> node = new(2);
            list.AddLast(node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }
    }

    [TestFixture]
    public class LListTest_AddBefore
    {
        [Test]
        public void AddBefore_EmptyList()
        {
            LList<int> list = new();
            Assert.That(() => list.AddBefore(list.First!, 1), Throws.InvalidOperationException);
        }

        [Test]
        public void AddBefore_BeforeFirstNode()
        {
            LList<int> list = new(1);
            list.AddBefore(list.First!, 2);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(1));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddBefore_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddBefore(list.First!, 2);
            list.AddBefore(node, 3);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(3));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void AddBefore_BeforeLastNode()
        {
            LList<int> list = new(1);
            list.AddFirst(2);
            list.AddBefore(list.Last!, 3);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(3));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(3));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void AddBefore_Node_EmptyList()
        {
            LList<int> list = new();
            Assert.That(() => list.AddBefore(list.First!, new LLNode<int>(2)), Throws.InvalidOperationException);
        }

        [Test]
        public void AddBefore_NodeAlreadyAssigned_ShouldThrowInvalidOperation()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddBefore(list.First!, list.Last!), Throws.InvalidOperationException);
        }

        [Test]
        public void AddBefore_ReferenceNodeDoesNotBelong_ShouldThrowInvalidOperation()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);

            Assert.That(() => list.AddBefore(node, new LLNode<int>(3)), Throws.InvalidOperationException);
        }

        [Test]
        public void AddBefore_NullNodes()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddBefore(null!, null!), Throws.ArgumentNullException);
        }

        [Test]
        public void AddBefore_Node_WithFirstNode()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);
            list.AddBefore(list.First!, node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(1));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddBefore_Node_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            LLNode<int> reference = list.AddFirst(2);
            LLNode<int> node = new(2);
            list.AddBefore(reference, node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }
    }

    [TestFixture]
    public class LListTests_AddAfter
    {
        [Test]
        public void AddAfter_EmptyList()
        {
            LList<int> list = new();
            Assert.That(() => list.AddAfter(list.First!, 1), Throws.InvalidOperationException);
        }

        [Test]
        public void AddAfter_AfterFirstNode()
        {
            LList<int> list = new(1);
            list.AddAfter(list.First!, 2);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddAfter_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddAfter(list.First!, 2);
            list.AddAfter(node, 3);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(3));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void AddAfter_AfterLastNode()
        {
            LList<int> list = new(1);
            list.AddFirst(2);
            list.AddAfter(list.Last!, 3);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(3));
            Assert.That(list.First.Next!.Value, Is.EqualTo(1));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void AddAfter_Node_EmptyList()
        {
            LList<int> list = new();
            Assert.That(() => list.AddAfter(list.First!, new LLNode<int>(2)), Throws.InvalidOperationException);
        }

        [Test]
        public void AddAfter_NodeAlreadyAssigned_ShouldThrowInvalidOperation()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddAfter(list.First!, list.Last!), Throws.InvalidOperationException);
        }

        [Test]
        public void AddAfter_ReferenceNodeDoesNotBelong_ShouldThrowInvalidOperation()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);

            Assert.That(() => list.AddAfter(node, new LLNode<int>(3)), Throws.InvalidOperationException);
        }

        [Test]
        public void AddAfter_NullNodes()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddAfter(null!, null!), Throws.ArgumentNullException);
        }

        [Test]
        public void AddAfter_Node_WithFirstNode()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);
            list.AddAfter(list.First!, node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddAfter_Node_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            LLNode<int> reference = list.AddFirst(2);
            LLNode<int> node = new(2);
            list.AddAfter(reference, node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }
    }

    [TestFixture]
    public class LListTests_AddAt
    {
        [Test]
        public void AddAt_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.AddAt(0, 1), Throws.InvalidOperationException);
        }

        [Test]
        public void AddAt_IndexOutOfRange()
        {
            LList<int> list = new(1);

            Assert.Throws<IndexOutOfRangeException>(() => list.AddAt(2, 2));
        }

        [Test]
        public void AddAt_OneNode()
        {
            LList<int> list = new(1);
            list.AddAt(0, 2);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
        }

        [Test]
        public void AddAt_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddFirst(3);
            list.AddAt(1, 2);

            Assert.That(list, Has.Count.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(3));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next.Next!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Previous.Previous!.Value, Is.EqualTo(3));
        }

        [Test]
        public void AddAt_MiddleOfList()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            list.AddAt(3, 10);

            Assert.That(list, Has.Count.EqualTo(6));
            Assert.That(list[3], Is.EqualTo(10));
            Assert.That(list[4], Is.EqualTo(4));
            Assert.That(list[5], Is.EqualTo(5));
            Assert.That(list.Last!.Previous!.Previous!.Value, Is.EqualTo(10));
        }

        [Test]
        public void AddAt_Node_EmptyList()
        {
            LList<int> list = new();
            Assert.That(() => list.AddAt(0, new LLNode<int>(2)), Throws.InvalidOperationException);
        }

        [Test]
        public void AddAt_NodeAlreadyAssigned_ShouldThrowInvalidOperation()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddAt(0, list.Last!), Throws.InvalidOperationException);
        }

        [Test]
        public void AddAt_NullNodes()
        {
            LList<int> list = new(1);

            Assert.That(() => list.AddAt(0, null!), Throws.ArgumentNullException);
        }

        [Test]
        public void AddAt_Node_IndexOutOfRange()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);

            Assert.Throws<IndexOutOfRangeException>(() => list.AddAt(2, node));
        }

        [Test]
        public void AddAt_Node_WithFirstNode()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(2);
            list.AddAt(0, node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(1));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(2));
        }

        [Test]
        public void AddAt_Node_WithFirstNode_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddFirst(2);
            LLNode<int> node = new(2);
            list.AddAt(0, node);

            Assert.That(list.First, !Is.Null);
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.Last, !Is.Null);
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Previous, Is.Null);
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Next, Is.Null);
            Assert.That(list, Has.Count.EqualTo(3));
        }
    
        [Test]
        public void AddAt_Node_MiddleOfList()
        {
             LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            list.AddAt(3, new LLNode<int>(10));

            Assert.That(list, Has.Count.EqualTo(6));
            Assert.That(list[3], Is.EqualTo(10));
            Assert.That(list[4], Is.EqualTo(4));
            Assert.That(list[5], Is.EqualTo(5));
            Assert.That(list.Last!.Previous!.Previous!.Value, Is.EqualTo(10));
        }
    }

    [TestFixture]
    public class LListTests_RemoveFirst
    {
        [Test]
        public void RemoveFirst_EmptyList()
        {
            LList<int> list = new();

            Assert.That(list.RemoveFirst(), Is.False);
        }

        [Test]
        public void RemoveFirst_OneNode()
        {
            LList<int> list = new(1);
            bool removed = list.RemoveFirst();

            Assert.That(removed, Is.True);
            Assert.That(list.IsEmpty(), Is.True);
            Assert.That(list, Has.Count.EqualTo(0));
        }

        [Test]
        public void RemoveFirst_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            bool removed = list.RemoveFirst();

            Assert.That(removed, Is.True);
            Assert.That(list.First, Is.SameAs(list.Last));
            Assert.That(list.First.Value, Is.EqualTo(1));
            Assert.That(list, Has.Count.EqualTo(1));
        }
    }

    [TestFixture]
    public class LListTests_RemoveLast
    {
        [Test]
        public void RemoveLast_EmptyList()
        {
            LList<int> list = new();

            Assert.That(list.RemoveLast(), Is.False);
        }

         [Test]
        public void RemoveLast_OneNode()
        {
            LList<int> list = new(1);
            bool removed = list.RemoveLast();

            Assert.That(removed, Is.True);
            Assert.That(list.IsEmpty(), Is.True);
            Assert.That(list, Has.Count.EqualTo(0));
        }

        [Test]
        public void RemoveLast_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            bool removed = list.RemoveLast();

            Assert.That(removed, Is.True);
            Assert.That(list.First, Is.SameAs(list.Last));
            Assert.That(list.First.Value, Is.EqualTo(2));
            Assert.That(list, Has.Count.EqualTo(1));
        }
    }

    [TestFixture]
    public class LListTests_Remove
    {
        [Test]
        public void Remove_Value_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.Remove(1), Throws.InvalidOperationException);
        }

        [Test]
        public void Remove_Value_OnlyOneNode()
        {
            LList<int> list = new(1);
            bool removed = list.Remove(1);

            Assert.That(removed, Is.True);
            Assert.That(list.IsEmpty(), Is.True);
            Assert.That(list.First, Is.Null);
            Assert.That(list, Has.Count.EqualTo(0));
        }   

        [Test]
        public void Remove_Value_LastNode()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);

            bool removed = list.Remove(3);

            Assert.That(removed, Is.True);
            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
        }

        [Test]
        public void Remove_Value_MiddleNode()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            bool removed = list.Remove(3);

            Assert.That(removed, Is.True);
            Assert.That(list, Has.Count.EqualTo(4));
            Assert.That(list[0], Is.EqualTo(1));
            Assert.That(list[1], Is.EqualTo(2));
            Assert.That(list[2], Is.EqualTo(4));
            Assert.That(list[3], Is.EqualTo(5));
            Assert.That(list.First!.Next!.Next!.Value, Is.EqualTo(4));
            Assert.That(list.Last!.Previous!.Previous!.Value, Is.EqualTo(2));
        }

        [Test]
        public void Remove_Node_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.Remove(new LLNode<int>(1)), Throws.InvalidOperationException);
        }

        [Test]
        public void Remove_Node_NullNode()
        {
            LList<int> list = new(1);

            Assert.That(() => list.Remove(null!), Throws.ArgumentNullException);
        }

        [Test]
        public void Remove_Node_NodeDoesNotBelong()
        {
            LList<int> list = new(1);

            Assert.That(() => list.Remove(new LLNode<int>(1)), Throws.InvalidOperationException);
        }

        [Test]
        public void Remove_Node_OnlyOneNode()
        {
            LList<int> list = new(1);
            bool removed = list.Remove(list.First!);

            Assert.That(removed, Is.True);
            Assert.That(list.IsEmpty(), Is.True);
            Assert.That(list.First, Is.Null);
            Assert.That(list, Has.Count.EqualTo(0));
        } 

        [Test]
        public void Remove_Node_LastNode()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            LLNode<int> node = list.AddLast(3);

            bool removed = list.Remove(node);

            Assert.That(removed, Is.True);
            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
        }

        [Test]
        public void Remove_Node_MiddleNode()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            LLNode<int> node = list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            bool removed = list.Remove(node);

            Assert.That(removed, Is.True);
            Assert.That(list, Has.Count.EqualTo(4));
            Assert.That(list[0], Is.EqualTo(1));
            Assert.That(list[1], Is.EqualTo(2));
            Assert.That(list[2], Is.EqualTo(4));
            Assert.That(list[3], Is.EqualTo(5));
            Assert.That(list.First!.Next!.Next!.Value, Is.EqualTo(4));
            Assert.That(list.Last!.Previous!.Previous!.Value, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class LListTests_RemoveAt
    {
        [Test]
        public void RemoveAt_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.RemoveAt(0), Throws.InvalidOperationException);
        }

        [Test]
        public void RemoveAt_IndexOutOfRange()
        {
            LList<int> list = new(1);

            Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(10));
        }

        [Test]
        public void RemoveAt_OnlyOneNode()
        {
            LList<int> list = new(1);
            bool removed = list.RemoveAt(0);

            Assert.That(removed, Is.True);
            Assert.That(list.IsEmpty(), Is.True);
            Assert.That(list.First, Is.Null);
            Assert.That(list, Has.Count.EqualTo(0));
        } 

        [Test]
        public void RemoveAt_LastNode()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);

            bool removed = list.RemoveAt(2);

            Assert.That(removed, Is.True);
            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(1));
        }

        [Test]
        public void RemoveAt_MiddleNode()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            bool removed = list.RemoveAt(2);

            Assert.That(removed, Is.True);
            Assert.That(list, Has.Count.EqualTo(4));
            Assert.That(list[0], Is.EqualTo(1));
            Assert.That(list[1], Is.EqualTo(2));
            Assert.That(list[2], Is.EqualTo(4));
            Assert.That(list[3], Is.EqualTo(5));
            Assert.That(list.First!.Next!.Next!.Value, Is.EqualTo(4));
            Assert.That(list.Last!.Previous!.Previous!.Value, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class LListTest_Clear
    {
        [Test]
        public void Clear_EmptyList()
        {
            LList<int> list = new();
            list.Clear();
            
            Assert.That(list, !Is.Null);
            Assert.That(list, Has.Count.EqualTo(0));
        }

        [Test]
        public void Clear_CorrectClearsAllNodes()
        {
            LList<int> list = new();
            LLNode<int> node1 = list.AddLast(1);
            LLNode<int> node2 = list.AddLast(2);

            list.Clear();

            Assert.That(list, Has.Count.EqualTo(0));
            Assert.That(list.IsEmpty(), Is.True);
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
            Assert.That(node1.List, !Is.EqualTo(list));
            Assert.That(node2.List, !Is.EqualTo(list));
            Assert.That(node1.Value, Is.EqualTo(1));
            Assert.That(node2.Value, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class LListTests_Enumerator
    {
        [Test]
        public void Enumerator_CorrectTraversal()
        {
            LList<int> list = new();
            list.AddFirst(1);
            list.AddFirst(1);
            list.AddFirst(1);

            Assert.That(list, Has.Count.EqualTo(3));

            foreach (int item in list)
            {
                Assert.That(item, !Is.Null);
                Assert.That(item, Is.EqualTo(1));
            }
        }

        [Test]
        public void Enumerator_IndexOutOfRange_ShouldThrowIndexOutOfRange()
        {
            LList<int> list = new();
            for (int i = 5; i >= 0; --i)
            {
                list.AddFirst(i);
            }

            Assert.Throws<IndexOutOfRangeException>(() => list.GetValue(10));
        }

        [Test]
        public void Enumerator_EmptyList_ShouldThrowInvalidOperation()
        {
            LList<int> list = new();

            Assert.That(() => list[1], Throws.InvalidOperationException);
        }

        [Test]
        public void Enumerator_Operator_CorrectTraversal()
        {
            LList<int> list = new();
            for (int i = 5; i >= 0; --i)
            {
                list.AddFirst(i);
            }

            Assert.That(list, Has.Count.EqualTo(6));

            for (int i = 0; i <= 5; ++i)
            {
                Assert.That(list[i], Is.EqualTo(i));
            }
        }

        [Test]
        public void Enumerator_Operator_SetValue_FirstIndex()
        {
            LList<int> list = new(1);
            list[0] = 10;

            Assert.That(list, Has.Count.EqualTo(1));
            Assert.That(list[0], Is.EqualTo(10));
        }

        [Test]
        public void Enumerator_Operator_SetValue_LastIndex()
        {
            LList<int> list = new(1);
            list.AddFirst(2);
            list[1] = 10;

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list[0], Is.EqualTo(2));
            Assert.That(list[1], Is.EqualTo(10));
        }
    
        [Test]
        public void Enumerator_GetNode_IndexOutOfRange_ShouldThrowIndexOutOfRange()
        {
            LList<int> list = new(1);

            Assert.Throws<IndexOutOfRangeException>(() => list.GetNode(10));
        }

        [Test]
        public void Enumerator_GetNode_EmptyList_ShouldThrowInvalidOperation()
        {
            LList<int> list = new();

            Assert.That(() => list.GetNode(1), Throws.InvalidOperationException);
        }

        [Test]
        public void Enumerator_GetNode_ReturnsCorrectNode()
        {
            LList<int> list = new(1);

            Assert.That(list.GetNode(0), !Is.Null);
            Assert.That(list.GetNode(0).Value, Is.EqualTo(1));
        }

        [Test]
        public void Enumerator_SetNode_EmptyList_ShouldThrowInvalidOperation()
        {
            LList<int> list = new();

            Assert.That(() => list.SetNode(0, new LLNode<int>(1)), Throws.InvalidOperationException);
        }

        [Test]
        public void Enumerator_SetNode_IndexOutOfRange_ShouldThrowIndexOutOfRange()
        {
            LList<int> list = new(1);

            Assert.Throws<IndexOutOfRangeException>(() => list.SetNode(10, new LLNode<int>(1)));
        }

        [Test]
        public void Enumerator_SetNode_NullNode_ShouldThrowArgumentNullException()
        {
            LList<int> list = new(1);

            Assert.That(() => list.SetNode(0, null!), Throws.ArgumentNullException);
        }

        [Test]
        public void Enumerator_SetNode_SetCorrectNode()
        {
            LList<int> list = new(1);
            list.SetNode(0, new LLNode<int>(10));

            Assert.That(list, Has.Count.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

        [Test]
        public void Enumerator_SetNode_MultipleNode_UpdatesCorrectNodes()
        {
            LList<int> list = new(1);
            list.AddFirst(2);
            list.AddFirst(3);

            list.SetNode(0, new LLNode<int>(10));

            Assert.That(list, Has.Count.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next.Next!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
        }

        [Test]
        public void Enumerator_SetNode_Duplicates_ShouldStillSet()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddFirst(2);

            list.SetNode(1, node);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list.Last!.Value, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class LListTests_IndexOf
    {
        [Test]
        public void IndexOf_EmptyList_ShouldThrowInvalidOperation()
        {
            LList<int> list = new();

            Assert.That(() => list.IndexOf(1), Throws.InvalidOperationException);
        }

        [Test]
        public void IndexOf_ReturnsCorrectIndex()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            int index = list.IndexOf(1);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(index, Is.EqualTo(1));
        }

        [Test]
        public void IndexOf_InvalidIndex_ReturnsNegativeOne()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            int index = list.IndexOf(3);

            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_MultipleValues_ReturnsCorrectIndex()
        {
            LList<int> list = new();
            list.AddFirst(2);
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);

            int index = list.IndexOf(2);

            Assert.That(index, Is.EqualTo(1));
        }

        [Test]
        public void LastIndexOf_EmptyList_ShouldThrowInvalidOperation()
        {
            LList<int> list = new();

            Assert.That(() => list.LastIndexOf(1), Throws.InvalidOperationException);
        }

        [Test]
        public void LastIndexOf_ReturnsCorrectIndex()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            int index = list.LastIndexOf(1);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(index, Is.EqualTo(1));
        }

        [Test]
        public void LastIndexOf_InvalidIndex_ReturnsNegativeOne()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            int index = list.LastIndexOf(3);

            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void LastIndexOf_MultipleValues_ReturnsCorrectIndex()
        {
            LList<int> list = new();
            list.AddFirst(2);
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);

            int index = list.LastIndexOf(2);

            Assert.That(index, Is.EqualTo(3));
        }
    
        [Test]
        public void IndexOf_Node_EmptyList_ShouldThrowInvalidOperation()
        {
            LList<int> list = new();

            Assert.That(() => list.IndexOf(new LLNode<int>(1)), Throws.InvalidOperationException);
        }

        [Test]
        public void IndexOf_Node_ReturnsCorrectIndex()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddFirst(2);

            int index = list.IndexOf(node);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(index, Is.EqualTo(0));
        }

        [Test]
        public void IndexOf_Node_InvalidIndex_ReturnsNegativeOne()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            int index = list.IndexOf(new LLNode<int>(3));

            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void IndexOf_Node_MultipleValues_ReturnsCorrectIndex()
        {
            LList<int> list = new();
            list.AddFirst(2);
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);

            int index = list.IndexOf(new LLNode<int>(2));

            Assert.That(index, Is.EqualTo(1));
        }
    
        [Test]
        public void LastIndexOf_Node_EmptyList_ShouldThrowInvalidOperation()
        {
            LList<int> list = new();

            Assert.That(() => list.LastIndexOf(new LLNode<int>(1)), Throws.InvalidOperationException);
        }

        [Test]
        public void LastIndexOf_Node_ReturnsCorrectIndex()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddFirst(2);

            int index = list.LastIndexOf(node);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(index, Is.EqualTo(0));
        }

        [Test]
        public void LastIndexOf_Node_InvalidIndex_ReturnsNegativeOne()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            int index = list.LastIndexOf(new LLNode<int>(3));

            Assert.That(index, Is.EqualTo(-1));
        }

        [Test]
        public void LastIndexOf_Node_MultipleValues_ReturnsCorrectIndex()
        {
            LList<int> list = new();
            list.AddFirst(2);
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);

            int index = list.LastIndexOf(new LLNode<int>(2));

            Assert.That(index, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class LListTests_Contains
    {
        [Test]
        public void Contains_Value_EmptyList_ReturnsFalse()
        {
            LList<int> list = new();

            Assert.That(list.Contains(1), Is.False);
        }

        [Test]
        public void Contains_Value_ValueExists_ReturnsTrue()
        {
            LList<int> list = new(1);

            Assert.That(list.Contains(1), Is.True);
        }

        [Test]
        public void Contains_Value_MultipleValuesExists_ReturnsTrue()
        {
            LList<int> list = new(1);
            list.AddFirst(1);
            list.AddFirst(2);

            Assert.That(list.Contains(1), Is.True);
        }

        [Test]
        public void Contains_Value_ValueDoesNotExists_ReturnsFalse()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            Assert.That(list.Contains(3), Is.False);
        }

        [Test]
        public void Contains_Node_EmptyList_ReturnsFalse()
        {
            LList<int> list = new();

            Assert.That(list.Contains(new LLNode<int>(1)), Is.False);
        }

        [Test]
        public void Contains_Node_ValueExistsButDoesNotBelong_ReturnsFalse()
        {
            LList<int> list = new(1);

            Assert.That(list.Contains(new LLNode<int>(1)), Is.False);
        }

        [Test]
        public void Contains_Node_NodeExistsAndBelongs_ReturnsTrue()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddFirst(2);

            Assert.That(list.Contains(node), Is.True);
            Assert.That(list.Contains(list.First!), Is.True);
        }

        [Test]
        public void Contains_Node_MultipleValuesExists_ReturnsTrue()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddFirst(1);
            list.AddFirst(2);

            Assert.That(list.Contains(node), Is.True);
        }

        [Test]
        public void Contains_Node_NodeExistsButDoesNotBelong_ReturnsFalse()
        {
            LList<int> list = new(1);
            LLNode<int> node = new(1);

            Assert.That(list.Contains(node), Is.False);
        }

        [Test]
        public void Contains_Node_ValueDoesNotExists_ReturnsFalse()
        {
            LList<int> list = new(1);
            list.AddFirst(2);

            Assert.That(list.Contains(new LLNode<int>(3)), Is.False);
        }
    }

    [TestFixture]
    public class LListTests_Find
    {
        [Test]
        public void Find_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.Find(1), Throws.InvalidOperationException);
        }

        [Test]
        public void Find_ValueExists_OnlyOneNode()
        {
            LList<int> list = new(1);
            LLNode<int>? node = list.Find(1);
            
            Assert.That(node, !Is.Null);
            Assert.That(node.Value, Is.EqualTo(1));
        }

        [Test]
        public void Find_ValueExists_MultipleNode_FindsFirstNode()
        {
            LList<int> list = new(1);
            list.AddFirst(1);
            LLNode<int>? node = list.Find(1);
            
            Assert.That(node, !Is.Null);
            Assert.That(node.Value, Is.EqualTo(1));
            Assert.That(node, Is.SameAs(list.First));
        }

        [Test]
        public void Find_ValueDoesNotExists_ReturnsNull()
        {
            LList<int> list = new(1);
            LLNode<int>? node = list.Find(2);

            Assert.That(node, Is.Null);
        }
        
    }

    [TestFixture]
    public class LListTests_Overrides
    {
        [Test]
        public void Equals_Empty()
        {
            LList<int> list1 = new();
            LList<int> list2 = new();

            Assert.That(list1.Equals(list2), Is.True);
        }

        [Test]
        public void Equals_SameLists_ReturnsTrue()
        {
            LList<int> list1 = new(1);
            list1.AddLast(2);
            LList<int> list2 = new(2);
            list2.AddFirst(1);

            Assert.That(list1.Equals(list2), Is.True);
        }

        [Test]
        public void Equals_DifferentLists_ReturnsFalse()
        {
            LList<int> list1 = new(1);
            list1.AddLast(2);
            LList<int> list2 = new(2);
            list2.AddLast(1);

            Assert.That(list1.Equals(list2), Is.False);
        }

        [Test]
        public void Equals_DifferentLengths_ReturnsFalse()
        {
            LList<int> list1 = new(1);
            list1.AddLast(2);
            LList<int> list2 = new(1);

            Assert.That(list1.Equals(list2), Is.False);
        }

        [Test]
        public void ToString_EmptyList()
        {
            LList<int> list = new();

            Assert.That(list.ToString(), Is.EqualTo("Empty list."));
        }

        [Test]
        public void ToString_CorrectString()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);

            Assert.That(list.ToString(), Is.EqualTo("1, 2, 3, "));
        }
    }

    [TestFixture]
    public class LListTests_ToEnumerable
    {
        [Test]
        public void ToArray_EmptyList()
        {
            LList<int> list = new();

            Assert.That(list.ToArray(), Is.Null);
        }

        [Test]
        public void ToArray_CorrectResult()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);

            int[] array = list.ToArray()!;

            Assert.That(array.Length, Is.EqualTo(3));
            Assert.That(array[0], Is.EqualTo(list.First!.Value));
            Assert.That(array[1], Is.EqualTo(list.First.Next!.Value));
            Assert.That(array[2], Is.EqualTo(list.First.Next.Next!.Value));
        }

        [Test]
        public void ToList_EmptyList()
        {
            LList<int> list = new();

            Assert.That(list.ToList(), Is.Null);
        }

        [Test]
        public void ToList_CorrectResult()
        {
            LList<int> list = new(1);
            list.AddLast(2);
            list.AddLast(3);

            List<int> list2 = list.ToList()!;

            Assert.That(list2.Count, Is.EqualTo(3));
            Assert.That(list2[0], Is.EqualTo(list.First!.Value));
            Assert.That(list2[1], Is.EqualTo(list.First.Next!.Value));
            Assert.That(list2[2], Is.EqualTo(list.First.Next.Next!.Value));
        }
    }

    [TestFixture]
    public class LListTests_Min
    {
        [Test]
        public void Min_Value_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.MinValue(), Throws.InvalidOperationException);
        }

        [Test]
        public void Min_Value_OnlyOneNode()
        {
            LList<int> list = new(1);

            Assert.That(list.MinValue(), Is.EqualTo(1));
        }

        [Test]
        public void Min_Value_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddLast(5);
            list.AddLast(-10);

            Assert.That(list.MinValue(), Is.EqualTo(-10));
        }

        [Test]
        public void Min_Node_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.MinNode(), Throws.InvalidOperationException);
        }

        [Test]
        public void Min_Node_OnlyOneNode()
        {
            LList<int> list = new(1);

            Assert.That(list.MinNode(), Is.EqualTo(list.First!));
        }

        [Test]
        public void Min_Node_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddLast(5);
            LLNode<int> node = list.AddLast(-10);

            Assert.That(list.MinNode(), Is.EqualTo(node));
        }
    }

    [TestFixture]
    public class LListTests_Max
    {
        [Test]
        public void Max_Value_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.MaxValue(), Throws.InvalidOperationException);
        }

        [Test]
        public void Max_Value_OnlyOneNode()
        {
            LList<int> list = new(1);

            Assert.That(list.MaxValue(), Is.EqualTo(1));
        }

        [Test]
        public void Max_Value_MultipleNodes()
        {
            LList<int> list = new(1);
            list.AddLast(5);
            list.AddLast(-10);

            Assert.That(list.MaxValue(), Is.EqualTo(5));
        }

        [Test]
        public void Max_Node_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.MaxNode(), Throws.InvalidOperationException);
        }

        [Test]
        public void Max_Node_OnlyOneNode()
        {
            LList<int> list = new(1);

            Assert.That(list.MaxNode(), Is.EqualTo(list.First!));
        }

        [Test]
        public void Max_Node_MultipleNodes()
        {
            LList<int> list = new(1);
            LLNode<int> node = list.AddLast(5);
            list.AddLast(-10);

            Assert.That(list.MaxNode(), Is.EqualTo(node));
        }
    
        [Test]
        public void Max_Node_DuplicateNodes()
        {
            LList<int> list = new();
            LLNode<int> node1 = list.AddLast(1);
            LLNode<int> node2 = list.AddFirst(1);

            Assert.That(list.MaxNode(), Is.SameAs(node2));
        }
    }

    [TestFixture]
    public class LListTests_Reverse
    {
        [Test]
        public void Reverse_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.Reverse(), Throws.InvalidOperationException);
        }

        [Test]
        public void Reverse_OneNodeOnly()
        {
            LList<int> list = new(1);
            list.Reverse();

            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.First, Is.SameAs(list.Last));
        }

        [Test]
        public void Reverse_CorrectlyReversal()
        {
            LList<int> list = new([1, 2, 3, 4, 5]);
            list.Reverse();

            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.First.Next!.Value, Is.EqualTo(4));
            Assert.That(list.First.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(list.First.Next.Next.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First.Next.Next.Next.Next!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(list.Last.Previous!.Value, Is.EqualTo(2));
            Assert.That(list.Last.Previous.Previous!.Value, Is.EqualTo(3));
            Assert.That(list.Last.Previous.Previous.Previous!.Value, Is.EqualTo(4));
            Assert.That(list.Last.Previous.Previous.Previous.Previous!.Value, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class LListTests_MergeSort
    {
        [Test]
        public void MergeSort_EmptyList()
        {
            LList<int> list = new();

            Assert.That(() => list.MergeSort(), Throws.InvalidOperationException);
        }

        [Test]
        public void MergeSort_OneNodeOnly()
        {
            LList<int> list = new(1);
            list.MergeSort();
            
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.First, Is.SameAs(list.Last));
        }

        [Test]
        public void MergeSort_CorrectSorting_Ascending()
        {
            LList<int> list = new();
            list.AddLast(2);
            list.AddLast(5);
            list.AddLast(1);
            list.AddLast(3);
            list.AddLast(4);

            list.MergeSort(isAscending: true);

            Assert.That(list, Has.Count.EqualTo(5));
            for (int i = 0; i < 5; ++i)
            {
                Assert.That(list[i], Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void MergeSort_CorrectSorting_Descending()
        {
            LList<int> list = new();
            list.AddLast(2);
            list.AddLast(5);
            list.AddLast(1);
            list.AddLast(3);
            list.AddLast(4);

            list.MergeSort(isAscending: false);

            Assert.That(list, Has.Count.EqualTo(5));
            for (int i = 0; i < 5; ++i)
            {
                Assert.That(list[i], Is.EqualTo(5 - i));
            }
        }
    }

    [TestFixture]
    public class LListTests_IsSorted
    {
        [Test]
        public void IsSorted_EmptyList()
        {
            LList<int> list = new();

            Assert.That(list.IsSorted(), Is.True);
        }

        [Test]
        public void IsSorted_OneNodeOnly()
        {
            LList<int> list = new(1);

            Assert.That(list.IsSorted(), Is.True);
        }

        [Test]
        public void IsSorted_MultipleNodes_NotSorted()
        {
            LList<int> list = new();
            list.AddLast(2);
            list.AddFirst(3);
            list.AddLast(1);

            Assert.That(list.IsSorted(), Is.False);
        }

        [Test]
        public void IsSorted_MultipleNodes_Ascending_Sorted()
        {
            LList<int> list = new();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            Assert.That(list.IsSorted(isAscending: true), Is.True);
        }

        [Test]
        public void IsSorted_MultipleNodes_Descending_Sorted()
        {
            LList<int> list = new();
            list.AddLast(3);
            list.AddLast(2);
            list.AddLast(1);

            Assert.That(list.IsSorted(isAscending: false), Is.True);
        }

        [Test]
        public void IsSorted_DuplicateNodes_Ascending_Sorted()
        {
            LList<int> list = new();
            list.AddLast(1);
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            Assert.That(list.IsSorted(isAscending: true), Is.True);
        }

        [Test]
        public void IsSorted_DuplicateNodes_Descending_Sorted()
        {
            LList<int> list = new();
            list.AddLast(3);
            list.AddLast(2);
            list.AddLast(2);
            list.AddLast(1);

            Assert.That(list.IsSorted(isAscending: false), Is.True);
        }

        [Test]
        public void IsSorted_MergeSorted_Ascending()
        {
            LList<int> list = new();
            list.AddFirst(2);
            list.AddLast(4);
            list.AddFirst(5);
            list.AddLast(1);
            list.AddFirst(3);

            list.MergeSort(isAscending: true);

            Assert.That(list.IsSorted(isAscending: true), Is.True);
        }

        [Test]
        public void IsSorted_MergeSorted_Descending()
        {
            LList<int> list = new();
            list.AddFirst(2);
            list.AddLast(4);
            list.AddFirst(5);
            list.AddLast(1);
            list.AddFirst(3);

            list.MergeSort(isAscending: false);

            Assert.That(list.IsSorted(isAscending: false), Is.True);
        }
    }

    [TestFixture]
    public class LListTests_Split
    {
        [Test]
        public void Split_EmptyList()
        {
            LList<int> list = new();
            
            Assert.That(() => list.Split(1), Throws.InvalidOperationException);
        }

        [Test]
        public void Split_ValueNotInList()
        {
            LList<int> list = new(1);

            Assert.That(() => list.Split(2), Throws.InvalidOperationException);
        }

        [Test]
        public void Split_FirstNode()
        {
            LList<int> list = new([1, 2, 3]);
            LList<int> list2 = list.Split(1);

            Assert.That(list, Has.Count.EqualTo(0));
            Assert.That(list2, Has.Count.EqualTo(3));
        }

        [Test]
        public void Split_LastNode()
        {
            LList<int> list = new([1, 2, 3]);
            LList<int> list2 = list.Split(3);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list2, Has.Count.EqualTo(1));
        }

        [Test]
        public void Split_MiddleNode()
        {
            LList<int> list = new([1, 2, 3, 4, 5, 6]);
            LList<int> list2 = list.Split(4);

            Assert.That(list, Has.Count.EqualTo(3));
            Assert.That(list2, Has.Count.EqualTo(3));
        }

        [Test]
        public void Split_Node_EmptyList()
        {
            LList<int> list = new();
            
            Assert.That(() => list.Split(new LLNode<int>(1)), Throws.InvalidOperationException);
        }

        [Test]
        public void Split_Node_NodeNotInList()
        {
            LList<int> list = new(1);

            Assert.That(() => list.Split(new LLNode<int>(2)), Throws.InvalidOperationException);
        }

        [Test]
        public void Split_Node_Node_FirstNode()
        {
            LList<int> list = new([1, 2, 3]);
            LList<int> list2 = list.Split(list.First!);

            Assert.That(list, Has.Count.EqualTo(0));
            Assert.That(list2, Has.Count.EqualTo(3));
        }

        [Test]
        public void Split_Node_LastNode()
        {
            LList<int> list = new([1, 2, 3]);
            LList<int> list2 = list.Split(list.Last!);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list2, Has.Count.EqualTo(1));
        }

        [Test]
        public void Split_Node_MiddleNode()
        {
            LList<int> list = new([1, 2, 3, 4, 5, 6]);
            LLNode<int> node = list.GetNode(3)!;
            LList<int> list2 = list.Split(node);

            Assert.That(list, Has.Count.EqualTo(3));
            Assert.That(list2, Has.Count.EqualTo(3));
        }

        [Test]
        public void SplitAt_EmptyList()
        {
            LList<int> list = new();
            
            Assert.That(() => list.SplitAt(1), Throws.InvalidOperationException);
        }

        [Test]
        public void SplitAt_NodeNotInList()
        {
            LList<int> list = new(1);

            Assert.Throws<IndexOutOfRangeException>(() => list.SplitAt(3));
        }

        [Test]
        public void SplitAt_FirstNode()
        {
            LList<int> list = new([1, 2, 3]);
            LList<int> list2 = list.SplitAt(0);

            Assert.That(list, Has.Count.EqualTo(0));
            Assert.That(list2, Has.Count.EqualTo(3));
        }

        [Test]
        public void SplitAt_LastNode()
        {
            LList<int> list = new([1, 2, 3]);
            LList<int> list2 = list.SplitAt(2);

            Assert.That(list, Has.Count.EqualTo(2));
            Assert.That(list2, Has.Count.EqualTo(1));
        }

        [Test]
        public void SplitAt_MiddleNode()
        {
            LList<int> list = new([1, 2, 3, 4, 5, 6]);
            LList<int> list2 = list.SplitAt(3);

            Assert.That(list, Has.Count.EqualTo(3));
            Assert.That(list2, Has.Count.EqualTo(3));
        }
    }

    [TestFixture]
    public class LListTests_MergeTwoLists
    {
        [Test]
        public void Merge_BothListEmpty()
        {
            LList<int> list1 = new();
            LList<int> list2 = new();

            Assert.That(() => LList<int>.Merge(list1, list2), Throws.InvalidOperationException);
        }

        [Test]
        public void Merge_OneListNull()
        {
            LList<int> list1 = null!;
            LList<int> list2 = new();

            Assert.That(() => LList<int>.Merge(list1, list2), Throws.ArgumentNullException);
        }

        [Test]
        public void Merge_SecondListEmpty_FirstListInFront()
        {
            LList<int> list1 = new([1, 2, 3]);
            LList<int> list2 = new();

            LList<int> list3 = LList<int>.Merge(list1, list2, firstListInFront: true);

            Assert.That(list3, Has.Count.EqualTo(3));
        }

        [Test]
        public void Merge_FirstListEmpty_FirstListInFront()
        {
            LList<int> list2 = new([1, 2, 3]);
            LList<int> list1 = new();

            LList<int> list3 = LList<int>.Merge(list1, list2, firstListInFront: true);

            Assert.That(list3, Has.Count.EqualTo(3));
        }

        [Test]
        public void Merge_BothList_FirstListInFront()
        {
            LList<int> list1 = new([1, 2, 3]);
            LList<int> list2 = new([4, 5, 6]);

            LList<int> list3 = LList<int>.Merge(list1, list2, firstListInFront: true);

            Assert.That(list3, Has.Count.EqualTo(6));
        }

        [Test]
        public void Merge_BothList_FirstListAtBack()
        {
            LList<int> list1 = new([1, 2, 3]);
            LList<int> list2 = new([4, 5, 6]);

            LList<int> list3 = LList<int>.Merge(list1, list2, firstListInFront: false);

            Assert.That(list3, Has.Count.EqualTo(6));
        }
    }

    [TestFixture]
    public class LListTests_Append
    {
        [Test]
        public void Append_NullCollection()
        {
            LList<int> list = new();
            
            Assert.That(() => list.Append(null!), Throws.ArgumentNullException);
        }

        [Test]
        public void Append_EmptyList()
        {
            LList<int> list = new();
            list.Append([1, 2, 3]);

            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void Append_Array()
        {
            LList<int> list = new(1);
            list.Append([2, 3, 4, 5]);

            Assert.That(list, Has.Count.EqualTo(5));
        }

        [Test]
        public void Append_LinkedList()
        {
            LList<int> list = new(1);
            list.Append(new LList<int>([2, 3, 4, 5]));

            Assert.That(list, Has.Count.EqualTo(5));
        }

        [Test]
        public void Append_List()
        {
            LList<int> list = new(1);
            list.Append(new List<int>() { 2, 3, 4, 5});

            Assert.That(list, Has.Count.EqualTo(5));
        }
    }

    [TestFixture]
    public class LListTests_Prepend
    {
        [Test]
        public void Prepend_NullCollection()
        {
            LList<int> list = new();
            
            Assert.That(() => list.Prepend(null!), Throws.ArgumentNullException);
        }

        [Test]
        public void Prepend_EmptyList()
        {
            LList<int> list = new();
            list.Prepend([1, 2, 3]);

            Assert.That(list, Has.Count.EqualTo(3));
        }

        [Test]
        public void Prepend_Array()
        {
            LList<int> list = new(1);
            list.Prepend([2, 3, 4, 5]);

            Assert.That(list, Has.Count.EqualTo(5));
        }

        [Test]
        public void Prepend_LinkedList()
        {
            LList<int> list = new(1);
            list.Prepend(new LList<int>([2, 3, 4, 5]));

            Assert.That(list, Has.Count.EqualTo(5));
        }

        [Test]
        public void Prepend_List()
        {
            LList<int> list = new(1);
            list.Prepend(new List<int>() { 2, 3, 4, 5});

            Assert.That(list, Has.Count.EqualTo(5));
        }
    }
}