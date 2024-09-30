using SpeedRacing;

int n = int.Parse(Console.ReadLine());

Dictionary<string, Car> cars = new Dictionary<string, Car>();
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    Car currentCar = new Car(data[0], double.Parse(data[1]), double.Parse(data[2]));
    
    cars[currentCar.Model] = currentCar;
}

string command = Console.ReadLine();
while (command != "End")
{
    string[] data = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (data[0] == "Drive")
    {
        string model = data[1];
        double distance = double.Parse(data[2]);

        Car carToDrive = cars[model];
        if (!carToDrive.Drive(distance))
            Console.WriteLine("Insufficient fuel for the drive");
    }

    command = Console.ReadLine();
}

foreach (Car car in cars.Values)
    Console.WriteLine($"{car.Model} {car.FuelAmount:f2} {car.TravelledDistance}");
