int n = int.Parse(Console.ReadLine());

int[,] matrix = new int[n, n];
for (int i = 0; i < n; i++)
{
    int[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    for (int j = 0; j < n; j++) matrix[i, j] = data[j];
}

int primaryDiagonalSum = 0, secondaryDiagonalSum = 0;
for (int i = 0; i < n; i++)
{
    primaryDiagonalSum += matrix[i, i];
    secondaryDiagonalSum += matrix[i, n - (i + 1)];
}

Console.WriteLine(Math.Abs(primaryDiagonalSum - secondaryDiagonalSum));