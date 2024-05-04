namespace DSA.BSTrees.Tests;

public class BSTrees_Tests
{
    [TestFixture]
    public class BSTreeTests_Constructors
    {
        [Test]
        public void DefaultConstructor_ShouldCreateEmptyTree()
        {
            // Arrange
            var tree = new BSTree();

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(0));
        }

        [Test]
        public void ValuedConstructor_ShouldCreateTreeWithRootNode()
        {
            // Arrange
            int value = 42;

            // Act
            var tree = new BSTree(value);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(value));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void ValuedConstructor_ShouldCreateTreeWithRootNode_WhenValueIsComparable()
        {
            // Act
            var tree = new BSTree(42);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(42));
            Assert.That(tree.Count, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class BSTreeTests_Insert
    {
        [Test]
        public void Insert_ShouldInsertValueIntoEmptyTree()
        {
            // Arrange
            var tree = new BSTree();
            int value = 42;

            // Act
            tree.Insert(value);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(value));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void Insert_ShouldInsertValueIntoNonEmptyTree()
        {
            // Arrange
            var tree = new BSTree(50);
            int value1 = 25;
            int value2 = 75;

            // Act
            tree.Insert(value1);
            tree.Insert(value2);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(50));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(value1));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(value2));
            Assert.That(tree.Count, Is.EqualTo(3));
        }

        [Test]
        public void Insert_ShouldThrowInvalidOperationException_WhenDuplicateValuesInserted()
        {
            // Arrange
            var tree = new BSTree();
            int value = 42;

            // Act & Assert
            tree.Insert(value);
            Assert.That(() => tree.Insert(value), Throws.InvalidOperationException);
        }

        [Test]
        public void Insert_ShouldUpdateCountCorrectly_WhenInsertingValues()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = [50, 25, 75, 10, 40, 60, 90];

            // Act
            foreach (int value in values)
            {
                tree.Insert(value);
            }

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(50));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(25));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(75));
            Assert.That(tree.Root.Left.Left!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(40));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(60));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(90));
            Assert.That(tree.Count, Is.EqualTo(values.Length));
        }
    }

    [TestFixture]
    public class BSTreeTests_Remove
    {
        [Test]
        public void Remove_ShouldDoNothingWhenTreeIsEmpty()
        {
            // Arrange
            var tree = new BSTree();
            int value = 42;

            // Act
            bool removed = tree.Remove(value);

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(removed, Is.False);
            Assert.That(tree.Count, Is.EqualTo(0));
        }

        [Test]
        public void Remove_ShouldRemoveRootNodeWhenTreeHasOnlyOneNode()
        {
            // Arrange
            var tree = new BSTree(42);

            // Act
            bool removed = tree.Remove(42);

            // Assert
            Assert.That(tree.Root, Is.Null);
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(0));
        }

        [Test]
        public void Remove_ShouldRemoveNodeWithNoChildren()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = { 50, 25, 75, 10, 40, 60, 90 };
            foreach (int value in values)
            {
                tree.Insert(value);
            }

            // Act
            bool removed = tree.Remove(10);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(50));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(25));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(75));
            Assert.That(tree.Root.Left!.Left, Is.Null);
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(40));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(60));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(90));
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(6));
        }

        [Test]
        public void Remove_ShouldRemoveNodeWithOneChild()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = { 50, 25, 75, 10, 40, 60, 90 };
            foreach (int value in values)
            {
                tree.Insert(value);
            }

            // Act
            bool removed = tree.Remove(25);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(50));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(40));
            Assert.That(tree.Root.Left.Left, Is.Null);
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(75));
            Assert.That(tree.Root.Right.Left!.Value, Is.EqualTo(60));
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(90));
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(6));
        }

        [Test]
        public void Remove_ShouldRemoveNodeWithTwoChildren()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = { 50, 25, 75, 10, 40, 60, 90 };
            foreach (int value in values)
            {
                tree.Insert(value);
            }

            // Act
            bool removed = tree.Remove(50);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(60));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(25));
            Assert.That(tree.Root.Left.Left!.Value, Is.EqualTo(10));
            Assert.That(tree.Root.Left.Right!.Value, Is.EqualTo(40));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(75));
            Assert.That(tree.Root.Right.Left, Is.Null);
            Assert.That(tree.Root.Right.Right!.Value, Is.EqualTo(90));
            Assert.That(removed, Is.True);
            Assert.That(tree.Count, Is.EqualTo(6));
        }
    }

    [TestFixture]
    public class BSTreeTests_Inorder
    {
        [Test]
        public void PrintInorder_ShouldReturnNullForEmptyTree()
        {
            // Arrange
            var tree = new BSTree();

            // Act
            string? result = tree.PrintInorder();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void PrintInorder_ShouldReturnCorrectOrderForSingleNode()
        {
            // Arrange
            var tree = new BSTree(42);

            // Act
            string? result = tree.PrintInorder();

            // Assert
            Assert.That(result, Is.EqualTo("42, "));
        }

        [Test]
        public void PrintInorder_ShouldReturnCorrectOrderForMultipleNodes()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = { 50, 25, 75, 10, 40, 60, 90 };
            foreach (int value in values)
            {
                tree.Insert(value);
            }

            // Act
            string? result = tree.PrintInorder();

            // Assert
            Assert.That(result, Is.EqualTo("10, 25, 40, 50, 60, 75, 90, "));
        }

        [Test]
        public void PrintInorder_ShouldReturnCorrectOrderAfterRemovingNodes()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = { 50, 25, 75, 10, 40, 60, 90 };
            foreach (int value in values)
            {
                tree.Insert(value);
            }
            tree.Remove(25);
            tree.Remove(75);

            // Act
            string? result = tree.PrintInorder();

            // Assert
            Assert.That(result, Is.EqualTo("10, 40, 50, 60, 90, "));
        }
    }

    [TestFixture]
    public class BSTreeTests_Preorder
    {
        [Test]
        public void PrintPreorder_ShouldReturnNullForEmptyTree()
        {
            // Arrange
            var tree = new BSTree();

            // Act
            string? result = tree.PrintPreorder();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void PrintPreorder_ShouldReturnCorrectOrderForSingleNode()
        {
            // Arrange
            var tree = new BSTree(42);

            // Act
            string? result = tree.PrintPreorder();

            // Assert
            Assert.That(result, Is.EqualTo("42, "));
        }

        [Test]
        public void PrintPreorder_ShouldReturnCorrectOrderForMultipleNodes()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = { 50, 25, 75, 10, 40, 60, 90 };
            foreach (int value in values)
            {
                tree.Insert(value);
            }

            // Act
            string? result = tree.PrintPreorder();

            // Assert
            Assert.That(result, Is.EqualTo("50, 25, 10, 40, 75, 60, 90, "));
        }

        [Test]
        public void PrintPreorder_ShouldReturnCorrectOrderAfterRemovingNodes()
        {
            // Arrange
            var tree = new BSTree();
            int[] values = { 50, 25, 75, 10, 40, 60, 90 };
            foreach (int value in values)
            {
                tree.Insert(value);
            }
            tree.Remove(25);
            tree.Remove(75);

            // Act
            string? result = tree.PrintPreorder();

            // Assert
            Assert.That(result, Is.EqualTo("50, 40, 10, 60, 90, "));
        }
    }







}