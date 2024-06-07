function solve(input) {
    const tripPrice = Number(input[0]);
    const puzzles = Number(input[1]);
    const dolls = Number(input[2]);
    const bears = Number(input[3]);
    const minions = Number(input[4]);
    const trucks = Number(input[5]);

    let profit = puzzles * 2.6 + dolls * 3 + bears * 4.1 + minions * 8.2 + trucks * 2;
    if (puzzles + dolls + bears + minions + trucks >= 50) profit *= 0.75; // Make discount of 25%

    profit *= 0.9; // Pay rent (10% of the profit)

    if (profit >= tripPrice) console.log(`Yes! ${(profit - tripPrice).toFixed(2)} lv left.`);
    else console.log(`Not enough money! ${(tripPrice - profit).toFixed(2)} lv needed.`);
}
