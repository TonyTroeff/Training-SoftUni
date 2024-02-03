#include <iostream>
#include <vector>
using namespace std;

class DestructionObserver {
private:
	string instanceName;
	int instanceId;

public:
	DestructionObserver(const string& instanceName, const int& instanceId)
		: instanceName(instanceName), instanceId(instanceId)
	{
	}

	~DestructionObserver()
	{
		cout << "The instance \"" << this->instanceName << "\" #" << this->instanceId << " was destroyed." << endl;
	}
};

static DestructionObserver globalInstance("Global", 1);

void allocateAutomaticMemory(const size_t mb) {
	size_t bitsCount = mb * 1024 * 1024;
	vector<char> bits(bitsCount);

	for (size_t i = 0; i < bitsCount; i++)
		bits[i] = (i % 7) * (i % 17);

	cout << "Allocated " << mb << "MB of automatic memory." << endl;
	cout << "Please, review the memory consumption and press enter when ready.";
	cin.get();
}

void demo1() {
	allocateAutomaticMemory(10);

	cout << "Automatic memory should be freed by now. Please, verify and press enter to continue.";
	cin.get();
}

void demo2() {
	cout << "----- Demonstrate difference between automatic and dynamic memory -----" << endl;

	const int iterations = 5;

	for (int i = 0; i < iterations; i++) {
		cout << "Beginning iteration #" << i + 1 << endl;
		DestructionObserver automaticInstance("Automatic", i + 1);
		DestructionObserver* dynamicInstance = new DestructionObserver("Dynamic", i + 1);
		cout << "Finishing iteration #" << i + 1 << endl;
	}

	cout << "Finishing the demonstration" << endl;
}

void demo3() {
	int* i = new int(10);
	delete i;
	// delete i; // If this line is uncommented, a runtime exception will occur.
}

void demo4() {
	int* i = nullptr;
	delete i;
}

int main()
{
	demo1();
	demo2();
	demo3();
	demo4();
}
