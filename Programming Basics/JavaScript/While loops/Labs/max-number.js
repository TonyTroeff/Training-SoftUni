function solve(input) {
    let max = Number.NEGATIVE_INFINITY;

    let index = 0;
    while (input[index] != "Stop") {
        const currentNumber = Number(input[index]);
        if (currentNumber > max) max = currentNumber;

        index++;
    }

    console.log(max);
}
