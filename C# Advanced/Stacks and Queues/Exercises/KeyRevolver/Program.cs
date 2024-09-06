int bulletPrice = int.Parse(Console.ReadLine());
int barrelSize = int.Parse(Console.ReadLine());
Stack<int> bullets = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
Queue<int> locks = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
int intelligence = int.Parse(Console.ReadLine());

int shot = 0;
while (locks.Count > 0 && bullets.Count > 0)
{
    int currentBullet = bullets.Pop();
    int currentLock = locks.Peek();

    if (currentBullet <= currentLock)
    {
        locks.Dequeue();
        Console.WriteLine("Bang!");
    }
    else Console.WriteLine("Ping!");

    intelligence -= bulletPrice;
    
    if (++shot % barrelSize == 0 && bullets.Count > 0) Console.WriteLine("Reloading!");
}

if (locks.Count == 0) Console.WriteLine($"{bullets.Count} bullets left. Earned ${intelligence}");
else Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}");