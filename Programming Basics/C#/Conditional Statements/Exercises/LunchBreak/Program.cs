string seriesName = Console.ReadLine();
int episodeDuration = int.Parse(Console.ReadLine());
int breakDuration = int.Parse(Console.ReadLine());

double requiredTime = episodeDuration + 0.125 * breakDuration + 0.25 * breakDuration;

if (requiredTime <= breakDuration) Console.WriteLine($"You have enough time to watch {seriesName} and left with {Math.Ceiling(breakDuration - requiredTime)} minutes free time.");
else Console.WriteLine($"You don't have enough time to watch {seriesName}, you need {Math.Ceiling(requiredTime - breakDuration)} more minutes.");
