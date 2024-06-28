int durationInMinutes = int.Parse(Console.ReadLine());
int walksCount = int.Parse(Console.ReadLine());
int calories = int.Parse(Console.ReadLine());

int burnedCalories = walksCount * durationInMinutes * 5;
if (burnedCalories * 2 >= calories) Console.WriteLine($"Yes, the walk for your cat is enough. Burned calories per day: {burnedCalories}.");
else Console.WriteLine($"No, the walk for your cat is not enough. Burned calories per day: {burnedCalories}.");
