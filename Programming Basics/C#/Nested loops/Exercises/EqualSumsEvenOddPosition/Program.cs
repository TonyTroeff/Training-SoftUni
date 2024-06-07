int start = int.Parse(Console.ReadLine());
int end = int.Parse(Console.ReadLine());

for (int i = start; i <= end; i++)
{
    int evenSum = 0, oddSum = 0;
    bool isEven = true;

    int num = i;
    while (num != 0)
    {
        int digit = num % 10;
        if (isEven) evenSum += digit;
        else oddSum += digit;

        isEven = !isEven;
        num /= 10;
    }

    if (evenSum == oddSum) Console.Write($"{i} ");
}

Console.WriteLine();
