function solve(input) {
    const length = Number(input[0]);
    const width = Number(input[1]);
    const height = Number(input[2]);
    const percentage = Number(input[3]);

    const volume = 0.001 * length * width * height;
    const freeSpace = volume * (1 - 0.01 * percentage);
    console.log(freeSpace);
}
