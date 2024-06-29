function solve(input) {
    let amount = 0;

    let index = 0;
    while (input[index] != "NoMoreMoney") {
        const operation = Number(input[index]);
        if (operation < 0) {
            console.log("Invalid operation!");
            break;
        }

        console.log(`Increase: ${operation.toFixed(2)}`);
        amount += operation;
        index++;
    }

    console.log(`Total: ${amount.toFixed(2)}`);
}
