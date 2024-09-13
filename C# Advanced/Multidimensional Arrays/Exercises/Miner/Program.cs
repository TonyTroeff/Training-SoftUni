Dictionary<string, int[]> directions = new Dictionary<string, int[]>
{
    ["up"] = new int[] { -1, 0 },
    ["right"] = new int[] { 0, 1 },
    ["down"] = new int[] { 1, 0 },
    ["left"] = new int[] { 0, -1 }
};

int n = int.Parse(Console.ReadLine());
string[] commands = Console.ReadLine().Split();

string[,] matrix = new string[n, n];
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for (int j = 0; j < n; j++) matrix[i, j] = data[j];
}

int minerRow = -1, minerCol = -1;
int endRow = -1, endCol = -1;
int totalCoals = 0, collectedCoals = 0;
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < n; j++)
    {
        if (matrix[i, j] == "c") totalCoals++;
        else if (matrix[i, j] == "s")
        {
            minerRow = i;
            minerCol = j;
        }
        else if (matrix[i, j] == "e")
        {
            endRow = i;
            endCol = j;
        }
    }
}

bool finishedEarly = false;

for (int i = 0; !finishedEarly && i < commands.Length; i++)
{
    if (!directions.ContainsKey(commands[i])) continue;
    int[] coordinatesChanges = directions[commands[i]];

    int nextRow = minerRow + coordinatesChanges[0];
    if (nextRow < 0 || nextRow >= n) continue;

    int nextCol = minerCol + coordinatesChanges[1];
    if (nextCol < 0 || nextCol >= n) continue;

    bool nextPosIsCoal = matrix[nextRow, nextCol] == "c";

    matrix[minerRow, minerCol] = "*";

    minerRow = nextRow;
    minerCol = nextCol;

    if (nextPosIsCoal && ++collectedCoals == totalCoals)
    {
        Console.WriteLine($"You collected all coals! ({minerRow}, {minerCol})");
        finishedEarly = true;
    }
    else if (minerRow == endRow && minerCol == endCol)
    {
        Console.WriteLine($"Game over! ({minerRow}, {minerCol})");
        finishedEarly = true;
    }
}

if (!finishedEarly)
    Console.WriteLine($"{totalCoals - collectedCoals} coals left. ({minerRow}, {minerCol})");