function solve(input) {
    const flowerType = input[0];
    const flowersCount = Number(input[1]);
    const budget = Number(input[2]);

    let costs = 0;
    if (flowerType === "Roses") {
        costs = flowersCount * 5;
        if (flowersCount > 80) costs *= 0.9;
    } else if (flowerType === "Dahlias") {
        costs = flowersCount * 3.8;
        if (flowersCount > 90) costs *= 0.85;
    } else if (flowerType === "Tulips") {
        costs = flowersCount * 2.8;
        if (flowersCount > 80) costs *= 0.85;
    } else if (flowerType === "Narcissus") {
        costs = flowersCount * 3;
        if (flowersCount < 120) costs *= 1.15;
    } else if (flowerType === "Gladiolus") {
        costs = flowersCount * 2.5;
        if (flowersCount < 80) costs *= 1.2;
    }

    if (costs <= budget) console.log(`Hey, you have a great garden with ${flowersCount} ${flowerType} and ${(budget - costs).toFixed(2)} leva left.`);
    else console.log(`Not enough money, you need ${(costs - budget).toFixed(2)} leva more.`);
}
