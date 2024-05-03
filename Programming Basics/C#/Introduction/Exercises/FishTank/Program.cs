int length = int.Parse(Console.ReadLine());
int width = int.Parse(Console.ReadLine());
int height = int.Parse(Console.ReadLine());
double percentage = double.Parse(Console.ReadLine());

double volume = 0.001 * length * width * height;
double freeSpace = volume * (1 - 0.01 * percentage);
Console.WriteLine(freeSpace);