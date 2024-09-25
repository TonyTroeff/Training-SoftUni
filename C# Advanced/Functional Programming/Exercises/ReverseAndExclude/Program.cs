int[] numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
int n = int.Parse(Console.ReadLine());

Predicate<int> predicate = x => x % n != 0;

List<int> result = new List<int>();
for (int i = numbers.Length - 1; i >= 0; i--)
{
    if (predicate(numbers[i])) result.Add(numbers[i]);
}

Console.WriteLine(string.Join(' ', result));