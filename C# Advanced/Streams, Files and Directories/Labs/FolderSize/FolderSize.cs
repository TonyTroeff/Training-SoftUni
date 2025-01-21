using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FolderSize;

public class FolderSize
{
    public static void Main(string[] args)
    {
        string folderPath = @"..\..\..\Files\TestFolder";
        string outputPath = @"..\..\..\Files\output.txt";

        GetFolderSize(folderPath, outputPath);
    }

    public static void GetFolderSize(string folderPath, string outputFilePath)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(folderPath);

        long totalSize = 0;
        while (queue.Count > 0)
        {
            string currentFolder = queue.Dequeue();

            string[] files = Directory.GetFiles(currentFolder);
            foreach (string filePath in files)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                totalSize += fileInfo.Length;
            }

            string[] subFolders = Directory.GetDirectories(currentFolder);
            foreach (string subFolderPath in subFolders)
                queue.Enqueue(subFolderPath);
        }

        using (StreamWriter outputWriter = new StreamWriter(outputFilePath))
            outputWriter.WriteLine($"{totalSize / 1024m} KB");
    }
}
