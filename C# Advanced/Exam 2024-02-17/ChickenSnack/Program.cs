Stack<int> money = new Stack<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

Queue<int> foodPrices = new Queue<int>(Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

int eaten = 0;
while (money.Count > 0 && foodPrices.Count > 0)
{
    int currentAmount = money.Pop();
    int currentPrice = foodPrices.Dequeue();

    if (currentAmount >= currentPrice)
    {
        eaten++;

        int change = currentAmount - currentPrice;
        if (change > 0)
        {
            int additive = 0;
            if (money.Count > 0) additive = money.Pop();

            money.Push(additive + change);
        }
    }
}

if (eaten == 0)
    Console.WriteLine("Henry remained hungry. He will try next weekend again.");
else if (eaten == 1)
    Console.WriteLine("Henry ate: 1 food.");
else if (eaten < 4)
    Console.WriteLine($"Henry ate: {eaten} foods.");
else
    Console.WriteLine($"Gluttony of the day! Henry ate {eaten} foods.");