using OpinionPoll;

int n = int.Parse(Console.ReadLine());
Person[] people = new Person[n];

for (int i = 0; i < n; i++)
{
    string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    string name = data[0];
    int age = int.Parse(data[1]);

    Person person = new Person(name, age);
    people[i] = person;
}

foreach (Person person in people.Where(p => p.Age > 30).OrderBy(p => p.Name))
    Console.WriteLine($"{person.Name} - {person.Age}");
