int[] dimensions = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
int rows = dimensions[0], cols = dimensions[1];

char[,] matrix = new char[rows, cols];
for (int i = 0; i < rows; i++)
{
    char[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();
    for (int j = 0; j < cols; j++) matrix[i, j] = data[j];
}

int count = 0;
for (int i = 0; i < rows - 1; i++)
{
    for (int j = 0; j < cols - 1; j++)
    {
        if (matrix[i, j] == matrix[i, j + 1] && matrix[i, j] == matrix[i + 1, j] && matrix[i, j] == matrix[i + 1, j + 1])
            count++;
    }
}

Console.WriteLine(count);