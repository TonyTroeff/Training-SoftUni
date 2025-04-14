int n = int.Parse(Console.ReadLine());

Queue<string> queue = new Queue<string>();

int counter = 0;
string input;
while ((input = Console.ReadLine()) != "end")
{
    if (input == "green")
    {
        for (int i = 0; i < n && queue.Count > 0; i++)
        {
            Console.WriteLine($"{queue.Dequeue()} passed!");
            counter++;
        }
    }
    else
    {
        queue.Enqueue(input);
    }
}

Console.WriteLine($"{counter} cars passed the crossroads.");