namespace SecureOpsSystem;

public class SecurityTool
{
    public SecurityTool(string name, string category, double effectiveness)
    {
        this.Name = name;
        this.Category = category;
        this.Effectiveness = effectiveness;
    }

    public string Name { get; private set; }
    public string Category { get; private set; }
    public double Effectiveness { get; private set; }

    public override string ToString()
        => $"Name: {Name}, Category: {Category}, Effectiveness: {Effectiveness:F2}";
}
