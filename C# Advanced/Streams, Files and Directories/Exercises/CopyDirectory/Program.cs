namespace CopyDirectory;

public static class Program
{
    public static void Main(string[] args)
    {
        string inputPath = Console.ReadLine();
        string outputPath = Console.ReadLine();

        CopyDirectory.CopyAllFiles(inputPath, outputPath);
    }
}