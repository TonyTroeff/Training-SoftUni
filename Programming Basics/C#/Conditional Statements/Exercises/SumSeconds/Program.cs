int firstTime = int.Parse(Console.ReadLine());
int secondTime = int.Parse(Console.ReadLine());
int thirdTime = int.Parse(Console.ReadLine());

int total = firstTime + secondTime + thirdTime;

int minutes = total / 60, seconds = total % 60;

if (seconds < 10) Console.WriteLine($"{minutes}:0{seconds}");
else Console.WriteLine($"{minutes}:{seconds}");
