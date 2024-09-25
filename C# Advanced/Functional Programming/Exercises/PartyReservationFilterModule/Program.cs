Dictionary<string, Func<string, Func<string, bool>>> filterBuilders = new Dictionary<string, Func<string, Func<string, bool>>>
{
    ["Starts with"] = arg =>
    {
        return el => el.StartsWith(arg);
    },
    ["Ends with"] = arg =>
    {
        return el => el.EndsWith(arg);
    },
    ["Length"] = arg =>
    {
        int length = int.Parse(arg);
        return el => el.Length == length;
    },
    ["Contains"] = arg =>
    {
        return el => el.Contains(arg);
    }
};

string[] invitations = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

Dictionary<(string Condition, string Arg), Func<string, bool>> activeFilters = new Dictionary<(string Condition, string Arg), Func<string, bool>>();

string command = Console.ReadLine();
while (command != "Print")
{
    string[] data = command.Split(';');
    string operation = data[0], conditionKey = data[1], arg = data[2];

    if (operation == "Add filter")
    {
        Func<string, Func<string, bool>> conditionBuilder = filterBuilders[conditionKey];
        activeFilters[(conditionKey, arg)] = conditionBuilder(arg);
    }
    else if (operation == "Remove filter")
    {
        activeFilters.Remove((conditionKey, arg));
    }

    command = Console.ReadLine();
}

IEnumerable<string> result = invitations;
foreach (Func<string, bool> filter in activeFilters.Values)
    result = result.Where(x => !filter(x));

Console.WriteLine(string.Join(' ', result));