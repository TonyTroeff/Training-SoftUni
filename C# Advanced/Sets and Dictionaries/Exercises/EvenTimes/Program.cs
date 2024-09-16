int n = int.Parse(Console.ReadLine());

Dictionary<int, int> occurrencesCounter = new Dictionary<int, int>();
for (int i = 0; i < n; i++)
{
    int number = int.Parse(Console.ReadLine());

    if (!occurrencesCounter.ContainsKey(number))
        occurrencesCounter[number] = 0;
    occurrencesCounter[number]++;
}

Console.WriteLine(occurrencesCounter.Single(x => x.Value % 2 == 0).Key);