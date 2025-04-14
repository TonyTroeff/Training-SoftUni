string[] players = Console.ReadLine().Split();
int n = int.Parse(Console.ReadLine());

Queue<string> queue = new Queue<string>(players);
while (queue.Count > 1)
{
    int iterations = (n - 1) % queue.Count; // This line is for performance improvements - full rotations can be ignored.
    for (int i = 0; i < iterations; i++)
        queue.Enqueue(queue.Dequeue());

    Console.WriteLine($"Removed {queue.Dequeue()}");
}

Console.WriteLine($"Last is {queue.Dequeue()}");