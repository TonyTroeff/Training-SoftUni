#include <iostream>

#define _USE_MATH_DEFINES
#include <math.h>

int main()
{
    double radians;
    std::cin >> radians;

    double degrees = radians * 180 / M_PI;

    std::cout << round(degrees) << std::endl;
}