namespace BlackFriday.Models
{
    public class Item : Product
    {
        private const double DiscountPercentage = 0.3;

        public Item(string productName, double basePrice) : base(productName, basePrice)
        {
        }

        public override double BlackFridayPrice => (1 - DiscountPercentage) * this.BasePrice;
    }
}
