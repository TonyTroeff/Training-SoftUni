using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordCount;

public class WordCount
{
    public static void Main()
    {
        string wordPath = @"..\..\..\Files\words.txt";
        string textPath = @"..\..\..\Files\text.txt";
        string outputPath = @"..\..\..\Files\output.txt";

        CalculateWordCounts(wordPath, textPath, outputPath);
    }

    public static void CalculateWordCounts(string wordsFilePath, string textFilePath, string outputFilePath)
    {
        Dictionary<string, int> occurrencesMap = new Dictionary<string, int>();

        using (StreamReader wordsReader = new StreamReader(wordsFilePath))
        {
            string[] allWords = wordsReader.ReadToEnd().ToLower().Split();
            foreach (string word in allWords)
                occurrencesMap[word] = 0;
        }

        using (StreamReader textReader = new StreamReader(textFilePath))
        {
            string[] allWords = textReader.ReadToEnd().ToLower().Split(".,?!\n\r ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in allWords)
            {
                if (occurrencesMap.ContainsKey(word))
                    occurrencesMap[word]++;
            }
        }

        using (StreamWriter outputWriter = new StreamWriter(outputFilePath))
        {
            foreach (var (word, frequency) in occurrencesMap.OrderByDescending(x => x.Value))
                outputWriter.WriteLine($"{word} - {frequency}");
        }
    }
}
