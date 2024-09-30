namespace RawData;

public class Cargo
{
    public Cargo(int weight, string type)
    {
        this.Weight = weight;
        this.Type = type;
    }

    public int Weight { get; }
    public string Type { get; }
}
