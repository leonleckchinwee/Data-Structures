namespace DSA.Array.Tests;

public class Array_Tests
{
    [TestFixture]
    public class SArray_Ctor
    {
        [Test]
        public void Constructor_WithPositiveSize_ShouldCreateArrayWithSpecifiedSize()
        {
            // Arrange
            int size = 5;

            // Act
            SArray<int> intArray = new SArray<int>(size);

            // Assert
            Assert.That(intArray.Length, Is.EqualTo(size));
        }

        [Test]
        public void Constructor_WithZeroSize_ShouldCreateEmptyArray()
        {
            // Act
            SArray<string> stringArray = new SArray<string>(0);

            // Assert
            Assert.That(stringArray.Length, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WithNegativeSize_ShouldThrowException()
        {
            // Arrange
            int negativeSize = -5;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SArray<double>(negativeSize));
        }

        [Test]
        public void Constructor_WithMaxIntSize_ShouldThrowException()
        {
            // Arrange
            int maxIntSize = int.MaxValue;

            // Act & Assert
            Assert.Throws<OutOfMemoryException>(() => new SArray<object>(maxIntSize));
        }

        [Test]
        [TestCase(typeof(int))]
        [TestCase(typeof(string))]
        [TestCase(typeof(DateTime))]
        public void Constructor_WithDifferentTypes_ShouldCreateArrayOfSpecifiedType<T>(T typeArgument)
        {
            // Arrange
            int size = 3;

            // Act
            SArray<T> array = new SArray<T>(size) { typeArgument, typeArgument, typeArgument };

            // Assert
            Assert.IsNotNull(array);
            Assert.That(array.Length, Is.EqualTo(size));
            Assert.That(array[0], Is.InstanceOf<T>());
            Assert.That(array[1], Is.InstanceOf<T>());
            Assert.That(array[2], Is.InstanceOf<T>());
        }
    
        [Test]
        public void Constructor_WithNullList_ShouldThrowArgumentNullException()
        {
            // Arrange
            List<int> nullList = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SArray<int>(nullList));
        }

        [Test]
        public void Constructor_WithNonNullList_ShouldCreateArrayWithListItems()
        {
            // Arrange
            List<string> list = new List<string> { "apple", "banana", "cherry" };

            // Act
            SArray<string> array = new SArray<string>(list);

            // Assert
            Assert.That(array.Length, Is.EqualTo(list.Count));
            Assert.That(array[0], Is.EqualTo("apple"));
            Assert.That(array[1], Is.EqualTo("banana"));
            Assert.That(array[2], Is.EqualTo("cherry"));
        }

        [Test]
        public void Constructor_WithNullEnumerable_ShouldThrowArgumentNullException()
        {
            // Arrange
            IEnumerable<double> nullEnumerable = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SArray<double>(nullEnumerable));
        }

        [Test]
        public void Constructor_WithNonNullEnumerable_ShouldCreateArrayWithEnumerableItems()
        {
            // Arrange
            IEnumerable<int> enumerable = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            SArray<int> array = new SArray<int>(enumerable);

            // Assert
            Assert.That(array.Length, Is.EqualTo(5));
            Assert.That(array[0], Is.EqualTo(1));
            Assert.That(array[1], Is.EqualTo(2));
            Assert.That(array[2], Is.EqualTo(3));
            Assert.That(array[3], Is.EqualTo(4));
            Assert.That(array[4], Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class SArray_Add
    {
        [Test]
        public void Add_WithNonFullArray_Int_ShouldAddItemAtFirstAvailableIndex()
        {
            // Arrange
            SArray<int> array = new SArray<int>(5);
            array[0] = 1;
            array[2] = 3;

            // Act
            array.Add(4);

            // Assert
            Assert.That(array[1], Is.EqualTo(4));
            Assert.That(array[3], Is.EqualTo(0));
            Assert.That(array[4], Is.EqualTo(0));
        }

        [Test]
        public void Add_WithNonFullArray_String_ShouldAddItemAtFirstAvailableIndex()
        {
            // Arrange
            SArray<string> array = new SArray<string>(5);
            array[0] = "Hello";
            array[2] = "World";

            // Act
            array.Add("Another");

            // Assert
            Assert.That(array[1], Is.EqualTo("Another"));
            Assert.That(array[3], Is.EqualTo(null));
            Assert.That(array[4], Is.EqualTo(null));
        }

        [Test]
        public void Add_WithFullArray_ShouldThrowInvalidOperationException()
        {
            // Arrange
            SArray<string> array = new SArray<string>(3);
            array[0] = "apple";
            array[1] = "banana";
            array[2] = "cherry";

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => array.Add("date"));
        }

        [Test]
        public void Indexer_SetValue_ShouldUpdateExistingValue()
        {
            // Arrange
            SArray<double> array = new SArray<double>(3);
            array[0] = 1.5;
            array[1] = 2.5;

            // Act
            array[1] = 3.5;

            // Assert
            Assert.That(array[0], Is.EqualTo(1.5));
            Assert.That(array[1], Is.EqualTo(3.5));
            Assert.That(array[2], Is.EqualTo(0));
        }

        [Test]
        public void Indexer_GetValue_ShouldReturnCorrectValue()
        {
            // Arrange
            SArray<bool> array = new SArray<bool>(2);
            array[0] = true;
            array[1] = false;

            // Act & Assert
            Assert.That(array[0], Is.True);
            Assert.That(array[1], Is.False);
        }

        [Test]
        public void Indexer_OutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            // Arrange
            SArray<int> array = new SArray<int>(3);

            // Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => array[-1] = 10);
            Assert.Throws<IndexOutOfRangeException>(() => array[3] = 10);
            Assert.Throws<IndexOutOfRangeException>(() => { int value = array[-1]; });
            Assert.Throws<IndexOutOfRangeException>(() => { int value = array[3]; });
        }    
    }

