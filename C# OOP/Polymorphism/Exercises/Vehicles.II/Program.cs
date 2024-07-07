namespace Vehicles.II;

using System;
using System.Collections.Generic;
using Vehicles.II.Interfaces;
using Vehicles.II.Models;

public static class Program
{
    public static void Main()
    {
        var carInfo = Console.ReadLine().Split();
        var car = new Car(double.Parse(carInfo[1]), double.Parse(carInfo[2]), double.Parse(carInfo[3]));

        var truckInfo = Console.ReadLine().Split();
        var truck = new Truck(double.Parse(truckInfo[1]), double.Parse(truckInfo[2]), double.Parse(truckInfo[3]));

        var busInfo = Console.ReadLine().Split();
        var bus = new Bus(double.Parse(busInfo[1]), double.Parse(busInfo[2]), double.Parse(busInfo[3]));

        var n = int.Parse(Console.ReadLine());
        var vehiclesMap = new Dictionary<string, IVehicle> { [nameof(Car)] = car, [nameof(Truck)] = truck, [nameof(Bus)] = bus };
        
        ProcessCommands(n, vehiclesMap);

        Console.WriteLine(car);
        Console.WriteLine(truck);
        Console.WriteLine(bus);
    }

    private static void ProcessCommands(int n, Dictionary<string, IVehicle> vehiclesMap)
    {
        for (var i = 0; i < n; i++)
        {
            var command = Console.ReadLine().Split();

            var commandType = command[0];
            var vehicleType = command[1];
            var vehicle = vehiclesMap[vehicleType];

            if (commandType == "Drive")
            {
                var distance = double.Parse(command[2]);
                DriveVehicle(vehicleType, vehicle, distance, ecoMode: false);
            }
            else if (commandType == "DriveEmpty")
            {
                var distance = double.Parse(command[2]);
                DriveVehicle(vehicleType, vehicle, distance, ecoMode: true);
            }
            else if (commandType == "Refuel")
            {
                try
                {
                    var liters = double.Parse(command[2]);
                    if (!vehicle.Refuel(liters)) Console.WriteLine($"Cannot fit {liters} fuel in the tank");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

    private static void DriveVehicle(string vehicleType, IVehicle vehicle, double distance, bool ecoMode)
    {
        if (vehicle.Drive(distance, ecoMode)) Console.WriteLine($"{vehicleType} travelled {distance} km");
        else Console.WriteLine($"{vehicleType} needs refueling");
    }
}