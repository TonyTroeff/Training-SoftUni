#include <iostream>
#include <string>
using namespace std;

int main()
{
    int width, height, depth;
    cin >> width >> height >> depth;

    int freeSpace = width * height * depth;
    bool spaceIsEnough = true;
    
    string input;
    while (getline(cin >> ws, input) && input != "Done") {
        int spaceToTake = stoi(input);
        freeSpace -= spaceToTake;

        if (freeSpace < 0) {
            spaceIsEnough = false;
            break;
        }
    }

    if (spaceIsEnough) cout << freeSpace << " Cubic meters left." << endl;
    else cout << "No more free space! You need " << freeSpace * -1 << " Cubic meters more." << endl;
}
