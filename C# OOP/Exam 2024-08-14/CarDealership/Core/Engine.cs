using CarDealership.Core.Contracts;
using CarDealership.IO;
using CarDealership.IO.Contracts;

namespace CarDealership.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IController controller;

        public Engine()
        {
            reader = new Reader();
            writer = new Writer();
            controller = new Controller();
        }
        public void Run()
        {
            while (true)
            {
                string[] input = reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    string result = string.Empty;

                    if (input[0] == "AddCustomer")
                    {
                        string customerTypeName = input[1];
                        string customerName = input[2];

                        result = controller.AddCustomer(customerTypeName, customerName);
                    }
                    else if (input[0] == "AddVehicle")
                    {
                        string vehicleTypeName = input[1];
                        string model = input[2];
                        double price = double.Parse(input[3]);

                        result = controller.AddVehicle(vehicleTypeName, model, price);
                    }
                    else if (input[0] == "PurchaseVehicle")
                    {
                        string vehicleTypeName = input[1];
                        string customerName = input[2];
                        double budget = double.Parse(input[3]);

                        result = controller.PurchaseVehicle(vehicleTypeName, customerName, budget);
                    }
                    else if (input[0] == "CustomerReport")
                    {
                        result = controller.CustomerReport();
                    }
                    else if (input[0] == "SalesReport")
                    {
                        string vehicleTypeName = input[1];

                        result = controller.SalesReport(vehicleTypeName);
                    }
                    writer.WriteLine(result);
                    writer.WriteText(result);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                    writer.WriteText(ex.Message);
                }
            }
        }
    }
}
