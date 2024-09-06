string text = Console.ReadLine();

Dictionary<char, char> parenthesis = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' } };

Stack<char> expectedClosures = new Stack<char>();

bool isValid = true;
for (int i = 0; isValid && i < text.Length; i++)
{
    if (parenthesis.ContainsKey(text[i])) expectedClosures.Push(parenthesis[text[i]]);
    else
    {
        if (expectedClosures.Count > 0 && expectedClosures.Peek() == text[i]) expectedClosures.Pop();
        else isValid = false;
    }
}

if (isValid && expectedClosures.Count > 0) isValid = false;

if (isValid) Console.WriteLine("YES");
else Console.WriteLine("NO");