namespace CustomRandomList;

public class StartUp
{
    static void Main()
    {
        RandomList list = new RandomList { "first", "second", "third", "hello", "world" };

        while (list.Count > 0)
            Console.WriteLine(list.RandomString());
    }
}
