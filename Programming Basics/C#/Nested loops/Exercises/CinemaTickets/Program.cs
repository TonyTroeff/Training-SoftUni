string movie = Console.ReadLine();

int totalSoldTickets = 0, studentTickets = 0, standardTickets = 0, kidTickets = 0;
while (movie != "Finish")
{
    int freeSeats = int.Parse(Console.ReadLine());
    string ticketType = Console.ReadLine();

    int soldTicketsForCurrentMovie = 0;
    while (ticketType != "End")
    {
        if (ticketType == "student") studentTickets++;
        else if (ticketType == "standard") standardTickets++;
        else if (ticketType == "kid") kidTickets++;

        soldTicketsForCurrentMovie++;
        if (soldTicketsForCurrentMovie >= freeSeats) break;

        ticketType = Console.ReadLine();
    }

    totalSoldTickets += soldTicketsForCurrentMovie;

    double usagePercentage = (double)soldTicketsForCurrentMovie / freeSeats * 100;
    Console.WriteLine($"{movie} - {usagePercentage:f2}% full.");

    movie = Console.ReadLine();
}

double studentTicketsPercentage = (double)studentTickets / totalSoldTickets * 100;
double standardTicketsPercentage = (double)standardTickets / totalSoldTickets * 100;
double kidTicketsPercentage = (double)kidTickets / totalSoldTickets * 100;

Console.WriteLine($"Total tickets: {totalSoldTickets}");
Console.WriteLine($"{studentTicketsPercentage:f2}% student tickets.");
Console.WriteLine($"{standardTicketsPercentage:f2}% standard tickets.");
Console.WriteLine($"{kidTicketsPercentage:f2}% kids tickets.");