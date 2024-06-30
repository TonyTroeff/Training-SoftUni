namespace BirthdayCelebrations;

public class Pet : IWithBirthdate, IWithName
{
    public Pet(string name, string birthdate)
    {
        this.Name = name;
        this.Birthdate = birthdate;
    }

    public string Name { get; }
    public string Birthdate { get; }
}