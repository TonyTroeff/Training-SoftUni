namespace Stealer;

public static class StartUp
{
    public static void Main()
    {
        var spy = new Spy();
        Console.WriteLine(spy.StealFieldInfo("Stealer.Hacker", "username", "password"));
        Console.WriteLine(spy.AnalyzeAccessModifiers("Stealer.Hacker"));
        Console.WriteLine(spy.RevealPrivateMethods("Stealer.Hacker"));
        Console.WriteLine(spy.CollectGettersAndSetters("Stealer.Hacker"));
    }
}