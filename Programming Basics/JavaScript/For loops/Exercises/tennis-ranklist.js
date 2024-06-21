function solve(input) {
    const n = Number(input[0]);
    const initialPoints = Number(input[1]);

    let newSeasonPoints = 0;
    let wonTournamentsCount = 0;
    for (let i = 0; i < n; i++) {
        const position = input[2 + i];

        if (position == "W") {
            newSeasonPoints += 2000;
            wonTournamentsCount++;
        } else if (position == "F") {
            newSeasonPoints += 1200;
        } else if (position == "SF") {
            newSeasonPoints += 720;
        }
    }

    console.log(`Final points: ${initialPoints + newSeasonPoints}`);
    console.log(`Average points: ${Math.floor(newSeasonPoints / n)}`);
    console.log(`${((wonTournamentsCount * 100) / n).toFixed(2)}%`);
}
