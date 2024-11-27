using System;
using System.Collections.Generic;
using System.Text;

namespace AutoTrade
{
    public class DealerShop
    {
        private List<Vehicle> vehicles;
        private int capacity;

        public DealerShop(int capacity)
        {
            Capacity = capacity;
            vehicles = new List<Vehicle>();
        }

        public int Capacity 
        {
            get => this.capacity;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Capacity must be a positive value.");
                }
                this.capacity = value;
            } 
        }

        public IReadOnlyCollection<Vehicle> Vehicles => vehicles.AsReadOnly();

        public string AddVehicle(Vehicle vehicle)
        {
            if(vehicles.Count == Capacity)
            {
                throw new InvalidOperationException("Inventory is full");
            }
            vehicles.Add(vehicle);
            return $"Added {vehicle}";
        }

        public bool SellVehicle(Vehicle vehicle)
        {
            if(this.vehicles.Contains(vehicle))
            {
                this.vehicles.Remove(vehicle);
                return true;
            }

            return false;
        }

        public string InventoryReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Inventory Report");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Vehicles: {vehicles.Count}");

            foreach (Vehicle vehicle in vehicles)
            {
                sb.AppendLine(vehicle.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
