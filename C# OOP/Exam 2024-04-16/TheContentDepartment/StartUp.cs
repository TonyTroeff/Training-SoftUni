namespace TheContentDepartment;

using TheContentDepartment.Core;
using TheContentDepartment.Core.Contracts;

public class StartUp
{
    static void Main(string[] args)
    {
        IEngine engine = new Engine();
        engine.Run();
    }
}