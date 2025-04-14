string[] ops = Console.ReadLine().Split();

Stack<int> stack = new Stack<int>();
stack.Push(int.Parse(ops[0]));

for (int i = 1; i < ops.Length; i += 2)
{
    string operation = ops[i];
    int arg = int.Parse(ops[i + 1]);

    int next = stack.Pop(); // Peek() can be used here as well - then we will have "history" of operation results.
    if (operation == "+") next += arg;
    else if (operation == "-") next -= arg;

    stack.Push(next);
}

Console.WriteLine(stack.Peek());