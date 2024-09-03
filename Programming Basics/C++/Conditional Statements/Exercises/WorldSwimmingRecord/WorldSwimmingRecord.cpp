#include <iostream>
#include <math.h>
using namespace std;

int main()
{
    // 1. Read input
    double existingRecordInSeconds, distanceInMeters, timePerMeter;
    cin >> existingRecordInSeconds >> distanceInMeters >> timePerMeter;

    // 2. Calculate travel time
    double totalTimeInSeconds = distanceInMeters * timePerMeter + floor(distanceInMeters / 15) * 12.5;

    // 3. Print output
    cout.setf(ios::fixed);
    cout.precision(2);
    if (totalTimeInSeconds < existingRecordInSeconds)
        cout << "Yes, he succeeded! The new world record is " << totalTimeInSeconds << " seconds." << endl;
    else
        cout << "No, he failed! He was " << totalTimeInSeconds - existingRecordInSeconds << " seconds slower." << endl;
}
