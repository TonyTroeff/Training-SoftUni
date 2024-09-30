namespace RawData;

public class Car
{
    public Car(string model, Engine engine, Cargo cargo, params Tire[] tires)
    {
        this.Model = model;
        this.Engine = engine;
        this.Cargo = cargo;
        this.Tires = new List<Tire>(tires).AsReadOnly();
    }

    public string Model { get; }
    public Engine Engine { get; }
    public Cargo Cargo { get; }
    public IReadOnlyCollection<Tire> Tires { get; }
}
