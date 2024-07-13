namespace AuthorProblem;

public static class StartUp
{
    [Author("Tony Troeff")]
    public static void Main()
    {
        var tracker = new Tracker();
        tracker.PrintMethodsByAuthor();
    }
}