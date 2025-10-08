function solve(startingYield) {
    const days = startingYield >= 100 ? Math.floor((startingYield - 100) / 10) + 1 : 0;
    const spice = Math.max(days * startingYield - 5 * days * (days - 1) - 26 * (days + 1), 0);

    console.log(days);
    console.log(spice);
}

solve(111);
solve(450);
