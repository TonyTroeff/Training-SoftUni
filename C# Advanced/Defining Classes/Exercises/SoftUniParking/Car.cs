using System.Text;

namespace SoftUniParking;

public class Car
{
    public Car(string make, string model, int horsePower, string registrationNumber)
    {
        this.Make = make;
        this.Model = model;
        this.HorsePower = horsePower;
        this.RegistrationNumber = registrationNumber;
    }

    public string Make { get; }
    public string Model { get; }
    public int HorsePower { get; }
    public string RegistrationNumber { get; }

    public override string ToString()
    {
        StringBuilder resultBuilder = new StringBuilder();

        resultBuilder.AppendLine($"Make: {this.Make}");
        resultBuilder.AppendLine($"Model: {this.Model}");
        resultBuilder.AppendLine($"HorsePower: {this.HorsePower}");
        resultBuilder.Append($"RegistrationNumber: {this.RegistrationNumber}");

        return resultBuilder.ToString();
    }
}
