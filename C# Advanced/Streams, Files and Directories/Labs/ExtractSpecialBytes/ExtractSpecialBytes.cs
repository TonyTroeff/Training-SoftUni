using System.Collections.Generic;
using System.IO;

namespace ExtractSpecialBytes;

public class ExtractSpecialBytes
{
    public static void Main()
    {
        string binaryFilePath = @"..\..\..\Files\example.png";
        string bytesFilePath = @"..\..\..\Files\bytes.txt";
        string outputPath = @"..\..\..\Files\output.bin";

        ExtractBytesFromBinaryFile(binaryFilePath, bytesFilePath, outputPath);
    }

    public static void ExtractBytesFromBinaryFile(string binaryFilePath, string bytesFilePath, string outputPath)
    {
        HashSet<byte> specialBytes = new HashSet<byte>();

        using (StreamReader bytesReader = new StreamReader(bytesFilePath))
        {
            while (!bytesReader.EndOfStream)
            {
                byte current = byte.Parse(bytesReader.ReadLine());
                specialBytes.Add(current);
            }
        }

        using FileStream inputFileStream = new FileStream(binaryFilePath, FileMode.Open, FileAccess.Read);
        using FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write);

        byte[] buffer = new byte[1024];

        int inputBytes;
        while ((inputBytes = inputFileStream.Read(buffer)) != 0)
        {
            int outputBytes = 0;
            for (int i = 0; i < inputBytes; i++)
            {
                if (specialBytes.Contains(buffer[i]))
                    buffer[outputBytes++] = buffer[i];
            }

            outputFileStream.Write(buffer, 0, outputBytes);
        }
    }
}
