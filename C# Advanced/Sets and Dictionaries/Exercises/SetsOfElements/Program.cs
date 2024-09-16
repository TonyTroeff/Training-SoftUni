int[] sizes = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

HashSet<int> first = ReadUniqueNumbers(sizes[0]);
HashSet<int> second = ReadUniqueNumbers(sizes[1]);

Console.WriteLine(string.Join(' ', first.Where(second.Contains)));

static HashSet<int> ReadUniqueNumbers(int n)
{
    HashSet<int> result = new HashSet<int>();
    for (int i = 0; i < n; i++)
    {
        int number = int.Parse(Console.ReadLine());
        result.Add(number);
    }

    return result;
}