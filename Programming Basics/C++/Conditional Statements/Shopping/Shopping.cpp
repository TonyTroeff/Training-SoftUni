#include <iostream>
using namespace std;

int main()
{
    double budget;
    int gpuCount, cpuCount, ramCount;
    cin >> budget >> gpuCount >> cpuCount >> ramCount;

    double gpuCosts = gpuCount * 250;

    double cpuPrice = 0.35 * gpuCosts;
    double cpuCosts = cpuCount * cpuPrice;

    double ramPrice = 0.1 * gpuCosts;
    double ramCosts = ramCount * ramPrice;

    double totalCosts = gpuCosts + cpuCosts + ramCosts;
    if (gpuCount > cpuCount)
        totalCosts *= 0.85; // Apply 15% discount

    cout.setf(ios::fixed);
    cout.precision(2);

    if (budget >= totalCosts) cout << "You have " << budget - totalCosts << " leva left!" << endl;
    else cout << "Not enough money! You need " << totalCosts - budget << " leva more!" << endl;
}
