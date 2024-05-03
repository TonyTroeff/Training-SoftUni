const double dogFoodPrice = 2.5;
const double catFoodPrice = 4;

int dogs = int.Parse(Console.ReadLine());
int cats = int.Parse(Console.ReadLine());

double totalCosts = dogs * dogFoodPrice + cats * catFoodPrice;
Console.WriteLine($"{totalCosts} lv.");