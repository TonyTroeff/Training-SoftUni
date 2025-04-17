using CyberSecurityDS.IO.Contracts;

namespace CyberSecurityDS.IO;

public class Reader : IReader
{
    public string ReadLine() => Console.ReadLine();
}
