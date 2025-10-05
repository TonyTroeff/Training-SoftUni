function solve(names) {
    const employees = names.map((x) => ({ name: x, personalNumber: x.length }));
    for (const employee of employees) console.log(`Name: ${employee.name} -- Personal Number: ${employee.personalNumber}`);
}

solve(["Silas Butler", "Adnaan Buckley", "Juan Peterson", "Brendan Villarreal"]);
solve(["Samuel Jackson", "Will Smith", "Bruce Willis", "Tom Holland"]);
