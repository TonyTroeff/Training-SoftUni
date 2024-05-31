function solve(input) {
    const depositSum = new Number(input[0]);
    const depositLength = new Number(input[1]);
    const yearlyRate = new Number(input[2]);

    const result = depositSum + depositLength * ((depositSum * yearlyRate * 0.01) / 12);
    console.log(result);
}
