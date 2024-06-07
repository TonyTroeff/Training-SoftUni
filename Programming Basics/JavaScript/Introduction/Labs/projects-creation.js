function solve(input) {
    const architectName = input[0];
    const projectsCount = Number(input[1]);

    const requiredHours = projectsCount * 3;
    console.log(`The architect ${architectName} will need ${requiredHours} hours to complete ${projectsCount} project/s.`);
}
