Dictionary<string, Dictionary<string, double>> shops = new Dictionary<string, Dictionary<string, double>>();

string input;
while ((input = Console.ReadLine()) != "Revision")
{
    string[] data = input.Split(", ");
    string shopName = data[0], productName = data[1];
    double productPrice = double.Parse(data[2]);

    if (!shops.ContainsKey(shopName)) shops[shopName] = new Dictionary<string, double>();
    shops[shopName][productName] = productPrice;
}

foreach (var (shopName, products) in shops.OrderBy(x => x.Key))
{
    Console.WriteLine($"{shopName}->");
    foreach (var (productName, productPrice) in products)
        Console.WriteLine($"Product: {productName}, Price: {productPrice}");
}