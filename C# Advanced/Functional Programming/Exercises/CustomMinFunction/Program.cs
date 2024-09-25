Func<int, int, int> minFunc = (x, y) => y < x ? y : x;

int[] numbers = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

int min = int.MaxValue;
for (int i = 0; i < numbers.Length; i++)
    min = minFunc(min, numbers[i]);

Console.WriteLine(min);