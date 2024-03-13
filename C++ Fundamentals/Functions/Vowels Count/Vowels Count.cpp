#include <iostream>
using namespace std;

bool isVowel(const char& symbol) {
    char lowerSymbol = tolower(symbol);
    return lowerSymbol == 'a' || lowerSymbol == 'e' || lowerSymbol == 'i' || lowerSymbol == 'o' || lowerSymbol == 'u';
}

int countVowels(const string& text) {
    int result = 0;

    for (int i = 0; i < text.length(); i++) {
        if (isVowel(text[i])) result++;
    }

    return result;
}

int main()
{
    string text;
    cin >> text;

    cout << countVowels(text) << endl;
}