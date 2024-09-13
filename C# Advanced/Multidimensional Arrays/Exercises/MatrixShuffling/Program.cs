int[] dimensions = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
int rows = dimensions[0], cols = dimensions[1];

string[,] matrix = new string[rows, cols];
for (int i = 0; i < rows; i++)
{
    string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    for (int j = 0; j < cols; j++) matrix[i, j] = data[j];
}

string command = Console.ReadLine();
while (command != "END")
{
    string[] data = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    
    if (Swap(matrix, data)) PrintMatrix(matrix);
    else Console.WriteLine("Invalid input!");

    command = Console.ReadLine();
}

static bool Swap(string[,] matrix, string[] data)
{
    if (data.Length != 5 || data[0] != "swap") return false;
    
    int r1 = int.Parse(data[1]), c1 = int.Parse(data[2]);
    int r2 = int.Parse(data[3]), c2 = int.Parse(data[4]);

    bool isValid = 0 <= r1 && r1 < matrix.GetLength(0) && 0 <= c1 && c1 < matrix.GetLength(1)
        && 0 <= r2 && r2 < matrix.GetLength(0) && 0 <= c2 && c2 < matrix.GetLength(1);
    if (!isValid) return false;

    string swap = matrix[r1, c1];
    matrix[r1, c1] = matrix[r2, c2];
    matrix[r2, c2] = swap;

    return true;
}

static void PrintMatrix(string[,] matrix)
{
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (j > 0) Console.Write(' ');
            Console.Write(matrix[i, j]);
        }

        Console.WriteLine();
    }
}