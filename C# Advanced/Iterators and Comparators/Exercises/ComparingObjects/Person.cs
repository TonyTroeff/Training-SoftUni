namespace ComparingObjects;

public class Person : IComparable<Person>
{
    public Person(string name, int age, string town)
    {
        this.Name = name;
        this.Age = age;
        this.Town = town;
    }

    public string Name { get; }
    public int Age { get; }
    public string Town { get; }

    public int CompareTo(Person? other)
    {
        if (other is null) return -1;

        int nameCompareResult = this.Name.CompareTo(other.Name);
        if (nameCompareResult != 0) return nameCompareResult;

        int ageCompareResult = this.Age.CompareTo(other.Age);
        if (ageCompareResult != 0) return ageCompareResult;

        return this.Town.CompareTo(other.Town);
    }
}
