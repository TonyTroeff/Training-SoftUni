function solve(input) {
    const patterns = [/(?<course>.+): (?<capacity>\d+)/, /(?<username>.+)\[(?<credits>\d+)\] with email (?<email>.+) joins (?<course>.+)/];

    const courses = {};
    for (const element of input) {
        if (patterns[0].test(element)) {
            const match = element.match(patterns[0]);
            const course = match.groups.course;
            const capacity = Number(match.groups.capacity);

            if (!courses.hasOwnProperty(course)) courses[course] = { capacity: 0, students: [] };
            courses[course].capacity += capacity;
        } else if (patterns[1].test(element)) {
            const match = element.match(patterns[1]);
            const username = match.groups.username;
            const credits = Number(match.groups.credits);
            const email = match.groups.email;
            const courseName = match.groups.course;

            if (courseName in courses) {
                const course = courses[courseName];
                if (course.capacity > course.students.length) course.students.push({ username, credits, email });
            }
        }
    }

    for (const [name, course] of Object.entries(courses).sort((a, b) => b[1].students.length - a[1].students.length)) {
        console.log(`${name}: ${course.capacity - course.students.length} places left`);
        for (const student of course.students.sort((a, b) => b.credits - a.credits)) {
            console.log(`--- ${student.credits}: ${student.username}, ${student.email}`);
        }
    }
}

solve([
    "JavaBasics: 2",
    "user1[25] with email user1@user.com joins C#Basics",
    "C#Advanced: 3",
    "JSCore: 4",
    "user2[30] with email user2@user.com joins C#Basics",
    "user13[50] with email user13@user.com joins JSCore",
    "user1[25] with email user1@user.com joins JSCore",
    "user8[18] with email user8@user.com joins C#Advanced",
    "user6[85] with email user6@user.com joins JSCore",
    "JSCore: 2",
    "user11[3] with email user11@user.com joins JavaBasics",
    "user45[105] with email user45@user.com joins JSCore",
    "user007[20] with email user007@user.com joins JSCore",
    "user700[29] with email user700@user.com joins JSCore",
    "user900[88] with email user900@user.com joins JSCore",
]);
solve(["JavaBasics: 15", "user1[26] with email user1@user.com joins JavaBasics", "user2[36] with email user11@user.com joins JavaBasics", "JavaBasics: 5", "C#Advanced: 5", "user1[26] with email user1@user.com joins C#Advanced", "user2[36] with email user11@user.com joins C#Advanced", "user3[6] with email user3@user.com joins C#Advanced", "C#Advanced: 1", "JSCore: 8", "user23[62] with email user23@user.com joins JSCore"]);
