double[] array = Console.ReadLine().Split().Select(double.Parse).ToArray();

Dictionary<double, int> counter = new Dictionary<double, int>();
foreach (double el in array)
{
    if (!counter.ContainsKey(el)) counter[el] = 0;
    counter[el]++;
}

foreach (var (el, count) in counter)
    Console.WriteLine($"{el} - {count} times");
