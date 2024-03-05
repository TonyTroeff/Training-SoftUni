#include <iostream>
#include <math.h>
using namespace std;

int main()
{
	double a, b, c;
	cin >> a >> b >> c;

	double d = b * b - 4 * a * c;
	if (d < 0) {
		cout << "no roots" << endl;
	}
	else if (d == 0) {
		double root = - 1 * b / (2 * a);
		cout << root << endl;
	}
	else {
		double root1 = (-1 * b + sqrt(d)) / (2 * a);
		double root2 = (-1 * b - sqrt(d)) / (2 * a);

		cout << root1 << ' ' << root2 << endl;
	}
}
