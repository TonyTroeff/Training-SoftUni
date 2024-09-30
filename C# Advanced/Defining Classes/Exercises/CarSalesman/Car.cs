using System.Text;

namespace CarSalesman;

public class Car
{
    public Car(string model, Engine engine, int? weight, string? color)
    {
        this.Model = model;
        this.Engine = engine;
        this.Weight = weight;
        this.Color = color;
    }

    public string Model { get; }
    public Engine Engine { get; }
    public int? Weight { get; }
    public string? Color { get; }

    public override string ToString()
    {
        StringBuilder resultBuilder = new StringBuilder();

        resultBuilder.AppendLine($"{this.Model}:");
        resultBuilder.AppendLine(this.Engine.ToString());
        resultBuilder.AppendLine($"  Weight: {this.Weight?.ToString() ?? "n/a"}");
        resultBuilder.Append($"  Color: {this.Color ?? "n/a"}");

        return resultBuilder.ToString();
    }
}
