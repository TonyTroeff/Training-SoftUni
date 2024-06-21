function solve(input) {
    const boundary = 1250.5;

    const actor = input[0];
    let points = Number(input[1]);
    const juriesCount = Number(input[2]);

    for (let i = 0; points <= boundary && i < juriesCount; i++) {
        const juryName = input[3 + i * 2];
        const juryPoints = Number(input[4 + i * 2]);

        points += (juryPoints * juryName.length) / 2;
    }

    if (points > boundary) console.log(`Congratulations, ${actor} got a nominee for leading role with ${points.toFixed(1)}!`);
    else console.log(`Sorry, ${actor} you need ${(boundary - points).toFixed(1)} more!`);
}
