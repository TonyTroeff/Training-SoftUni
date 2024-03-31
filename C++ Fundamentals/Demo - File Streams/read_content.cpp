#include <iostream>
#include <fstream>
using namespace std;

int main() {
    ifstream ifs("./files/sample_text.txt");
    if (!ifs.is_open()) {
        cerr << "The requested file could not be opened. Please check if it exists." << endl;
        return 1;
    }

    string line;
    int index = 0;
    while (getline(ifs, line))
        cout << "Line #" << ++index << ": " << line << endl;

    ifs.close();
}
