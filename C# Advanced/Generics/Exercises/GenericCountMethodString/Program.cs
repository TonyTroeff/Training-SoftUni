namespace GenericCountMethodString;

public class Program
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine()!);

        Box<string>[] boxedValues = new Box<string>[n];
        for (int i = 0; i < n; i++)
        {
            string value = Console.ReadLine()!;
            boxedValues[i] = new Box<string>(value);
        }

        string queryValue = Console.ReadLine()!;

        int result = CountGreaterThan(boxedValues, queryValue);
        Console.WriteLine(result);
    }

    private static IComparer<Box<T>> CreateBoxComparer<T>(IComparer<T>? comparer = null)
    {
        if (comparer is null) comparer = Comparer<T>.Default;
        return Comparer<Box<T>>.Create((x, y) => comparer.Compare(x.Value, y.Value));
    }

    private static int CountGreaterThan<T>(Box<T>[] array, T queryValue, IComparer<T>? comparer = null)
    {
        Box<T> boxedQueryValue = new Box<T>(queryValue);
        IComparer<Box<T>> boxComparer = CreateBoxComparer(comparer);
        return CountGreaterThan(array, boxedQueryValue, boxComparer);
    }

    private static int CountGreaterThan<T>(T[] array, T queryValue, IComparer<T>? comparer = null)
    {
        if (comparer is null) comparer = Comparer<T>.Default;

        int count = 0;
        foreach (T value in array)
        {
            int compareResult = comparer.Compare(value, queryValue);
            if (compareResult > 0) count++;
        }

        return count;
    }

    /*private static int CountGreaterThan<T>(Box<T>[] array, T queryValue)
        where T : IComparable<T>
    {
        int count = 0;
        foreach (Box<T> box in array)
        {
            int compareResult = box.Value.CompareTo(queryValue);
            if (compareResult > 0) count++;
        }

        return count;
    }*/

    /*private static int CountGreaterThan<T>(Box<T>[] array, T queryValue, IComparer<T>? comparer = null)
    {
        if (comparer is null) comparer = Comparer<T>.Default;

        int count = 0;
        foreach (Box<T> box in array)
        {
            int compareResult = comparer.Compare(box.Value, queryValue);
            if (compareResult > 0) count++;
        }

        return count;
    }*/
}
