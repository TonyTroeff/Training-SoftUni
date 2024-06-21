function solve(input) {
    const n = Number(input[0]);

    let p1 = 0;
    let p2 = 0;
    let p3 = 0;
    let p4 = 0;
    let p5 = 0;
    for (let i = 0; i < n; i++) {
        const currentNumber = Number(input[i + 1]);

        if (currentNumber < 200) p1++;
        else if (currentNumber < 400) p2++;
        else if (currentNumber < 600) p3++;
        else if (currentNumber < 800) p4++;
        else p5++;
    }

    console.log(`${((p1 * 100) / n).toFixed(2)}%`);
    console.log(`${((p2 * 100) / n).toFixed(2)}%`);
    console.log(`${((p3 * 100) / n).toFixed(2)}%`);
    console.log(`${((p4 * 100) / n).toFixed(2)}%`);
    console.log(`${((p5 * 100) / n).toFixed(2)}%`);
}
