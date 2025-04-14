HashSet<string> guests = new HashSet<string>();

string input;
while ((input = Console.ReadLine()) != "PARTY")
    guests.Add(input);

while ((input = Console.ReadLine()) != "END")
    guests.Remove(input);

Console.WriteLine(guests.Count);

foreach (string vipGuest in guests.Where(x => char.IsDigit(x[0])))
    Console.WriteLine(vipGuest);

foreach (string regularGuest in guests.Where(x => !char.IsDigit(x[0])))
    Console.WriteLine(regularGuest);