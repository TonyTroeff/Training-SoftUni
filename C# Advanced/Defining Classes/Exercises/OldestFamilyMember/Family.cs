namespace OldestFamilyMember;

public class Family
{
    private readonly List<Person> _people = new List<Person>();

    public void AddMember(Person member)
    {
        if (member is not null)
            this._people.Add(member);
    }

    public Person GetOldestMember() => this._people.MaxBy(p => p.Age);
}
