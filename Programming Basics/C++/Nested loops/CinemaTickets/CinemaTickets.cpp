#include <iostream>
#include <string>
using namespace std;

int main()
{
    string movie;
    getline(cin, movie);

    cout.setf(ios::fixed);
    cout.precision(2);

    int totalSoldTickets = 0, studentTickets = 0, standardTickets = 0, kidTickets = 0;
    while (movie != "Finish")
    {
        int freeSeats;
        string ticketType;
        cin >> freeSeats >> ticketType;

        int soldTicketsForCurrentMovie = 0;
        while (ticketType != "End")
        {
            if (ticketType == "student") studentTickets++;
            else if (ticketType == "standard") standardTickets++;
            else if (ticketType == "kid") kidTickets++;

            soldTicketsForCurrentMovie++;
            if (soldTicketsForCurrentMovie >= freeSeats) break;

            cin >> ticketType;
        }

        totalSoldTickets += soldTicketsForCurrentMovie;

        double usagePercentage = (double)soldTicketsForCurrentMovie / freeSeats * 100;
        cout << movie << " - " << usagePercentage << "% full." << endl;

        cin >> ws;
        getline(cin, movie);
    }

    double studentTicketsPercentage = (double)studentTickets / totalSoldTickets * 100;
    double standardTicketsPercentage = (double)standardTickets / totalSoldTickets * 100;
    double kidTicketsPercentage = (double)kidTickets / totalSoldTickets * 100;
    cout << "Total tickets: " << totalSoldTickets << endl;
    cout << studentTicketsPercentage << "% student tickets." << endl;
    cout << standardTicketsPercentage << "% standard tickets." << endl;
    cout << kidTicketsPercentage << "% kids tickets." << endl;
}
