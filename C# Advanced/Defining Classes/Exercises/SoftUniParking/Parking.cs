namespace SoftUniParking;

public class Parking
{
    private readonly Dictionary<string, Car> cars;
    private readonly int capacity;

    public Parking(int capacity)
    {
        this.cars = new Dictionary<string, Car>();
        this.capacity = capacity;
    }

    public int Count => this.cars.Count;

    public string AddCar(Car car)
    {
        if (this.cars.ContainsKey(car.RegistrationNumber))
            return "Car with that registration number, already exists!";

        if (this.Count == this.capacity)
            return "Parking is full!";

        this.cars[car.RegistrationNumber] = car;
        return $"Successfully added new car {car.Make} {car.RegistrationNumber}";
    }

    public string RemoveCar(string registrationNumber)
    {
        if (!this.cars.Remove(registrationNumber))
            return "Car with that registration number, doesn't exist!";

        return $"Successfully removed {registrationNumber}";
    }

    public Car GetCar(string registrationNumber) => this.cars[registrationNumber];

    public void RemoveSetOfRegistrationNumber(List<string> registrationNumbers)
    {
        foreach (string registrationNumber in registrationNumbers)
            this.cars.Remove(registrationNumber);
    }
}
