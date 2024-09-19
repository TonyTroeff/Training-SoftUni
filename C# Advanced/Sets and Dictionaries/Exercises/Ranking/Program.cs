Dictionary<string, string> passwordByContest = new Dictionary<string, string>();
Dictionary<string, Dictionary<string, int>> ranking = new Dictionary<string, Dictionary<string, int>>();

string input = Console.ReadLine();
while (input != "end of contests")
{
    string[] contestData = input.Split(':');
    string name = contestData[0], password = contestData[1];

    passwordByContest[name] = password;

    input = Console.ReadLine();
}

input = Console.ReadLine();
while (input != "end of submissions")
{
    string[] submissionData = input.Split("=>");
    string contest = submissionData[0], password = submissionData[1], username = submissionData[2];
    int points = int.Parse(submissionData[3]);

    if (passwordByContest.ContainsKey(contest) && passwordByContest[contest] == password)
    {
        if (!ranking.ContainsKey(username))
            ranking[username] = new Dictionary<string, int>();

        if (!ranking[username].ContainsKey(contest) || points > ranking[username][contest])
            ranking[username][contest] = points;
    }

    input = Console.ReadLine();
}

string bestCandidate = string.Empty;
int maxScore = 0;
foreach (var (username, contests) in ranking)
{
    int currentScore = contests.Sum(x => x.Value);
    if (currentScore > maxScore)
    {
        bestCandidate = username;
        maxScore = currentScore;
    }
}

Console.WriteLine($"Best candidate is {bestCandidate} with total {maxScore} points.");
Console.WriteLine("Ranking:");

foreach (var (username, contests) in ranking.OrderBy(x => x.Key))
{
    Console.WriteLine(username);

    foreach (var (contest, points) in contests.OrderByDescending(x => x.Value))
        Console.WriteLine($"#  {contest} -> {points}");
}