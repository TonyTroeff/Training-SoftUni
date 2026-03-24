using BlackFriday.Core.Contracts;
using BlackFriday.Models;
using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;
using System.Text;

namespace BlackFriday.Core
{
    public class Controller : IController
    {
        private IApplication application = new Application();

        public string AddProduct(string productType, string productName, string userName, double basePrice)
        {
            if (productType != nameof(Item) && productType != nameof(Service))
                return string.Format(OutputMessages.ProductIsNotPresented, productType);

            if (this.application.Products.Exists(productName))
                return string.Format(OutputMessages.ProductNameDuplicated, productName);

            IUser user = this.application.Users.GetByName(userName);
            if (user is null || !user.HasDataAccess)
                return string.Format(OutputMessages.UserIsNotAdmin, userName);

            IProduct product;
            if (productType == nameof(Item)) product = new Item(productName, basePrice);
            else product = new Service(productName, basePrice);

            this.application.Products.AddNew(product);
            return string.Format(OutputMessages.ProductAdded, productType, productName, $"{basePrice:f2}");
        }

        public string ApplicationReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Application administration:");
            foreach (var admin in this.application.Users.Models.Where(u => u.HasDataAccess).OrderBy(u => u.UserName))
                sb.AppendLine(admin.ToString());

            sb.Append("Clients:");
            foreach (var client in this.application.Users.Models.OfType<Client>().OrderBy(u => u.UserName))
            {
                sb.AppendLine();
                sb.Append(client.ToString());

                string[] purchasedProducts = client.Purchases.Where(p => p.Value).Select(p => p.Key).ToArray();
                if (purchasedProducts.Length == 0)
                    continue;
                
                sb.AppendLine();
                sb.Append($"-Black Friday Purchases: {purchasedProducts.Length}");
                foreach (var productName in purchasedProducts)
                {
                    sb.AppendLine();
                    sb.Append($"--{productName}");
                }
            }


            // I hate doing `sb.ToString().Trim()`
            return sb.ToString();
        }

        public string PurchaseProduct(string userName, string productName, bool blackFridayFlag)
        {
            // NOTE: We can extract a method to reduce duplications.
            IUser user = this.application.Users.GetByName(userName);
            if (user is not Client client)
                return string.Format(OutputMessages.UserIsNotClient, userName);

            IProduct product = this.application.Products.GetByName(productName);
            if (product is null)
                return string.Format(OutputMessages.ProductDoesNotExist, productName);
            else if (product.IsSold)
                return string.Format(OutputMessages.ProductOutOfStock, productName);

            // Alternatives:
            // (Client)user
            // user as Client
            client.PurchaseProduct(productName, blackFridayFlag);
            product.ToggleStatus();

            double price = blackFridayFlag ? product.BlackFridayPrice : product.BasePrice;
            return string.Format(OutputMessages.ProductPurchased, userName, productName, $"{price:f2}");
        }

        public string RefreshSalesList(string userName)
        {
            // NOTE: We can extract a method to reduce duplications.
            IUser user = this.application.Users.GetByName(userName);
            if (user is null || !user.HasDataAccess)
                return string.Format(OutputMessages.UserIsNotAdmin, userName);

            IProduct[] productsToRefresh = this.application.Products.Models.Where(p => p.IsSold).ToArray();
            foreach (var product in productsToRefresh)
                product.ToggleStatus();

            return string.Format(OutputMessages.SalesListRefreshed, productsToRefresh.Length);
        }

        public string RegisterUser(string userName, string email, bool hasDataAccess)
        {
            if (this.application.Users.Exists(userName))
                return string.Format(OutputMessages.UserAlreadyRegistered, userName);

            if (this.application.Users.Models.Any(x => x.Email == email))
                return string.Format(OutputMessages.SameEmailIsRegistered, email);

            if (hasDataAccess)
            {
                if (this.application.Users.Models.Count(x => x.HasDataAccess) >= 2)
                    return OutputMessages.AdminCountLimited;

                IUser user = new Admin(userName, email);
                this.application.Users.AddNew(user);

                return string.Format(OutputMessages.AdminRegistered, userName);
            }
            else
            {
                IUser user = new Client(userName, email);
                this.application.Users.AddNew(user);

                return string.Format(OutputMessages.ClientRegistered, userName);
            }
        }

        public string UpdateProductPrice(string productName, string userName, double newPriceValue)
        {
            IProduct product = this.application.Products.GetByName(productName);
            if (product is null)
                return string.Format(OutputMessages.ProductDoesNotExist, productName);

            // NOTE: We can extract a method to reduce duplications.
            IUser user = this.application.Users.GetByName(userName);
            if (user is null || !user.HasDataAccess)
                return string.Format(OutputMessages.UserIsNotAdmin, userName);

            double oldPriceValue = product.BasePrice;
            product.UpdatePrice(newPriceValue);
            return string.Format(OutputMessages.ProductPriceUpdated, productName, $"{oldPriceValue:f2}", $"{newPriceValue:f2}");
        }
    }
}
