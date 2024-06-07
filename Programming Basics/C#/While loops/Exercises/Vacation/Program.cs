double necessaryAmount = double.Parse(Console.ReadLine());
double savings = double.Parse(Console.ReadLine());

int consecutiveSpendOperations = 0, totalDays = 0;
bool canSave = true;
while (savings < necessaryAmount)
{
    string operation = Console.ReadLine();
    double operationAmount = double.Parse(Console.ReadLine());

    totalDays++;

    if (operation == "save")
    {
        consecutiveSpendOperations = 0;
        savings += operationAmount;
    }
    else if (operation == "spend")
    {
        if (++consecutiveSpendOperations == 5)
        {
            canSave = false;
            break;
        }

        savings = Math.Max(savings - operationAmount, 0.0);
    }
}

if (canSave) Console.WriteLine($"You saved the money for {totalDays} days.");
else
{
    Console.WriteLine("You can't save the money.");
    Console.WriteLine(totalDays);
}