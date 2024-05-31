double budget = double.Parse(Console.ReadLine());
string season = Console.ReadLine();

string destination = "", accommodationType = "";
double totalCosts = 0;

if (budget <= 100)
{
    destination = "Bulgaria";
    if (season == "summer") totalCosts = 0.3 * budget;
    else if (season == "winter") totalCosts = 0.7 * budget;
}
else if (budget <= 1000)
{
    destination = "Balkans";
    if (season == "summer") totalCosts = 0.4 * budget;
    else if (season == "winter") totalCosts = 0.8 * budget;
}
else
{
    destination = "Europe";
    accommodationType = "Hotel";
    totalCosts = 0.9 * budget;
}

if (accommodationType.Length == 0)
{
    if (season == "summer")
    {
        accommodationType = "Camp";
    }
    else if (season == "winter")
    {
        accommodationType = "Hotel";
    }
}

Console.WriteLine($"Somewhere in {destination}");
Console.WriteLine($"{accommodationType} - {totalCosts:f2}");
