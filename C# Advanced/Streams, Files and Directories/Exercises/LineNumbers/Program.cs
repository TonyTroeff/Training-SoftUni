namespace LineNumbers;

public static class Program
{
    public static void Main()
    {
        string inputFilePath = @"..\..\..\text.txt";
        string outputFilePath = @"..\..\..\output.txt";
        LineNumbers.ProcessLines(inputFilePath, outputFilePath);
    }
}