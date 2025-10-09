function solve(length) {
    let index = 0;
    const sequence = "ATCGTTAGGG";

    function getNext() {
        const symbol = sequence[index];
        index = (index + 1) % sequence.length;
        return symbol;
    }

    function createRow(period) {
        return `${"*".repeat(period)}${getNext()}${"-".repeat(2 * (2 - period))}${getNext()}${"*".repeat(period)}`;
    }

    for (let i = 0; i < length; i++) {
        const period = Math.abs(2 - (i % 4));
        const currentRow = createRow(period);
        console.log(currentRow);
    }
}

solve(4);
solve(10);
