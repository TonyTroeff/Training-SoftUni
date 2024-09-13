int n = int.Parse(Console.ReadLine());

double[][] matrix = new double[n][];
for (int i = 0; i < n; i++)
    matrix[i] = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

for (int i = 1; i < n; i++)
{
    double multiplier;
    if (matrix[i - 1].Length == matrix[i].Length) multiplier = 2;
    else multiplier = 0.5;
     
    for (int j = 0; j < matrix[i - 1].Length; j++)
        matrix[i - 1][j] *= multiplier;

    for (int j = 0; j < matrix[i].Length; j++)
        matrix[i][j] *= multiplier;
}

string command = Console.ReadLine();
while (command != "End")
{
    string[] data = command.Split();
    ProcessCommand(matrix, data);

    command = Console.ReadLine();
}

foreach (double[] row in matrix)
    Console.WriteLine(string.Join(' ', row));

static void ProcessCommand(double[][] matrix, string[] data)
{
    if (data.Length != 4) return;

    string operation = data[0];
    int row = int.Parse(data[1]), col = int.Parse(data[2]);
    double val = double.Parse(data[3]);

    if (row < 0 || row >= matrix.Length || col < 0 || col >= matrix[row].Length)
        return;

    if (operation == "Add") matrix[row][col] += val;
    else if (operation == "Subtract") matrix[row][col] -= val;
}