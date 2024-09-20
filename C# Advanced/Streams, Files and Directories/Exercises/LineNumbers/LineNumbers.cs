namespace LineNumbers;

public static class LineNumbers
{
    public static void ProcessLines(string inputFilePath, string outputFilePath)
    {
        using FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using StreamReader inputStreamReader = new StreamReader(inputFileStream);
        
        using FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
        using StreamWriter outputStreamWriter = new StreamWriter(outputFileStream);

        int lineNumber = 1;
        while (!inputStreamReader.EndOfStream)
        {
            string line = inputStreamReader.ReadLine();

            int letters = 0, punctuations = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (char.IsLetter(line[i])) letters++;
                else if (char.IsPunctuation(line[i])) punctuations++;
            }

            outputStreamWriter.WriteLine($"Line {lineNumber++}: {line} ({letters})({punctuations})");
        }
    }
}
