namespace DSA.LinkedList.Tests;

public class Tests
{
    [TestFixture]
    public class LListTests_Ctors
    {
        [Test]
        public void Constructor_EmptyList_CountZero()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            int count = list.Count;

            // Assert
            Assert.That(count, Is.EqualTo(0));
            Assert.IsNull(list.First);
            Assert.IsNull(list.Last);
        }

        [Test]
        public void Constructor_FromArray_ValidList()
        {
            // Arrange
            int[] array = { 1, 2, 3 };

            // Act
            LList<int> list = new LList<int>(array);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(3));
        }

        [Test]
        public void Constructor_FromList_ValidList()
        {
            // Arrange
            var inputList = new List<string> { "apple", "banana", "orange" };

            // Act
            LList<string> list = new LList<string>(inputList);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo("apple"));
            Assert.That(list.Last!.Value, Is.EqualTo("orange"));
        }

        [Test]
        public void Constructor_NullCollection_EmptyList()
        {
            // Arrange
            IEnumerable<int>? collection = null;

            // Act
            LList<int> list = new LList<int>(collection!);

            // Assert
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.IsNull(list.First);
            Assert.IsNull(list.Last);
        }
    }

    [TestFixture]
    public class LListTests_Pointers
    {
        // Testing if First and Last pointers are pointing to the correct elements after adding to the front of the list
        [Test]
        public void FirstAndLast_AfterAddFirst_AreCorrect()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(15));
            Assert.That(list.Last!.Value, Is.EqualTo(5));
        }

        // Testing if First and Last pointers are pointing to the correct elements after adding to the end of the list
        [Test]
        public void FirstAndLast_AfterAddLast_AreCorrect()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(15));
        }

        // Testing if First and Last pointers are pointing to the correct elements after removing the first item
        [Test]
        public void FirstAndLast_AfterRemoveFirst_AreCorrect()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act
            list.RemoveFirst();

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(15));
        }

        // Testing if First and Last pointers are pointing to the correct elements after removing the last item
        [Test]
        public void FirstAndLast_AfterRemoveLast_AreCorrect()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act
            list.RemoveLast();

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

        // Testing if First and Last pointers are pointing to null after clearing the list
        [Test]
        public void FirstAndLast_AfterClear_AreNull()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act
            list.Clear();

            // Assert
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_Count
    {
        // Testing if Count is incremented correctly after adding items
        [Test]
        public void Count_AfterAddingItems_IsCorrect()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
        }

        // Testing if Count is decremented correctly after removing items
        [Test]
        public void Count_AfterRemovingItems_IsCorrect()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act
            list.RemoveFirst();
            list.RemoveLast();

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
        }

        // Testing if Count is correct after clearing the list
        [Test]
        public void Count_AfterClearingList_IsZero()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act
            list.Clear();

            // Assert
            Assert.That(list.Count, Is.EqualTo(0));
        }

        // Testing if Count remains zero for an empty list
        [Test]
        public void Count_EmptyList_IsZero()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Assert
            Assert.That(list.Count, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class LListTests_PrintList
    {
        [Test]
        public void PrintList_Integers_FrontToBack()
        {
            LList<int> list = new LList<int>();
            string result   = "[Front to back]: ";

            for (int i = 0; i < 10; ++i)
            {
                list.AddLast(i);
                result += i.ToString() + " -> ";
            }

            result += '\n';
            string resultList = list.ListAsString(frontToBack: true);

            Assert.That(resultList, Is.EqualTo(result));
        }

        [Test]
        public void PrintList_Integers_BackToFront()
        {
            LList<int> list = new LList<int>();
            string result   = "[Back to front]: ";

            for (int i = 0; i < 10; ++i)
            {
                list.AddFirst(i);
                result += i.ToString() + " -> ";
            }

            result += '\n';
            string resultList = list.ListAsString(frontToBack: false);

            Assert.That(resultList, Is.EqualTo(result));
        }
    }

    [TestFixture]
    public class LListTests_AddFirst
    {
        // Existing list is empty, adding the first item
        [Test]
        public void AddFirst_EmptyList_AddsFirstItem()
        {
            // Arrange
            LList<int> list = new();

            // Act
            list.AddFirst(10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

        // Existing list has items, adding a new item to the beginning
        [Test]
        public void AddFirst_ExistingItems_AddsItemAtBeginning()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(20);
            list.AddLast(30);

            // Act
            list.AddFirst(10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(30));
        }

        // Adding a node that belongs to another list should throw InvalidOperationException
        [Test]
        public void AddFirst_NodeFromAnotherList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            LList<int> list2 = new LList<int>();
            var node = list1.AddFirst(10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list2.AddFirst(node));
        }

        // Adding an item and ensuring it becomes the first element
        [Test]
        public void AddFirst_AddItem_ItemBecomesFirst()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(20);

            // Act
            list.AddFirst(10);

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(10));
        }

        // Adding multiple items to the beginning of the list and checking their order
        [Test]
        public void AddFirst_MultipleItems_AddedInCorrectOrder()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddFirst(30);
            list.AddFirst(20);

            // Act
            list.AddFirst(10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.First.Next!.Value, Is.EqualTo(20));
            Assert.That(list.Last!.Value, Is.EqualTo(30));
        }

        // Count should increase after adding an item
        [Test]
        public void AddFirst_CountIncreasesAfterAddingItem()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            list.AddFirst(10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
        }

        // First and Last pointers should be updated correctly
        [Test]
        public void AddFirst_UpdatePointers()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            list.AddFirst(10);

            // Assert
            Assert.That(list.Last, Is.EqualTo(list.First));
        }
    
        // Existing list is empty, adding the first node
        [Test]
        public void AddFirst_Node_EmptyList_AddsFirstNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(10);

            // Act
            list.AddFirst(node);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
            Assert.That(list.First, Is.SameAs(node));
            Assert.That(list.Last, Is.SameAs(node));
        }

        // Existing list has items, adding a new node to the beginning
        [Test]
        public void AddFirst_Node_ExistingItems_AddsNodeAtBeginning()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(20);
            list.AddLast(30);
            var node = new LLNode<int>(10);

            // Act
            list.AddFirst(node);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(30));
            Assert.That(list.First, Is.SameAs(node));
        }

        // Adding a node and ensuring it becomes the first element
        [Test]
        public void AddFirst_AddNode_NodeBecomesFirst()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(10);

            // Act
            list.AddFirst(node);

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(10));
        }

        // Adding multiple nodes to the beginning of the list and checking their order
        [Test]
        public void AddFirst_MultipleNodes_AddedInCorrectOrder()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = new LLNode<int>(30);
            var node2 = new LLNode<int>(20);
            var node3 = new LLNode<int>(10);
            list.AddFirst(node1);
            list.AddFirst(node2);

            // Act
            list.AddFirst(node3);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.First.Next!.Value, Is.EqualTo(20));
            Assert.That(list.Last!.Value, Is.EqualTo(30));
        }

        // Count should increase after adding a node
        [Test]
        public void AddFirst_CountIncreasesAfterAddingNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(10);

            // Act
            list.AddFirst(node);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class LListTests_AddLast
    {
        // Existing list is empty, adding the first item
        [Test]
        public void AddLast_Item_EmptyList_AddsFirstItem()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            list.AddLast(10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
            Assert.That(list.First, Is.SameAs(list.Last));
        }

        // Existing list has items, adding a new item to the end
        [Test]
        public void AddLast_Item_ExistingItems_AddsItemAtEnd()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(10);
            list.AddLast(20);

            // Act
            list.AddLast(30);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(30));
            Assert.That(list.First, Is.Not.SameAs(list.Last));
        }

        // Adding a node that belongs to another list should throw InvalidOperationException
        [Test]
        public void AddLast_NodeFromAnotherList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            LList<int> list2 = new LList<int>();
            var node = list1.AddLast(10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list2.AddLast(node));
        }

        // Adding an item and ensuring it becomes the last element
        [Test]
        public void AddLast_AddItem_ItemBecomesLast()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(10);

            // Act
            list.AddLast(20);

            // Assert
            Assert.That(list.Last!.Value, Is.EqualTo(20));
        }

        // Adding multiple items to the end of the list and checking their order
        [Test]
        public void AddLast_MultipleItems_AddedInCorrectOrder()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(10);
            list.AddLast(20);

            // Act
            list.AddLast(30);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(30));
            Assert.That(list.First, Is.Not.SameAs(list.Last));
        }

        // Count should increase after adding an item
        [Test]
        public void AddLast_CountIncreasesAfterAddingItem()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            list.AddLast(10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
        }

        // First and Last pointers should be updated correctly
        [Test]
        public void AddLast_UpdatePointers()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            list.AddLast(10);

            // Assert
            Assert.That(list.First, Is.SameAs(list.Last));
        }

        // Existing list is empty, adding the first node
        [Test]
        public void AddLast_Node_EmptyList_AddsFirstNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(10);

            // Act
            list.AddLast(node);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
            Assert.That(list.First, Is.SameAs(list.Last));
            Assert.That(list.First, Is.SameAs(node));
            Assert.That(list.Last, Is.SameAs(node));
        }

        // Existing list has items, adding a new node to the end
        [Test]
        public void AddLast_Node_ExistingNodes_AddsNodeAtEnd()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = new LLNode<int>(10);
            var node2 = new LLNode<int>(20);
            list.AddLast(node1);

            // Act
            list.AddLast(node2);

            // Assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(20));
            Assert.That(list.First, Is.Not.SameAs(list.Last));
            Assert.That(list.First, Is.SameAs(node1));
            Assert.That(list.Last, Is.SameAs(node2));
        }

        // Adding a node and ensuring it becomes the last element
        [Test]
        public void AddLast_AddNode_NodeBecomesLast()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(10);

            // Act
            list.AddLast(node);

            // Assert
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

          // Adding multiple nodes to the end of the list and checking their order
        [Test]
        public void AddLast_MultipleNodes_AddedInCorrectOrder()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = new LLNode<int>(10);
            var node2 = new LLNode<int>(20);
            list.AddLast(node1);

            // Act
            list.AddLast(node2);

            // Assert
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(20));
            Assert.That(list.First, Is.Not.SameAs(list.Last));
            Assert.That(list.First, Is.SameAs(node1));
            Assert.That(list.Last, Is.SameAs(node2));
        }

        // Count should increase after adding a node
        [Test]
        public void AddLast_CountIncreasesAfterAddingNode()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            list.AddLast(new LLNode<int>(10));

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class LListTests_AddBefore
    {
        // Adding a new item before the specified existing item in an empty list
        [Test]
        public void AddBefore_Item_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            LLNode<int> node = new LLNode<int>(5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddBefore(node, 10));
        }

        // Adding a new item before the specified existing item in a non-empty list
        [Test]
        public void AddBefore_Item_ExistingItem_AddsItemBefore()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            LLNode<int> node = list.AddLast(15);

            // Act
            list.AddBefore(node, 10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(15));
            Assert.That(list.First.Next!.Value, Is.EqualTo(10));
        }

        // Adding a new item before the specified existing item when the item does not exist in the list
        [Test]
        public void AddBefore_Item_ItemNotInList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(15);

            LLNode<int> node = new LLNode<int>(20);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddBefore(node, 10));
        }

        // Adding a node before the specified existing node in an empty list
        [Test]
        public void AddBefore_Node_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddBefore(node, 10));
        }

        // Adding a new node before the specified existing node in a non-empty list
        [Test]
        public void AddBefore_Node_ExistingNode_AddsNodeBefore()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            var node = list.AddLast(15);

            // Act
            list.AddBefore(node, 10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(15));
            Assert.That(list.First.Next!.Value, Is.EqualTo(10));
        }

        // Adding a new node before the specified existing node when the node does not exist in the list
        [Test]
        public void AddBefore_Node_NodeNotInList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            var node = list.AddLast(15);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddBefore(new LLNode<int>(20), 10));
        }
    }

    [TestFixture]
    public class LListTests_AddAfter
    {
        // Adding a new item after the specified existing item in an empty list
        [Test]
        public void AddAfter_Item_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            LLNode<int> node = new LLNode<int>(5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddAfter(node, 10));
        }

        // Adding a new item after the specified existing item in a non-empty list
        [Test]
        public void AddAfter_Item_ExistingItem_AddsItemAfter()
        {
            // Arrange
            LList<int> list = new LList<int>();
            LLNode<int> node = list.AddLast(5);
            list.AddLast(15);

            // Act
            list.AddAfter(node, 10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(15));
            Assert.That(list.First.Next!.Value, Is.EqualTo(10));
        }

        // Adding a new item after the specified existing item when the item does not exist in the list
        [Test]
        public void AddAfter_Item_ItemNotInList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(15);

            LLNode<int> node = new LLNode<int>(20);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddAfter(node, 10));
        }

        // Adding a node after the specified existing node in an empty list
        [Test]
        public void AddAfter_Node_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddAfter(node, 10));
        }

        // Adding a new node after the specified existing node in a non-empty list
        [Test]
        public void AddAfter_Node_ExistingNode_AddsNodeAfter()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            var node = list.AddLast(15);

            // Act
            list.AddAfter(node, 10);

            // Assert
            Assert.That(list.Count, Is.EqualTo(3));
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
            Assert.That(list.First.Next!.Value, Is.EqualTo(15));
        }

        // Adding a new node after the specified existing node when the node does not exist in the list
        [Test]
        public void AddAfter_Node_NodeNotInList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            var node = list.AddLast(15);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.AddAfter(new LLNode<int>(20), 10));
        }
    }

    [TestFixture]
    public class LListTests_RemoveFirst
    {
        // Removing the first item from a non-empty list
        [Test]
        public void RemoveFirst_NonEmptyList_RemovesFirstItem()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);

            // Act
            list.RemoveFirst();

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

        // Removing the first item from an empty list should throw InvalidOperationException
        [Test]
        public void RemoveFirst_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.RemoveFirst());
        }

        // Removing the only item from a list
        [Test]
        public void RemoveFirst_OnlyItem_RemovesItemAndMakesListEmpty()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);

            // Act
            list.RemoveFirst();

            // Assert
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_RemoveLast
    {
        // Removing the last item from a non-empty list
        [Test]
        public void RemoveLast_NonEmptyList_RemovesLastItem()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);

            // Act
            list.RemoveLast();

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(5));
        }

        // Removing the last item from an empty list should throw InvalidOperationException
        [Test]
        public void RemoveLast_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.RemoveLast());
        }

        // Removing the only item from a list
        [Test]
        public void RemoveLast_OnlyItem_RemovesItemAndMakesListEmpty()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);

            // Act
            list.RemoveLast();

            // Assert
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_Remove
    {
        // Removing an existing item from a non-empty list
        [Test]
        public void Remove_Item_ExistingItem_RemovesItem()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);

            // Act
            bool removed = list.Remove(5);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

        // Removing a non-existing item from a list
        [Test]
        public void Remove_Item_NonExistingItem_ReturnsFalse()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);

            // Act
            bool removed = list.Remove(15);

            // Assert
            Assert.That(removed, Is.False);
            Assert.That(list.Count, Is.EqualTo(2));
            Assert.That(list.First!.Value, Is.EqualTo(5));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

        // Removing an item from an empty list should return false
        [Test]
        public void Remove_Item_EmptyList_ReturnsFalse()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            bool removed = list.Remove(5);

            // Assert
            Assert.That(removed, Is.False);
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
        }

        // Removing the only item from a list
        [Test]
        public void Remove_Item_OnlyItem_RemovesItemAndMakesListEmpty()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);

            // Act
            bool removed = list.Remove(5);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
        }

        // Removing a node from a non-empty list
        [Test]
        public void Remove_Node_ExistingNode_RemovesNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = list.AddLast(5);
            var node2 = list.AddLast(10);

            // Act
            list.Remove(node1);

            // Assert
            Assert.That(list.Count, Is.EqualTo(1));
            Assert.That(list.First!.Value, Is.EqualTo(10));
            Assert.That(list.Last!.Value, Is.EqualTo(10));
        }

        // Removing a node that doesn't belong to the list should throw InvalidOperationException
        [Test]
        public void Remove_Node_NotInList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            LList<int> list2 = new LList<int>();
            var node1 = list1.AddLast(5);
            var node2 = list2.AddLast(10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list1.Remove(node2));
        }

        // Removing a node from an empty list should throw InvalidOperationException
        [Test]
        public void Remove_Node_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = new LLNode<int>(5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list.Remove(node));
        }

        // Removing the only node from a list
        [Test]
        public void Remove_Node_OnlyNode_RemovesNodeAndMakesListEmpty()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = list.AddLast(5);

            // Act
            list.Remove(node);

            // Assert
            Assert.That(list.Count, Is.EqualTo(0));
            Assert.That(list.First, Is.Null);
            Assert.That(list.Last, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_Contains
    {
        // Testing if Contains returns true for an item present in the list
        [Test]
        public void Contains_ItemPresent_ReturnsTrue()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act & Assert
            Assert.That(list.Contains(10), Is.True);
        }

        // Testing if Contains returns false for an item not present in the list
        [Test]
        public void Contains_ItemNotPresent_ReturnsFalse()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act & Assert
            Assert.That(list.Contains(20), Is.False);
        }

        // Testing if Contains returns false for an empty list
        [Test]
        public void Contains_EmptyList_ReturnsFalse()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act & Assert
            Assert.That(list.Contains(5), Is.False);
        }
    }

    [TestFixture]
    public class LListTests_LinearSearch
    {
        // Testing if LinearSearch finds the first occurrence of an item in the list
        [Test]
        public void LinearSearch_FindFirst_OccurrenceFound_ReturnsNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);
            list.AddLast(10);

            // Act
            var node = list.LinearSearch(10, findFirst: true);

            // Assert
            Assert.That(node, Is.Not.Null);
            Assert.That(node!.Value, Is.EqualTo(10));
            Assert.That(node.Previous!.Value, Is.EqualTo(5));
            Assert.That(node.Next!.Value, Is.EqualTo(15));
        }

        // Testing if LinearSearch returns null when finding the first occurrence of an item not in the list
        [Test]
        public void LinearSearch_FindFirst_ItemNotInList_ReturnsNull()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act
            var node = list.LinearSearch(20, findFirst: true);

            // Assert
            Assert.That(node, Is.Null);
        }

        // Testing if LinearSearch finds the last occurrence of an item in the list
        [Test]
        public void LinearSearch_FindLast_OccurrenceFound_ReturnsNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);
            list.AddLast(10);

            // Act
            var node = list.LinearSearch(10, findFirst: false);

            // Assert
            Assert.That(node, Is.Not.Null);
            Assert.That(node!.Value, Is.EqualTo(10));
            Assert.That(node.Previous!.Value, Is.EqualTo(15));
            Assert.That(node.Next, Is.Null);
        }

        // Testing if LinearSearch returns null when finding the last occurrence of an item not in the list
        [Test]
        public void LinearSearch_FindLast_ItemNotInList_ReturnsNull()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            // Act
            var node = list.LinearSearch(20, findFirst: false);

            // Assert
            Assert.That(node, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_Cycle
    {
        // Testing if HasCycle returns true when a cycle is present in the list
        [Test]
        public void HasCycle_CyclePresent_ReturnsTrue()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = list.AddLast(5);
            var node2 = list.AddLast(10);
            var node3 = list.AddLast(15);
            node3.Next = node1; // Creating a cycle

            // Act
            bool hasCycle = list.HasCycle(node1);

            // Assert
            Assert.That(hasCycle, Is.True);
        }

        // Testing if HasCycle returns false when no cycle is present in the list
        [Test]
        public void HasCycle_NoCycle_ReturnsFalse()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = list.AddLast(5);
            var node2 = list.AddLast(10);
            var node3 = list.AddLast(15);

            // Act
            bool hasCycle = list.HasCycle(node1);

            // Assert
            Assert.That(hasCycle, Is.False);
        }

        // Testing if HasCycle throws InvalidOperationException when the input node belongs to another list
        [Test]
        public void HasCycle_NodeBelongsToAnotherList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            LList<int> list2 = new LList<int>();
            var node = list2.AddLast(5);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => list1.HasCycle(node));
        }

        // Testing if HasCycle returns false for a single-node list
        [Test]
        public void HasCycle_SingleNodeList_ReturnsFalse()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = list.AddLast(5);

            // Act
            bool hasCycle = list.HasCycle(node);

            // Assert
            Assert.That(hasCycle, Is.False);
        }

        // Testing if HasCycle returns true for a cycle starting from the first node
        [Test]
        public void HasCycle_CycleStartingFromFirstNode_ReturnsTrue()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = list.AddLast(5);
            var node2 = list.AddLast(10);
            var node3 = list.AddLast(15);
            node3.Next = node1; // Creating a cycle

            // Act
            bool hasCycle = list.HasCycle(node1);

            // Assert
            Assert.That(hasCycle, Is.True);
        }

        // Testing if HasCycle returns true for a cycle starting from a middle node
        [Test]
        public void HasCycle_CycleStartingFromMiddleNode_ReturnsTrue()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = list.AddLast(5);
            var node2 = list.AddLast(10);
            var node3 = list.AddLast(15);
            var node4 = list.AddLast(20);
            var node5 = list.AddLast(25);
            node5.Next = node2; // Creating a cycle

            // Act
            bool hasCycle = list.HasCycle(node2);

            // Assert
            Assert.That(hasCycle, Is.True);
        }

        // Testing if HasCycle returns false for a non-cyclic list with nodes pointing to null
        [Test]
        public void HasCycle_NonCyclicListWithNodesPointingToNull_ReturnsFalse()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = list.AddLast(5);
            var node2 = list.AddLast(10);
            var node3 = list.AddLast(15);

            // Act
            bool hasCycle = list.HasCycle(node1);

            // Assert
            Assert.That(hasCycle, Is.False);
        }
    }

    [TestFixture]
    public class LListTests_Reverse
    {
        // Testing if Reverse method reverses the order of the list correctly
        [Test]
        public void Reverse_ReversesListCorrectly()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            // Act
            var newFirst = list.Reverse();

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(3));
            Assert.That(list.First!.Next!.Value, Is.EqualTo(2));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(newFirst.Previous, Is.Null);
        }

        // Testing if Reverse method returns the first node of the reversed list
        [Test]
        public void Reverse_ReturnsFirstNodeOfReversedList()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            // Act
            var newFirst = list.Reverse();

            // Assert
            Assert.That(newFirst, Is.EqualTo(list.First));
            Assert.That(list.First!.Value, Is.EqualTo(3));
            Assert.That(list.First!.Next!.Value, Is.EqualTo(2));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(newFirst.Previous, Is.Null);
        }

        // Testing if Reverse throws InvalidOperationException for an empty list
        [Test]
        public void Reverse_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => list.Reverse());
        }

        // Testing if Reverse returns the only node in a single-node list
        [Test]
        public void Reverse_SingleNodeList_ReturnsFirstNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = list.AddLast(5);

            // Act
            var newFirst = list.Reverse();

            // Assert
            Assert.That(newFirst, Is.EqualTo(node));
            Assert.That(newFirst.Previous, Is.Null);
            Assert.That(newFirst.Next, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_Swap
    {
        // Testing if Swap method swaps two nodes correctly
        [Test]
        public void Swap_SwapsNodesCorrectly()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node1 = list.AddLast(1);
            var node2 = list.AddLast(2);
            var node3 = list.AddLast(3);

            Assert.That(node1.Previous, Is.Null);

            // Act
            list.Swap(node1, node3);

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(3));
            Assert.That(list.First!.Next!.Value, Is.EqualTo(2));
            Assert.That(list.First!.Next!.Next!.Value, Is.EqualTo(1));
            Assert.That(list.Last!.Value, Is.EqualTo(1));
            Assert.That(node1.Next, Is.Null);
            Assert.That(node1.Previous, Is.EqualTo(node2));
            Assert.That(node3.Previous, Is.Null);
        }

        // Testing if Swap method throws InvalidOperationException for nodes not in the current list
        [Test]
        public void Swap_NodeNotInList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            LList<int> list2 = new LList<int>();
            var node1 = list1.AddLast(1);
            var node2 = list2.AddLast(2);

            // Assert
            Assert.Throws<InvalidOperationException>(() => list1.Swap(node1, node2));
            Assert.Throws<InvalidOperationException>(() => list2.Swap(node2, node1));
        }
    }

    [TestFixture]
    public class LListTests_Append
    {
        // Testing if Append method appends another list correctly
        [Test]
        public void Append_AppendsListCorrectly()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(1);
            list1.AddLast(2);

            LList<int> list2 = new LList<int>();
            list2.AddLast(3);
            list2.AddLast(4);

            // Act
            list1.Append(list2);

            // Assert
            Assert.That(list1.Count, Is.EqualTo(4));
            Assert.That(list1.First!.Value, Is.EqualTo(1));
            Assert.That(list1.Last!.Value, Is.EqualTo(4));
            Assert.That(list1.First.Next!.Value, Is.EqualTo(2));
            Assert.That(list1.Last.Previous!.Value, Is.EqualTo(3));
        }

        // Testing if Append method throws InvalidOperationException when either list is empty
        [Test]
        public void Append_EitherListEmpty_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            LList<int> list2 = new LList<int>();
            list1.AddLast(1);

            // Assert
            Assert.Throws<InvalidOperationException>(() => list1.Append(list2));
            Assert.Throws<InvalidOperationException>(() => list2.Append(list1));
        }
    }

    [TestFixture]
    public class LListTests_ToList
    {
        // Testing if CopyTo method copies values to the array correctly
        [Test]
        public void CopyTo_CopiesValuesToArrayCorrectly()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            int[] array = new int[3];

            // Act
            list.CopyTo(array, 0);

            // Assert
            Assert.That(array, Is.EqualTo(new int[] { 1, 2, 3 }));
        }

        // Testing if CopyTo method throws ArgumentNullException for null array
        [Test]
        public void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);

            // Assert
            Assert.Throws<ArgumentNullException>(() => list.CopyTo(null!, 0));
        }

        // Testing if CopyTo method throws ArgumentOutOfRangeException for negative index
        [Test]
        public void CopyTo_NegativeIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            int[] array = new int[1];

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => list.CopyTo(array, -1));
        }

        // Testing if CopyTo method throws ArgumentException for insufficient array capacity
        [Test]
        public void CopyTo_InsufficientCapacity_ThrowsArgumentException()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            int[] array = new int[2];

            // Assert
            Assert.Throws<ArgumentException>(() => list.CopyTo(array, 0));
        }
    }

    [TestFixture]
    public class LListTests_GetMiddle
    {
        // Testing GetMiddleNode method for a list with odd number of elements
        [Test]
        public void GetMiddleNode_OddList_ReturnsMiddleNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            // Act
            var middleNode = list.GetMiddleNode();

            // Assert
            Assert.That(middleNode!.Value, Is.EqualTo(3));
        }

        // Testing GetMiddleNode method for a list with even number of elements
        [Test]
        public void GetMiddleNode_EvenList_ReturnsMiddleNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);

            // Act
            var middleNode = list.GetMiddleNode();

            // Assert
            Assert.That(middleNode!.Value, Is.EqualTo(2));
        }

        // Testing GetMiddleNode method throws InvalidOperationException for an empty list
        [Test]
        public void GetMiddleNode_EmptyList_ThrowsInvalidOperationException()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => list.GetMiddleNode());
        }

        // Testing GetMiddleNode method for a node in the list
        [Test]
        public void GetMiddleNode_NodeInList_ReturnsMiddleNode()
        {
            // Arrange
            LList<int> list = new LList<int>();
            var node = list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);

            // Act
            var middleNode = list.GetMiddleNode(node);

            // Assert
            Assert.That(middleNode!.Value, Is.EqualTo(2));
        }

        // Testing GetMiddleNode method for a null node
        [Test]
        public void GetMiddleNode_NullNode_ReturnsNull()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            // Act
            var middleNode = list.GetMiddleNode(null!);

            // Assert
            Assert.That(middleNode, Is.Null);
        }

        [Test]
        public void GetMiddleNode_BetweenTwoNodes_EvenNodes()
        {
            LList<int> list = new LList<int>();
            LLNode<int> first = list.AddFirst(1);
            list.AddLast(2);
            LLNode<int> second = list.AddLast(3);
            list.AddLast(4);

            var middleNode = list.GetMiddleNode(first, second);

            Assert.That(middleNode!.Value, Is.EqualTo(2));
        }

        [Test]
        public void GetMiddleNode_BetweenTwoNodes_OddNodes()
        {
            LList<int> list = new LList<int>();
            LLNode<int> first = list.AddFirst(1);
            list.AddLast(2);
            list.AddLast(5);
            LLNode<int> second = list.AddLast(3);
            list.AddLast(4);

            var middleNode = list.GetMiddleNode(first, second);

            Assert.That(middleNode!.Value, Is.EqualTo(5));
        }

        [Test]
        public void GetMiddleNode_BetweenTwoNodes_EndBeforeStart()
        {
            LList<int> list = new LList<int>();
            LLNode<int> first = list.AddFirst(1);
            list.AddLast(2);
            LLNode<int> second = list.AddLast(3);
            list.AddLast(4);

            var middleNode = list.GetMiddleNode(second, first);

            Assert.That(middleNode!.Value, Is.EqualTo(2));
        }
    }

    [TestFixture]
    public class LListTests_SortAndMerge
    {
        // Testing SortAndMerge method for ascending order
        [Test]
        public void SortAndMerge_AscendingOrder_ReturnsMergedSortedLinkedList()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(1);
            list1.AddLast(3);
            list1.AddLast(5);

            LList<int> list2 = new LList<int>();
            list2.AddLast(2);
            list2.AddLast(4);
            list2.AddLast(6);

            // Act
            var mergedList = list1.SortAndMerge(list1.First, list2.First);

            // Assert
            Assert.That(mergedList!.Value, Is.EqualTo(1));
            Assert.That(mergedList.Next!.Previous!.Value, Is.EqualTo(1));
            Assert.That(mergedList.Next!.Value, Is.EqualTo(2));
            Assert.That(mergedList.Next.Next!.Previous!.Value, Is.EqualTo(2));
            Assert.That(mergedList.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next!.Previous!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next!.Value, Is.EqualTo(4));
            Assert.That(mergedList.Next.Next.Next.Next!.Previous!.Value, Is.EqualTo(4));
            Assert.That(mergedList.Next.Next.Next.Next!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Previous!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Value, Is.EqualTo(6));
        }

        // Testing SortAndMerge method for descending order
        [Test]
        public void SortAndMerge_DescendingOrder_ReturnsMergedSortedLinkedList()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(5);
            list1.AddLast(3);
            list1.AddLast(1);

            LList<int> list2 = new LList<int>();
            list2.AddLast(6);
            list2.AddLast(4);
            list2.AddLast(2);

            // Act
            var mergedList = list1.SortAndMerge(list1.First, list2.First, ascendingOrder: false);

            // Assert
            Assert.That(mergedList!.Value, Is.EqualTo(6));
            Assert.That(mergedList.Next!.Previous!.Value, Is.EqualTo(6));
            Assert.That(mergedList.Next!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next!.Previous!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next!.Value, Is.EqualTo(4));
            Assert.That(mergedList.Next.Next.Next!.Previous!.Value, Is.EqualTo(4));
            Assert.That(mergedList.Next.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next.Next!.Previous!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next.Next!.Value, Is.EqualTo(2));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Previous!.Value, Is.EqualTo(2));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Value, Is.EqualTo(1));
        }

        // Testing SortAndMerge method when one list is empty
        [Test]
        public void SortAndMerge_OneListEmpty_ReturnsNonEmptyList()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(1);
            list1.AddLast(3);
            list1.AddLast(5);

            LList<int> list2 = new LList<int>();

            // Act
            var mergedList1 = list1.SortAndMerge(list1.First, list2.First);
            var mergedList2 = list1.SortAndMerge(list2.First, list1.First);

            // Assert
            Assert.That(mergedList1!.Value, Is.EqualTo(1));
            Assert.That(mergedList1.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList1.Next.Next!.Value, Is.EqualTo(5));

            Assert.That(mergedList2!.Value, Is.EqualTo(1));
            Assert.That(mergedList2.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList2.Next.Next!.Value, Is.EqualTo(5));
        }

        // Testing SortAndMerge method when both lists are empty
        [Test]
        public void SortAndMerge_BothListsEmpty_ReturnsNull()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            LList<int> list2 = new LList<int>();

            // Act
            var mergedList = list1.SortAndMerge(list1.First, list2.First);

            // Assert
            Assert.That(mergedList, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_MergeSort
    {
        [Test]
        public void MergeSort_AscendingOrder_UniqueValues()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(1);
            list1.AddLast(5);
            list1.AddLast(3);
            list1.AddLast(2);
            list1.AddLast(6);
            list1.AddLast(4);

            var mergedList = list1.MergeSort(list1.First, ascendingOrder: true);

            Assert.That(mergedList, !Is.Null);
            Assert.That(mergedList.Value, Is.EqualTo(1));
            Assert.That(mergedList.Next!.Previous!.Value, Is.EqualTo(1));
            Assert.That(mergedList.Next!.Value, Is.EqualTo(2));
            Assert.That(mergedList.Next.Next!.Previous!.Value, Is.EqualTo(2));
            Assert.That(mergedList.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next!.Previous!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next!.Value, Is.EqualTo(4));
            Assert.That(mergedList.Next.Next.Next.Next!.Previous!.Value, Is.EqualTo(4));
            Assert.That(mergedList.Next.Next.Next.Next!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Previous!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Value, Is.EqualTo(6));
        }

        [Test]
        public void MergeSort_AscendingOrder_NonUniqueValues()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(1);
            list1.AddLast(5);
            list1.AddLast(3);
            list1.AddLast(1);
            list1.AddLast(6);
            list1.AddLast(3);

            var mergedList = list1.MergeSort(list1.First, ascendingOrder: true);

            Assert.That(mergedList, !Is.Null);
            Assert.That(mergedList.Value, Is.EqualTo(1));
            Assert.That(mergedList.Next!.Value, Is.EqualTo(1));
            Assert.That(mergedList.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next.Next!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Value, Is.EqualTo(6));
        }

        [Test]
        public void MergeSort_DescendingOrder_UniqueValues()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(1);
            list1.AddLast(5);
            list1.AddLast(3);
            list1.AddLast(2);
            list1.AddLast(6);
            list1.AddLast(4);

            var mergedList = list1.MergeSort(list1.First, ascendingOrder: false);

            Assert.That(mergedList, !Is.Null);
            Assert.That(mergedList.Value, Is.EqualTo(6));
            Assert.That(mergedList.Next!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next!.Previous!.Value, Is.EqualTo(6));
            Assert.That(mergedList.Next.Next!.Value, Is.EqualTo(4));
            Assert.That(mergedList.Next.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next.Next!.Value, Is.EqualTo(2));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Value, Is.EqualTo(1));
        }

        [Test]
        public void MergeSort_DescendingOrder_NonUniqueValues()
        {
            // Arrange
            LList<int> list1 = new LList<int>();
            list1.AddLast(1);
            list1.AddLast(5);
            list1.AddLast(3);
            list1.AddLast(1);
            list1.AddLast(6);
            list1.AddLast(3);

            var mergedList = list1.MergeSort(list1.First, ascendingOrder: false);

            Assert.That(mergedList, !Is.Null);
            Assert.That(mergedList.Value, Is.EqualTo(6));
            Assert.That(mergedList.Next!.Value, Is.EqualTo(5));
            Assert.That(mergedList.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next!.Previous!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next!.Value, Is.EqualTo(3));
            Assert.That(mergedList.Next.Next.Next.Next!.Value, Is.EqualTo(1));
            Assert.That(mergedList.Next.Next.Next.Next.Next!.Value, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class LListTests_BinarySearch
    {
        [Test]
        public void BinarySearch_ReturnsNodeWithValue()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(4);
            list.AddLast(4);
            list.AddLast(5);

            // Act
            var foundNode = list.BinarySearch(2);

            // Assert
            Assert.That(foundNode!.Value, Is.EqualTo(2));
        }

        // Testing BinarySearch method for finding the last occurrence
        [Test]
        public void BinarySearch_Unsorted()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(2);
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(4);
            list.AddLast(4);
            list.AddLast(5);
            list.AddLast(4);
            list.AddLast(3);

            // Act
            var foundNode = list.BinarySearch(4);

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(foundNode!.Value, Is.EqualTo(4));
        }

        // Testing BinarySearch method for non-existing value
        [Test]
        public void BinarySearch_NonExistingValue_ReturnsNull()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            // Act
            var foundNode = list.BinarySearch(6);

            // Assert
            Assert.That(foundNode, Is.Null);
        }

        // Testing BinarySearch method for an empty list
        [Test]
        public void BinarySearch_EmptyList_ReturnsNull()
        {
            // Arrange
            LList<int> list = new LList<int>();

            // Act
            var foundNode = list.BinarySearch(1);

            // Assert
            Assert.That(foundNode, Is.Null);
        }
    }

    [TestFixture]
    public class LListTests_MinMax
    {
        [Test]
        public void Min_ReturnsNodeWithValue()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(15);
            list.AddLast(10);
            list.AddLast(5);
            list.AddLast(1);
            list.AddLast(2);

            var max = list.Min();

            // Assert
            Assert.That(max!.Value, Is.EqualTo(1));
        }

        [Test]
        public void Min_ListUntouched()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(22);
            list.AddLast(10);
            list.AddLast(1);
            list.AddLast(15);
            list.AddLast(5);

            var max = list.Min();

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(22));
            Assert.That(max!.Value, Is.EqualTo(1));
        }

        [Test]
        public void Max_ReturnsNodeWithValue()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(10);
            list.AddLast(5);
            list.AddLast(15);
            list.AddLast(2);

            var max = list.Max();

            // Assert
            Assert.That(max!.Value, Is.EqualTo(15));
        }

        [Test]
        public void Max_ListUntouched()
        {
            // Arrange
            LList<int> list = new LList<int>();
            list.AddLast(1);
            list.AddLast(10);
            list.AddLast(5);
            list.AddLast(15);
            list.AddLast(22);

            var max = list.Max();

            // Assert
            Assert.That(list.First!.Value, Is.EqualTo(1));
            Assert.That(max!.Value, Is.EqualTo(22));
        }
    }

    [TestFixture]
    public class LListTests_QuickSort
    {
        // // Testing QuickSort method for sorting a list in ascending order
        // [Test]
        // public void QuickSort_AscendingOrder_ReturnsSortedLinkedList()
        // {
        //     // Arrange
        //     LList<int> list = new LList<int>();
        //     list.AddLast(3);
        //     list.AddLast(2);
        //     list.AddLast(1);
        //     list.AddLast(5);
        //     list.AddLast(4);

        //     // Act
        //     var sortedList = list.QuickSort();

        //     // Assert
        //     Assert.That(sortedList!.Value, Is.EqualTo(1));
        //     Assert.That(sortedList.Next!.Value, Is.EqualTo(2));
        //     Assert.That(sortedList.Next.Next!.Value, Is.EqualTo(3));
        //     Assert.That(sortedList.Next.Next.Next!.Value, Is.EqualTo(4));
        //     Assert.That(sortedList.Next.Next.Next.Next!.Value, Is.EqualTo(5));
        // }

        // // Testing QuickSort method for sorting a list in descending order
        // [Test]
        // public void QuickSort_DescendingOrder_ReturnsSortedLinkedList()
        // {
        //     // Arrange
        //     LList<int> list = new LList<int>();
        //     list.AddLast(3);
        //     list.AddLast(2);
        //     list.AddLast(1);
        //     list.AddLast(5);
        //     list.AddLast(4);

        //     // Act
        //     var sortedList = list.QuickSort();
        //     var currentNode = sortedList;

        //     // Move to the last node
        //     while (currentNode.Next != null)
        //     {
        //         currentNode = currentNode.Next;
        //     }

        //     // Assert
        //     Assert.That(currentNode.Value, Is.EqualTo(5));
        //     Assert.That(currentNode.Previous!.Value, Is.EqualTo(4));
        //     Assert.That(currentNode.Previous.Previous!.Value, Is.EqualTo(3));
        //     Assert.That(currentNode.Previous.Previous.Previous!.Value, Is.EqualTo(2));
        //     Assert.That(currentNode.Previous.Previous.Previous.Previous!.Value, Is.EqualTo(1));
        // }

        // // Testing QuickSort method for an empty list
        // [Test]
        // public void QuickSort_EmptyList_ReturnsNull()
        // {
        //     // Arrange
        //     LList<int> list = new LList<int>();

        //     // Act
        //     var sortedList = list.QuickSort();

        //     // Assert
        //     Assert.That(sortedList, Is.Null);
        // }
    }



}