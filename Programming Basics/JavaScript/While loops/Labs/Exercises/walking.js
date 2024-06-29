function solve(input) {
    const goalLimit = 10000;

    let totalSteps = 0;
    let shouldContinue = true;
    let goalIsReached = false;
    let index = 0;

    while (shouldContinue && !goalIsReached) {
        let stepsInput = input[index];

        if (stepsInput === "Going home") {
            index++;
            shouldContinue = false;
        }

        let currentSteps = Number(input[index]);

        totalSteps += currentSteps;
        if (totalSteps >= goalLimit) goalIsReached = true;

        index++;
    }

    if (goalIsReached) {
        console.log("Goal reached! Good job!");
        console.log(`${totalSteps - goalLimit} steps over the goal!`);
    } else console.log(`${goalLimit - totalSteps} more steps to reach goal.`);
}
