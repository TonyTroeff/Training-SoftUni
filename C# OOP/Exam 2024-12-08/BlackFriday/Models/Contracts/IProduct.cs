namespace BlackFriday.Models.Contracts
{
    public interface IProduct
    {
        string ProductName { get; }

        double BasePrice { get; }

        double BlackFridayPrice { get; }

        bool IsSold { get; }

        void UpdatePrice(double newPriceValue);

        void ToggleStatus();
    }
}
