int[] dimensions = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
int rows = dimensions[0], cols = dimensions[1];

string text = Console.ReadLine();

char[,] matrix = new char[rows, cols];
int textIndex = 0;

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < cols; j++, textIndex = (textIndex + 1) % text.Length)
    {
        int col;
        if (i % 2 == 0) col = j;
        else col = cols - (j + 1);

        matrix[i, col] = text[textIndex];
    }
}

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < cols; j++)
        Console.Write(matrix[i, j]);

    Console.WriteLine();
}