string companyName = Console.ReadLine();
int adultTickets = int.Parse(Console.ReadLine());
int childTickets = int.Parse(Console.ReadLine());
double adultTicketPrice = double.Parse(Console.ReadLine());
double serviceFee = double.Parse(Console.ReadLine());

double childTicketPrice = 0.3 * adultTicketPrice;

double totalIncome = adultTickets * (adultTicketPrice + serviceFee) + childTickets * (childTicketPrice + serviceFee);
double profit = 0.2 * totalIncome;

Console.WriteLine($"The profit of your agency from {companyName} tickets is {profit:f2} lv.");