namespace EscapeTheMaze;

public class Program
{
    const char PlayerCharacter = 'P';
    const char ExitCharacter = 'X';
    const char MonsterCharacter = 'M';
    const char HealthPotionCharacter = 'H';
    const char EmptySpaceCharacter = '-';

    static void Main()
    {
        Dictionary<string, (int RowChange, int ColChange)> moves = new Dictionary<string, (int RowChange, int ColChange)>
        {
            ["up"] = (-1, 0),
            ["right"] = (0, 1),
            ["down"] = (1, 0),
            ["left"] = (0, -1)
        };

        int n = int.Parse(Console.ReadLine());
        char[,] field = ReadSquareMatrix(n);

        (int playerRow, int playerCol) = FindPlayer(field);
        int health = 100;
        bool exitIsReached = false;
        while (health > 0 && !exitIsReached)
        {
            string command = Console.ReadLine();

            (int rowChange, int colChange) = moves[command];
            int nextRow = playerRow + rowChange, nextCol = playerCol + colChange;
            if (nextRow < 0 || nextRow >= n || nextCol < 0 || nextCol >= n) continue;

            if (field[nextRow, nextCol] == ExitCharacter)
            {
                exitIsReached = true;
            }
            else if (field[nextRow, nextCol] == MonsterCharacter)
            {
                health = Math.Max(health - 40, 0);
            }
            else if (field[nextRow, nextCol] == HealthPotionCharacter)
            {
                health = Math.Min(health + 15, 100);
            }

            field[playerRow, playerCol] = EmptySpaceCharacter;

            playerRow = nextRow;
            playerCol = nextCol;

            field[playerRow, playerCol] = PlayerCharacter;
        }

        if (exitIsReached) Console.WriteLine("Player escaped the maze. Danger passed!");
        else Console.WriteLine("Player is dead. Maze over!");

        Console.WriteLine($"Player's health: {health} units");
        PrintMatrix(field);
    }

    static char[,] ReadSquareMatrix(int n)
    {
        char[,] field = new char[n, n];
        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine();
            for (int j = 0; j < n; j++)
                field[i, j] = input[j];
        }

        return field;
    }

    static (int Row, int Col) FindPlayer(char[,] field)
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == PlayerCharacter)
                    return (i, j);
            }
        }

        return (-1, -1);
    }

    static void PrintMatrix(char[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                Console.Write(matrix[i, j]);

            Console.WriteLine();
        }
    }
}
