namespace Person;

public class Person
{
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public string Name { get; }

    public int Age { get; }

    public override string ToString() => $"{this.GetType().Name} -> Name: {this.Name}, Age: {this.Age}";
}