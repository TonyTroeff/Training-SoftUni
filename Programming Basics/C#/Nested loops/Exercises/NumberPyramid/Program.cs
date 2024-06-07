int n = int.Parse(Console.ReadLine());

int row = 1;
int current = 1;

while (current <= n)
{
    for (int i = 0; i < row; i++)
    {
        Console.Write($"{current} ");
        if (++current > n) break;
    }

    row++;
    Console.WriteLine();
}