namespace NetTraderSystem
{
    public class Product
    {
        public Product(string name, string category, double price)
        {
            Name = name;
            Category = category;
            Price = price;
        }

        public string Name { get; set; }
        public string Category { get; set; }
        public  double Price { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Category: {Category} - ${Price:F2}";
        }
    }
}
