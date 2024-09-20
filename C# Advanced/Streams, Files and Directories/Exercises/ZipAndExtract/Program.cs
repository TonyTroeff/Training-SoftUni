namespace ZipAndExtract;

public static class Program
{
    public static void Main()
    {
        string inputFile = @"..\..\..\copyMe.png";
        string zipArchiveFile = @"..\..\..\archive.zip";
        string extractedFile = @"..\..\..\extracted.png";

        ZipAndExtract.ZipFileToArchive(inputFile, zipArchiveFile);

        string fileNameOnly = Path.GetFileName(inputFile);
        ZipAndExtract.ExtractFileFromArchive(zipArchiveFile, fileNameOnly, extractedFile);
    }
}