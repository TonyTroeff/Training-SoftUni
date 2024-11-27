using CarDealership.IO.Contracts;

namespace CarDealership.IO
{
    public class Reader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}
