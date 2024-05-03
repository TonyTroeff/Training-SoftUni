string figure = Console.ReadLine();

double area = 0;
if (figure == "square")
{
    double a = double.Parse(Console.ReadLine());
    area = a * a;
}
else if (figure == "rectangle")
{
    double a = double.Parse(Console.ReadLine());
    double b = double.Parse(Console.ReadLine());
    area = a * b;
}
else if (figure == "circle")
{
    double r = double.Parse(Console.ReadLine());
    area = Math.PI * r * r;
}
else if (figure == "triangle")
{
    double a = double.Parse(Console.ReadLine());
    double h = double.Parse(Console.ReadLine());
    area = a * h / 2;
}

Console.WriteLine($"{area:f3}");