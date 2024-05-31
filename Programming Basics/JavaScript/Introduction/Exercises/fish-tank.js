function solve(input) {
    const length = new Number(input[0]);
    const width = new Number(input[1]);
    const height = new Number(input[2]);
    const percentage = new Number(input[3]);

    const volume = 0.001 * length * width * height;
    const freeSpace = volume * (1 - 0.01 * percentage);
    console.log(freeSpace);
}
