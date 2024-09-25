int[] limits = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
string parity = Console.ReadLine();

Predicate<int> predicate;
if (parity == "even") predicate = x => x % 2 == 0;
else if (parity == "odd") predicate = x => x % 2 != 0;
else predicate = x => false;

List<int> numbers = new List<int>();
for (int i = limits[0]; i <= limits[1]; i++)
{
    if (predicate(i)) numbers.Add(i);
}

Console.WriteLine(string.Join(' ', numbers));