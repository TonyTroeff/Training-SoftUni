int max = int.MinValue;

string input = Console.ReadLine();
while (input != "Stop")
{
    int currentNumber = int.Parse(input);
    if (currentNumber > max) max = currentNumber;

    input = Console.ReadLine();
}

Console.WriteLine(max);