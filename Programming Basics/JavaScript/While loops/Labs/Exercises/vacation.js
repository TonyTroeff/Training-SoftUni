function solve(input) {
    const necessaryAmount = Number(input[0]);
    let savings = Number(input[1]);

    let consecutiveSpendOperations = 0;
    let totalDays = 0;
    let canSave = true;
    let index = 2;
    while (savings < necessaryAmount) {
        const operation = input[index];
        const operationAmount = Number(input[index + 1]);

        totalDays++;

        if (operation === "save") {
            consecutiveSpendOperations = 0;
            savings += operationAmount;
        } else if (operation === "spend") {
            if (++consecutiveSpendOperations == 5) {
                canSave = false;
                break;
            }

            savings = Math.max(savings - operationAmount, 0);
        }

        index += 2;
    }

    if (canSave) console.log(`You saved the money for ${totalDays} days.`);
    else {
        console.log("You can't save the money.");
        console.log(totalDays);
    }
}
