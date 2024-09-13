int n = int.Parse(Console.ReadLine());
int[,] matrix = new int[n, n];

for (int i = 0; i < n; i++)
{
    int[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    for (int j = 0; j < n; j++) matrix[i, j] = data[j];
}

string[] bombsInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
foreach (string[] coordinates in bombsInfo.Select(x => x.Split(',')))
{
    int row = int.Parse(coordinates[0]), col = int.Parse(coordinates[1]);
    int damage = matrix[row, col];
    if (damage <= 0) continue;

    int rowIterStart = Math.Max(0, row - 1), rowIterEnd = Math.Min(n - 1, row + 1);
    int colIterStart = Math.Max(0, col - 1), colIterEnd = Math.Min(n - 1, col + 1);

    for (int i = rowIterStart; i <= rowIterEnd; i++)
    {
        for (int j = colIterStart; j <= colIterEnd; j++)
        {
            if (matrix[i, j] > 0)
                matrix[i, j] -= damage;
        }
    }
}

int aliveCells = 0, sum = 0;
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        if (matrix[i, j] > 0)
        {
            aliveCells++;
            sum += matrix[i, j];
        }
    }
}

Console.WriteLine($"Alive cells: {aliveCells}");
Console.WriteLine($"Sum: {sum}");

for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        if (j > 0) Console.Write(' ');
        Console.Write(matrix[i, j]);
    }

    Console.WriteLine();
}