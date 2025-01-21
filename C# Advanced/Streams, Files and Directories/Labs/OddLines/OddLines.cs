using System.IO;

namespace OddLines;

public class OddLines
{
    public static void Main()
    {
        string inputFilePath = @"..\..\..\Files\input.txt";
        string outputFilePath = @"..\..\..\Files\output.txt";

        ExtractOddLines(inputFilePath, outputFilePath);
    }

    public static void ExtractOddLines(string inputFilePath, string outputFilePath)
    {
        using StreamReader inputReader = new StreamReader(inputFilePath);
        using StreamWriter outputWriter = new StreamWriter(outputFilePath);

        int rowNumber = 0;
        while (!inputReader.EndOfStream)
        {
            string line = inputReader.ReadLine();
            if (rowNumber % 2 != 0)
                outputWriter.WriteLine(line);

            rowNumber++;
        }
    }
}
