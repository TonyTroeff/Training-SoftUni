namespace BlackFriday.Models
{
    public class Service : Product
    {
        private const double DiscountPercentage = 0.2;

        public Service(string productName, double basePrice) : base(productName, basePrice)
        {
        }

        public override double BlackFridayPrice => (1 - DiscountPercentage) * this.BasePrice;
    }
}
