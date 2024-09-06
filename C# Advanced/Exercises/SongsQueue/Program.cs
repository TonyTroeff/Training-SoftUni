string[] initialSongs = Console.ReadLine().Split(", ");

Queue<string> queue = new Queue<string>(initialSongs);
HashSet<string> actualSongs = new HashSet<string>(initialSongs);

while (queue.Count > 0)
{
    string command = Console.ReadLine();

    if (command == "Play")
    {
        string song = queue.Dequeue();
        actualSongs.Remove(song);
    }
    else if (command.StartsWith("Add"))
    {
        string song = command.Substring(4);
        if (!actualSongs.Add(song)) Console.WriteLine($"{song} is already contained!");
        else queue.Enqueue(song);
    }
    else if (command == "Show")
    {
        Console.WriteLine(string.Join(", ", queue));
    }
}

Console.WriteLine("No more songs!");