namespace BlackFriday.Utilities.Messages
{
    public static class OutputMessages
    {
        //Common
        public const string UserIsNotAdmin = "{0} has no data access.";
        public const string ProductDoesNotExist = "{0} does not exist in the application.";

        //RegisterUser
        public const string UserAlreadyRegistered = "{0} is already registered.";
        public const string SameEmailIsRegistered = "{0} is already used by another user.";
        public const string AdminCountLimited = "The number of application administrators is limited.";
        public const string AdminRegistered = "Admin {0} is successfully registered with data access.";
        public const string ClientRegistered = "Client {0} is successfully registered.";

        //AddProduct
        public const string ProductIsNotPresented = "{0} is not a valid type for the application.";
        public const string ProductNameDuplicated = "{0} already exists in the application.";
        public const string ProductAdded = "{0}: {1} is added in the application. Price: {2}";

        //UpdateProductPrice
        public const string ProductPriceUpdated = "{0} -> Price is updated: {1} -> {2}";

        //RefreshSalesList
        public const string SalesListRefreshed = "{0} products are listed again.";

        //PurchaseProduct
        public const string UserIsNotClient = "{0} has no authorization for this functionality.";
        public const string ProductOutOfStock = "{0} is out of stock.";
        public const string ProductPurchased = "{0} purchased {1}. Price: {2}";
    }
}
