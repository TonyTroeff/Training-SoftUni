namespace BlackFriday.Core.Contracts
{
    public interface IController
    {
        string RegisterUser(string userName, string email, bool hasDataAccess);

        string AddProduct(string productType, string productName, string userName, double basePrice);

        string UpdateProductPrice(string productName, string userName, double newPriceValue);

        string RefreshSalesList(string userName);

        string PurchaseProduct(string userName, string productName, bool blackFridayFlag);

        string ApplicationReport();
    }
}
