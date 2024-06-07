function solve(input) {
    const budget = Number(input[0]);
    const gpuCount = Number(input[1]);
    const cpuCount = Number(input[2]);
    const ramCount = Number(input[3]);

    const gpuCosts = gpuCount * 250;

    const cpuPrice = 0.35 * gpuCosts;
    const cpuCosts = cpuCount * cpuPrice;

    const ramPrice = 0.1 * gpuCosts;
    const ramCosts = ramCount * ramPrice;

    let totalCosts = gpuCosts + cpuCosts + ramCosts;
    if (gpuCount > cpuCount) totalCosts *= 0.85; // Apply 15% discount

    if (budget >= totalCosts) console.log(`You have ${(budget - totalCosts).toFixed(2)} leva left!`);
    else console.log(`Not enough money! You need ${(totalCosts - budget).toFixed(2)} leva more!`);
}
