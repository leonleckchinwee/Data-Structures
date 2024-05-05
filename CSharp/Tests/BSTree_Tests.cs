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
        public void GetDepth_EmptyTree_ReturnsNegativeOne()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>();

            // Act
            int depth = tree.GetDepth(null, null);

            // Assert
            Assert.That(depth, Is.EqualTo(-1));
        }

        [Test]
        public void GetDepth_TargetNodeIsRoot_ReturnsZero()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(42);
            BSTreeNode<int> root = tree.Root!;

            // Act
            int depth = tree.GetDepth(root, root);

            // Assert
            Assert.That(depth, Is.EqualTo(0));
        }

        [TestCase(10, 5, 1, 15, 1, 12, -1, 20, 2)]
        [TestCase(50, 25, 1, 75, 1, 12, 2, 37, 2, 62, 2, 87, 2)]
        public void GetDepth_TargetNodeInTree_ReturnsCorrectDepth(int rootValue, int value1, 
        int expectedDepth1, int value2, int expectedDepth2, int value3, int expectedDepth3, 
        int value4, int expectedDepth4, int value5 = 0, int expectedDepth5 = 0, int value6 = 0, 
        int expectedDepth6 = 0)
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(rootValue);
            tree.Insert(value1);
            tree.Insert(value2);
            tree.Insert(value3);
            tree.Insert(value4);
            if (value5 != 0)
                tree.Insert(value5);
            if (value6 != 0)
                tree.Insert(value6);

            BSTreeNode<int> targetNode1 = tree.Root!.Left!;
            BSTreeNode<int> targetNode2 = tree.Root!.Right!;
            BSTreeNode<int> targetNode3 = tree.Root!.Left!.Left!;
            BSTreeNode<int> targetNode4 = tree.Root!.Right!.Right!;
            BSTreeNode<int>? targetNode5 = value5 != 0 ? tree!.Root!.Left!.Right! : null;
            BSTreeNode<int>? targetNode6 = value6 != 0 ? tree!.Root!.Right!.Left! : null;

            // Act
            int depth1 = tree.GetDepth(tree.Root, targetNode1);
            int depth2 = tree.GetDepth(tree.Root, targetNode2);
            int depth3 = tree.GetDepth(tree.Root, targetNode3);
            int depth4 = tree.GetDepth(tree.Root, targetNode4);
            int depth5 = value5 != 0 ? tree.GetDepth(tree.Root, targetNode5) : 0;
            int depth6 = value6 != 0 ? tree.GetDepth(tree.Root, targetNode6) : 0;

            // Assert
            Assert.That(depth1, Is.EqualTo(expectedDepth1));
            Assert.That(depth2, Is.EqualTo(expectedDepth2));
            Assert.That(depth3, Is.EqualTo(expectedDepth3));
            Assert.That(depth4, Is.EqualTo(expectedDepth4));
            Assert.That(depth5, Is.EqualTo(expectedDepth5));
            Assert.That(depth6, Is.EqualTo(expectedDepth6));
        }

        [Test]
        public void GetDepth_TargetNodeNotInTree_ReturnsNegativeOne()
        {
            // Arrange
            BSTree<int> tree = new BSTree<int>(10);
            tree.Insert(5);
            tree.Insert(15);
            BSTreeNode<int> nonExistentNode = new BSTreeNode<int>(42);

            // Act
            int depth = tree.GetDepth(tree.Root, nonExistentNode);

            // Assert
            Assert.That(depth, Is.EqualTo(-1));
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


}