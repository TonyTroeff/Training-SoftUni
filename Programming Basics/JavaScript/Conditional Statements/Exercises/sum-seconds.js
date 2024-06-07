function solve(input) {
    const firstTime = Number(input[0]);
    const secondTime = Number(input[1]);
    const thirdTime = Number(input[2]);

    const total = firstTime + secondTime + thirdTime;

    const seconds = total % 60;
    const minutes = (total - seconds) / 60;

    if (seconds < 10) console.log(`${minutes}:0${seconds}`);
    else console.log(`${minutes}:${seconds}`);
}
