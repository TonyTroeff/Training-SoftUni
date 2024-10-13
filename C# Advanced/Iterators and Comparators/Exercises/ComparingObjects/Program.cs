namespace ComparingObjects;

public class Program
{
    public static void Main()
    {
        List<Person> people = new List<Person>();

        string input = Console.ReadLine();
        while (input != "END")
        {
            string[] data = input.Split();
            Person currentPerson = new Person(data[0], int.Parse(data[1]), data[2]);

            people.Add(currentPerson);

            input = Console.ReadLine();
        }

        int n = int.Parse(Console.ReadLine());
        
        int matchesCount = people.Count(x => x.CompareTo(people[n - 1]) == 0);
        if (matchesCount == 1) Console.WriteLine("No matches");
        else Console.WriteLine($"{matchesCount} {people.Count - matchesCount} {people.Count}");
    }
}
