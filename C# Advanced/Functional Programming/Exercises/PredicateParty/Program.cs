Dictionary<string, Func<string, Predicate<string>>> predicateBuilders = new Dictionary<string, Func<string, Predicate<string>>>
{
    ["StartsWith"] = arg =>
    {
        return el => el.StartsWith(arg);
    },
    ["EndsWith"] = arg =>
    {
        return el => el.EndsWith(arg);
    },
    ["Length"] = arg =>
    {
        int length = int.Parse(arg);
        return el => el.Length == length;
    },
};

Dictionary<string, Action<List<string>, Predicate<string>>> actions = new Dictionary<string, Action<List<string>, Predicate<string>>>
{
    ["Double"] = (list, predicate) =>
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (predicate(list[i]))
                list.Insert(i, list[i]);
        }
    },
    ["Remove"] = (list, predicate) =>
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (predicate(list[i]))
                list.RemoveAt(i);
        }
    }
};

List<string> people = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

string command = Console.ReadLine();
while (command != "Party!")
{
    string[] data = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    Predicate<string> predicate = predicateBuilders[data[1]](data[2]);
    actions[data[0]](people, predicate);

    command = Console.ReadLine();
}

if (people.Count == 0) Console.WriteLine("Nobody is going to the party!");
else Console.WriteLine($"{string.Join(", ", people)} are going to the party!");