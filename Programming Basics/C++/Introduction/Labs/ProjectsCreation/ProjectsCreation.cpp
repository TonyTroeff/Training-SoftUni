#include <iostream>
#include <string>
using namespace std;

int main()
{
    string architectName;
    int projectsCount;
    cin >> architectName >> projectsCount;

    int requiredHours = projectsCount * 3;
    cout << "The architect " << architectName << " will need " << requiredHours << " hours to complete " << projectsCount << " project/s." << endl;
}
