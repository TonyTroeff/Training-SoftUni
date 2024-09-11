int n = int.Parse(Console.ReadLine());

int[][] matrix = new int[n][];
for (int i = 0; i < n; i++)
    matrix[i] = Console.ReadLine().Split().Select(int.Parse).ToArray();

string command = Console.ReadLine();
while (command != "END")
{
    string[] data = command.Split();
    string operation = data[0];
    int row = int.Parse(data[1]), col = int.Parse(data[2]), val = int.Parse(data[3]);

    if (row < 0 || row >= n || col < 0 || col >= matrix[row].Length)
        Console.WriteLine("Invalid coordinates");
    else if (operation == "Add")
        matrix[row][col] += val;
    else if (operation == "Subtract")
        matrix[row][col] -= val;

    command = Console.ReadLine();
}

for (int i = 0; i < n; i++) Console.WriteLine(string.Join(' ', matrix[i]));