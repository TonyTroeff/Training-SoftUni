#include <fstream>
#include <iostream>
#include <sstream>
using namespace std;

constexpr streamsize CHUNK_SIZE = 1024;

string constructChunkName(const int& chunkId) {
    ostringstream oss;
    oss << "./chunk-" << chunkId;

    return oss.str();
}

int main() {
    ifstream ifs("./files/software-university-logo.svg", ios_base::binary);
    if (!ifs.is_open()) {
        cerr << "The requested file could not be opened. Please check if it exists." << endl;
        return 1;
    }

    ofstream ofs;
    char buffer[CHUNK_SIZE];
    int chunkId = 0;
    do {
        ifs.read(buffer, CHUNK_SIZE);

        // ifs.gcount() returns the number of characters extracted by the previous input function.
        streamsize readBytes = ifs.gcount();
        string chunkName = constructChunkName(++chunkId);

        // If the files do not exist, they will be created.
        // If the files do exist, they will be overwritten.
        ofs.open(chunkName, ios_base::out | ios_base::binary);
        if (!ofs.is_open()) {
            cerr << "The output file \"" << chunkName << "\" could not be opened." << endl;
            return 1;
        }

        ofs.write(buffer, readBytes);
        ofs.close();
    } while (ifs.good());

    ifs.close();
}
