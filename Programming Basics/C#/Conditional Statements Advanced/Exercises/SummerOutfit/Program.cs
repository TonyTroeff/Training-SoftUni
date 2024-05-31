int degrees = int.Parse(Console.ReadLine());
string partOfDay = Console.ReadLine();

string outfit = string.Empty, shoes = string.Empty;
if (degrees <= 18)
{
    if (partOfDay == "Morning")
    {
        outfit = "Sweatshirt";
        shoes = "Sneakers";
    }
    else if (partOfDay == "Afternoon" || partOfDay == "Evening")
    {
        outfit = "Shirt";
        shoes = "Moccasins";
    }
}
else if (degrees <= 24)
{
    if (partOfDay == "Morning" || partOfDay == "Evening")
    {
        outfit = "Shirt";
        shoes = "Moccasins";
    }
    else if (partOfDay == "Afternoon")
    {
        outfit = "T-Shirt";
        shoes = "Sandals";
    }
}
else
{
    if (partOfDay == "Morning")
    {
        outfit = "T-Shirt";
        shoes = "Sandals";
    }
    else if (partOfDay == "Afternoon")
    {
        outfit = "Swim Suit";
        shoes = "Barefoot";
    }
    else if (partOfDay == "Evening")
    {
        outfit = "Shirt";
        shoes = "Moccasins";
    }
}

Console.WriteLine($"It's {degrees} degrees, get your {outfit} and {shoes}.");
