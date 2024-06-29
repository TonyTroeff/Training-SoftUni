function solve(input) {
    const username = input[0];
    const password = input[1];

    let index = 2;
    while (input[index] !== password) index++;

    console.log(`Welcome ${username}!`);
}
