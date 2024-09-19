Dictionary<string, Dictionary<string, int>> clothesByColor = new Dictionary<string, Dictionary<string, int>>();

int n = int.Parse(Console.ReadLine());
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split(" -> ");
    
    string color = data[0];
    string[] clothes = data[1].Split(',');

    if (!clothesByColor.ContainsKey(color))
        clothesByColor[color] = new Dictionary<string, int>();

    foreach (string item in clothes)
    {
        if (!clothesByColor[color].ContainsKey(item))
            clothesByColor[color][item] = 0;
        clothesByColor[color][item]++;
    }
}

string[] searchRequest = Console.ReadLine().Split();
foreach (var (color, clothes) in clothesByColor)
{
    Console.WriteLine($"{color} clothes:");

    foreach (var (item, count) in clothes)
    {
        string suffix = string.Empty;
        if (color == searchRequest[0] && item == searchRequest[1])
            suffix = " (found!)";

        Console.WriteLine($"* {item} - {count}{suffix}");
    }
}