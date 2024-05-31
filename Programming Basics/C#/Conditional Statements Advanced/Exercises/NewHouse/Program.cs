string flowerType = Console.ReadLine();
int flowersCount = int.Parse(Console.ReadLine());
int budget = int.Parse(Console.ReadLine());

double costs = 0;
if (flowerType == "Roses")
{
    costs = flowersCount * 5;
    if (flowersCount > 80) costs *= 0.9;
}
else if (flowerType == "Dahlias")
{
    costs = flowersCount * 3.8;
    if (flowersCount > 90) costs *= 0.85;
}
else if (flowerType == "Tulips")
{
    costs = flowersCount * 2.8;
    if (flowersCount > 80) costs *= 0.85;
}
else if (flowerType == "Narcissus")
{
    costs = flowersCount * 3;
    if (flowersCount < 120) costs *= 1.15;
}
else if (flowerType == "Gladiolus")
{
    costs = flowersCount * 2.5;
    if (flowersCount < 80) costs *= 1.2;
}

if (costs <= budget) Console.WriteLine($"Hey, you have a great garden with {flowersCount} {flowerType} and {budget - costs:f2} leva left.");
else Console.WriteLine($"Not enough money, you need {costs - budget:f2} leva more.");