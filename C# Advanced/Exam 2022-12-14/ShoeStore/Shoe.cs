namespace ShoeStore;

public class Shoe
{
    public string Brand { get; }
    public string Type { get; }
    public double Size { get; }
    public string Material { get; }

    public Shoe(string brand, string type, double size, string material)
    {
        this.Brand = brand;
        this.Type = type;
        this.Size = size;
        this.Material = material;
    }

    public override string ToString()
        => $"Size {this.Size}, {this.Material} {this.Brand} {this.Type} shoe.";
}
