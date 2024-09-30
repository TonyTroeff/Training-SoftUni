namespace DateModifier;

public class Program
{
    public static void Main()
    {
        string first = Console.ReadLine();
        string second = Console.ReadLine();

        int difference = DateModifier.CalculateDifferenceInDays(first, second);
        Console.WriteLine(difference);
    }
}