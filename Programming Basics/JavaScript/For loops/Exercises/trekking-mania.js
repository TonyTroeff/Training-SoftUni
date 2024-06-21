function solve(input) {
    const n = Number(input[0]);

    let climbersCount = 0;
    let p1 = 0;
    let p2 = 0;
    let p3 = 0;
    let p4 = 0;
    let p5 = 0;
    for (let i = 0; i < n; i++) {
        const peopleCount = Number(input[i + 1]);

        if (peopleCount <= 5) p1 += peopleCount;
        else if (peopleCount <= 12) p2 += peopleCount;
        else if (peopleCount <= 25) p3 += peopleCount;
        else if (peopleCount <= 40) p4 += peopleCount;
        else p5 += peopleCount;

        climbersCount += peopleCount;
    }

    console.log(`${((p1 * 100) / climbersCount).toFixed(2)}%`);
    console.log(`${((p2 * 100) / climbersCount).toFixed(2)}%`);
    console.log(`${((p3 * 100) / climbersCount).toFixed(2)}%`);
    console.log(`${((p4 * 100) / climbersCount).toFixed(2)}%`);
    console.log(`${((p5 * 100) / climbersCount).toFixed(2)}%`);
}
