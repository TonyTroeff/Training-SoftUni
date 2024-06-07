const double boundary = 1250.5;

string actor = Console.ReadLine();
double points = double.Parse(Console.ReadLine());
int juriesCount = int.Parse(Console.ReadLine());

for (int i = 0; points <= boundary && i < juriesCount; i++)
{
    string juryName = Console.ReadLine();
    double juryPoints = double.Parse(Console.ReadLine());

    points += juryPoints * juryName.Length / 2;
}

if (points > boundary) Console.WriteLine($"Congratulations, {actor} got a nominee for leading role with {points:f1}!");
else Console.WriteLine($"Sorry, {actor} you need {boundary - points:f1} more!");