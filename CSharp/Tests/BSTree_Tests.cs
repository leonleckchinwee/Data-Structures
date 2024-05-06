namespace DSA.BSTrees.Tests;

public class BSTrees_Tests
{
    [TestFixture]
    public class BSTreeTests_Constructors
    {
        [Test]
        public void Constructor_WithoutParameters_InitializesEmptyTree()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(0));
        }

        [TestCase(1)]
        [TestCase(-5)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void Constructor_WithValue_InitializesTreeWithRootNode(int value)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(value);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(value));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_WithNullValue_DoesNotThrowArgumentNullException()
        {
            // Arrange & Act
            BSTree<string> tree = new BSTree<string>(null!);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void Constructor_WithDefaultStruct_InitializesTreeWithDefaultValue()
        {
            // Arrange
            BSTree<DateTime> tree = new BSTree<DateTime>(default(DateTime));

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(default(DateTime)));
            Assert.That(tree.Count, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class BSTreeTests_Insert
    {
        [Test]
        public void Insert_ValueInEmptyTree_SetsRootAndIncrementsCount()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            int value = 42;

            // Act
            tree.Insert(value);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(value));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void Insert_DuplicateValue_DoesNotInsertDuplicate()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            int value = 10;

            // Assert
            Assert.That(() => tree.Insert(value), Throws.InvalidOperationException);
        }

        [TestCase(5, 10, 15)]
        [TestCase(10, 20, 30)]
        public void Insert_MultipleValues_InsertsInCorrectOrder(int value1, int value2, int value3)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(value2);

            // Act
            tree.Insert(value1);
            tree.Insert(value3);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(value2));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(value1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(value3));
            Assert.That(tree.Count, Is.EqualTo(3));
        }

        [TestCase(5, 10, 15)]
        [TestCase(10, 20, 30)]
        public void Insert_MultipleValues_ReturnsCorrectNewNode(int value1, int value2, int value3)
        {
            BSTree<int> tree = new BSTree<int>(value2);

            // Act
            BSTreeNode<int> node1 = tree.Insert(value1);
            BSTreeNode<int> node2 = tree.Insert(value3);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(value2));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(value1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(value3));
            Assert.That(node1.Value, Is.EqualTo(value1));
            Assert.That(node2.Value, Is.EqualTo(value3));
            Assert.That(tree.Count, Is.EqualTo(3));
        }
    
        [Test]
        public void Insert_DuplicateNode_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            BSTreeNode<int> duplicateNode = new BSTreeNode<int>(10);

            // Act & Assert
            Assert.That(() => tree.Insert(duplicateNode), Throws.InvalidOperationException);
        }
    
        [Test]
        public void Insert_NodeFromAnotherTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree1 = new BSTree<int>(10);
            BSTree<int> tree2 = new BSTree<int>(20);
            BSTreeNode<int> nodeFromAnotherTree = tree2.Root!;

            // Act & Assert
            Assert.That(() => tree1.Insert(nodeFromAnotherTree), Throws.InvalidOperationException);
        }

        [Test]
        public void Insert_NewNodeIntoEmptyTree_SetsRootAndIncrementsCount()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            BSTreeNode<int> newNode = new BSTreeNode<int>(42);

            // Act
            tree.Insert(newNode);

            // Assert
            Assert.That(tree.Root, Is.SameAs(newNode));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void Insert_NewNodeIntoNonEmptyTree_InsertsNodeCorrectly()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            BSTreeNode<int> newNode = new BSTreeNode<int>(5);

            // Act
            tree.Insert(newNode);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left, Is.SameAs(newNode));
            Assert.That(tree.Count, Is.EqualTo(2));
        }

    }

    [TestFixture]
    public class BSTreeTests_Remove
    {
        [Test]
        public void Remove_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            int value = 42;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Remove(value));
        }

        [Test]
        public void Remove_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            BSTree<string> tree = new BSTree<string>("hello");
            string value = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Remove(value));
        }

        [Test]
        public void Remove_ValueNotInTree_ReturnsFalse()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            int value = 20;

            // Act
            bool removed = tree.Remove(value);

            // Assert
            Assert.That(removed, Is.False);
            Assert.That(tree.Count, Is.EqualTo(3));
        }

        [TestCase(10, 20, ExpectedResult = true)]
        [TestCase(50, 25, ExpectedResult = true)]
        [TestCase(-5, 0, ExpectedResult = true)]
        public bool Remove_ValueInTree_ReturnsTrue(int rootValue, int valueToRemove)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(valueToRemove);

            // Act
            bool removed = tree.Remove(valueToRemove);

            // Assert
            return removed;
        }

        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue01()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(6);
            tree.Insert(10);

            // Act
            bool removed = tree.Remove(10);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(2));
            Assert.That(tree.Root!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(6));
            Assert.That(tree.Root.Right, Is.Null);
        }

        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue02()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(6);
            tree.Insert(10);
            tree.Insert(4);

            // Act
            bool removed = tree.Remove(6);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(3));
            Assert.That(tree.Root!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(4));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left.Left, Is.Null);
        }

        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue03()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(6);
            tree.Insert(10);
            tree.Insert(7);

            // Act
            bool removed = tree.Remove(6);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(3));
            Assert.That(tree.Root!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left.Right, Is.Null);
        }

        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue04()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(6);
            tree.Insert(10);
            tree.Insert(4);
            tree.Insert(7);

            // Act
            bool removed = tree.Remove(6);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(4));
            Assert.That(tree.Root!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(4));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Left.Left, Is.Null);
        }
    
        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue05()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(10);
            tree.Insert(9);
            tree.Insert(11);

            // Act
            bool removed = tree.Remove(10);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(3));
            Assert.That(tree.Root!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Left, Is.Null);
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(9));
            Assert.That(tree.Root.Right.Left, Is.Null);
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(11));
        }

        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue06()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(10);
            tree.Insert(9);
            tree.Insert(11);

            // Act
            bool removed = tree.Remove(8);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(3));
            Assert.That(tree.Root!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(9));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(11));
            Assert.That(tree.Root.Right.Left, Is.Null);
        }

        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue07()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(5);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(6);

            // Act
            bool removed = tree.Remove(5);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(4));
            Assert.That(tree.Root!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(2));
            Assert.That(tree.Root.Left.Left, Is.Null);
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(7));
            Assert.That(tree.Root.Left.Right.Left!.Value, Is.EqualTo(6));
        }
    
        [Test]
        public void Remove_RemoveRootNode_ReturnsTrue08()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);
            tree.Insert(5);
            tree.Insert(2);
            tree.Insert(7);
            tree.Insert(1);
            tree.Insert(3);

            // Act
            bool removed = tree.Remove(5);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(5));
            Assert.That(tree.Root!.Value, Is.EqualTo(8));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(3));
            Assert.That(tree.Root.Left.Left!.Value, Is.EqualTo(2));
            Assert.That(tree.Root.Left.Left.Left!.Value, Is.EqualTo(1));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(7));
        }
    }

    [TestFixture]
    public class BSTreeTests_TryRemove
    {
        [Test]
        public void TryRemove_EmptyTree_ReturnsFalse()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            int value = 42;

            // Act
            bool removed = tree.TryRemove(value);

            // Assert
            Assert.That(removed, Is.False);
        }

        [Test]
        public void TryRemove_NullValue_ReturnsFalse()
        {
            // Arrange
            BSTree<string> tree = new BSTree<string>("hello");
            string value = null!;

            // Act
            bool removed = tree.TryRemove(value);

            // Assert
            Assert.That(removed, Is.False);
        }

        [Test]
        public void TryRemove_ValueNotInTree_ReturnsFalse()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            int value = 20;

            // Act
            bool removed = tree.TryRemove(value);

            // Assert
            Assert.That(removed, Is.False);
            Assert.That(tree.Count, Is.EqualTo(3));
        }

        [TestCase(10, 20, ExpectedResult = true)]
        [TestCase(50, 25, ExpectedResult = true)]
        [TestCase(-5, 0, ExpectedResult = true)]
        public bool TryRemove_ValueInTree_ReturnsTrue(int rootValue, int valueToRemove)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(valueToRemove);

            // Act
            bool removed = tree.TryRemove(valueToRemove);

            // Assert
            return removed;
        }

        [Test]
        public void TryRemove_RemoveRootValue_UpdatesRootAndCount()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);

            // Act
            bool removed = tree.TryRemove(10);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(2));
            Assert.That(tree.Root!.Value, Is.EqualTo(5));
        }

        [Test]
        public void TryRemove_EmptyTree_NodeIsNull_ReturnsFalse()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            BSTreeNode<int> node = null!;

            // Act
            bool removed = tree.TryRemove(node);

            // Assert
            Assert.That(removed, Is.False);
        }

        [Test]
        public void TryRemove_NonEmptyTree_NodeIsNull_ReturnsFalse()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            BSTreeNode<int> node = null!;

            // Act
            bool removed = tree.TryRemove(node);

            // Assert
            Assert.That(removed, Is.False);
        }

        [Test]
        public void TryRemove_NodeNotInTree_ReturnsFalse()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            BSTreeNode<int> node = new BSTreeNode<int>(20);

            // Act
            bool removed = tree.TryRemove(node);

            // Assert
            Assert.That(removed, Is.False);
            Assert.That(tree.Count, Is.EqualTo(3));
        }

        [TestCase(10, 5, ExpectedResult = true)]
        [TestCase(50, 25, ExpectedResult = true)]
        [TestCase(-5, 0, ExpectedResult = true)]
        public bool TryRemove_NodeInTree_ReturnsTrue(int rootValue, int nodeValue)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            BSTreeNode<int> node = new BSTreeNode<int>(nodeValue);
            tree.Insert(node);

            // Act
            bool removed = tree.TryRemove(node);

            // Assert
            return removed;
        }

        [Test]
        public void TryRemove_RemoveRootNode_UpdatesRootAndCount()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            BSTreeNode<int> rootNode = tree.Root!;

            // Act
            bool removed = tree.TryRemove(rootNode);

            // Assert
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(2));
            Assert.That(tree.Root!.Value, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class BSTreeTests_Clear
    {
        [Test]
        public void Clear_EmptyTree_TreeRemainsEmpty()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act
            tree.Clear();

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(0));
        }

        [Test]
        public void Clear_SingleNodeTree_TreeBecomesEmpty()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(42);

            // Act
            tree.Clear();

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(0));
        }

        [Test]
        public void Clear_NonEmptyTree_TreeBecomesEmpty()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(12);
            tree.Insert(20);

            // Act
            tree.Clear();

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class BSTreeTests_Height
    {
        [Test]
        public void Height_EmptyTree_ReturnsEmpty()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act
            int height = tree.Height();

            // Assert
            Assert.That(height, Is.EqualTo(-1));
        }

        [Test]
        public void Height_SingleNodeTree_ReturnsZero()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(42);

            // Act
            int height = tree.Height();

            // Assert
            Assert.That(height, Is.EqualTo(0));
        }

        [TestCase(1, 2, 3, 4, 5, 6, 7, ExpectedResult = 2)]
        [TestCase(10, 5, 15, 3, 7, 12, 20, ExpectedResult = 4)]
        [TestCase(50, 25, 75, 12, 37, 62, 87, ExpectedResult = 4)]
        public int Height_BalancedTree_ReturnsCorrectHeight(int value1, int value2, int value3, int value4, int value5, int value6, int value7)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(value4);
            tree.Insert(value2);
            tree.Insert(value6);
            tree.Insert(value1);
            tree.Insert(value3);
            tree.Insert(value5);
            tree.Insert(value7);

            // Act
            int height = tree.Height();

            // Assert
            return height;
        }

        [Test]
        public void Height_UnbalancedTree_ReturnsCorrectHeight()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(8);

            // Act
            int height = tree.Height();

            // Assert
            Assert.That(height, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class BSTreeTests_Depth
    {
        [Test]
        public void GetDepth_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Depth(null, null));
        }

        [Test]
        public void GetDepth_TargetNodeIsRoot_ReturnsZero()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(42);
            BSTreeNode<int> root = tree.Root!;

            // Act
            int depth = tree.Depth(root, root);

            // Assert
            Assert.That(depth, Is.EqualTo(0));
        }

        [Test]
        public void GetDepth_TargetNodeNotInTree_ThrowsArgumentException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            BSTreeNode<int> nonExistentNode = new BSTreeNode<int>(42);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => tree.Depth(tree.Root, nonExistentNode));
        }

        [TestCase(8, 3, 2, 1, ExpectedResult = 3)]
        [TestCase(8, 3, 10, 2, ExpectedResult = 2)]
        [TestCase(8, 3, 2, 10, ExpectedResult = 1)]
        public int GetDepth_TargetNodeIsChildren_ReturnsCorrectDepth(int root, int one, int two, int three)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(root);
            BSTreeNode<int> node1 = tree.Insert(one);
            BSTreeNode<int> node2 = tree.Insert(two);
            BSTreeNode<int> node3 = tree.Insert(three);

            // Act
            int depth = tree.Depth(tree.Root, node3);

            // Assert
            return depth;
        }
    }

    [TestFixture]
    public class BSTreeTest_BalanceFactor
    {
        [Test]
        public void BalanceFactor_EmptyTree_ReturnsZero()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act
            int balanceFactor = tree.BalanceFactor();

            // Assert
            Assert.That(balanceFactor, Is.EqualTo(0));
        }

        [Test]
        public void BalanceFactor_SingleNodeTree_ReturnsZero()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(42);

            // Act
            int balanceFactor = tree.BalanceFactor();

            // Assert
            Assert.That(balanceFactor, Is.EqualTo(0));
        }

        [TestCase(10, 5, 15, ExpectedResult = 0)]
        [TestCase(50, 25, 75, 12, 37, ExpectedResult = 1)]
        [TestCase(10, 5, 15, 3, 7, 12, 20, ExpectedResult = 0)]
        public int BalanceFactor_BalancedTree_ReturnsCorrectFactor(int rootValue, int value1, int value2, int value3 = 0, int value4 = 0, int value5 = 0, int value6 = 0)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(value1);
            tree.Insert(value2);
            if (value3 != 0)
                tree.Insert(value3);
            if (value4 != 0)
                tree.Insert(value4);
            if (value5 != 0)
                tree.Insert(value5);
            if (value6 != 0)
                tree.Insert(value6);

            // Act
            int balanceFactor = tree.BalanceFactor();

            // Assert
            return balanceFactor;
        }

        [Test]
        public void BalanceFactor_UnbalancedTree_ReturnsCorrectFactor()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(6);
            tree.Insert(8);

            // Act
            int balanceFactor = tree.BalanceFactor();

            // Assert
            Assert.That(balanceFactor, Is.EqualTo(3));
        }
    }

    [TestFixture]
    public class BSTreeTests_Max
    {
        [Test]
        public void MaxValue_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.MaxValue());
        }

        [TestCase(42, ExpectedResult = 42)]
        [TestCase(-10, ExpectedResult = -10)]
        [TestCase(int.MaxValue, ExpectedResult = int.MaxValue)]
        public int MaxValue_SingleNodeTree_ReturnsRootValue(int value)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(value);

            // Act
            int maxValue = tree.MaxValue();

            // Assert
            return maxValue;
        }

        [TestCase(10, 5, 15, ExpectedResult = 15)]
        [TestCase(50, 25, 75, 12, 37, 62, 87, ExpectedResult = 87)]
        [TestCase(-5, -10, 0, 3, -8, -2, 5, ExpectedResult = 5)]
        public int MaxValue_NonEmptyTree_ReturnsMaxValue(int rootValue, int value1, int value2, int value3 = 0, int value4 = 0, int value5 = 0, int value6 = 0)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(value1);
            tree.Insert(value2);
            if (value3 != 0)
                tree.Insert(value3);
            if (value4 != 0)
                tree.Insert(value4);
            if (value5 != 0)
                tree.Insert(value5);
            if (value6 != 0)
                tree.Insert(value6);

            // Act
            int maxValue = tree.MaxValue();

            // Assert
            return maxValue;
        }
    
        [Test]
        public void MaxNode_EmptyTree_ReturnsNull()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => tree.MaxNode());
        }

        [Test]
        public void MaxNode_SingleNodeTree_ReturnsRootNode()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(42);

            // Act
            BSTreeNode<int>? maxNode = tree.MaxNode();

            // Assert
            Assert.That(maxNode, Is.SameAs(tree.Root));
        }

        [TestCase(10, 5, 15, ExpectedResult = 15)]
        [TestCase(50, 25, 75, 12, 37, 62, 87, ExpectedResult = 87)]
        [TestCase(-5, -10, 0, 3, -8, -2, 5, ExpectedResult = 5)]
        public int MaxNode_NonEmptyTree_ReturnsMaxNode(int rootValue, int value1, int value2, int value3 = 0, int value4 = 0, int value5 = 0, int value6 = 0)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(value1);
            tree.Insert(value2);
            if (value3 != 0)
                tree.Insert(value3);
            if (value4 != 0)
                tree.Insert(value4);
            if (value5 != 0)
                tree.Insert(value5);
            if (value6 != 0)
                tree.Insert(value6);

            // Act
            BSTreeNode<int>? maxNode = tree.MaxNode();

            // Assert
            return maxNode?.Value ?? default(int);
        }
    }

    [TestFixture]
    public class BSTreeTests_Min
    {
        [Test]
        public void MinValue_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.MinValue());
        }

        [TestCase(42, ExpectedResult = 42)]
        [TestCase(-10, ExpectedResult = -10)]
        [TestCase(int.MinValue, ExpectedResult = int.MinValue)]
        public int MinValue_SingleNodeTree_ReturnsRootValue(int value)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(value);

            // Act
            int minValue = tree.MinValue();

            // Assert
            return minValue;
        }

        [TestCase(10, 5, 15, ExpectedResult = 5)]
        [TestCase(50, 25, 75, 12, 37, 62, 87, ExpectedResult = 12)]
        [TestCase(-5, -10, 0, 3, -8, -2, 5, ExpectedResult = -10)]
        public int MinValue_NonEmptyTree_ReturnsMinValue(int rootValue, int value1, int value2, int value3 = 0, int value4 = 0, int value5 = 0, int value6 = 0)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(value1);
            tree.Insert(value2);
            if (value3 != 0)
                tree.Insert(value3);
            if (value4 != 0)
                tree.Insert(value4);
            if (value5 != 0)
                tree.Insert(value5);
            if (value6 != 0)
                tree.Insert(value6);

            // Act
            int minValue = tree.MinValue();

            // Assert
            return minValue;
        }

        [Test]
        public void MinNode_EmptyTree_ReturnsNull()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.MinValue());
        }

        [Test]
        public void MinNode_SingleNodeTree_ReturnsRootNode()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(42);

            // Act
            BSTreeNode<int>? minNode = tree.MinNode();

            // Assert
            Assert.That(minNode, Is.SameAs(tree.Root));
        }

        [TestCase(10, 5, 15, ExpectedResult = 5)]
        [TestCase(50, 25, 75, 12, 37, 62, 87, ExpectedResult = 12)]
        [TestCase(-5, -10, 0, 3, -8, -2, 5, ExpectedResult = -10)]
        public int MinNode_NonEmptyTree_ReturnsMinNode(int rootValue, int value1, int value2, int value3 = 0, int value4 = 0, int value5 = 0, int value6 = 0)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(value1);
            tree.Insert(value2);
            if (value3 != 0)
                tree.Insert(value3);
            if (value4 != 0)
                tree.Insert(value4);
            if (value5 != 0)
                tree.Insert(value5);
            if (value6 != 0)
                tree.Insert(value6);

            // Act
            BSTreeNode<int>? minNode = tree.MinNode();

            // Assert
            return minNode?.Value ?? default;
        }
    }

    [TestFixture]
    public class BSTreeTest_Predecessor
    {
        [Test]
        public void Predecessor_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            BSTreeNode<int> node = null!;


            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Predecessor(node));
        }

        [Test]
        public void Predecessor_NodeIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            BSTreeNode<int> node = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Predecessor(node));
        }

        [Test]
        public void Predecessor_RootNode_ReturnsNull()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            BSTreeNode<int> rootNode = tree.Root!;

            // Act
            BSTreeNode<int>? predecessor = tree.Predecessor(rootNode);

            // Assert
            Assert.That(predecessor, Is.Null);
        }

        [Test]
        public void Predecessor_LeafNode_ReturnsCorrectPredecessor()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(3);
            BSTreeNode<int> node2 = tree.Insert(6);
            BSTreeNode<int> node3 = tree.Insert(4);

            // Act
            BSTreeNode<int>? node4 = tree.Predecessor(node2);

            // Assert
            Assert.That(node4, Is.SameAs(node3));
        }

        [Test]
        public void Predecessor_LeafNode_ReturnsCorrectPredecessor2()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(3);
            BSTreeNode<int> node2 = tree.Insert(6);

            // Act
            BSTreeNode<int>? node4 = tree.Predecessor(node2);

            // Assert
            Assert.That(node4, Is.SameAs(node1));
        }

        [Test]
        public void Predecessor_LeafNode_ReturnsCorrectPredecessor3()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(10);
            BSTreeNode<int> node2 = tree.Insert(14);
            BSTreeNode<int> node3 = tree.Insert(13);

            // Act
            BSTreeNode<int>? node4 = tree.Predecessor(node3);

            // Assert
            Assert.That(node4, Is.SameAs(node1));
        }
   
        [Test]
        public void Predecessor_LeafNode_ReturnsCorrectPredecessor4()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(3);
            BSTreeNode<int> node2 = tree.Insert(6);
            BSTreeNode<int> node3 = tree.Insert(7);

            // Act
            BSTreeNode<int>? node4 = tree.Predecessor(tree.Root);

            // Assert
            Assert.That(node4, Is.SameAs(node3));
        }
    }

    [TestFixture]
    public class BSTreeTest_Successor
    {
        [Test]
        public void Successor_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            BSTreeNode<int> node = null!;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Successor(node));
        }

        [Test]
        public void Successor_NodeIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            BSTreeNode<int> node = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Successor(node));
        }

        [Test]
        public void Successor_MaxNode_ReturnsNull()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            BSTreeNode<int>? maxNode = tree.MaxNode();

            // Act
            BSTreeNode<int>? successor = tree.Successor(maxNode);

            // Assert
            Assert.That(successor, Is.Null);
        }

        [Test]
        public void Successor_LeafNode_ReturnsCorrectSuccessor()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(3);
            BSTreeNode<int> node2 = tree.Insert(6);
            BSTreeNode<int> node3 = tree.Insert(4);

            // Act
            BSTreeNode<int>? node4 = tree.Successor(node1);

            // Assert
            Assert.That(node4, Is.SameAs(node3));
        }

        [Test]
        public void Successor_LeafNode_ReturnsCorrectSuccessor2()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(3);
            BSTreeNode<int> node2 = tree.Insert(6);

            // Act
            BSTreeNode<int>? node4 = tree.Successor(node2);

            // Assert
            Assert.That(node4, Is.SameAs(tree.Root));
        }

        [Test]
        public void Successor_LeafNode_ReturnsCorrectSuccessor3()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(10);
            BSTreeNode<int> node2 = tree.Insert(14);
            BSTreeNode<int> node3 = tree.Insert(13);

            // Act
            BSTreeNode<int>? node4 = tree.Successor(node3);

            // Assert
            Assert.That(node4, Is.SameAs(node2));
        }
   
        [Test]
        public void Successor_LeafNode_ReturnsCorrectSuccessor4()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(8);   
            BSTreeNode<int> node1 = tree.Insert(3);
            BSTreeNode<int> node2 = tree.Insert(6);
            BSTreeNode<int> node3 = tree.Insert(7);

            // Act
            BSTreeNode<int>? node4 = tree.Successor(tree.Root);

            // Assert
            Assert.That(node4, Is.Null);
        }
    }

    [TestFixture]
    public class BSTreeTest_Find
    {
        [Test]
        public void Find_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            int value = 42;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Find(value));
        }

        [Test]
        public void Find_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            BSTree<string> tree = new BSTree<string>("hello");
            string value = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Find(value));
        }

        [TestCase(10, 20, ExpectedResult = true)]
        [TestCase(50, 25, ExpectedResult = true)]
        [TestCase(-5, 0, ExpectedResult = true)]
        public bool Find_ValueInTree_ReturnsNode(int rootValue, int valueToFind)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(valueToFind);

            // Act
            BSTreeNode<int>? node = tree.Find(valueToFind);

            // Assert
            return node != null;
        }

        [TestCase(10, 20, ExpectedResult = true)]
        [TestCase(50, 100, ExpectedResult = true)]
        [TestCase(-5, 10, ExpectedResult = true)]
        public bool Find_ValueNotInTree_ReturnsNull(int rootValue, int valueToFind)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(rootValue + 1);
            tree.Insert(rootValue - 1);

            // Act
            BSTreeNode<int>? node = tree.Find(valueToFind);

            // Assert
            return node == null;
        }
    }

    [TestFixture]
    public class BSTreeTests_Contains
    {
        [Test]
        public void Contains_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            int value = 42;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Contains(value));
        }

        [Test]
        public void Contains_NullValue_ThrowsArgumentNullException()
        {
            // Arrange
            BSTree<string> tree = new BSTree<string>("hello");
            string value = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Contains(value));
        }

        [TestCase(10, 20, ExpectedResult = true)]
        [TestCase(50, 25, ExpectedResult = true)]
        [TestCase(-5, 0, ExpectedResult = true)]
        public bool Contains_ValueInTree_ReturnsTrue(int rootValue, int valueToFind)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(valueToFind);

            // Act
            bool contains = tree.Contains(valueToFind);

            // Assert
            return contains;
        }

        [TestCase(10, 20, ExpectedResult = false)]
        [TestCase(50, 100, ExpectedResult = false)]
        [TestCase(-5, 10, ExpectedResult = false)]
        public bool Contains_ValueNotInTree_ReturnsFalse(int rootValue, int valueToFind)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(rootValue + 1);
            tree.Insert(rootValue - 1);

            // Act
            bool contains = tree.Contains(valueToFind);

            // Assert
            return contains;
        }
    
        [Test]
        public void Contains_EmptyTree_ContainsNode_ThrowsInvalidOperationException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            BSTreeNode<int> node = null!;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Contains(node));
        }

        [Test]
        public void Contains_EmptyTree_NodeIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            BSTreeNode<int> node = new BSTreeNode<int>(1);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => tree.Contains(node));
        }
    
        [TestCase(10, 20, ExpectedResult = true)]
        [TestCase(50, 25, ExpectedResult = true)]
        [TestCase(-5, 0, ExpectedResult = true)]
        public bool Contains_NodeInTree_ReturnsTrue(int rootValue, int nodeValue)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            BSTreeNode<int> node = new BSTreeNode<int>(nodeValue);
            tree.Insert(node);

            // Act
            bool contains = tree.Contains(node);

            // Assert
            return contains;
        }

        [Test]
        public void Contains_NodeNotInTree_ReturnsFalse()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            BSTreeNode<int> node = new BSTreeNode<int>(20);

            // Act
            bool contains = tree.Contains(node);

            // Assert
            Assert.That(contains, Is.False);
        }
    }

    [TestFixture]
    public class BSTreeTests_Traversal_Printing
    {
        [Test]
        public void PrintInorder_EmptyTree_PrintsNothing()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                tree.PrintInorder(tree.Root);
                string output = sw.ToString().Trim();

                // Assert
                Assert.That(output, Is.EqualTo(""));
            }
        }

        [Test]
        public void PrintPreorder_EmptyTree_PrintsNothing()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                tree.PrintPreorder(tree.Root);
                string output = sw.ToString().Trim();

                // Assert
                Assert.That(output, Is.EqualTo(""));
            }
        }

        [Test]
        public void PrintPostorder_EmptyTree_PrintsNothing()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                tree.PrintPostorder(tree.Root);
                string output = sw.ToString().Trim();

                // Assert
                Assert.That(output, Is.EqualTo(""));
            }
        }

        [Test]
        public void PrintTraversals_NonEmptyTree_PrintsCorrectly()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(12);
            tree.Insert(20);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                tree.PrintInorder(tree.Root);
                string inorderOutput = sw.ToString().Trim();
                sw.GetStringBuilder().Clear();

                tree.PrintPreorder(tree.Root);
                string preorderOutput = sw.ToString().Trim();
                sw.GetStringBuilder().Clear();

                tree.PrintPostorder(tree.Root);
                string postorderOutput = sw.ToString().Trim();

                // Assert
                Assert.That(inorderOutput, Is.EqualTo("3, 5, 7, 10, 12, 15, 20,"));
                Assert.That(preorderOutput, Is.EqualTo("10, 5, 3, 7, 15, 12, 20,"));
                Assert.That(postorderOutput, Is.EqualTo("3, 7, 5, 12, 20, 15, 10,"));
            }
        }
    }

    [TestFixture]
    public class BSTreeTests_KthLargest
    {
        [Test]
        public void KthLargest_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            var tree = new BSTree<int>();
            int k = 1;

            // Act & Assert
            Assert.That(() => tree.KthLargest(k), Throws.InvalidOperationException);
        }

        [Test]
        public void KthLargest_SingleNodeTree_ReturnsRoot()
        {
            // Arrange
            var tree = new BSTree<int>(5);
            int k = 1;

            // Act
            var result = tree.KthLargest(k);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(5));
        }

        [Test]
        public void KthLargest_KGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var tree = new BSTree<int>();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            int k = 4;

            // Act & Assert
            Assert.That(() => tree.KthLargest(k), Throws.ArgumentException);
        }

        [Test]
        public void KthLargest_KLessThanOne_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var tree = new BSTree<int>();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            int k = 0;

            // Act & Assert
            Assert.That(() => tree.KthLargest(k), Throws.ArgumentException);
        }

        [Test]
        public void KthLargest_ValidInput_ReturnsCorrectNode()
        {
            // Arrange
            var tree = new BSTree<int>();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(6);
            tree.Insert(8);
            int k = 3;

            // Act
            var result = tree.KthLargest(k);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(6));
        }


    }

    [TestFixture]
    public class BSTreeTests_KthSmallest
    {
        [Test]
        public void KthSmallest_EmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            var tree = new BSTree<int>();
            int k = 1;

            // Act & Assert
            Assert.That(() => tree.KthSmallest(k), Throws.InvalidOperationException);
        }

        [Test]
        public void KthSmallest_SingleNodeTree_ReturnsRoot()
        {
            // Arrange
            var tree = new BSTree<int>(5);
            int k = 1;

            // Act
            var result = tree.KthSmallest(k);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(5));
        }

        [Test]
        public void KthSmallest_KGreaterThanCount_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var tree = new BSTree<int>();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            int k = 4;

            // Act & Assert
            Assert.That(() => tree.KthSmallest(k), Throws.ArgumentException);
        }

        [Test]
        public void KthSmallest_KLessThanOne_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var tree = new BSTree<int>();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            int k = 0;

            // Act & Assert
            Assert.That(() => tree.KthSmallest(k), Throws.ArgumentException);
        }

        [Test]
        public void KthSmallest_ValidInput_ReturnsCorrectNode()
        {
            // Arrange
            var tree = new BSTree<int>();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(6);
            tree.Insert(8);
            int k = 3;

            // Act
            var result = tree.KthSmallest(k);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(4));
        }
    }



}