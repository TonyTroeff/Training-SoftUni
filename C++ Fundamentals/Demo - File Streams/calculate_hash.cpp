#include <fstream>
#include <iomanip>
#include <iostream>
#include <sstream>

#include <openssl/evp.h>

using namespace std;

constexpr streamsize BUFFER_SIZE = 1024;

string hashLength(const unsigned char *hash, const unsigned int &length) {
    ostringstream oss;
    for (unsigned int i = 0; i < length; i++) {
        oss << hex << setw(2) << setfill('0') << (int)hash[i];
    }

    return oss.str();
}

int main() {
    // NOTE: When calculating hash, it is important to set the `ios_base::binary` flag.
    // Otherwise, when reading/writing data, line-ending conversions may occur depending on the file structure and the operating system.
    ifstream ifs("./files/software-university-logo.svg", ios_base::binary);
    if (!ifs.is_open()) {
        cerr << "The requested file could not be opened. Please check if it exists." << endl;
        return 1;
    }

    EVP_MD_CTX *hashAlgorithm = EVP_MD_CTX_new();
    if (!EVP_DigestInit_ex(hashAlgorithm, EVP_sha256(), nullptr)) {
        cerr << "The hash algorithm digest was not initialized successfully." << endl;
        EVP_MD_CTX_free(hashAlgorithm);
        return 1;
    }

    // NOTE: See the `read_binary_content` demo first.
    char buffer[BUFFER_SIZE];
    do {
        ifs.read(buffer, BUFFER_SIZE);
        streamsize readBytes = ifs.gcount();

        if (!EVP_DigestUpdate(hashAlgorithm, buffer, readBytes)) {
            cerr << "The hash algorithm digest was not updated successfully." << endl;
            EVP_MD_CTX_free(hashAlgorithm);
            return 1;
        }
    } while (ifs.good());

    ifs.close();

    unsigned char hash[EVP_MAX_MD_SIZE];
    unsigned int lengthOfHash = 0;
    if (!EVP_DigestFinal_ex(hashAlgorithm, hash, &lengthOfHash)) {
        cerr << "The hash algorithm digest was not finalized successfully." << endl;
        EVP_MD_CTX_free(hashAlgorithm);
        return 1;
    }

    EVP_MD_CTX_free(hashAlgorithm);

    // In order to validate the implementation, you can execute (in PowerShell) the following command:
    // Get-FileHash "./files/software-university-logo.svg" -Algorithm SHA256
    cout << hashLength(hash, lengthOfHash) << endl;
}
