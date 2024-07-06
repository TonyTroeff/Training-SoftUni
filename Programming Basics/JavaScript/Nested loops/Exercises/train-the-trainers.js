function solve(input) {
    const juries = Number(input[0]);

    let presentationScoresSum = 0;
    let presentationsCount = 0;

    let index = 1;
    let presentationName = input[index];
    while (presentationName !== "Finish") {
        let currentScoresSum = 0;
        for (let i = 0; i < juries; i++) {
            const juryScore = Number(input[++index]);
            currentScoresSum += juryScore;
        }

        const grade = currentScoresSum / juries;
        console.log(`${presentationName} - ${grade.toFixed(2)}.`);

        presentationScoresSum += grade;
        presentationsCount++;

        presentationName = input[++index];
    }

    const averageAssessment = presentationScoresSum / presentationsCount;
    console.log(`Student's final assessment is ${averageAssessment.toFixed(2)}.`);
}
