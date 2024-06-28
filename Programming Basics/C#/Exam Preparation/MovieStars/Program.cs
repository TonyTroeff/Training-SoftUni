double budget = double.Parse(Console.ReadLine());

string actor = Console.ReadLine();
while (actor != "ACTION")
{
    double wage;
    if (actor.Length > 15) wage = 0.2 * budget;
    else wage = double.Parse(Console.ReadLine());

    budget -= wage;
    if (budget <= 0) break;

    actor = Console.ReadLine();
}

if (budget > 0) Console.WriteLine($"We are left with {budget:f2} leva.");
else Console.WriteLine($"We need {-1 * budget:f2} leva for our actors.");