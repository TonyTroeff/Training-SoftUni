int[] setup = Console.ReadLine().Split().Select(int.Parse).ToArray();
int n = setup[0], s = setup[1], x = setup[2];

int[] data = Console.ReadLine().Split().Select(int.Parse).ToArray();

Stack<int> stack = new Stack<int>();
for (int i = 0; i < n; i++) stack.Push(data[i]);
for (int i = 0; i < s; i++) stack.Pop();

if (stack.Count == 0) Console.WriteLine(0);
else
{
    int min = int.MaxValue;
    bool isFound = false;
    while (stack.Count != 0)
    {
        int number = stack.Pop();
        if (!isFound && number == x) isFound = true;

        min = Math.Min(min, number);
    }

    if (isFound) Console.WriteLine("true");
    else Console.WriteLine(min);
}