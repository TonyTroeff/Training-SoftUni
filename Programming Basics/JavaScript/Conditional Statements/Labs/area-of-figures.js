function solve(input) {
    const figure = input[0];

    let area = 0;
    if (figure == "square") {
        const a = Number(input[1]);
        area = a * a;
    } else if (figure == "rectangle") {
        const a = Number(input[1]);
        const b = Number(input[2]);
        area = a * b;
    } else if (figure == "circle") {
        const r = Number(input[1]);
        area = Math.PI * r * r;
    } else if (figure == "triangle") {
        const a = Number(input[1]);
        const h = Number(input[2]);
        area = (a * h) / 2;
    }

    console.log(area);
}
