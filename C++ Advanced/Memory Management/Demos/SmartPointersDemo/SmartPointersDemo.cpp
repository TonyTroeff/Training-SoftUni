#include <iostream>
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

void printPtr(unique_ptr<DestructionObserver> ptr) {
}

void printPtrRef(unique_ptr<DestructionObserver>& ptr) {
	cout << ptr << endl;
}

void demo1() {
	cout << "----- Demonstrate what is possible to do with unique_ptr -----" << endl;
	unique_ptr<DestructionObserver> ptr = make_unique<DestructionObserver>("Gen A", 1);

	// unique_ptr<DestructionObserver> ptr2 = ptr; // This is not possible
	// printPtr(ptr); // This is not possible

	printPtrRef(ptr);
}

void demo2() {
	cout << "----- Demonstrate unique_ptr -----" << endl;
	for (int i = 0; i < 5; i++) {
		cout << "Start of iteration #" << i + 1 << endl;
		unique_ptr<DestructionObserver> ptr = make_unique<DestructionObserver>("Gen B", i + 1);
		cout << "End of iteration #" << i + 1 << endl;
	}

	cout << "End of demonstration" << endl;
}

void demo3() {
	cout << "----- Demonstrate move and reset when using unique_ptr -----" << endl;

	unique_ptr<DestructionObserver> ptr = make_unique<DestructionObserver>("Gen C", 1);
	
	cout << "Before move" << endl;
	unique_ptr<DestructionObserver> newPtr = move(ptr);
	cout << "After move" << endl;

	cout << "Before reset" << endl;
	newPtr.reset();
	cout << "After reset" << endl;
}

void demo4() {
	cout << "----- Demonstrate shared_ptr -----" << endl;

	shared_ptr<DestructionObserver> ptr = make_shared<DestructionObserver>("Gen D", 1);

	cout << "Before re-assign" << endl;
	ptr = make_shared<DestructionObserver>("Gen D", 2);
	cout << "After re-assign" << endl;

	for (int i = 0; i < 5; i++) {
		shared_ptr<DestructionObserver> ptr2 = ptr;
		cout << "Created a copy of the shared pointer" << endl;
	}

	cout << "End of demonstration" << endl;
}

void demo5() {
	cout << "----- Demonstrate differences between unique_ptr and raw pointers when dealing with arrays -----" << endl;

	const int n = 15;

	const int* rawPtr = new int[15] {}; // If the braces are missing, the array will not be initialized.
	const unique_ptr<int[]> smartPtr = make_unique<int[]>(n);

	cout << "Raw pointer: ";
	for (int i = 0; i < n; i++) cout << rawPtr[i] << ' ';
	cout << endl;

	cout << "Smart pointer: ";
	for (int i = 0; i < n; i++) cout << smartPtr[i] << ' ';
	cout << endl;

	delete[] rawPtr;
}

void demo6() {
	cout << "----- Demonstrate matrices with unique_ptr -----" << endl;

	const int rows = 5, cols = 5;

	unique_ptr<unique_ptr<unique_ptr<DestructionObserver>[]>[]> ptr = make_unique<unique_ptr<unique_ptr<DestructionObserver>[]>[]>(rows);

	cout << "Before initialization: " << endl;
	for (int i = 0; i < rows; i++) cout << "At index #" << i << ": " << ptr[i] << endl;

	for (int i = 0; i < rows; i++) ptr[i] = make_unique<unique_ptr<DestructionObserver>[]>(cols);

	cout << "After initialization: " << endl;
	for (int i = 0; i < rows; i++) cout << "At index #" << i << ": " << ptr[i] << endl;


	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			ptr[i][j] = make_unique<DestructionObserver>("Cell", i * cols + j + 1);
		}
	}
}

int main()
{
	demo1(); cout << endl;
	demo2(); cout << endl;
	demo3(); cout << endl;
	demo4(); cout << endl;
	demo5(); cout << endl;
	demo6(); cout << endl;
}