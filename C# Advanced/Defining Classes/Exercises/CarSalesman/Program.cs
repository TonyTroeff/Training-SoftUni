using CarSalesman;

int n = int.Parse(Console.ReadLine());

Dictionary<string, Engine> engineByModel = new Dictionary<string, Engine>();
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    string model = data[0];
    int power = int.Parse(data[1]);
    int? displacement = null;
    string? efficiency = null;

    if (data.Length == 3)
    {
        if (int.TryParse(data[2], out int numericValue)) displacement = numericValue;
        else efficiency = data[2];
    }
    else if (data.Length == 4)
    {
        displacement = int.Parse(data[2]);
        efficiency = data[3];
    }

    Engine engine = new Engine(model, power, displacement, efficiency);
    engineByModel[model] = engine;
}

int m = int.Parse(Console.ReadLine());

Car[] cars = new Car[m];
for (int i = 0; i < m; i++)
{
    string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    string model = data[0];
    Engine engine = engineByModel[data[1]];
    int? weight = null;
    string? color = null;

    if (data.Length == 3)
    {
        if (int.TryParse(data[2], out int numericValue)) weight = numericValue;
        else color = data[2];
    }
    else if (data.Length == 4)
    {
        weight = int.Parse(data[2]);
        color = data[3];
    }

    cars[i] = new Car(model, engine, weight, color);
}

foreach (Car car in cars) Console.WriteLine(car);