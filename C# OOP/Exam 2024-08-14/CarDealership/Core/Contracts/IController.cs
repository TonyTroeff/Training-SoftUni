namespace CarDealership.Core.Contracts
{
    public interface IController
    {
        string AddCustomer(string customerTypeName, string customerName);

        string AddVehicle(string vehicleTypeName, string model, double price);

        string PurchaseVehicle(string vehicleTypeName, string customerName, double budget);

        string CustomerReport();

        string SalesReport(string vehicleTypeName);
    }
}
