#include <iostream>
#include <string>
using namespace std;

int main()
{
	string firstName, lastName, town;
	int age;
	cin >> firstName >> lastName >> age >> town;

	cout << "You are " << firstName << " " << lastName << ", a "
		<< age << "-years old person from " << town << "." << endl;
}
