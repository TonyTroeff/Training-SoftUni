int[] initialNumbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

Stack<int> stack = new Stack<int>(initialNumbers);

string input;
while ((input = Console.ReadLine().ToLower()) != "end")
{
    string[] commandData = input.Split();
    string commandName = commandData[0];

    if (commandName == "add")
    {
        int first = int.Parse(commandData[1]);
        int second = int.Parse(commandData[2]);

        stack.Push(first);
        stack.Push(second);
    }
    else if (commandName == "remove")
    {
        int n = int.Parse(commandData[1]);
        if (n > stack.Count) continue;

        for (int i = 0; i < n; i++)
            stack.Pop();
    }
}

int sum = 0;
while (stack.Count > 0)
    sum += stack.Pop();

Console.WriteLine($"Sum: {sum}");