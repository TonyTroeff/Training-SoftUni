int n = int.Parse(Console.ReadLine());

int climbersCount = 0, p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0;
for (int i = 0; i < n; i++)
{
    int peopleCount = int.Parse(Console.ReadLine());

    if (peopleCount <= 5) p1 += peopleCount;
    else if (peopleCount <= 12) p2 += peopleCount;
    else if (peopleCount <= 25) p3 += peopleCount;
    else if (peopleCount <= 40) p4 += peopleCount;
    else p5 += peopleCount;

    climbersCount += peopleCount;
}

Console.WriteLine($"{p1 * 100.0 / climbersCount:f2}%");
Console.WriteLine($"{p2 * 100.0 / climbersCount:f2}%");
Console.WriteLine($"{p3 * 100.0 / climbersCount:f2}%");
Console.WriteLine($"{p4 * 100.0 / climbersCount:f2}%");
Console.WriteLine($"{p5 * 100.0 / climbersCount:f2}%");