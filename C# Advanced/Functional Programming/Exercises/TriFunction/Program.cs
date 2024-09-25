int n = int.Parse(Console.ReadLine());
string[] names = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

Func<string, int, bool> isValid = (word, min) => word.Sum(y => y) >= min;
Func<string[], int, Func<string, int, bool>, string?> find = (all, min, predicate) => all.FirstOrDefault(x => predicate(x, min));

string? result = find(names, n, isValid);
if (result is not null) Console.WriteLine(result);