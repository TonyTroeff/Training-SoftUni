namespace PiePursuit;

public class Program
{
    static void Main()
    {
        Queue<int> contestants = new Queue<int>(Console.ReadLine().Split().Select(int.Parse));
        Stack<int> pies = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));

        while (contestants.Count > 0 && pies.Count > 0)
        {
            int diff = contestants.Dequeue() - pies.Pop();
            if (diff > 0) contestants.Enqueue(diff);
            else if (diff == -1 && pies.Count > 0) pies.Push(pies.Pop() + 1);
            else if (diff < 0) pies.Push(-1 * diff);
        }

        if (contestants.Count > 0)
        {
            Console.WriteLine("We will have to wait for more pies to be baked!");
            Console.WriteLine($"Contestants left: {string.Join(", ", contestants)}");
        }
        else if (pies.Count > 0)
        {
            Console.WriteLine("Our contestants need to rest!");
            Console.WriteLine($"Pies left: {string.Join(", ", pies)}");
        }
        else
        {
            Console.WriteLine("We have a champion!");
        }
    }
}
