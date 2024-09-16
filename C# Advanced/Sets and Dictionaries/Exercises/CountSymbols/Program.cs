string text = Console.ReadLine();

Dictionary<char, int> symbolsCounter = new Dictionary<char, int>();
foreach (char symbol in text)
{
    if (!symbolsCounter.ContainsKey(symbol)) symbolsCounter[symbol] = 0;
    symbolsCounter[symbol]++;
}

foreach (var (symbol, count) in symbolsCounter.OrderBy(x => x.Key))
    Console.WriteLine($"{symbol}: {count} time/s");
