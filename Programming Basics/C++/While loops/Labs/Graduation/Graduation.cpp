#include <iostream>
using namespace std;

int main()
{
    string studentName;
    cin >> studentName;

    int grade = 0;
    int fails = 0;
    double sum = 0;

    while (grade < 12)
    {
        double marks;
        cin >> marks;

        if (marks < 4)
        {
            if (fails == 1) break;
            fails++;
        }
        else
        {
            sum += marks;
            grade++;
            fails = 0;
        }
    }

    if (grade == 12)
    {
        double average = sum / 12;

        cout.setf(ios::fixed);
        cout.precision(2);
        cout << studentName << " graduated. Average grade: " << average << endl;
    }
    else
    {
        cout << studentName << " has been excluded at " << grade + 1 << " grade";
    }
}
