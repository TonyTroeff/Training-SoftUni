function solve(input) {
    const totalPages = new Number(input[0]);
    const readPagesPerHour = new Number(input[1]);
    const remainingDays = new Number(input[2]);

    const result = totalPages / readPagesPerHour / remainingDays;
    console.log(result);
}
