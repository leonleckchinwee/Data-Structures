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
            var tree = new BSTree<int>();

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
            var tree = new BSTree<int>(value);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(value));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        [Test]
        public void ValuedConstructor_ShouldCreateTreeWithRootNode_WhenValueIsComparable()
        {
            // Arrange
            var value = new ComparableClass(42);

            // Act
            var tree = new BSTree<ComparableClass>(value);

            // Assert
            Assert.That(tree.Root, Is.Not.Null);
            Assert.That(tree.Root.Value, Is.EqualTo(value));
            Assert.That(tree.Count, Is.EqualTo(1));
        }

        private class ComparableClass : IComparable<ComparableClass>
        {
            public int Value { get; set; }

            public ComparableClass(int value)
            {
                Value = value;
            }

            public int CompareTo(ComparableClass? other)
            {
                if (other == null)
                    return 1;

                return Value.CompareTo(other.Value);
            }
        }
    }

    [TestFixture]
    public class BSTreeTests_Insertion
    {
        [Test]
        public void Insert_ShouldInsertValueIntoEmptyTree()
        {
            // Arrange
            var tree = new BSTree<int>();
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
            var tree = new BSTree<int>(50);
            int value1 = 25;
            int value2 = 75;

            // Act
            tree.Insert(value1);            tree.Insert(value2);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(50));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(value1));            
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(value2));
            Assert.That(tree.Count, Is.EqualTo(3));
        }

        [Test]
        public void Insert_ShouldInsertDuplicateValuesOnlyOnce()
        {
            // Arrange
            var tree = new BSTree<int>();
            int value = 42;

            // Act
            tree.Insert(value);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(value));
            Assert.That(tree.Root.Left, Is.Null);
            Assert.That(tree.Root.Right, Is.Null);
            Assert.That(tree.Count, Is.EqualTo(1));
            Assert.Throws<InvalidOperationException>(() => tree.Insert(value));
        }

        [Test]
        public void Insert_ShouldInsertComparableValues()
        {
            // Arrange
            var tree = new BSTree<ComparableClass>();
            var value1 = new ComparableClass(42);
            var value2 = new ComparableClass(25);
            var value3 = new ComparableClass(75);

            // Act
            tree.Insert(value1);
            tree.Insert(value2);
            tree.Insert(value3);

            // Assert
            Assert.That(tree.Root!.Value, Is.EqualTo(value1));
            Assert.That(tree.Root.Left!.Value, Is.EqualTo(value2));
            Assert.That(tree.Root.Right!.Value, Is.EqualTo(value3));
            Assert.That(tree.Count, Is.EqualTo(3));
        }

        private class ComparableClass : IComparable<ComparableClass>
        {
            public int Value { get; set; }

            public ComparableClass(int value)
            {
                Value = value;
            }

            public int CompareTo(ComparableClass? other)
            {
                if (other == null)
                    return 1;

                return Value.CompareTo(other.Value);
            }
        }
    }
}