namespace ClearSkies;

public class Program
{
    private const char EmptySymbol = '-';
    private const char JetfighterSymbol = 'J';
    private const char EnemySymbol = 'E';
    private const char RepairSymbol = 'R';
    private const int InitialArmor = 300;

    private static readonly Dictionary<string, (int RowChange, int ColChange)> _directions = new Dictionary<string, (int RowChange, int ColChange)>
    {
        ["up"] = (-1, 0),
        ["right"] = (0, 1),
        ["down"] = (1, 0),
        ["left"] = (0, -1)
    };

    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        char[,] matrix = new char[n, n];
        for (int i = 0; i < n; i++)
        {
            string data = Console.ReadLine();
            for (int j = 0; j < n; j++)
                matrix[i, j] = data[j];
        }

        (int jetfighterRow, int jetfighterCol) = FindJetfighter(matrix);

        int enemiesCount = CountEnemies(matrix), armor = InitialArmor;
        while (enemiesCount > 0 && armor > 0)
        {
            string command = Console.ReadLine();
            (int rowChange, int colChange) = _directions[command];

            int nextRow = jetfighterRow + rowChange;
            int nextCol = jetfighterCol + colChange;

            if (matrix[nextRow, nextCol] == EnemySymbol)
            {
                if (--enemiesCount > 0)
                    armor -= 100;
            }
            else if (matrix[nextRow, nextCol] == RepairSymbol)
            {
                armor = InitialArmor;
            }

            matrix[jetfighterRow, jetfighterCol] = EmptySymbol;
            matrix[nextRow, nextCol] = JetfighterSymbol;

            jetfighterRow = nextRow;
            jetfighterCol = nextCol;
        }

        if (enemiesCount == 0)
            Console.WriteLine("Mission accomplished, you neutralized the aerial threat!");
        else
            Console.WriteLine($"Mission failed, your jetfighter was shot down! Last coordinates [{jetfighterRow}, {jetfighterCol}]!");

        PrintMatrix(matrix);
    }

    private static (int Row, int Col) FindJetfighter(char[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == JetfighterSymbol)
                    return (i, j);
            }
        }

        return (-1, -1);
    }

    private static int CountEnemies(char[,] matrix)
    {
        int count = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == EnemySymbol)
                    count++;
            }
        }

        return count;
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
