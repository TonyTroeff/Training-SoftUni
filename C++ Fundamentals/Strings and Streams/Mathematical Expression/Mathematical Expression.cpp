#include <iostream>
#include <string>
using namespace std;

bool isCorrect(const string& text) {
    int openBraces = 0;

    for (int i = 0; i < text.length(); i++) {
        if (text[i] == '(') openBraces++;
        else if (text[i] == ')') {
            openBraces--;

            if (openBraces < 0) return false;
        }
    }

    return openBraces == 0;
}

int main()
{
    string input;
    getline(cin, input);

    if (isCorrect(input)) cout << "correct" << endl;
    else cout << "incorrect" << endl;
}
