function solve(input) {
    let index = 0;
    let destination = input[index];
    while (destination != "End") {
        const minBudget = Number(input[++index]);

        let savedMoney = 0;
        while (savedMoney < minBudget) savedMoney += Number(input[++index]);

        console.log(`Going to ${destination}!`);
        destination = input[++index];
    }
}
