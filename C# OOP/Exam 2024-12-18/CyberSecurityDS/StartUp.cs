using CyberSecurityDS.Core;
using CyberSecurityDS.Core.Contracts;

namespace CyberSecurityDS;

public class StartUp
{
    public static void Main()
    {
        IEngine engine = new Engine();
        engine.Run();
    }
}
