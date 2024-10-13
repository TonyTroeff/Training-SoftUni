namespace Tuple;

public class Program
{
    public static void Main()
    {
        // What is a tuple?
        // Tuple<int, string> t1 = new Tuple<int, string>(13, "Hello");
        // Console.WriteLine($"{t1.Item1}, {t1.Item2}");

        // ValueTuple<int, string> t2 = new ValueTuple<int, string>(13, "Hello");
        // Console.WriteLine($"{t2.Item1}, {t2.Item2}");

        // (int Number, string Text) t3 = (Number: 13, Text: "Hello");
        // Console.WriteLine($"{t3.Number}, {t3.Text}");

        string[] firstData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string firstName = firstData[0], lastName = firstData[1], address = firstData[2];

        Tuple<string, string> firstTuple = new Tuple<string, string>($"{firstName} {lastName}", address);

        string[] secondData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string name = secondData[0];
        int litersOfBeer = int.Parse(secondData[1]);

        Tuple<string, int> secondTuple = new Tuple<string, int>(name, litersOfBeer);

        string[] thirdData = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int number1 = int.Parse(thirdData[0]);
        double number2 = double.Parse(thirdData[1]);

        Tuple<int, double> thirdTuple = new Tuple<int, double>(number1, number2);

        Console.WriteLine(firstTuple);
        Console.WriteLine(secondTuple);
        Console.WriteLine(thirdTuple);
    }
}
