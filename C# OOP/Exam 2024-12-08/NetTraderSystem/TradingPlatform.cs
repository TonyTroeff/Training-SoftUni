using System.Text;

namespace NetTraderSystem
{
    public class TradingPlatform
    {
        private List<Product> products;
        private int inventoryLimit;

        public TradingPlatform(int inventoryLimit)
        {
            this.inventoryLimit = inventoryLimit;
            products = new List<Product>();
        }

        public IReadOnlyCollection<Product> Products => this.products.AsReadOnly();

        public string AddProduct(Product product)
        {
            if (products.Count < inventoryLimit)
            {
                products.Add(product);
                return $"Product {product.Name} added successfully";
            }
            else
            {
                return "Inventory is full";
            }
        }

        public bool RemoveProduct(Product product)
        {
            return products.Remove(product);
        }

        public Product SellProduct(Product product)
        {
            if (products.Remove(product))
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public string InventoryReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Inventory Report:");
            sb.AppendLine($"Available Products: {products.Count}");

            foreach (var product in products)
            {
                sb.AppendLine(product.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
