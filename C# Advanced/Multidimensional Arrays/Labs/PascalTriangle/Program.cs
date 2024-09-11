int n = int.Parse(Console.ReadLine());

long[][] matrix = new long[n][];
for (int i = 0; i < n; i++)
{
    matrix[i] = new long[i + 1];
    matrix[i][0] = 1;
    matrix[i][i] = 1;
}

for (int i = 2; i < n; i++)
{
    for (int j = 1; j < i; j++)
        matrix[i][j] = matrix[i - 1][j - 1] + matrix[i - 1][j];
}

for (int i = 0; i < n; i++) Console.WriteLine(string.Join(' ', matrix[i]));