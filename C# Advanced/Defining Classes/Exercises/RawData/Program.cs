namespace RawData;

public class Program
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        Car[] cars = new Car[n];
        for (int i = 0; i < n; i++)
        {
            string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Engine engine = new Engine(int.Parse(data[1]), int.Parse(data[2]));
            Cargo cargo = new Cargo(int.Parse(data[3]), data[4]);
            Tire tire1 = new Tire(double.Parse(data[5]), int.Parse(data[6]));
            Tire tire2 = new Tire(double.Parse(data[7]), int.Parse(data[8]));
            Tire tire3 = new Tire(double.Parse(data[9]), int.Parse(data[10]));
            Tire tire4 = new Tire(double.Parse(data[11]), int.Parse(data[12]));

            cars[i] = new Car(data[0], engine, cargo, tire1, tire2, tire3, tire4);
        }

        string filter = Console.ReadLine();

        IEnumerable<Car> result = cars.Where(c => c.Cargo.Type == filter);
        if (filter == "fragile")
            result = result.Where(c => c.Tires.Any(t => t.Pressure < 1));
        else if (filter == "flammable")
            result = result.Where(c => c.Engine.Power > 250);

        foreach (Car car in result)
            Console.WriteLine(car.Model);
    }
}