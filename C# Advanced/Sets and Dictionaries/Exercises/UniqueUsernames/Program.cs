int n = int.Parse(Console.ReadLine());

HashSet<string> seen = new HashSet<string>();
for (int i = 0; i < n; i++)
{
    string username = Console.ReadLine();
    if (seen.Add(username)) Console.WriteLine(username);
}