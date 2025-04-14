Dictionary<string, Dictionary<string, List<string>>> map = new Dictionary<string, Dictionary<string, List<string>>>();

int n = int.Parse(Console.ReadLine());
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split();
    string continent = data[0], country = data[1], city = data[2];

    if (!map.ContainsKey(continent)) map[continent] = new Dictionary<string, List<string>>();
    if (!map[continent].ContainsKey(country)) map[continent][country] = new List<string>();
    map[continent][country].Add(city);
}

foreach (var (continent, countries) in map)
{
    Console.WriteLine($"{continent}:");

    foreach (var (country, cities) in countries)
        Console.WriteLine($"  {country} -> {string.Join(", ", cities)}");
}