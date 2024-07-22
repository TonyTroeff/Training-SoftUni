int days = int.Parse(Console.ReadLine());
int leftFoodKg = int.Parse(Console.ReadLine());
double dogFoodKg = double.Parse(Console.ReadLine());
double catFoodKg = double.Parse(Console.ReadLine());
double turtleFoodKg = double.Parse(Console.ReadLine()) / 1000;

double requiredFood = days * (dogFoodKg + catFoodKg + turtleFoodKg);
if (leftFoodKg >= requiredFood) Console.WriteLine($"{Math.Floor(leftFoodKg - requiredFood)} kilos of food left.");
else Console.WriteLine($"{Math.Ceiling(requiredFood - leftFoodKg)} more kilos of food are needed.");