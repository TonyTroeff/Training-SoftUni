function solve(input) {
    const seriesName = input[0];
    const episodeDuration = Number(input[1]);
    const breakDuration = Number(input[2]);

    const requiredTime = episodeDuration + 0.125 * breakDuration + 0.25 * breakDuration;

    if (requiredTime <= breakDuration) console.log(`You have enough time to watch ${seriesName} and left with ${Math.ceil(breakDuration - requiredTime)} minutes free time.`);
    else console.log(`You don't have enough time to watch ${seriesName}, you need ${Math.ceil(requiredTime - breakDuration)} more minutes.`);
}
