string text = Console.ReadLine();

Stack<char> stack = new Stack<char>();
foreach (char symbol in text)
    stack.Push(symbol);

while (stack.Count > 0)
{
    char currentCharacter = stack.Pop();
    Console.Write(currentCharacter);
}

Console.WriteLine();