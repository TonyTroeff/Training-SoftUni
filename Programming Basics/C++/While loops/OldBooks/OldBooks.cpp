#include <iostream>
#include <string>
using namespace std;

int main()
{
    string bookToLookFor;
    getline(cin, bookToLookFor);

    int booksCount = 0;
    bool isFound = false;

    string input;
    getline(cin, input);
    while (input != "No More Books") {
        if (input == bookToLookFor) {
            isFound = true;
            break;
        }

        booksCount++;
        getline(cin, input);
    }

    if (isFound) cout << "You checked " << booksCount << " books and found it." << endl;
    else cout << "The book you search is not here!" << endl << "You checked " << booksCount << " books." << endl;
}
