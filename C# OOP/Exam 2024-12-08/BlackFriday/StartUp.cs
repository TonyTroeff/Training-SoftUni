using BlackFriday.Core.Contracts;
using BlackFriday.Core;

namespace BlackFriday
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
