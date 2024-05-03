double budget = double.Parse(Console.ReadLine());
int gpuCount = int.Parse(Console.ReadLine());
int cpuCount = int.Parse(Console.ReadLine());
int ramCount = int.Parse(Console.ReadLine());

double gpuCosts = gpuCount * 250;

double cpuPrice = 0.35 * gpuCosts;
double cpuCosts = cpuCount * cpuPrice;

double ramPrice = 0.1 * gpuCosts;
double ramCosts = ramCount * ramPrice;

double totalCosts = gpuCosts + cpuCosts + ramCosts;
if (gpuCount > cpuCount)
    totalCosts *= 0.85; // Apply 15% discount

if (budget >= totalCosts) Console.WriteLine($"You have {budget - totalCosts:f2} leva left!");
else Console.WriteLine($"Not enough money! You need {totalCosts - budget:f2} leva more!");