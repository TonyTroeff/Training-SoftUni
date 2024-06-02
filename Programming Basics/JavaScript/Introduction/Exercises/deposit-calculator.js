function solve(input) {
    const depositSum = Number(input[0]);
    const depositLength = Number(input[1]);
    const yearlyRate = Number(input[2]);

    const result = depositSum + depositLength * ((depositSum * yearlyRate * 0.01) / 12);
    console.log(result);
}
