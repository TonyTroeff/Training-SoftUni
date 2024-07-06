function solve(input) {
    let totalSoldTickets = 0;
    let studentTickets = 0;
    let standardTickets = 0;
    let kidTickets = 0;

    let index = 0;
    let movie = input[index];
    while (movie != "Finish") {
        const freeSeats = Number(input[++index]);
        let ticketType = input[++index];

        let soldTicketsForCurrentMovie = 0;
        while (ticketType !== "End") {
            if (ticketType === "student") studentTickets++;
            else if (ticketType === "standard") standardTickets++;
            else if (ticketType === "kid") kidTickets++;

            soldTicketsForCurrentMovie++;
            if (soldTicketsForCurrentMovie >= freeSeats) break;

            ticketType = input[++index];
        }

        totalSoldTickets += soldTicketsForCurrentMovie;

        const usagePercentage = (soldTicketsForCurrentMovie / freeSeats) * 100;
        console.log(`${movie} - ${usagePercentage.toFixed(2)}% full.`);

        movie = input[++index];
    }

    const studentTicketsPercentage = (studentTickets / totalSoldTickets) * 100;
    const standardTicketsPercentage = (standardTickets / totalSoldTickets) * 100;
    const kidTicketsPercentage = (kidTickets / totalSoldTickets) * 100;

    console.log(`Total tickets: ${totalSoldTickets}`);
    console.log(`${studentTicketsPercentage.toFixed(2)}% student tickets.`);
    console.log(`${standardTicketsPercentage.toFixed(2)}% standard tickets.`);
    console.log(`${kidTicketsPercentage.toFixed(2)}% kids tickets.`);
}
