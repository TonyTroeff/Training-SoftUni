namespace GenericCountMethodDouble;

public class Box<TValue>
{
    public Box(TValue value)
    {
        this.Value = value;
    }

    public TValue Value { get; }

    public override string ToString() => $"{typeof(TValue)}: {this.Value}";
}
