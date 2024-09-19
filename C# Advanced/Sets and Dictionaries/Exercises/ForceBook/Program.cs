public class Program
{
    private const string firstSeparator = " | ";
    private const string secondSeparator = " -> ";

    public static void Main()
    {
        Dictionary<string, string> sideByUser = new Dictionary<string, string>();
        Dictionary<string, HashSet<string>> usersBySide = new Dictionary<string, HashSet<string>>();

        string input = Console.ReadLine();
        while (input != "Lumpawaroo")
        {
            int firstSeparatorIndex = input.IndexOf(firstSeparator);
            if (firstSeparatorIndex != -1)
            {
                string side = input[..firstSeparatorIndex];
                string user = input[(firstSeparatorIndex + firstSeparator.Length)..];

                if (!sideByUser.ContainsKey(user))
                    AddUser(user, side, sideByUser, usersBySide);
            }
            else
            {
                int secondSeparatorIndex = input.IndexOf(" -> ");
                if (secondSeparatorIndex != -1)
                {
                    string user = input[..secondSeparatorIndex];
                    string side = input[(secondSeparatorIndex + secondSeparator.Length)..];

                    if (sideByUser.ContainsKey(user))
                        RemoveUser(user, sideByUser, usersBySide);

                    AddUser(user, side, sideByUser, usersBySide);

                    Console.WriteLine($"{user} joins the {side} side!");
                }
            }


            input = Console.ReadLine();
        }

        foreach (var (side, users) in usersBySide.Where(x => x.Value.Count > 0).OrderByDescending(x => x.Value.Count).ThenBy(x => x.Key))
        {
            Console.WriteLine($"Side: {side}, Members: {users.Count}");

            foreach (var user in users.OrderBy(x => x))
                Console.WriteLine($"! {user}");
        }
    }

    private static void RemoveUser(string user, Dictionary<string, string> sideByUser, Dictionary<string, HashSet<string>> usersBySide)
    {
        if (!sideByUser.ContainsKey(user)) return;

        string prevSide = sideByUser[user];
        sideByUser.Remove(user);
        usersBySide[prevSide].Remove(user);
    }

    private static void AddUser(string user, string side, Dictionary<string, string> sideByUser, Dictionary<string, HashSet<string>> usersBySide)
    {
        sideByUser[user] = side;

        if (!usersBySide.ContainsKey(side)) usersBySide[side] = new HashSet<string>();
        usersBySide[side].Add(user);
    }
}