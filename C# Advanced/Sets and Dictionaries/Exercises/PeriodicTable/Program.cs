int n = int.Parse(Console.ReadLine());

HashSet<string> uniqueElements = new HashSet<string>();
for (int i = 0; i < n; i++)
{
    string[] currentElements = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    foreach (string element in currentElements) uniqueElements.Add(element);
}

Console.WriteLine(string.Join(' ', uniqueElements.OrderBy(x => x)));