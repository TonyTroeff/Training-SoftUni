#include <iostream>

#define _USE_MATH_DEFINES
#include <math.h>

using namespace std;

int main()
{
    string figure;
    cin >> figure;

    double area = 0;
    if (figure == "square")
    {
        double a;
        cin >> a;

        area = a * a;
    }
    else if (figure == "rectangle")
    {
        double a, b;
        cin >> a >> b;

        area = a * b;
    }
    else if (figure == "circle")
    {
        double r;
        cin >> r;

        area = M_PI * r * r;
    }
    else if (figure == "triangle")
    {
        double a, h;
        cin >> a >> h;

        area = a * h / 2;
    }

    cout.setf(ios::fixed);
    cout.precision(3);
    cout << area << endl;
}
