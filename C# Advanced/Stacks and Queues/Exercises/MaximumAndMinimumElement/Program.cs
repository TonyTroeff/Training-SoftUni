Stack<int> stack = new Stack<int>();

int n = int.Parse(Console.ReadLine());
for (int i = 0; i < n; i++)
{
    int[] data = Console.ReadLine().Split().Select(int.Parse).ToArray();
    int queryCode = data[0];

    if (queryCode == 1) stack.Push(data[1]);
    else if (queryCode == 2) stack.Pop();
    else if (queryCode == 3)
    {
        if (stack.Count > 0) Console.WriteLine(stack.Max());
    }
    else if (queryCode == 4)
    {
        if (stack.Count > 0) Console.WriteLine(stack.Min());
    }
}

Console.WriteLine(string.Join(", ", stack));