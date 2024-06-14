function solve(input) {
    const budget = Number(input[0]);
    const season = input[1];
    const fishermenCount = Number(input[2]);

    let rentPrice = 0;
    if (season === "Spring") rentPrice = 3000;
    else if (season === "Summer" || season === "Autumn") rentPrice = 4200;
    else if (season === "Winter") rentPrice = 2600;

    let discountMultiplier = 0;
    if (fishermenCount <= 6) discountMultiplier = 0.9;
    else if (fishermenCount <= 11) discountMultiplier = 0.85;
    else discountMultiplier = 0.75;

    let totalCosts = rentPrice * discountMultiplier;
    if (fishermenCount % 2 === 0 && season != "Autumn") totalCosts *= 0.95;

    if (budget >= totalCosts) console.log(`Yes! You have ${(budget - totalCosts).toFixed(2)} leva left.`);
    else console.log(`Not enough money! You need ${(totalCosts - budget).toFixed(2)} leva.`);
}
