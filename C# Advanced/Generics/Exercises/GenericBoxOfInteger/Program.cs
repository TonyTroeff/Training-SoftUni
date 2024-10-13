namespace GenericBoxOfInteger;

public class Program
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        Box<int>[] boxedValues = new Box<int>[n];

        for (int i = 0; i < n; i++)
        {
            int number = int.Parse(Console.ReadLine());
            boxedValues[i] = new Box<int>(number);
        }

        foreach (Box<int> box in boxedValues)
            Console.WriteLine(box);
    }
}
