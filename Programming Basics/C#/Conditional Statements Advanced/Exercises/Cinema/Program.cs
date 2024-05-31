string projectionType = Console.ReadLine();
int rows = int.Parse(Console.ReadLine());
int cols = int.Parse(Console.ReadLine());

int seats = rows * cols;

double ticketPrice = 0;
if (projectionType == "Premiere") ticketPrice = 12;
else if (projectionType == "Normal") ticketPrice = 7.5;
else if (projectionType == "Discount") ticketPrice = 5;

double totalProfit = seats * ticketPrice;

Console.WriteLine($"{totalProfit:f2} leva");
