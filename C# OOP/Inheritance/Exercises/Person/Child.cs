namespace Person;

using System;

public class Child : Person
{
    public Child(string name, int age) : base(name, age)
    {
    }

    public override int Age
    {
        get => base.Age;
        set
        {
            if (value > 15) throw new ArgumentException("Children are not allowed to have age greater than 15.");
            base.Age = value;
        }
    }
}