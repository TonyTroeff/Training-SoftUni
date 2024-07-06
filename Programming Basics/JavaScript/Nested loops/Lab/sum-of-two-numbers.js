function solve(input) {
    const start = Number(input[0]);
    const end = Number(input[1]);
    const magicNumber = Number(input[2]);

    let isFound = false;
    let order = 0;
    for (let i = start; i <= end; i++) {
        for (let j = start; j <= end; j++) {
            order++;

            if (i + j == magicNumber) {
                console.log(`Combination N:${order} (${i} + ${j} = ${magicNumber})`);
                isFound = true;
                break;
            }
        }

        if (isFound) break;
    }

    if (!isFound) console.log(`${order} combinations - neither equals ${magicNumber}`);
}
