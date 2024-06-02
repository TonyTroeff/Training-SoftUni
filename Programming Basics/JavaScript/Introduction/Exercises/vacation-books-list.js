function solve(input) {
    const totalPages = Number(input[0]);
    const readPagesPerHour = Number(input[1]);
    const remainingDays = Number(input[2]);

    const result = totalPages / readPagesPerHour / remainingDays;
    console.log(result);
}
