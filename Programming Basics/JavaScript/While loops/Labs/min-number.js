function solve(input) {
    let min = Number.POSITIVE_INFINITY;

    let index = 0;
    while (input[index] != "Stop") {
        const currentNumber = Number(input[index]);
        if (currentNumber < min) min = currentNumber;

        index++;
    }

    console.log(min);
}
