HashSet<string> vloggers = new HashSet<string>();
Dictionary<string, HashSet<string>> inEdges = new Dictionary<string, HashSet<string>>();
Dictionary<string, HashSet<string>> outEdges = new Dictionary<string, HashSet<string>>();

string input = Console.ReadLine();
while (input != "Statistics")
{
    string[] commandData = input.Split();

    if (commandData.Length == 4 && commandData[1] == "joined")
    {
        string vloggerName = commandData[0];

        if (vloggers.Add(vloggerName))
        {
            inEdges[vloggerName] = new HashSet<string>();
            outEdges[vloggerName] = new HashSet<string>();
        }
    }
    else if (commandData.Length == 3 && commandData[1] == "followed")
    {
        string sender = commandData[0], receiver = commandData[2];

        if (sender != receiver && vloggers.Contains(sender) && vloggers.Contains(receiver))
        {
            outEdges[sender].Add(receiver);
            inEdges[receiver].Add(sender);
        }
    }

    input = Console.ReadLine();
}

Console.WriteLine($"The V-Logger has a total of {vloggers.Count} vloggers in its logs.");

int index = 1;
foreach (string vlogger in vloggers.OrderByDescending(x => inEdges[x].Count).ThenBy(x => outEdges[x].Count))
{
    Console.WriteLine($"{index}. {vlogger} : {inEdges[vlogger].Count} followers, {outEdges[vlogger].Count} following");

    if (index == 1)
    {
        foreach (string follower in inEdges[vlogger].OrderBy(x => x))
            Console.WriteLine($"*  {follower}");
    }

    index++;
}