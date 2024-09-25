Action<int[], Func<int, int>> modify = (arr, change) =>
{
    for (int i = 0; i < arr.Length; i++)
        arr[i] = change(arr[i]);
};

Dictionary<string, Action<int[]>> operations = new Dictionary<string, Action<int[]>>
{
    ["add"] = arr => modify(arr, x => x + 1),
    ["multiply"] = arr => modify(arr, x => x * 2),
    ["subtract"] = arr => modify(arr, x => x - 1),
    ["print"] = arr => Console.WriteLine(string.Join(' ', arr)),
};

int[] numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

string command = Console.ReadLine();
while (command != "end")
{
    if (operations.ContainsKey(command))
        operations[command](numbers);

    command = Console.ReadLine();
}