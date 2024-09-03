#include <iostream>

int main()
{
    int totalPages, pagesPerHour, days;
    std::cin >> totalPages >> pagesPerHour >> days;

    int result = totalPages / pagesPerHour / days;
    std::cout << result << std::endl;

    return 0;
}