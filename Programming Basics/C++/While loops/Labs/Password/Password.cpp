#include <iostream>
#include <string>
using namespace std;

int main()
{
	string username, password, input;
	getline(cin, username);
	getline(cin, password);
	
	getline(cin, input);
	while (input != password) getline(cin, input);

	cout << "Welcome " << username << "!" << endl;
}
