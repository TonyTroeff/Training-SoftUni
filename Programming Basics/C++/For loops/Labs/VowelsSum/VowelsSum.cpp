#include <iostream>
#include <string>
using namespace std;

int main()
{
    string text;
    getline(cin, text);

    int sum = 0;
    for (int i = 0; i < text.length(); i++)
    {
        if (text[i] == 'a') sum += 1;
        else if (text[i] == 'e') sum += 2;
        else if (text[i] == 'i') sum += 3;
        else if (text[i] == 'o') sum += 4;
        else if (text[i] == 'u') sum += 5;
    }

    cout << sum << endl;
}
