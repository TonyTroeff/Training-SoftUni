function solve(input) {
    const studentName = input[0];

    let grade = 0;
    let fails = 0;
    let sum = 0;
    let index = 1;

    while (grade < 12) {
        const marks = Number(input[index]);
        if (marks < 4) {
            if (fails == 1) break;
            fails++;
        } else {
            sum += marks;
            grade++;
            fails = 0;
        }

        index++;
    }

    if (grade == 12) console.log(`${studentName} graduated. Average grade: ${(sum / 12).toFixed(2)}`);
    else console.log(`${studentName} has been excluded at ${grade + 1} grade`);
}
