using System.IO;

namespace MergeFiles;

public class MergeFiles
{
    public static void Main()
    {
        var firstInputFilePath = @"..\..\..\Files\input1.txt";
        var secondInputFilePath = @"..\..\..\Files\input2.txt";
        var outputFilePath = @"..\..\..\Files\output.txt";

        MergeTextFiles(firstInputFilePath, secondInputFilePath, outputFilePath);
    }

    public static void MergeTextFiles(string firstInputFilePath, string secondInputFilePath, string outputFilePath)
    {
        using StreamReader firstReader = new StreamReader(firstInputFilePath);
        using StreamReader secondReader = new StreamReader(secondInputFilePath);
        using StreamWriter outputWriter = new StreamWriter(outputFilePath);

        while (!firstReader.EndOfStream || !secondReader.EndOfStream)
        {
            if (!firstReader.EndOfStream)
                outputWriter.WriteLine(firstReader.ReadLine());

            if (!secondReader.EndOfStream)
                outputWriter.WriteLine(secondReader.ReadLine());
        }
    }
}
