namespace CollectingStars;

public class Program
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());

        char[,] matrix = new char[n, n];
        for (int i = 0; i < n; i++)
        {
            string data = Console.ReadLine();
            for (int j = 0; j < n; j++)
                matrix[i, j] = data[j * 2];
        }

        (int playerRow, int playerCol) = FindPlayer(matrix);

        int stars = 2;
        while (stars > 0 && stars < 10)
        {
            string command = Console.ReadLine();

            int nextRow = playerRow, nextCol = playerCol;
            if (command == "up") nextRow--;
            else if (command == "right") nextCol++;
            else if (command == "left") nextCol--;
            else if (command == "down") nextRow++;

            if (nextRow < 0 || nextRow >= n || nextCol < 0 || nextCol >= n)
            {
                nextRow = 0;
                nextCol = 0;
            }

            if (matrix[nextRow, nextCol] == '#')
            {
                stars--;
            }
            else
            {
                if (matrix[nextRow, nextCol] == '*')
                {
                    stars++;
                }

                matrix[playerRow, playerCol] = '.';
                matrix[nextRow, nextCol] = 'P';

                playerRow = nextRow;
                playerCol = nextCol;
            }
        }

        if (stars == 0) Console.WriteLine("Game over! You are out of any stars.");
        else Console.WriteLine("You won! You have collected 10 stars.");

        Console.WriteLine($"Your final position is [{playerRow}, {playerCol}]");
        PrintMatrix(matrix);
    }

    private static (int Row, int Col) FindPlayer(char[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 'P') return (i, j);
            }
        }

        return (-1, -1);
    }

    private static void PrintMatrix(char[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (j > 0) Console.Write(' ');
                Console.Write(matrix[i, j]);
            }

            Console.WriteLine();
        }
    }
}
