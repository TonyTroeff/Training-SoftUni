bool cIsFound = false, oIsFound = false, nIsFound = false;
string currentWord = "";

string input = Console.ReadLine();
while (input != "End")
{
    char symbol = input[0];
    if (char.IsLetter(symbol))
    {
        if (symbol == 'c' && !cIsFound) cIsFound = true;
        else if (symbol == 'o' && !oIsFound) oIsFound = true;
        else if (symbol == 'n' && !nIsFound) nIsFound = true;
        else currentWord += symbol;

        if (cIsFound && oIsFound && nIsFound)
        {
            Console.Write(currentWord);
            Console.Write(' ');

            cIsFound = false;
            oIsFound = false;
            nIsFound = false;
            currentWord = "";
        }
    }

    input = Console.ReadLine();
}

Console.WriteLine();