namespace CopyBinaryFile;

public static class Program
{
    public static void Main()
    {
        string inputFilePath = @"..\..\..\copyMe.png";
        string outputFilePath = @"..\..\..\copyMe-copy.png";

        CopyBinaryFile.CopyFile(inputFilePath, outputFilePath);
    }
}