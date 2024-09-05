#include <iostream>
using namespace std;

int main()
{
    double age;
    string gender;
    cin >> age >> gender;

    string title = "";
    if (gender == "m")
    {
        if (age < 16) title = "Master";
        else title = "Mr.";
    }
    else if (gender == "f")
    {
        if (age < 16) title = "Miss";
        else title = "Ms.";
    }

    cout << title << endl;
}
