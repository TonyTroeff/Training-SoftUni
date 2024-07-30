namespace TheContentDepartment.IO;

using TheContentDepartment.IO.Contracts;

public class Reader : IReader
{
    public string ReadLine() => Console.ReadLine();
}