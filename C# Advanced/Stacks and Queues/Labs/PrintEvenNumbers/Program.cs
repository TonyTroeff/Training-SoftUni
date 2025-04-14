int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

Queue<int> result = new Queue<int>();
for (int i = 0; i < numbers.Length;i++)
{
    if (numbers[i] % 2 == 0) result.Enqueue(numbers[i]);
}

Console.WriteLine(string.Join(", ", result));