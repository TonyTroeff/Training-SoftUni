namespace NavyBattle;

public class Program
{
    const char SubmarineCharacter = 'S';

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

        (int submarineRow, int submarineCol) = FindSubmarine(field);
        int damage = 0, wins = 0;
        while (damage < 3 && wins < 3)
        {
            string command = Console.ReadLine();

            (int rowChange, int colChange) = moves[command];
            int nextRow = submarineRow + rowChange, nextCol = submarineCol + colChange;

            if (field[nextRow, nextCol] == '*') damage++;
            else if (field[nextRow, nextCol] == 'C') wins++;

            field[submarineRow, submarineCol] = '-';

            submarineRow = nextRow;
            submarineCol = nextCol;

            field[submarineRow, submarineCol] = SubmarineCharacter;
        }

        if (damage == 3) Console.WriteLine($"Mission failed, U-9 disappeared! Last known coordinates [{submarineRow}, {submarineCol}]!");
        else if (wins == 3) Console.WriteLine("Mission accomplished, U-9 has destroyed all battle cruisers of the enemy!");

        PrintMatrix(field);
    }

    static char[,] ReadSquareMatrix(int n)
    {
        char[,] matrix = new char[n, n];

        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine();

            for (int j = 0; j < n; j++)
                matrix[i, j] = input[j];
        }

        return matrix;
    }

    static (int Row, int Col) FindSubmarine(char[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == SubmarineCharacter)
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
