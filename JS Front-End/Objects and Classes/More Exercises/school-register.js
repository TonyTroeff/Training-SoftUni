function solve(input) {
    const register = {};

    for (const line of input) {
        const [nameInfo, gradeInfo, scoreInfo] = line.split(", ");
        const name = nameInfo.split(": ")[1];
        const grade = Number(gradeInfo.split(": ")[1]);
        const score = Number(scoreInfo.split(": ")[1]);

        if (score < 3) continue;

        const nextGrade = grade + 1;

        if (!register.hasOwnProperty(nextGrade)) register[nextGrade] = { students: [], totalScore: 0 };

        register[nextGrade].students.push(name);
        register[nextGrade].totalScore += score;
    }

    for (const [grade, data] of Object.entries(register).sort((a, b) => a[0] - b[0])) {
        console.log(`${grade} Grade`);
        console.log(`List of students: ${data.students.join(", ")}`);
        console.log(`Average annual score from last year: ${(data.totalScore / data.students.length).toFixed(2)}`);
        console.log();
    }
}

solve([
    "Student name: Mark, Grade: 8, Graduated with an average score: 4.75",
    "Student name: Ethan, Grade: 9, Graduated with an average score: 5.66",
    "Student name: George, Grade: 8, Graduated with an average score: 2.83",
    "Student name: Steven, Grade: 10, Graduated with an average score: 4.20",
    "Student name: Joey, Grade: 9, Graduated with an average score: 4.90",
    "Student name: Angus, Grade: 11, Graduated with an average score: 2.90",
    "Student name: Bob, Grade: 11, Graduated with an average score: 5.15",
    "Student name: Daryl, Grade: 8, Graduated with an average score: 5.95",
    "Student name: Bill, Grade: 9, Graduated with an average score: 6.00",
    "Student name: Philip, Grade: 10, Graduated with an average score: 5.05",
    "Student name: Peter, Grade: 11, Graduated with an average score: 4.88",
    "Student name: Gavin, Grade: 10, Graduated with an average score: 4.00",
]);
solve([
    "Student name: George, Grade: 5, Graduated with an average score: 2.75",
    "Student name: Alex, Grade: 9, Graduated with an average score: 3.66",
    "Student name: Peter, Grade: 8, Graduated with an average score: 2.83",
    "Student name: Boby, Grade: 5, Graduated with an average score: 4.20",
    "Student name: John, Grade: 9, Graduated with an average score: 2.90",
    "Student name: Steven, Grade: 2, Graduated with an average score: 4.90",
    "Student name: Darsy, Grade: 1, Graduated with an average score: 5.15",
]);
