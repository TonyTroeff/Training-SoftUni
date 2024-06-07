function solve(input) {
    const budget = Number(input[0]);
    const statistsCount = Number(input[1]);
    const clothingCostsPerStatist = Number(input[2]);

    const decorationCosts = 0.1 * budget; // 10% of the total budget

    let totalClothingCosts = statistsCount * clothingCostsPerStatist;
    if (statistsCount > 150) totalClothingCosts *= 0.9; // Apply 10% discount

    const totalCosts = decorationCosts + totalClothingCosts;

    if (budget >= totalCosts) {
        console.log("Action!");
        console.log(`Wingard starts filming with ${(budget - totalCosts).toFixed(2)} leva left.`);
    } else {
        console.log("Not enough money!");
        console.log(`Wingard needs ${(totalCosts - budget).toFixed(2)} leva more.`);
    }
}
