function solve(input) {
    const expected = "s3cr3t!P@ssw0rd";

    const password = input[0];

    if (password == expected) console.log("Welcome");
    else console.log("Wrong password!");
}
