double amount = 0;

string input = Console.ReadLine();
while (input != "NoMoreMoney")
{
    double operation = double.Parse(input);
    if (operation < 0)
    {
        Console.WriteLine("Invalid operation!");
        break;
    }

    Console.WriteLine($"Increase: {operation:f2}");
    amount += operation;
    input = Console.ReadLine();
}

Console.WriteLine($"Total: {amount:f2}");