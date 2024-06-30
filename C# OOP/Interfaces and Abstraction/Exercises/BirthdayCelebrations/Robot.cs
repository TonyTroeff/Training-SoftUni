namespace BirthdayCelebrations;

public class Robot : IIdentifiable
{
    public Robot(string model, string id)
    {
        this.Model = model;
        this.Id = id;
    }

    public string Model { get; }
    public string Id { get; }
}