namespace Person;

using System;

public class Person
{
    private int _age;

    public Person(string name, int age)
    {
        this.Name = name;

        // NOTE: We should be cautious with virtual calls within the constructor of a base type.
        this.Age = age;
    }

    public string Name { get; }

    public virtual int Age
    {
        get => this._age;
        set
        {
            if (value < 0) throw new ArgumentException("People are not allowed to have negative age.");
            this._age = value;
        }
    }

    public override string ToString() => $"Name: {this.Name}, Age: {this.Age}";
}