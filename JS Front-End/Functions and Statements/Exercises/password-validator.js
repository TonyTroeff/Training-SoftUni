function solve(password) {
    const errors = [];
    if (password.length < 6 || password.length > 10) errors.push("Password must be between 6 and 10 characters");

    let letters = 0,
        digits = 0;
    for (const ch of password) {
        if ((ch >= "A" && ch <= "Z") || (ch >= "a" && ch <= "z")) letters++;
        else if (ch >= "0" && ch <= "9") digits++;
    }

    if (letters + digits !== password.length) errors.push("Password must consist only of letters and digits");
    if (digits < 2) errors.push("Password must have at least 2 digits");

    if (errors.length === 0) console.log("Password is valid");
    else {
        for (const error of errors) console.log(error);
    }
}

solve("logIn");
solve("MyPass123");
solve("Pa$s$s");
