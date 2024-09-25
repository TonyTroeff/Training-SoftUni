string[] words = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

Action<string> print = Console.WriteLine;
Array.ForEach(words, print);