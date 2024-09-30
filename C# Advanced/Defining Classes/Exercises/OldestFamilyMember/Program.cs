using OldestFamilyMember;

int n = int.Parse(Console.ReadLine());

Family family = new Family();
for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string name = data[0];
    int age = int.Parse(data[1]);

    Person person = new Person(name, age);
    family.AddMember(person);
}

Person oldest = family.GetOldestMember();
Console.WriteLine($"{oldest.Name} {oldest.Age}");