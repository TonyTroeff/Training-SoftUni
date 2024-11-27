namespace CarDealership.Models.Contracts
{
    public interface IVehicle
    {
        string Model { get; }

        double Price { get; }

        IReadOnlyCollection<string> Buyers { get; }

        int SalesCount { get; }

        void SellVehicle(string buyerName);
    }
}
