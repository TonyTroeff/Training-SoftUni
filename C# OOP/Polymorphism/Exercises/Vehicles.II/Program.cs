namespace Vehicles.II;

using System;
using Vehicles.II.Interfaces;
using Vehicles.II.Models;

public static class Program
{
    public static void Main()
    {
        var carInfo = Console.ReadLine().Split();
        Car car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));

        var truckInfo = Console.ReadLine().Split();
        Truck truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));

        var busInfo = Console.ReadLine().Split();
        Bus bus = new Bus(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

        var n = int.Parse(Console.ReadLine());
        for (var i = 0; i < n; i++)
        {
            var commandData = Console.ReadLine().Split();

            var command = commandData[0];
            var vehicleType = commandData[1];

            if (command == "Drive")
            {
                var vehicle = GetVehicle(vehicleType, car, truck, bus);
                var distance = double.Parse(commandData[2]);

                Console.WriteLine(vehicle.Travel(distance));
            }
            else if (command == "DriveEmpty")
            {
                var distance = double.Parse(commandData[2]);
                
                bus.IsEmpty = true;
                Console.WriteLine(bus.Travel(distance));
                bus.IsEmpty = false;
            }
            else if (command == "Refuel")
            {
                var vehicle = GetVehicle(vehicleType, car, truck, bus);
                var liters = double.Parse(commandData[2]);

                var refuel = vehicle.Refuel(liters);
                if (!string.IsNullOrWhiteSpace(refuel)) Console.WriteLine(refuel);
            }
        }

        Console.WriteLine(car);
        Console.WriteLine(truck);
        Console.WriteLine(bus);
    }

    private static IVehicle GetVehicle(string vehicleType, IVehicle car, IVehicle truck, IVehicle bus)
        => vehicleType switch
        {
            "Car" => car,
            "Truck" => truck,
            "Bus" => bus,
            _ => throw new InvalidOperationException("Invalid vehicle type")
        };
}