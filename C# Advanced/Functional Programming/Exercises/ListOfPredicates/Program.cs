int n = int.Parse(Console.ReadLine());
int[] dividers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

List<Func<int, bool>> divisibilityPredicates = new List<Func<int, bool>>();
for (int i = 0; i < dividers.Length; i++)
{
    int currentDivider = dividers[i];
    divisibilityPredicates.Add(x => x % currentDivider == 0);
}

IEnumerable<int> result = Enumerable.Range(1, n);
foreach (Func<int, bool> predicate in divisibilityPredicates)
    result = result.Where(predicate);

Console.WriteLine(string.Join(' ', result));