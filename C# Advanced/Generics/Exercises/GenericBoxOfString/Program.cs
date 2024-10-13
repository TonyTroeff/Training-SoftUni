namespace GenericBoxOfString;

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

        foreach (Box<string> box in boxedValues)
            Console.WriteLine(box);
    }
}
