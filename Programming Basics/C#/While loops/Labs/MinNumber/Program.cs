int min = int.MaxValue;

string input = Console.ReadLine();
while (input != "Stop")
{
    int currentNumber = int.Parse(input);
    if (currentNumber < min) min = currentNumber;

    input = Console.ReadLine();
}

Console.WriteLine(min);