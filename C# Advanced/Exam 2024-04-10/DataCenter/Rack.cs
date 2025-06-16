using System.Text;

namespace DataCenter;

public class Rack
{
    public int Slots { get; }
    public List<Server> Servers { get; }
    public int GetCount => this.Servers.Count;

    public Rack(int slots)
    {
        this.Slots = slots;
        this.Servers = new List<Server>();
    }

    public void AddServer(Server server)
    {
        if (this.GetCount == this.Slots || this.Servers.Any(s => s.SerialNumber == server.SerialNumber)) return;

        this.Servers.Add(server);
    }

    public bool RemoveServer(string serialNumber)
    {
        for (int i = 0; i < this.GetCount; i++)
        {
            if (this.Servers[i].SerialNumber == serialNumber)
            {
                this.Servers.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public string GetHighestPowerUsage()
        => this.Servers.MaxBy(s => s.PowerUsage).ToString();

    public int GetTotalCapacity()
        => this.Servers.Sum(s => s.Capacity);

    public string DeviceManager()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{this.GetCount} servers operating:");

        foreach (Server server in this.Servers)
        {
            sb.AppendLine();
            sb.Append(server);
        }

        return sb.ToString();
    }
}
