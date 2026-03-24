using System.Collections.ObjectModel;

namespace BlackFriday.Models
{
    public class Client : User
    {
        private readonly Dictionary<string, bool> purchases;

        public IReadOnlyDictionary<string, bool> Purchases { get; }

        public Client(string userName, string email)
            : base(userName, email, hasDataAccess: false)
        {
            this.purchases = new Dictionary<string, bool>();
            this.Purchases = new ReadOnlyDictionary<string, bool>(this.purchases);
        }

        public void PurchaseProduct(string productName, bool blackFridayFlag)
            => this.purchases[productName] = blackFridayFlag;
    }
}
