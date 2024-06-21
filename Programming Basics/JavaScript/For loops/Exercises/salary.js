function solve(input) {
    const n = Number(input[0]);
    let salary = Number(input[1]);

    for (let i = 0; i < n && salary > 0; i++) {
        const tab = input[2 + i];

        if (tab == "Facebook") salary -= 150;
        else if (tab == "Instagram") salary -= 100;
        else if (tab == "Reddit") salary -= 50;
    }

    if (salary > 0) console.log(salary);
    else console.log("You have lost your salary.");
}
