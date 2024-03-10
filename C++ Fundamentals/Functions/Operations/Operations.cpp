#include <iostream>
using namespace std;

int add(const int& a, const int& b) {
    return a + b;
}

int subtract(const int& a, const int& b) {
    return a - b;
}

int multiply(const int& a, const int& b) {
    return a * b;
}

int divide(const int& a, const int& b) {
    return a / b;
}

int main()
{
    int leftOperand, rightOperand;
    string operation;
    cin >> leftOperand >> rightOperand >> operation;

    if (operation == "+") cout << add(leftOperand, rightOperand) << endl;
    else if (operation == "-") cout << subtract(leftOperand, rightOperand) << endl;
    else if (operation == "*") cout << multiply(leftOperand, rightOperand) << endl;
    else if (operation == "/") cout << divide(leftOperand, rightOperand) << endl;
}
