int n1 = int.Parse(Console.ReadLine());
int n2 = int.Parse(Console.ReadLine());
string operation = Console.ReadLine();

if ((operation == "/" || operation == "%") && n2 == 0)
{
    Console.WriteLine($"Cannot divide {n1} by zero");
}
else
{
    if (operation == "/")
    {
        double divisionResult = (double)n1 / n2;
        Console.WriteLine($"{n1} / {n2} = {divisionResult:f2}");
    }
    else
    {
        bool showParity = true;
        int result = 0;

        if (operation == "+") result = n1 + n2;
        else if (operation == "-") result = n1 - n2;
        else if (operation == "*") result = n1 * n2;
        else if (operation == "%")
        {
            result = n1 % n2;
            showParity = false;
        }

        Console.Write($"{n1} {operation} {n2} = {result}");

        if (showParity)
        {
            string parity = result % 2 == 0 ? "even" : "odd";
            Console.Write($" - {parity}");
        }

        Console.WriteLine();
    }
}