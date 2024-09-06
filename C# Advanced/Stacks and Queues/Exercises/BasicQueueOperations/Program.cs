int[] setup = Console.ReadLine().Split().Select(int.Parse).ToArray();
int n = setup[0], s = setup[1], x = setup[2];

int[] data = Console.ReadLine().Split().Select(int.Parse).ToArray();

Queue<int> queue = new Queue<int>();
for (int i = 0; i < n; i++) queue.Enqueue(data[i]);
for (int i = 0; i < s; i++) queue.Dequeue();

if (queue.Count == 0) Console.WriteLine(0);
else
{
    int min = int.MaxValue;
    bool isFound = false;
    while (queue.Count != 0)
    {
        int number = queue.Dequeue();
        if (!isFound && number == x) isFound = true;

        min = Math.Min(min, number);
    }

    if (isFound) Console.WriteLine("true");
    else Console.WriteLine(min);
}