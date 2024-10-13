namespace GenericSwapMethodString;

public class Program
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        Box<string>[] boxedValues = new Box<string>[n];
        for (int i = 0; i < n; i++)
        {
            string value = Console.ReadLine();
            boxedValues[i] = new Box<string>(value);
        }

        int[] swapIndices = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        Swap(boxedValues, swapIndices[0], swapIndices[1]);

        foreach (Box<string> box in boxedValues)
            Console.WriteLine(box);
    }

    private static void Swap<T>(T[] array, int firstIndex, int secondIndex)
    {
        T swapValue = array[firstIndex];
        array[firstIndex] = array[secondIndex];
        array[secondIndex] = swapValue;
    }
}
