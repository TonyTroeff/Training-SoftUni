namespace DirectoryTraversal;

public static class Program
{
    public static void Main()
    {
        string path = Console.ReadLine();
        string reportFileName = @"\report.txt";

        string reportContent = DirectoryTraversal.TraverseDirectory(path);
        Console.WriteLine(reportContent);

        DirectoryTraversal.WriteReportToDesktop(reportContent, reportFileName);
    }
}