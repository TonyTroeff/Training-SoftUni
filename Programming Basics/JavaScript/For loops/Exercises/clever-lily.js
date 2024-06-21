function solve(input) {
    const age = Number(input[0]);
    const washingMachinePrice = Number(input[1]);
    const toyPrice = Number(input[2]);

    let savedMoney = 0;
    let moneyToReceive = 10;
    for (let i = 1; i <= age; i++) {
        if (i % 2 == 0) {
            savedMoney += moneyToReceive - 1;
            moneyToReceive += 10;
        } else {
            savedMoney += toyPrice;
        }
    }

    if (savedMoney >= washingMachinePrice) console.log(`Yes! ${(savedMoney - washingMachinePrice).toFixed(2)}`);
    else console.log(`No! ${(washingMachinePrice - savedMoney).toFixed(2)}`);
}
