#include <iostream>
#include <math.h>
using namespace std;

double calculateDistanceFromOrigin(const double& x, const double& y) {
	return sqrt(x * x + y * y); // Equivalent to `hypot(x, y)`
}

void printCoordinates(const double& x, const double& y) {
	cout << '(' << x << ", " << y << ')' << endl;
}

int main()
{
	double x1, y1, x2, y2;
	cin >> x1 >> y1 >> x2 >> y2;

	double d1 = calculateDistanceFromOrigin(x1, y1);
	double d2 = calculateDistanceFromOrigin(x2, y2);

	if (d1 <= d2) printCoordinates(x1, y1);
	else printCoordinates(x2, y2);
}
