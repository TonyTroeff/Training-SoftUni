HashSet<string> parking = new HashSet<string>();

string input;
while ((input = Console.ReadLine()) != "END")
{
    string[] data = input.Split(", ");
    string direction = data[0], carNumber = data[1];

    if (direction == "IN") parking.Add(carNumber);
    else if (direction == "OUT") parking.Remove(carNumber);
}

if (parking.Count == 0)
{
    Console.WriteLine("Parking Lot is Empty");
}
else
{
    foreach (string carNumber in parking)
        Console.WriteLine(carNumber);
}