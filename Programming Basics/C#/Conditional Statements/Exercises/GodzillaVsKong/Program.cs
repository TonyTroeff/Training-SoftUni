double budget = double.Parse(Console.ReadLine());
int statistsCount = int.Parse(Console.ReadLine());
double clothingCostsPerStatist = double.Parse(Console.ReadLine());

double decorationCosts = 0.1 * budget; // 10% of the total budget

double totalClothingCosts = statistsCount * clothingCostsPerStatist;
if (statistsCount > 150)
    totalClothingCosts *= 0.9; // Apply 10% discount

double totalCosts = decorationCosts + totalClothingCosts;

if (budget >= totalCosts)
{
    Console.WriteLine("Action!");
    Console.WriteLine($"Wingard starts filming with {budget - totalCosts:f2} leva left.");
}
else
{
    Console.WriteLine("Not enough money!");
    Console.WriteLine($"Wingard needs {totalCosts - budget:f2} leva more.");
}