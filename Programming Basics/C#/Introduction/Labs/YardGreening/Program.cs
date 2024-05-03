const double pricePerSquareMeter = 7.61;
const double discountPercentage = 0.18;

double squareMeters = double.Parse(Console.ReadLine());

double totalCosts = squareMeters * pricePerSquareMeter;
double discount = discountPercentage * totalCosts;
double finalPrice = totalCosts - discount;

Console.WriteLine($"The final price is: {finalPrice} lv.");
Console.WriteLine($"The discount is: {discount} lv.");