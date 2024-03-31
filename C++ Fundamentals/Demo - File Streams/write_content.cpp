#include <fstream>
#include <iostream>
#include <string>
using namespace std;

int main() {
    ofstream ofs("./logs.txt");
    if (!ofs.is_open()) {
        cerr << "The output file could not be opened." << endl;
        return 1;
    }

    cout << "All lines you write will be appended to the output file." << endl
        << "The program will stop whenever you enter a blank line." << endl;

    string line;
    while (getline(cin, line) && !line.empty()) ofs << line << endl;

    ofs.close();
}
