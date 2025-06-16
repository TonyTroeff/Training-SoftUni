namespace ClimbThePeaks;

public class Program
{
    static void Main()
    {
        const int peaksCount = 5;
        string[] peakNames = new string[peaksCount] { "Vihren", "Kutelo", "Banski Suhodol", "Polezhan", "Kamenitza" };
        int[] peakDifficulties = new int[peaksCount] { 80, 90, 100, 60, 70 };
        int conquerIndex = 0;

        Stack<int> food = new Stack<int>(Console.ReadLine().Split(", ").Select(int.Parse));
        Queue<int> stamina = new Queue<int>(Console.ReadLine().Split(", ").Select(int.Parse));

        int day = 1;
        while (food.Count > 0 && stamina.Count > 0 && conquerIndex < peaksCount && day <= 7)
        {
            int sum = food.Pop() + stamina.Dequeue();
            if (sum >= peakDifficulties[conquerIndex])
                conquerIndex++;

            day++;
        }

        if (conquerIndex == peaksCount) Console.WriteLine("Alex did it! He climbed all top five Pirin peaks in one week -> @FIVEinAWEEK");
        else Console.WriteLine("Alex failed! He has to organize his journey better next time -> @PIRINWINS");

        if (conquerIndex > 0)
        {
            Console.WriteLine("Conquered peaks:");
            for (int i = 0; i < conquerIndex; i++)
                Console.WriteLine(peakNames[i]);
        }
    }
}
