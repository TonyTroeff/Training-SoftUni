namespace EqualityLogic;

public class Person : IEquatable<Person>, IComparable<Person>
{
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public string Name { get; }
    public int Age { get; }

    public int CompareTo(Person? other)
    {
        if (other is null) return -1;
        if (ReferenceEquals(this, other)) return 0;

        int nameCompareResult = string.Compare(this.Name, other.Name);
        if (nameCompareResult != 0) return nameCompareResult;

        return this.Age.CompareTo(other.Age);
    }

    public bool Equals(Person? other) => this.CompareTo(other) == 0;

    public override bool Equals(object? obj)
    {
        if (obj is not Person p) return false;
        return this.Equals(p);
    }

    public override int GetHashCode()
        => HashCode.Combine(this.Name, this.Age);
}
