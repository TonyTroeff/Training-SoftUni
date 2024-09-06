Stack<string> history = new Stack<string>();
history.Push(string.Empty);

int n = int.Parse(Console.ReadLine());
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split();
    string prev = history.Peek();

    int command = int.Parse(data[0]);
    if (command == 1)
    {
        string appendix = data[1];
        history.Push(prev + appendix);
    }
    else if (command == 2)
    {
        int deleteCount = int.Parse(data[1]);
        history.Push(prev.Substring(0, prev.Length - deleteCount));
    }
    else if (command == 3)
    {
        int index = int.Parse(data[1]);
        Console.WriteLine(prev[index - 1]);
    }
    else if (command == 4)
    {
        history.Pop();
    }
}
