#include <iostream>

int main()
{
    int length, width, height;
    double percentage;
    std::cin >> length >> width >> height >> percentage;

    double volume = 0.001 * length * width * height;
    double freeSpace = volume * (1 - 0.01 * percentage);
    std::cout << freeSpace << std::endl;
}