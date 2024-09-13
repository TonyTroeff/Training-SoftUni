internal class Program
{
    private static readonly int[] directions = new[] { 0, 1, 0, -1, 0 };
    private static readonly Dictionary<char, int[]> playerMovement = new Dictionary<char, int[]>
    {
        ['U'] = new int[] { -1, 0 },
        ['R'] = new int[] { 0, 1 },
        ['D'] = new int[] { 1, 0 },
        ['L'] = new int[] { 0, -1 }
    };

    public static void Main()
    {
        int[] dimensions = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        int rows = dimensions[0], cols = dimensions[1];

        char[,] matrix = new char[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            string data = Console.ReadLine();
            for (int j = 0; j < cols; j++) matrix[i, j] = data[j];
        }

        string commands = Console.ReadLine();

        int[] playerPos = new int[2] { -1, -1 };
        Queue<int[]> bunnies = new Queue<int[]>();
        ReadMatrix(matrix, playerPos, bunnies);

        bool isDead = false, isOutside = false;
        for (int i = 0; !isDead && !isOutside && i < commands.Length; i++)
        {
            if (!playerMovement.ContainsKey(commands[i])) continue;

            int[] coordinatesChanges = playerMovement[commands[i]];

            int nextRow = playerPos[0] + coordinatesChanges[0];
            int nextCol = playerPos[1] + coordinatesChanges[1];

            matrix[playerPos[0], playerPos[1]] = '.';

            if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols) isOutside = true;
            else
            {
                playerPos[0] = nextRow;
                playerPos[1] = nextCol;

                if (matrix[playerPos[0], playerPos[1]] == 'B') isDead = true;
                else matrix[playerPos[0], playerPos[1]] = 'P';
            }

            if (!SpreadBunnies(matrix, bunnies)) isDead = true;
        }

        PrintMatrix(matrix);
        if (isOutside) Console.WriteLine($"won: {playerPos[0]} {playerPos[1]}");
        else Console.WriteLine($"dead: {playerPos[0]} {playerPos[1]}");
    }

    private static void ReadMatrix(char[,] matrix, int[] playerPos, Queue<int[]> bunnies)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 'B') bunnies.Enqueue(new[] { i, j }); 
                else if (matrix[i, j] == 'P')
                {
                    playerPos[0] = i;
                    playerPos[1] = j;
                }
            }
        }
    }

    private static bool SpreadBunnies(char[,] matrix, Queue<int[]> bunnies)
    {
        bool stillAlive = true;

        int bunniesCount = bunnies.Count;
        for (int i = 0; i < bunniesCount; i++)
        {
            int[] bunnyPos = bunnies.Dequeue();
            for (int j = 1; j < directions.Length; j++)
            {
                int nextRow = bunnyPos[0] + directions[j - 1];
                if (nextRow < 0 || nextRow >= matrix.GetLength(0))
                    continue;

                int nextCol = bunnyPos[1] + directions[j];
                if (nextCol < 0 || nextCol >= matrix.GetLength(1))
                    continue;

                if (matrix[nextRow, nextCol] == 'P') stillAlive = false;
                
                if (matrix[nextRow, nextCol] != 'B')
                {
                    bunnies.Enqueue(new[] { nextRow, nextCol });
                    matrix[nextRow, nextCol] = 'B';
                }
            }
        }

        return stillAlive;
    }

    private static void PrintMatrix(char[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                Console.Write(matrix[i, j]);

            Console.WriteLine();
        }
    }
}