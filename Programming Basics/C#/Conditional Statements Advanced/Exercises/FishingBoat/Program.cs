int budget = int.Parse(Console.ReadLine());
string season = Console.ReadLine();
int fishermenCount = int.Parse(Console.ReadLine());

int rentPrice = 0;
if (season == "Spring") rentPrice = 3000;
else if (season == "Summer" || season == "Autumn") rentPrice = 4200;
else if (season == "Winter") rentPrice = 2600;

double discountMultiplier;
if (fishermenCount <= 6) discountMultiplier = 0.9;
else if (fishermenCount <= 11) discountMultiplier = 0.85;
else discountMultiplier = 0.75;

double totalCosts = rentPrice * discountMultiplier;

if (fishermenCount % 2 == 0 && season != "Autumn") totalCosts *= 0.95;

if (budget >= totalCosts) Console.WriteLine($"Yes! You have {budget - totalCosts:f2} leva left.");
else Console.WriteLine($"Not enough money! You need {totalCosts - budget:f2} leva.");