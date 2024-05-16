namespace DSA.AVLTrees.Tests;

public class AVLNode_Tests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenValueIsNull()
        {
            // Arrange
            AVLNode<string> node = new(null!);

            // Act & Assert
            Assert.That(node, !Is.Null);
            Assert.That(node.Value, Is.Null);
        }

        [Test]
        public void Constructor_ShouldInitializeNodeWithCustomType()
        {
            // Arrange
            var customObject = new CustomClass { Value = "Test" };

            // Act
            AVLNode<CustomClass> node = new AVLNode<CustomClass>(customObject);

            // Assert
            Assert.That(node.Value, Is.EqualTo(customObject));
        }

        public class CustomClass : IComparable<CustomClass>
        {
            public required string Value { get; set; }

            public int CompareTo(CustomClass? other)
            {
                return string.Compare(Value, other!.Value, StringComparison.Ordinal);
            }
        }
    }
}

public class AVLTree_Tests
{
    [TestFixture]
    public class Constructor
    {
        [Test]
        public void DefaultConstructor_ShouldCreateEmptyTree()
        {
            // Act
            AVLTree<int> tree = new AVLTree<int>();

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(0));
        }

        [Test]
        public void ValueConstructor_ShouldCreateTreeWithRootNode()
        {
            // Arrange
            int expectedValue = 42;

            // Act
            AVLTree<int> tree = new AVLTree<int>(expectedValue);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(expectedValue));
            Assert.That(tree.Root.Left, Is.Null);
            Assert.That(tree.Root.Right, Is.Null);
            Assert.That(tree.Root.Tree, Is.SameAs(tree));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void ValueConstructor_ShouldCreateTreeWithCustomType()
        {
            // Arrange
            var customObject = new CustomClass { Value = "Test" };

            // Act
            AVLTree<CustomClass> tree = new AVLTree<CustomClass>(customObject);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(customObject));
        }

        [Test]
        public void NodeConstructor_ShouldThrowArgumentNullExceptionWhenRootNodeIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AVLTree<int>(null!));
        }

        [Test]
        public void ValueConstructor_NullValue()
        {
            Assert.Throws<ArgumentNullException>(() => new AVLTree<int>(null!));
        }

        [Test]
        public void EnumerableConstructor_NullCollection()
        {
            int[] collection = null!;

            Assert.Throws<ArgumentNullException>(() => new AVLTree<int>(collection));
        }

        [Test]
        public void EnumerableConstructor_NullValueInCollection()
        {
            string[] collection = [ "a", "b", null! ];

            Assert.Throws<ArgumentNullException>(() => new AVLTree<string>(collection));
        }

        [Test]
        public void Enumerator()
        {
            AVLTree<int> tree = new([ 1, 2, 3, 4, 5 ]);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                foreach (var item in tree)
                {
                    Console.Write(item + ", ");
                }

                string output = sw.ToString().Trim();

                // Assert
                Assert.That(output, Is.EqualTo("1, 2, 3, 4, 5,"));
            }
        }

        public class CustomClass : IComparable<CustomClass>
        {
            public required string Value { get; set; }

            public int CompareTo(CustomClass? other)
            {
                return string.Compare(Value, other!.Value, StringComparison.Ordinal);
            }
        }
    }

    [TestFixture]
    public class Insert
    {
        [Test]
        public void Insert_NullValue()
        {
            AVLTree<string> tree = new();
            string temp = null!;

            Assert.Throws<ArgumentNullException>(() => tree.Insert(temp));
        }

        [Test]
        public void InsertNode_NullNode()
        {
            AVLTree<int> tree = new();

            Assert.Throws<ArgumentNullException>(() => tree.Insert(null!));
        }

        [Test]
        public void InsertNode_NullValueInNode()
        {
            AVLTree<string> tree = new("hello");
            AVLNode<string> node = new(null!);

            Assert.Throws<ArgumentNullException>(() => tree.Insert(node));
        }

        [Test]
        public void InsertOneItem()
        {
            AVLTree<int> tree = new();
            tree.Insert(1);

            Assert.That(tree.Root, !Is.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(1));
            Assert.That(tree, Has.Count.EqualTo(1)); 
        }

        [Test]
        public void InsertThreeItems_RequiresLeftRotation()
        {
            AVLTree<int> tree = new();
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);

            Assert.That(tree.Root, !Is.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(2));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(3));
            Assert.That(tree, Has.Count.EqualTo(3)); 
        }

        [Test]
        public void InsertThreeItems_RequiresRightRotation()
        {
            AVLTree<int> tree = new();
            tree.Insert(3);
            tree.Insert(2);
            tree.Insert(1);

            // Arrange
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                AVLTree<int>.PrintInorder(tree.Root);
                string output = sw.ToString().Trim();

                // Assert
                Assert.That(output, Is.EqualTo("1, 2, 3,"));
            }

            Assert.That(tree, Has.Count.EqualTo(3));
            Assert.That(tree.Root!.Value, Is.EqualTo(2));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(3));
        }

        [Test]
        public void Insert_RequiresLeftRightRotation()
        {
            AVLTree<int> tree = new();
            tree.Insert(10);
            tree.Insert(11);
            tree.Insert(5);
            tree.Insert(1);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(9);

            Assert.That(tree, Has.Count.EqualTo(7));
            Assert.That(tree.Root!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(5));
            Assert.That(tree.Root.Left.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(9));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(11));
        }
    
        [Test]
        public void Insert_RequiresRightLeftRotation()
        {
            AVLTree<int> tree = new([ 10, 7, 14, 15, 12, 11, 13 ]);

            Assert.That(tree, Has.Count.EqualTo(7));
            Assert.That(tree.Root!.Value, Is.EqualTo(12));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left.Left!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(11));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(14));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(13));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(15));
        }
    
        [Test]
        public void Insert_DuplicateValue()
        {
            AVLTree<int> tree = new(1);

            Assert.Throws<InvalidOperationException>(() => tree.Insert(1));
        }

        [Test]
        public void Insert_DuplicateNode()
        {
            AVLTree<int> tree = new(1);

            Assert.Throws<InvalidOperationException>(() => tree.Insert(new AVLNode<int>(1)));
        }

        [Test]
        public void Insert_NodeAlreadyAssigned()
        {
            AVLTree<int> tree1 = new();
            AVLNode<int> node = new(1);
            tree1.Insert(node);

            AVLTree<int> tree2 = new();
            tree2.Insert(node);
            
            Assert.That(tree1, Has.Count.EqualTo(1));
            Assert.That(tree2, Has.Count.EqualTo(1));
            Assert.That(tree1.Root, Is.EqualTo(tree2.Root));
        }
    
        [Test]
        public void TryInsertValue_NullValue()
        {
            AVLTree<string> tree = new();

            bool inserted = tree.TryInsert(null!, out var newNode);

            Assert.That(inserted, Is.False);
            Assert.That(newNode, Is.Null);
            Assert.That(tree, Has.Count.EqualTo(0));
        }

        [Test]
        public void TryInsertValue()
        {
            AVLTree<int> tree = new();
            bool inserted = tree.TryInsert(1, out var node);

            Assert.That(inserted, Is.True);
            Assert.That(node.Value, Is.EqualTo(1));
            Assert.That(tree, Has.Count.EqualTo(1));
        }

        [Test]
        public void TryInsertNode_NullNode()
        {
            AVLTree<int> tree = new();
            AVLNode<int> node = null!;
            bool inserted = tree.TryInsert(node);

            Assert.That(inserted, Is.False);
            Assert.That(tree, Has.Count.EqualTo(0));
        }

        [Test]
        public void TryInsertNode_NullValueInNode()
        {
            AVLTree<string> tree = new();
            AVLNode<string> node = null!;
            bool inserted = tree.TryInsert(node);

            Assert.That(inserted, Is.False);
            Assert.That(tree, Has.Count.EqualTo(0));
        }

        [Test]
        public void TryInsertNode()
        {
            AVLTree<int> tree = new();
            bool inserted = tree.TryInsert(new AVLNode<int>(1));

            Assert.That(inserted, Is.True);
            Assert.That(tree.Root!.Value, Is.EqualTo(1));
            Assert.That(tree, Has.Count.EqualTo(1));
        }
    }

    [TestFixture]
    public class Delete
    {
        [Test]
        public void EmptyTree()
        {
            AVLTree<int> tree = new();

            Assert.Throws<InvalidOperationException>(() => tree.Delete(1));
        }

        [Test]
        public void NullValue()
        {
            AVLTree<string> tree = new("a");
            string temp = null!;

            Assert.Throws<ArgumentNullException>(() => tree.Delete(temp));
        }

        [Test]
        public void NullNode()
        {
            AVLTree<int> tree = new(1);
            AVLNode<int> node = null!;

            Assert.Throws<ArgumentNullException>(() => tree.Delete(node));
        }

        [Test]
        public void NodeNotAssigned()
        {
            AVLTree<int> tree = new(1);
            AVLNode<int> node = new(1);
            tree.Delete(node);
            
            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
        }

        [Test]
        public void OneItem()
        {
            AVLTree<int> tree = new(1);
            tree.Delete(1);

            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
        }

        [Test]
        public void TwoItem_DeletingRoot()
        {
            AVLTree<int> tree = new();
            tree.Insert(1);
            tree.Insert(2);
            tree.Delete(1);

            Assert.That(tree, Has.Count.EqualTo(1));
            Assert.That(tree.Root, !Is.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(2));
        }

        [Test]
        public void DeleteRootNode_RequiresLeftRotation()
        {
            AVLTree<int> tree = new([ 5, 1, 8, 6, 10 ]);

            Assert.That(tree, Has.Count.EqualTo(5));
            Assert.That(tree.Root!.Value, Is.EqualTo(5));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(10));

            tree.Delete(5);

            Assert.That(tree, Has.Count.EqualTo(4));
            Assert.That(tree.Root!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(10));
        }

        [Test]
        public void DeleteRootNode_RequiresRightRotation()
        {
            AVLTree<int> tree = new([ 5, 3, 7, 1, 4 ]);

            Assert.That(tree, Has.Count.EqualTo(5));
            Assert.That(tree.Root!.Value, Is.EqualTo(5));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(3));
            Assert.That(tree.Root.Left.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(4));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(7));

            tree.Delete(5);

            Assert.That(tree, Has.Count.EqualTo(4));
            Assert.That(tree.Root!.Value, Is.EqualTo(3));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(4));
        }

        [Test]
        public void DeleteLeftNode_RequiresLeftRotation()
        {
            AVLTree<int> tree = new([ 5, 1, 7, 6, 10 ]);

            Assert.That(tree, Has.Count.EqualTo(5));
            Assert.That(tree.Root!.Value, Is.EqualTo(5));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(10));

            tree.Delete(1);

            Assert.That(tree, Has.Count.EqualTo(4));
            Assert.That(tree.Root!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(5));
            Assert.That(tree.Root.Left!.Right!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(10));
        }
    
        [Test]
        public void Delete_ValueNotInTree()
        {
            AVLTree<int> tree = new();
            tree.Insert(1);
            tree.Delete(2);

            Assert.That(tree, Has.Count.EqualTo(1));
            Assert.That(tree.Root!.Value, Is.EqualTo(1));
        }

        [Test]
        public void InsertNode_DeleteValue()
        {
            AVLTree<int> tree = new();
            AVLNode<int> node = new(1);
            tree.Insert(node);
            tree.Delete(1);

            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
            Assert.That(node, !Is.Null);
            Assert.That(node.Tree, Is.Null);
        }

        [Test]
        public void TryDeleteValue_EmptyTree()
        {
            AVLTree<int> tree = new();
            bool removed = tree.TryDelete(1);
           
            Assert.That(removed, Is.False);
            Assert.That(tree, Has.Count.EqualTo(0));
        }

        [Test]
        public void TryDeleteValue_NullValue()
        {
            AVLTree<string> tree = new("a");
            string temp = null!;
            bool removed = tree.TryDelete(temp);

            Assert.That(removed, Is.False);
            Assert.That(tree, Has.Count.EqualTo(1));
        }

        [Test]
        public void TryDeleteNode_EmptyTree()
        {
            AVLTree<int> tree = new();
            bool removed = tree.TryDelete(new AVLNode<int>(1));

            Assert.That(removed, Is.False);
            Assert.That(tree, Has.Count.EqualTo(0));
        }

        [Test]
        public void TryDeleteNode_NullNode()
        {
            AVLTree<int> tree = new(1);
            AVLNode<int> node = null!;
            bool removed = tree.TryDelete(node);

            Assert.That(removed, Is.False);
            Assert.That(tree, Has.Count.EqualTo(1));
        }

        [Test]
        public void TryDeleteNode_NullValueInNode()
        {
            AVLTree<string> tree = new("a");
            AVLNode<string> node = new(null!);
            bool removed = tree.TryDelete(node);

            Assert.That(removed, Is.False);
            Assert.That(tree, Has.Count.EqualTo(1));
        }

        [Test]
        public void TryDeleteValue_ValueNotInTree()
        {
            AVLTree<int> tree = new();
            bool removed = tree.TryDelete(1);

            Assert.That(removed, Is.False);
        }

        [Test]
        public void TryDeleteValue_ValueInTree()
        {
            AVLTree<int> tree = new(1);
            bool removed = tree.TryDelete(1);

            Assert.That(removed, Is.True);
            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
        }
        
        [Test]
        public void InsertNode_TryDeleteValue()
        {
            AVLTree<int> tree = new();
            AVLNode<int> node = new(1);
            tree.Insert(node);
            bool removed = tree.TryDelete(1);

            Assert.That(removed, Is.True);
            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
            Assert.That(node, !Is.Null);
            Assert.That(node.Tree, Is.Null);
        }
    
        [Test]
        public void DeleteNode_EmptyTree()
        {
            AVLTree<int> tree = new();

            Assert.Throws<InvalidOperationException>(() => tree.Delete(new AVLNode<int>(1)));
        }

        [Test]
        public void DeleteNode_NullNode()
        {
            AVLTree<int> tree = new(1);

            Assert.Throws<ArgumentNullException>(() => tree.Delete(null));
        }

        [Test]
        public void DeleteNode_OneNodeInTree()
        {
            AVLTree<int> tree = new(1);
            tree.Delete(tree.Root);

            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
        }

        [Test]
        public void DeleteNode_TwoNode_DeletingRootNode()
        {
            AVLTree<int> tree = new([1, 2]);
            tree.Delete(tree.Root);

            Assert.That(tree, Has.Count.EqualTo(1));
            Assert.That(tree.Root!.Value, Is.EqualTo(2));
        }

        [Test]
        public void DeleteNode_RequiresLeftRotation_DeletingRootNode()
        {
            AVLTree<int> tree = new([ 5, 1, 8, 6, 10 ]);

            Assert.That(tree, Has.Count.EqualTo(5));
            Assert.That(tree.Root!.Value, Is.EqualTo(5));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(10));

            tree.Delete(tree.Root);

            Assert.That(tree, Has.Count.EqualTo(4));
            Assert.That(tree.Root!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(10));
        }

        [Test]
        public void TryDeleteNode_ValueNotInTree()
        {
            AVLTree<int> tree = new();
            bool removed = tree.TryDelete(new AVLNode<int>(1));

            Assert.That(removed, Is.False);
        }

        [Test]
        public void TryDeleteNode_ValueInTree_ButNodeNotAssigned()
        {
            AVLTree<int> tree = new(1);
            bool removed = tree.TryDelete(new AVLNode<int>(1));

            Assert.That(removed, Is.True);
            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
        }
        
        [Test]
        public void TryDeleteNode_NodeIsUnassigned()
        {
            AVLTree<int> tree = new();
            AVLNode<int> node = tree.Insert(1);

            AVLTree<int> reference = node.Tree!;
            bool removed = tree.TryDelete(node);

            Assert.That(tree.Root, Is.Null);
            Assert.That(removed, Is.True);
            Assert.That(reference, Is.EqualTo(tree));
            Assert.That(node.Tree, Is.Null);
            Assert.That(tree, Has.Count.EqualTo(0));
        }
    
        [Test]
        public void Clear()
        {
            AVLTree<int> tree = new([ 1, 2, 3, 4, 5 ]);

            tree.Clear();

            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(tree.Root, Is.Null);
        }

        [Test]
        public void Clear_NodesCleared()
        {
            AVLTree<int> tree = new();
            AVLNode<int> node = new(1);

            tree.Insert(node);
            AVLTree<int> reference = node.Tree!;

            tree.Clear();
            
            Assert.That(tree, Has.Count.EqualTo(0));
            Assert.That(reference, Is.EqualTo(tree));
            Assert.That(node, !Is.Null);
            Assert.That(node.Tree, Is.Null);
        }
    }

    [TestFixture]
    public class Find
    {
        [Test]
        public void Find_EmptyTree()
        {
            AVLTree<int> tree = new();

            Assert.Throws<InvalidOperationException>(() => tree.Find(1));
        }

        [Test]
        public void Find_NullValue()
        {
            AVLTree<string> tree = new("a");

            Assert.Throws<ArgumentNullException>(() => tree.Find(null!));
        }

        [Test]
        public void Find_ValueNotInTree()
        {
            AVLTree<int> tree = new(1);
            AVLNode<int>? node = tree.Find(2);

            Assert.That(node, Is.Null);
        }

        [Test]
        public void Find_ValueInTree()
        {
            AVLTree<int> tree = new();
            AVLNode<int> node = new(3);
            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(node);

            AVLNode<int> found = tree.Find(3)!;

            Assert.That(found, Is.EqualTo(node));
        }

        [Test]
        public void TryFind_EmptyTree()
        {
            AVLTree<int> tree = new();
            bool found = tree.TryFind(1, out var node);

            Assert.That(found, Is.False);
            Assert.That(node, Is.Null);
        }

        [Test]
        public void TryFind_NullValue()
        {
            AVLTree<string> tree = new("a");
            bool found = tree.TryFind(null!, out var node);

            Assert.That(found, Is.False);
            Assert.That(node, Is.Null);
        }

        [Test]
        public void TryFind_ValueFound()
        {
            AVLTree<int> tree = new([ 1, 2, 3 ]);
            bool found = tree.TryFind(3, out var node);

            Assert.That(tree, Has.Count.EqualTo(3));
            Assert.That(tree.Root!.Value, Is.EqualTo(2));
            Assert.That(found, Is.True);
            Assert.That(node, Is.EqualTo(tree.Root!.Right));
        }
    }

}