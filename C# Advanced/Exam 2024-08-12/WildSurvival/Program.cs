Queue<int> bees = new Queue<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
Stack<int> beeEaters = new Stack<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

while (bees.Count > 0 && beeEaters.Count > 0)
{
    int defenders = bees.Dequeue();
    int attackers = beeEaters.Pop();

    int attackingCapacity = attackers * 7;
    if (attackingCapacity > defenders)
    {
        int survivors = attackers - defenders / 7;

        int additive = 0;
        if (beeEaters.Count > 0) additive = beeEaters.Pop();

        beeEaters.Push(additive + survivors);
    }
    else if (attackingCapacity < defenders)
    {
        bees.Enqueue(defenders - attackingCapacity);
    }
}

Console.WriteLine("The final battle is over!");
if (bees.Count > 0)
    Console.WriteLine($"Bee groups left: {string.Join(", ", bees)}");
else if (beeEaters.Count > 0)
    Console.WriteLine($"Bee-eater groups left: {string.Join(", ", beeEaters)}");
else
    Console.WriteLine("But no one made it out alive!");