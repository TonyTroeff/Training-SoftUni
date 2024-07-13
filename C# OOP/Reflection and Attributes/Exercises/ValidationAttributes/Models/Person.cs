namespace ValidationAttributes.Models;

using ValidationAttributes.Attributes;

public class Person
{
    public Person(string fullName, int age)
    {
        this.FullName = fullName;
        this.Age = age;
    }

    [MyRequired] public string FullName { get; }
    [MyRange(minValue: 12, maxValue: 90)] public int Age { get; }
}