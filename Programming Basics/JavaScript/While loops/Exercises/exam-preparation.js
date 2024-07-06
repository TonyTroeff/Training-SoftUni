function solve(input) {
    const maxBadGradesCount = Number(input[0]);

    let currentBadGradesCount = 0;
    let sum = 0;
    let count = 0;
    let lastProblem = "";
    let isSuccessful = true;

    let index = 1;
    while (input[index] != "Enough") {
        const grade = Number(input[index + 1]);

        sum += grade;
        count++;
        lastProblem = input[index];

        if (grade <= 4) {
            currentBadGradesCount++;
            if (currentBadGradesCount === maxBadGradesCount) {
                isSuccessful = false;
                break;
            }
        }

        // NOTE: The previous if statement is equivalent to:
        // if (grade <= 4 && ++currentBadGradesCount === maxBadGradesCount) {
        // 	  isSuccessful = false;
        //	  break;
        // }

        index += 2;
    }

    if (isSuccessful) {
        console.log(`Average score: ${(sum / count).toFixed(2)}`);
        console.log(`Number of problems: ${count}`);
        console.log(`Last problem: ${lastProblem}`);
    } else {
        console.log(`You need a break, ${currentBadGradesCount} poor grades.`);
    }
}
