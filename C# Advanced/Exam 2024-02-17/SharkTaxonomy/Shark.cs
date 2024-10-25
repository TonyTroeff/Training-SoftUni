using System.Text;

namespace SharkTaxonomy;

public class Shark
{
    public Shark(string kind, int length, string food, string habitat)
    {
        this.Kind = kind;
        this.Length = length;
        this.Food = food;
        this.Habitat = habitat;
    }

    public string Kind { get; }
    public int Length { get; }
    public string Food { get; }
    public string Habitat { get; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{this.Kind} shark: {this.Length}m long.");
        sb.Append($"Could be spotted in the {this.Habitat}, typical menu: {this.Food}");

        return sb.ToString();
    }
}
