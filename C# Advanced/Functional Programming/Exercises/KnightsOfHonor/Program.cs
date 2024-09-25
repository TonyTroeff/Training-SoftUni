string[] words = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

Action<string> print = x => Console.WriteLine($"Sir {x}");
Array.ForEach(words, print);