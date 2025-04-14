int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

int[] result = numbers.OrderByDescending(x => x).Take(3).ToArray();
Console.WriteLine(string.Join(' ', result));