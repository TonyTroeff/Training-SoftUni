int[] parameters = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
int rows = parameters[0], cols = parameters[1];

int[,] matrix = new int[rows, cols];
for (int i = 0; i < rows; i++)
{
    int[] data = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

    for (int j = 0; j < cols; j++) matrix[i, j] = data[j];
}

int originRow = -1, originCol = -1, maxSum = int.MinValue;
for (int i = 0; i < rows - 1; i++)
{
    for (int j = 0; j < cols - 1; j++)
    {
        int sum = matrix[i, j] + matrix[i, j + 1] + matrix[i + 1, j] + matrix[i + 1, j + 1];

        if (sum > maxSum)
        {
            originRow = i;
            originCol = j;
            maxSum = sum;
        }
    }
}

Console.WriteLine($"{matrix[originRow, originCol]} {matrix[originRow, originCol + 1]}");
Console.WriteLine($"{matrix[originRow + 1, originCol]} {matrix[originRow + 1, originCol + 1]}");
Console.WriteLine(maxSum);