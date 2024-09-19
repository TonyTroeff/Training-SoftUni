string input = Console.ReadLine();

HashSet<string> banned = new HashSet<string>();
Dictionary<string, int> results = new Dictionary<string, int>();
Dictionary<string, int> submissions = new Dictionary<string, int>();

while (input != "exam finished")
{
    string[] data = input.Split('-');

    if (data.Length == 3)
    {
        string student = data[0], language = data[1];
        int points = int.Parse(data[2]);

        if (!results.ContainsKey(student) || results[student] < points)
            results[student] = points;

        if (!submissions.ContainsKey(language))
            submissions[language] = 0;
        submissions[language]++;
    }
    else if (data.Length == 2 && data[1] == "banned")
    {
        banned.Add(data[0]);
    }

    input = Console.ReadLine();
}

Console.WriteLine("Results:");
foreach (var (student, points) in results.Where(x => !banned.Contains(x.Key)).OrderByDescending(x => x.Value).ThenBy(x => x.Key))
    Console.WriteLine($"{student} | {points}");

Console.WriteLine("Submissions:");
foreach (var (language, count) in submissions.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
    Console.WriteLine($"{language} - {count}");
