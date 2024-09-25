int n = int.Parse(Console.ReadLine());
string[] words = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

Predicate<string> predicate = x => x.Length <= n;

for (int i = 0; i < words.Length; i++)
{
    if (predicate(words[i]))
        Console.WriteLine(words[i]);
}