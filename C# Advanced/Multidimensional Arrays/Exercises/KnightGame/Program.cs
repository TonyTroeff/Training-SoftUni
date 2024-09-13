int[,] directions = { { 2, -1 }, { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 }, { -2, -1 }, { -1, -2 }, { 1, -2 } };

int n = int.Parse(Console.ReadLine());
char[,] board = new char[n, n];

for (int i = 0; i < n; i++)
{
    string data = Console.ReadLine();
    for (int j = 0; j < n; j++) board[i, j] = data[j];
}

int removalsCount = 0;

bool tryRemove = true;
while (tryRemove)
{
    int maxThreats = 0, maxRow = -1, maxCol = -1;

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            if (board[i, j] != 'K') continue;

            int currentThreats = 0;
            for (int k = 0; k < directions.GetLength(0); k++)
            {
                int row = i + directions[k, 0];
                if (row < 0 || row >= n) continue;

                int col = j + directions[k, 1];
                if (col < 0 || col >= n) continue;

                if (board[row, col] == 'K') currentThreats++;
            }

            if (currentThreats > maxThreats)
            {
                maxThreats = currentThreats;
                maxRow = i;
                maxCol = j;
            }
        }
    }

    if (maxThreats > 0)
    {
        removalsCount++;
        board[maxRow, maxCol] = '0';
    }
    else tryRemove = false;
}

Console.WriteLine(removalsCount);