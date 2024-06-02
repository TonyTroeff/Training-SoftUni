function solve(input) {
    const pens = Number(input[0]);
    const markers = Number(input[1]);
    const diluent = Number(input[2]);
    const discountPercentage = Number(input[3]);

    const total = (pens * 5.8 + markers * 7.2 + diluent * 1.2) * (1 - discountPercentage * 0.01);
    console.log(total);
}
