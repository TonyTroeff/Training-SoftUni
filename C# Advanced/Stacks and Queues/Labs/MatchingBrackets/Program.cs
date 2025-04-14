string input = Console.ReadLine();

Stack<int> openingIndices = new Stack<int>();
for (int i = 0; i < input.Length; i++)
{
    if (input[i] == '(') openingIndices.Push(i);
    else if (input[i] == ')')
    {
        int start = openingIndices.Pop();
        string segment = input.Substring(start, i - start + 1);
        Console.WriteLine(segment);
    }
}