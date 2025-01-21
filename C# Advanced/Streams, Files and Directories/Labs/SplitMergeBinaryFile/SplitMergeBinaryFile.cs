using System;
using System.IO;

namespace SplitMergeBinaryFile;

public class SplitMergeBinaryFile
{
    static void Main()
    {
        string sourceFilePath = @"..\..\..\Files\example.png";
        string joinedFilePath = @"..\..\..\Files\example-joined.png";
        string partOnePath = @"..\..\..\Files\part-1.bin";
        string partTwoPath = @"..\..\..\Files\part-2.bin";

        SplitBinaryFile(sourceFilePath, partOnePath, partTwoPath);
        MergeBinaryFiles(partOnePath, partTwoPath, joinedFilePath);
    }

    public static void SplitBinaryFile(string sourceFilePath, string partOneFilePath, string partTwoFilePath)
    {
        using FileStream sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);

        int partTwoLength = (int)(sourceStream.Length / 2);
        int partOneLength = (int)(partTwoLength + sourceStream.Length % 2);
        
        using (FileStream partOneStream = new FileStream(partOneFilePath, FileMode.Create, FileAccess.Write))
            CopyExact(sourceStream, partOneStream, partOneLength);

        using (FileStream partTwoStream = new FileStream(partTwoFilePath, FileMode.Create, FileAccess.Write))
            CopyExact(sourceStream, partTwoStream, partTwoLength);
    }

    private static void CopyExact(Stream source, Stream destination, int length)
    {
        byte[] buffer = new byte[1024];

        int totalReadBytes = 0;
        while (totalReadBytes < length)
        {
            int currentReadBytes = source.Read(buffer, 0, Math.Min(buffer.Length, length - totalReadBytes));
            destination.Write(buffer, 0, currentReadBytes);

            totalReadBytes += currentReadBytes;
        }
    }

    public static void MergeBinaryFiles(string partOneFilePath, string partTwoFilePath, string joinedFilePath)
    {
        using FileStream outputStream = new FileStream(joinedFilePath, FileMode.Create, FileAccess.Write);

        using (FileStream partOneStream = new FileStream(partOneFilePath, FileMode.Open, FileAccess.Read))
            partOneStream.CopyTo(outputStream);

        using (FileStream partTwoStream = new FileStream(partTwoFilePath, FileMode.Open, FileAccess.Read))
            partTwoStream.CopyTo(outputStream);
    }
}