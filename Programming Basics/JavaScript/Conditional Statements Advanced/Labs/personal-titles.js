function solve(input) {
    const age = Number(input[0]);
    const gender = input[1];

    let title = "";
    if (gender === "m") {
        if (age < 16) title = "Master";
        else title = "Mr.";
    } else if (gender === "f") {
        if (age < 16) title = "Miss";
        else title = "Ms.";
    }

    console.log(title);
}
