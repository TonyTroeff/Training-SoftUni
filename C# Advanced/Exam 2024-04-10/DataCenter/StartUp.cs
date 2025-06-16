namespace DataCenter;

public class StartUp
{
    static void Main()
    {
        //Initialize new repository (Rack) with slots for 8 servers
        Rack rack = new(8);

        //Initialize 10 servers
        Server server1 = new("SN001", "Dell PowerEdge T340", 100, 450);
        Server server2 = new("SN002", "HP Proliant DL360", 200, 220);
        Server server3 = new("SN003", "Dell PowerEdge T340", 250, 350);
        Server server4 = new("SN004", "IBM Power System S922", 220, 330);
        Server server5 = new("SN005", "Lenovo ThinkSystem SR650", 250, 550);
        Server server6 = new("SN006", "HPE Synergy 480 Gen10", 80, 180);
        Server server7 = new("SN007", "Fujitsu PRIMERGY RX2530 M5", 120, 250);
        Server server8 = new("SN008", "Dell EMC PowerEdge R740xd", 160, 380);
        Server server9 = new("SN006", "Supermicro SuperServer 1029P-WTR", 150, 280);
        Server server10 = new("SN009", "Cisco UCS B200 M5", 180, 400);

        //Add servers to the rack, all servers should be added successfully
        rack.AddServer(server1);
        rack.AddServer(server2);
        rack.AddServer(server3);
        rack.AddServer(server4);
        rack.AddServer(server5);
        rack.AddServer(server6);
        rack.AddServer(server7);

        //Try to add a server with a duplicated SerialNumber to the rack
        rack.AddServer(server9);
        //Add server to the last available Slot
        rack.AddServer(server8);

        //Try to add a server when all slots are busy
        rack.AddServer(server10);

        //Remove a server with a valid SerialNumber, should return True
        rack.RemoveServer("SN001");

        //Remove a server with an invalid SerialNumber, should return False
        rack.RemoveServer("SN011");

        //Check servers count
        Console.WriteLine(rack.GetCount);
        //7

        //Try to add a server when 1 slot is already empty
        rack.AddServer(server10);

        //Check servers count
        Console.WriteLine(rack.GetCount);
        //8

        //Get the server with the highest power usage
        Console.WriteLine(rack.GetHighestPowerUsage());
        //Server SN005: Lenovo ThinkSystem SR650, 250TB, 550W

        //Get the total capacity of the system
        Console.WriteLine(rack.GetTotalCapacity());
        //1460

        //DeviceManager report string
        Console.WriteLine(rack.DeviceManager());

        //8 servers operating:
        //Server SN002: HP Proliant DL360, 200TB, 220W
        //Server SN003: Dell PowerEdge T340, 250TB, 350W
        //Server SN004: IBM Power System S922, 220TB, 330W
        //Server SN005: Lenovo ThinkSystem SR650, 250TB, 550W
        //Server SN006: HPE Synergy 480 Gen10, 80TB, 180W
        //Server SN007: Fujitsu PRIMERGY RX2530 M5, 120TB, 250W
        //Server SN008: Dell EMC PowerEdge R740xd, 160TB, 380W
        //Server SN009: Cisco UCS B200 M5, 180TB, 400W
    }
}
