HashSet<string> set = new HashSet<string>();

int n = int.Parse(Console.ReadLine());
for (int i = 0; i < n; i++)
{
    string name = Console.ReadLine();
    set.Add(name);
}

foreach (string name in set)
    Console.WriteLine(name);