namespace DefiningClasses;

public class Person
{
    private string name;
    private int age;

    public Person()
        : this(1)
    {
    }

    public Person(int age)
        : this("No name", age)
    {
    }

    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public string Name { get => this.name; set => this.name = value; }
    public int Age { get => this.age; set => this.age = value; }
}
