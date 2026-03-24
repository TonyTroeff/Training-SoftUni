using BlackFriday.Core.Contracts;
using BlackFriday.IO.Contracts;
using CarDealership.IO;

namespace BlackFriday.Core
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

                    if (input[0] == "RegisterUser")
                    {
                        string userName = input[1];
                        string email = input[2];
                        string hasDataAccess = input[3];

                        result = controller.RegisterUser(userName, email, bool.Parse(hasDataAccess));
                    }
                    else if (input[0] == "AddProduct")
                    {
                        string productType = input[1];
                        string productName = input[2];
                        string userName = input[3];
                        double basePrice = double.Parse(input[4]);

                        result = controller.AddProduct(productType, productName, userName, basePrice);
                    }
                    else if (input[0] == "UpdateProductPrice")
                    {
                        string productName = input[1];
                        string userName = input[2];
                        double newPriceValue = double.Parse(input[3]);

                        result = controller.UpdateProductPrice(productName, userName, newPriceValue);
                    }
                    else if (input[0] == "RefreshSalesList")
                    {
                        string userName = input[1];

                        result = controller.RefreshSalesList(userName);
                    }
                    else if (input[0] == "PurchaseProduct")
                    {
                        string userName = input[1];
                        string productName = input[2];
                        string blackFridayFlag = input[3];

                        result = controller.PurchaseProduct(userName, productName, bool.Parse(blackFridayFlag));
                    }
                    else if (input[0] == "ApplicationReport")
                    {
                        result = controller.ApplicationReport();
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
