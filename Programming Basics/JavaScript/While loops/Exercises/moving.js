function solve(input) {
    const width = Number(input[0]);
    const height = Number(input[1]);
    const depth = Number(input[2]);

    let freeSpace = width * height * depth;
    let spaceIsEnough = true;
    let index = 3;

    while (input[index] != "Done") {
        const spaceToTake = Number(input[index]);
        freeSpace -= spaceToTake;

        if (freeSpace < 0) {
            spaceIsEnough = false;
            break;
        }

        index++;
    }

    if (spaceIsEnough) console.log(`${freeSpace} Cubic meters left.`);
    else console.log(`No more free space! You need ${freeSpace * -1} Cubic meters more.`);
}
