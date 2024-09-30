namespace SoftUniParking;

public class StartUp
{
    public static void Main()
    {
        Car car1 = new Car("Skoda", "Fabia", 65, "CC1856BG");
        Car car2 = new Car("Audi", "A3", 110, "EB8787MN");

        Console.WriteLine(car1.ToString());

        Parking parking = new Parking(5);
        Console.WriteLine(parking.AddCar(car1));

        Console.WriteLine(parking.AddCar(car1));
        Console.WriteLine(parking.AddCar(car2));
        Console.WriteLine(parking.GetCar("EB8787MN").ToString());
        Console.WriteLine(parking.RemoveCar("EB8787MN"));
        Console.WriteLine(parking.Count);
    }
}