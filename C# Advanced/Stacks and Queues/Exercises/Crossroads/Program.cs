int greenLight = int.Parse(Console.ReadLine());
int freeWindow = int.Parse(Console.ReadLine());

Queue<string> pending = new Queue<string>();

int totalCarsPassed = 0;
string input = Console.ReadLine();
while (input != "END")
{
    if (input == "green")
    {
        int passed = StartGreenLightCycle(greenLight, freeWindow, pending);
        if (passed < 0) return;

        totalCarsPassed += passed;
    }
    else
    {
        pending.Enqueue(input);
    }

    input = Console.ReadLine();
}

Console.WriteLine("Everyone is safe.");
Console.WriteLine($"{totalCarsPassed} total cars passed the crossroads.");

static int StartGreenLightCycle(int remainingSeconds, int freeWindow, Queue<string> pending)
{
    int passed = 0;

    while (pending.Count > 0 && remainingSeconds > 0)
    {
        string car = pending.Dequeue();
        remainingSeconds -= car.Length;

        if (remainingSeconds < 0)
        {
            int requiredWait = -1 * remainingSeconds;
            if (requiredWait > freeWindow)
            {
                Console.WriteLine($"A crash happened!");
                Console.WriteLine($"{car} was hit at {car[car.Length - (requiredWait - freeWindow)]}.");
                return -1;
            }
        }

        passed++;
    }

    return passed;
}