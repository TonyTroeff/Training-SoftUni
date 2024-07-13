namespace AuthorProblem;

using System.Reflection;

public class Tracker
{
    public void PrintMethodsByAuthor()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            var methods = type.GetMethods();
            foreach (var method in methods)
            {
                var authorAttributes = method.GetCustomAttributes<AuthorAttribute>();
                foreach (var authorAttribute in authorAttributes)
                    Console.WriteLine($"{method.Name} is written by {authorAttribute.Name}");
            }
        }
    }
}