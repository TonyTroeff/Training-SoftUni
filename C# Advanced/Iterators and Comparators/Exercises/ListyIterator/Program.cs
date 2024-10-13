using System.Diagnostics;

namespace ListyIterator;

public class Program
{
    public static void Main()
    {
        string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Debug.Assert(data[0] == "Create");

        ListyIterator<string> iter = new ListyIterator<string>(data.Skip(1).ToList());

        string command = Console.ReadLine();
        while (command != "END")
        {
            try
            {
                if (command == "Move") Console.WriteLine(iter.Move());
                else if (command == "HasNext") Console.WriteLine(iter.HasNext());
                else if (command == "Print") iter.Print();
                else if (command == "PrintAll") Console.WriteLine(string.Join(' ', iter));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            command = Console.ReadLine();
        }
    }
}
