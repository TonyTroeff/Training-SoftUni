int n = int.Parse(Console.ReadLine());
int initialPoints = int.Parse(Console.ReadLine());

int newSeasonPoints = 0, wonTournamentsCount = 0;
for (int i = 0; i < n; i++)
{
    string position = Console.ReadLine();

    if (position == "W")
    {
        newSeasonPoints += 2000;
        wonTournamentsCount++;
    }
    else if (position == "F") { newSeasonPoints += 1200; }
    else if (position == "SF") { newSeasonPoints += 720; }
}

Console.WriteLine($"Final points: {initialPoints + newSeasonPoints}");
Console.WriteLine($"Average points: {newSeasonPoints / n}");
Console.WriteLine($"{wonTournamentsCount * 100.0 / n:f2}%");
