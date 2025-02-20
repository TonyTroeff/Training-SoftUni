namespace CustomRandomList;

public class RandomList : List<string>
{
    public string RandomString()
    {
        int randomIndex = Random.Shared.Next(this.Count);

        string randomValue = this[randomIndex];
        this.RemoveAt(randomIndex);

        return randomValue;
    }
}
