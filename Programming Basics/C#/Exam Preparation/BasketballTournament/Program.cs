int wonMatches = 0, totalMatches = 0;

string tournament = Console.ReadLine();
while (tournament != "End of tournaments")
{
    int matchesCount = int.Parse(Console.ReadLine());

    for (int i = 0; i < matchesCount; i++)
    {
        int allyPoints = int.Parse(Console.ReadLine());
        int enemyPoints = int.Parse(Console.ReadLine());

        if (allyPoints > enemyPoints)
        {
            wonMatches++;
            Console.WriteLine($"Game {i + 1} of tournament {tournament}: win with {allyPoints - enemyPoints} points.");
        }
        else Console.WriteLine($"Game {i + 1} of tournament {tournament}: lost with {enemyPoints - allyPoints} points.");
    }

    totalMatches += matchesCount;
    tournament = Console.ReadLine();
}

double winPercentage = 100.0 * wonMatches / totalMatches;
Console.WriteLine($"{winPercentage:f2}% matches win");

double losePercentage = 100.0 * (totalMatches - wonMatches) / totalMatches;
Console.WriteLine($"{losePercentage:f2}% matches lost");