Stack<int> cups = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).Reverse());
Stack<int> bottles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

int wastedLitters = 0;
while (cups.Count > 0 && bottles.Count > 0)
{
    int currentCup = cups.Pop();
    int currentBottle = bottles.Pop();

    if (currentBottle < currentCup) cups.Push(currentCup - currentBottle);
    else wastedLitters += currentBottle - currentCup;
}

if (bottles.Count > 0) Console.WriteLine($"Bottles: {string.Join(' ', bottles)}");
else Console.WriteLine($"Cups: {string.Join(' ', cups)}");

Console.WriteLine($"Wasted litters of water: {wastedLitters}");