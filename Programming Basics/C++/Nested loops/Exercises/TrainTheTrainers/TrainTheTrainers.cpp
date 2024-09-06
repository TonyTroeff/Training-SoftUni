#include <iostream>
#include <string>
using namespace std;

int main()
{
    int juries;
    cin >> juries;

    double presentationScoresSum = 0;
    int presentationsCount = 0;

    cout.setf(ios::fixed);
    cout.precision(2);

    string presentationName;
    cin >> ws;
    getline(cin, presentationName);
    while (presentationName != "Finish")
    {
        double currentScoresSum = 0;
        for (int i = 0; i < juries; i++)
        {
            double juryScore;
            cin >> juryScore;
            currentScoresSum += juryScore;
        }

        double grade = currentScoresSum / juries;
        cout << presentationName << " - " << grade << "." << endl;

        presentationScoresSum += grade;
        presentationsCount++;

        cin >> ws;
        getline(cin, presentationName);
    }

    double averageAssessment = presentationScoresSum / presentationsCount;
    cout << "Student's final assessment is " << averageAssessment << "." << endl;
}
