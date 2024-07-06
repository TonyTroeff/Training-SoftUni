function solve(input) {
    const n = Number(input[0]);

    let row = 1;
    let current = 1;

    while (current <= n) {
        let result = "";
        for (let i = 0; i < row; i++) {
            if (i > 0) result += " ";

            result += `${current}`;
            if (++current > n) break;
        }

        row++;
        console.log(result);
    }
}
