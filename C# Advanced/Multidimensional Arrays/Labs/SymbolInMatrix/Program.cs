int n = int.Parse(Console.ReadLine());

char[,] matrix = new char[n, n];
for (int i = 0; i < n; i++)
{
    string data = Console.ReadLine();

    for (int j = 0; j < n; j++) matrix[i, j] = data[j];
}

char requestedSymbol = char.Parse(Console.ReadLine());

int row = -1, col = -1;
bool isFound = false;

for (int i = 0; !isFound && i < n; i++)
{
    for (int j = 0; !isFound && j < n; j++)
    {
        if (matrix[i, j] == requestedSymbol)
        {
            row = i;
            col = j;
            isFound = true;
        }
    }
}

if (isFound) Console.WriteLine($"({row}, {col})");
else Console.WriteLine($"{requestedSymbol} does not occur in the matrix");
