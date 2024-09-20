namespace EvenLines;

public class Program
{
    public static void Main()
    {
        const string filePath = @"..\..\..\text.txt";
        Console.Write(EvenLines.ProcessLines(filePath));
    }
}