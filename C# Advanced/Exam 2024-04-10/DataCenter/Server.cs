namespace DataCenter;

public class Server
{
    public string SerialNumber { get; }
    public string Model { get; }
    public int Capacity { get; }
    public int PowerUsage { get; }

    public Server(string serialNumber, string model, int capacity, int powerUsage)
    {
        this.SerialNumber = serialNumber;
        this.Model = model;
        this.Capacity = capacity;
        this.PowerUsage = powerUsage;
    }

    public override string ToString()
        => $"Server {this.SerialNumber}: {this.Model}, {this.Capacity}TB, {this.PowerUsage}W";
}
