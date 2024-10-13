namespace Threeuple;

public class Program
{
    public static void Main()
    {
        Threeuple<string, string, string> firstTuple = ReadFirstLine();
        Threeuple<string, int, bool> secondTuple = ReadSecondLine();
        Threeuple<string, double, string> thirdTuple = ReadThirdLine();

        Console.WriteLine(firstTuple);
        Console.WriteLine(secondTuple);
        Console.WriteLine(thirdTuple);
    }

    private static Threeuple<string, string, string> ReadFirstLine()
    {
        string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string name = string.Join(' ', data.Take(2));
        string address = data[2];
        string town = string.Join(' ', data.Skip(3));

        return new Threeuple<string, string, string>(name, address, town);
    }

    private static Threeuple<string, int, bool> ReadSecondLine()
    {
        string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string name = data[0], drunk = data[2];
        int litersOfBeer = int.Parse(data[1]);

        return new Threeuple<string, int, bool>(name, litersOfBeer, drunk == "drunk");
    }

    private static Threeuple<string, double, string> ReadThirdLine()
    {
        string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string name = data[0], bankName = data[2];
        double balance = double.Parse(data[1]);

        return new Threeuple<string, double, string>(name, balance, bankName);
    }
}
