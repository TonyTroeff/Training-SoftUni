namespace RawData;

public class Tire
{
    public Tire(double pressure, int age)
    {
        this.Pressure = pressure;
        this.Age = age;
    }

    public double Pressure { get; }
    public int Age { get; }
}
