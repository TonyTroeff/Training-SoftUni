namespace NeedForSpeed;

public static class StartUp
{
    public static void Main()
    {
        var vehicle = new Vehicle(100, 20);
        var motorcycle = new Motorcycle(200, 15);
        var raceMotorcycle = new Motorcycle(250, 10);
        var crossMotorcycle = new Motorcycle(120, 17.5);
        var car = new Car(100, 20);
        var familyCar = new FamilyCar(90, 25);
        var sportCar = new SportCar(300, 20);
    }
}