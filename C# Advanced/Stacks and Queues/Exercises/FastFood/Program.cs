int food = int.Parse(Console.ReadLine());

Queue<int> orders = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
int n = orders.Count;

int max = int.MinValue;
for (int i = 0; i < n; i++)
{
    int current = orders.Dequeue();
    max = Math.Max(max, current);
    orders.Enqueue(current);
}

while (orders.Count > 0)
{
    if (orders.Peek() > food) break;
    food -= orders.Dequeue();
}

Console.WriteLine(max);

if (orders.Count == 0) Console.WriteLine("Orders complete");
else Console.WriteLine($"Orders left: {string.Join(' ', orders)}");