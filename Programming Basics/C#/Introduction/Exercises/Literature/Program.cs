int totalPages = int.Parse(Console.ReadLine());
int readPagesPerHour = int.Parse(Console.ReadLine());
int remainingDays = int.Parse(Console.ReadLine());

int result = totalPages / readPagesPerHour / remainingDays;
Console.WriteLine(result);