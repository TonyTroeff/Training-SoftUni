int n = int.Parse(Console.ReadLine());

int max = int.MinValue, sum = 0;
for (int i = 0; i < n; i++)
{
    int currentNumber = int.Parse(Console.ReadLine());

    sum += currentNumber;
    if (currentNumber > max) max = currentNumber;
}

int diff = Math.Abs(sum - 2 * max);

if (diff == 0)
{
    Console.WriteLine("Yes");
    Console.WriteLine($"Sum = {max}");
}
else
{
    Console.WriteLine("No");
    Console.WriteLine($"Diff = {diff}");
}