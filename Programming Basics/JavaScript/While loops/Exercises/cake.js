function solve(input) {
    const width = Number(input[0]);
    const length = Number(input[1]);

    let totalPieces = width * length;
    let cakeIsEnough = true;
    let index = 2;

    while (input[index] != "STOP") {
        const piecesToTake = Number(input[index]);
        totalPieces -= piecesToTake;

        if (totalPieces < 0) {
            cakeIsEnough = false;
            break;
        }

        index++;
    }

    if (cakeIsEnough) console.log(`${totalPieces} pieces are left.`);
    else console.log(`No more cake left! You need ${Math.abs(totalPieces)} pieces more.`);
}
