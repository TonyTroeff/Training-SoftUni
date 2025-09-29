function solve(year) {
    const isLeap = year % 400 === 0 || (year % 4 === 0 && year % 100 !== 0);
    console.log(isLeap ? "yes" : "no");
}

solve(1984);
solve(2003);
solve(4);
