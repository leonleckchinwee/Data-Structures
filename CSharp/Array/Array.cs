using System.Collections;

namespace DSA.ArrayAlgorithms;

public static class ArrayAlgos
{
    public static int BinarySearch(Array array, object? value)
    {
        // Exceptions
        {
            // array is null
            ArgumentNullException.ThrowIfNull(array);
            
            // array is multi-dimensional
            if (array.Rank != 1)
                throw new RankException(nameof(array));

            Type? arrayType = array.GetType().GetElementType();
            Type? valueType = value?.GetType();

            // value type is not compatible with the elements of array
            if (arrayType != valueType)
                throw new ArgumentException("Value type is incompatible with array!");

            // value does not implement the IComparable interface
            if (value != null && !typeof(IComparable).IsAssignableFrom(valueType))
                throw new InvalidOperationException("Value does not implement IComparable interface!");
        }

        int left  = 0;
        int right = array.Length - 1;

        while (left < right)
        {
            int middle           = left + (right - left) / 2;
            object? currentValue = array.GetValue(middle);
            
            if (currentValue == null && value == null)
                return middle;
            else if (currentValue == null || value == null)
                continue;

            int compare = ((IComparable)currentValue).CompareTo(value);

            if (compare == 0)
                return middle;
            else if (compare < 0)
                left = middle + 1;
            else
                right = middle - 1;
        }

        return ~left;
    }
}