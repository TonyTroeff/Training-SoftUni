namespace CopyDirectory;

public static class CopyDirectory
{
    public static void CopyAllFiles(string inputPath, string outputPath)
    {
        DirectoryInfo outputDirectory = new DirectoryInfo(outputPath);

        if (outputDirectory.Exists) outputDirectory.Delete();
        outputDirectory.Create();

        DirectoryInfo inputDirectory = new DirectoryInfo(inputPath);
        FileInfo[] files = inputDirectory.GetFiles();
        foreach (FileInfo file in files)
        {
            string destinationPath = Path.Combine(outputPath, file.Name);
            file.CopyTo(destinationPath);
        }
    }
}
