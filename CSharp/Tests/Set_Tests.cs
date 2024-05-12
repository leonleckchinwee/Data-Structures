namespace DSA.Sets.Tests;

public class Set_Tests
{
    [TestFixture]
    public class SetTests_Constructor
    {
        [Test]
        public void Constructor_Empty()
        {
            Set<int> set = new();

            Assert.That(set, Has.Count.EqualTo(0));
            Assert.That(set.MaxCount, Is.EqualTo(16));
        }

        [Test]
        public void Constructor_NegativeCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Set<int>(-1));
        }

        [Test]
        public void Constructor_Capacity()
        {
            Set<int> set = new(10);

            Assert.That(set, Has.Count.EqualTo(0));
            Assert.That(set.MaxCount, Is.EqualTo(10));
        }
    
        [Test]
        public void Constructor_NullEnumerable()
        {
            Assert.Throws<ArgumentNullException>(() => new Set<int>(null!));
        }

        [Test]
        public void Constructor_Enumerable_Unique()
        {
            Set<int> set = new([1, 2, 3, 4, 5]);

            Assert.That(set, Has.Count.EqualTo(5));
            Assert.That(set.MaxCount, Is.EqualTo(5));
        }

        [Test]
        public void Constructor_Enumerable_Duplicate()
        {
            Set<int> set = new([1, 2, 3, 4, 5, 1, 2, 3, 4, 5]);

            Assert.That(set, Has.Count.EqualTo(5));
            Assert.That(set.MaxCount, Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class SetTests_Add
    {
        [Test]
        public void Add_AddingNewElement_ShouldIncreaseCount()
        {
            // Arrange
            Set<int> set = new Set<int>();

            // Act
            set.Add(1);

            // Assert
            Assert.That(set, Has.Count.EqualTo(1));
        }

        [Test]
        public void Add_AddingExistingElement_ShouldNotChangeCount()
        {
            // Arrange
            Set<int> set = new Set<int>([1, 2, 3]);

            // Act
            set.Add(2);

            // Assert
            Assert.That(set.Count, Is.EqualTo(3));
        }

        [Test]
        public void Add_AddingMultipleElements_ShouldIncreaseCountCorrectly()
        {
            // Arrange
            Set<int> set = new Set<int>();

            // Act
            set.Add(2);
            set.Add(3);
            set.Add(1); // Duplicate element

            // Assert
            Assert.That(set.Count, Is.EqualTo(3));
        }

        [Test]
        public void Add_AddingNullElement_ShouldThrowArgument()
        {
            // Arrange
            Set<string> set = new Set<string>();

            // Act & Assert
            Assert.That(() => set.Add(null!), Throws.ArgumentNullException);
        }
    }

}