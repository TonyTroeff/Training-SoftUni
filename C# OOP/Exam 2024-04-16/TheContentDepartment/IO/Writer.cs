namespace TheContentDepartment.IO;

using TheContentDepartment.IO.Contracts;

public class Writer : IWriter
{
    private readonly string _filePath;

    public Writer()
    {
        var resultsPath = GetProjectDirectory() + "Utilities/Results/";
        this._filePath = Path.Combine(resultsPath, "Actual.txt");

        File.WriteAllText(this._filePath, string.Empty);
    }

    public void WriteText(string message) => File.AppendAllText(this._filePath, message.TrimEnd() + Environment.NewLine);

    public void Write(string message) => Console.Write(message);

    public void WriteLine(string message) => Console.WriteLine(message);

    private static string GetProjectDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var directoryName = Path.GetFileName(currentDirectory);
        var relativePath = directoryName.StartsWith("net6.0") ? @"../../../" : string.Empty;

        return relativePath;
    }
}