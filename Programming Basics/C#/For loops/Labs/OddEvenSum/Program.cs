int n = int.Parse(Console.ReadLine());

int oddSum = 0, evenSum = 0;
for (int i = 0; i < n; i++)
{
    int currentNumber = int.Parse(Console.ReadLine());

    if (i % 2 == 0) evenSum += currentNumber;
    else oddSum += currentNumber;
}

if (oddSum == evenSum)
{
    Console.WriteLine("Yes");
    Console.WriteLine($"Sum = {oddSum}");
}
else
{
    Console.WriteLine("No");
    Console.WriteLine($"Diff = {Math.Abs(oddSum - evenSum)}");
}