using CarDealership.Core;
using CarDealership.Core.Contracts;

namespace CarDealership
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
