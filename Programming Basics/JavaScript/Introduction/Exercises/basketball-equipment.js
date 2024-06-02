function solve(input) {
    const taxPerYear = Number(input[0]);

    const shoes = 0.6 * taxPerYear;
    const kit = 0.8 * shoes;
    const ball = 0.25 * kit;
    const accessories = 0.2 * ball;

    const total = taxPerYear + shoes + kit + ball + accessories;
    console.log(total);
}
