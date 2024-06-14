function solve(input) {
    const projectionType = input[0];
    const rows = Number(input[1]);
    const cols = Number(input[2]);

    const seats = rows * cols;

    let ticketPrice = 0;
    if (projectionType === "Premiere") ticketPrice = 12;
    else if (projectionType === "Normal") ticketPrice = 7.5;
    else if (projectionType === "Discount") ticketPrice = 5;

    const totalProfit = seats * ticketPrice;
    console.log(`${totalProfit.toFixed(2)} leva`);
}