    [TestFixture]
    public class SArray_Remove
    {
        [Test]
        public void Clear_WithNonEmptyArray_ShouldSetAllElementsToDefault()
        {
            // Arrange
            SArray<int> array = new SArray<int>(5);
            array[0] = 1;
            array[1] = 2;
            array[2] = 3;
            array[3] = 4;
            array[4] = 5;

            // Act
            array.Clear();

            // Assert
            Assert.That(array[0], Is.EqualTo(default(int)));
            Assert.That(array[1], Is.EqualTo(default(int)));
            Assert.That(array[2], Is.EqualTo(default(int)));
            Assert.That(array[3], Is.EqualTo(default(int)));
            Assert.That(array[4], Is.EqualTo(default(int)));
        }

        [Test]
        public void Clear_WithEmptyArray_ShouldNotThrowException()
        {
            // Arrange
            SArray<string> array = new SArray<string>(0);

            // Act
            array.Clear();

            // Assert (no exceptions thrown)
            Assert.Pass();
        }

        [TestCase(typeof(int))]
        [TestCase(typeof(string))]
        [TestCase(typeof(DateTime))]
        public void Clear_WithDifferentTypes_ShouldSetAllElementsToDefaultForType<T>(T typeArgument)
        {
            // Arrange
            int size = 3;
            SArray<T> array = new SArray<T>(size);
            array[0] = default!;
            array[1] = default!;
            array[2] = default!;

            // Act
            array.Clear();

            // Assert
            Assert.That(array[0], Is.EqualTo(default(T)));
            Assert.That(array[1], Is.EqualTo(default(T)));
            Assert.That(array[2], Is.EqualTo(default(T)));
        }
    }

    [TestFixture]
    public class SArray_Indexer
    {
        [Test]
        public void Indexer_SetValue_ShouldUpdateExistingValue()
        {
            // Arrange
            SArray<double> array = new SArray<double>(3);
            array[0] = 1.5;
            array[1] = 2.5;

            // Act
            array[1] = 3.5;

            // Assert
            Assert.That(array[0], Is.EqualTo(1.5));
            Assert.That(array[1], Is.EqualTo(3.5));
            Assert.That(array[2], Is.EqualTo(0));
        }

        [Test]
        public void Indexer_GetValue_ShouldReturnCorrectValue()
        {
            // Arrange
            SArray<bool> array = new SArray<bool>(2);
            array[0] = true;
            array[1] = false;

            // Act & Assert
            Assert.That(array[0], Is.True);
            Assert.That(array[1], Is.False);
        }

        [Test]
        public void Indexer_SetValueAtNull_ShouldUpdateNull()
        {
            // Arrange
            SArray<string> array = new SArray<string>(3);
            array[0] = "apple";
            array[2] = "cherry";

            // Act
            array[1] = "banana";

            // Assert
            Assert.That(array[0], Is.EqualTo("apple"));
            Assert.That(array[1], Is.EqualTo("banana"));
            Assert.That(array[2], Is.EqualTo("cherry"));
        }

        [Test]
        public void Indexer_OutOfRange_ShouldThrowIndexOutOfRangeException()
        {
            // Arrange
            SArray<int> array = new SArray<int>(3);

            // Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => array[-1] = 10);
            Assert.Throws<IndexOutOfRangeException>(() => array[3] = 10);
            Assert.Throws<IndexOutOfRangeException>(() => { int value = array[-1]; });
            Assert.Throws<IndexOutOfRangeException>(() => { int value = array[3]; });
        }
    }





}