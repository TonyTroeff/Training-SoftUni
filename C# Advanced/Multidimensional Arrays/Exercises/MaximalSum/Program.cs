const int k = 3;

int[] dimensions = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
int rows = dimensions[0], cols = dimensions[1];

int[,] matrix = new int[rows, cols];
for (int i = 0; i < rows; i++)
{
    int[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    for (int j = 0; j < cols; j++) matrix[i, j] = data[j];
}

int maxSum = int.MinValue, originRow = -1, originCol = -1;
for (int i = 0; i < rows - (k - 1); i++)
{
    for (int j = 0; j < cols - (k - 1); j++)
    {
        int currentSum = 0;
        for (int rowOffset = 0; rowOffset < k; rowOffset++)
        {
            for (int colOffset = 0; colOffset < k; colOffset++)
                currentSum += matrix[i + rowOffset, j + colOffset];
        }

        if (currentSum > maxSum)
        {
            maxSum = currentSum;
            originRow = i;
            originCol = j;
        }
    }
}

Console.WriteLine($"Sum = {maxSum}");
for (int rowOffset = 0; rowOffset < k; rowOffset++)
{
    for (int colOffset = 0; colOffset < k; colOffset++)
    {
        if (colOffset > 0) Console.Write(' ');
        Console.Write(matrix[originRow + rowOffset, originCol + colOffset]);
    }

    Console.WriteLine();
}