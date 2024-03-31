#include <iostream>
#include <fstream>
using namespace std;

constexpr streamsize BUFFER_SIZE = 1024;

int main() {
    ifstream ifs("./files/software-university-logo.svg", ios_base::binary);

    if (!ifs.is_open()) {
        cerr << "The requested file could not be opened. Please check if it exists." << endl;
        return 1;
    }

    char buffer[BUFFER_SIZE];
    streamsize totalSize = 0;
    do {
        ifs.read(buffer, BUFFER_SIZE);

        // ifs.gcount() returns the number of characters extracted by the previous input function.
        streamsize readBytes = ifs.gcount();
        totalSize += readBytes;

        cout << "Read " << readBytes << " bytes: ";
        for (int i = 0; i < readBytes; i++) cout << (int)buffer[i] << ' ';
        cout << endl;
    } while (ifs.good());

    cout << "The total size of the stream was: " << totalSize << endl;

    ifs.close();
}
