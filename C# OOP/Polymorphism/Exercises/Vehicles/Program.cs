namespace Vehicles;

using System;
using Vehicles.Interfaces;
using Vehicles.Models;

public static class Program
{
    public static void Main()
    {
        var carInfo = Console.ReadLine().Split();
        IVehicle car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]));

        var truckInfo = Console.ReadLine().Split();
        IVehicle truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]));

        var n = int.Parse(Console.ReadLine());
        for (var i = 0; i < n; i++)
        {
            var commandData = Console.ReadLine().Split();

            var command = commandData[0];
            var vehicleType = commandData[1];
            var vehicle = GetVehicle(vehicleType, car, truck);

            if (command == "Drive")
            {
                var distance = double.Parse(commandData[2]);
                Console.WriteLine(vehicle.Travel(distance));
            }
            else if (command == "Refuel")
            {
                var liters = double.Parse(commandData[2]);
                vehicle.Refuel(liters);
            }
        }

        Console.WriteLine(car);
        Console.WriteLine(truck);
    }

    private static IVehicle GetVehicle(string vehicleType, IVehicle car, IVehicle truck)
        => vehicleType switch
        {
            "Car" => car,
            "Truck" => truck,
            _ => throw new InvalidOperationException("Invalid vehicle type")
        };
}