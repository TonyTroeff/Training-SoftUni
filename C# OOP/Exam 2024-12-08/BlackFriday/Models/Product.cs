using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Models
{
    public abstract class Product : IProduct
    {
        private string productName;
        private double basePrice;

        public string ProductName
        {
            get => this.productName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.ProductNameRequired);

                this.productName = value;
            }
        }

        public double BasePrice
        {
            get => this.basePrice;
            private set
            {
                // If the property is mutable, prefer validations within the `set` accessor.
                if (value <= 0)
                    throw new ArgumentException(ExceptionMessages.ProductPriceConstraints);
                
                this.basePrice = value;
            }
        }
        public virtual double BlackFridayPrice { get; }
        public bool IsSold { get; private set; }

        protected Product(string productName, double basePrice)
        {
            this.ProductName = productName;
            this.BasePrice = basePrice;
        }

        public void ToggleStatus()
            => this.IsSold = !this.IsSold;

        public void UpdatePrice(double newPriceValue)
            => this.BasePrice = newPriceValue;

        public override string ToString()
            => $"Product: {this.ProductName}, Price: {this.BasePrice:F2}, You Save: {(this.BasePrice - this.BlackFridayPrice):F2}";
    }
}
