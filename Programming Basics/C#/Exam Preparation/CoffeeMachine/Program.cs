string beverage = Console.ReadLine();
string sugar = Console.ReadLine();
int drinksCount = int.Parse(Console.ReadLine());

double price = 0;
if (beverage == "Espresso")
{
    if (sugar == "Without") price = 0.9;
    else if (sugar == "Normal") price = 1;
    else if (sugar == "Extra") price = 1.2;
}
else if (beverage == "Cappuccino")
{
    if (sugar == "Without") price = 1;
    else if (sugar == "Normal") price = 1.2;
    else if (sugar == "Extra") price = 1.6;
}
else if (beverage == "Tea")
{
    if (sugar == "Without") price = 0.5;
    else if (sugar == "Normal") price = 0.6;
    else if (sugar == "Extra") price = 0.7;
}

double totalCost = drinksCount * price;
if (sugar == "Without") totalCost *= 0.65;
if (beverage == "Espresso" && drinksCount >= 5) totalCost *= 0.75;
if (totalCost > 15) totalCost *= 0.8;

Console.WriteLine($"You bought {drinksCount} cups of {beverage} for {totalCost:f2} lv.");