function solve(base, increment) {
    let stone = 0;
    let marble = 0;
    let lapis = 0;
    let gold = 0;

    let layer = 1;
    while (base > 0) {
        if (base <= 2) gold += base * base;
        else {
            const inner = Math.pow(base - 2, 2);
            const outer = 4 * (base - 1);

            stone += inner;
            if (layer % 5 === 0) lapis += outer;
            else marble += outer;
        }

        base -= 2;
        layer++;
    }

    console.log(`Stone required: ${Math.ceil(stone * increment)}`);
    console.log(`Marble required: ${Math.ceil(marble * increment)}`);
    console.log(`Lapis Lazuli required: ${Math.ceil(lapis * increment)}`);
    console.log(`Gold required: ${Math.ceil(gold * increment)}`);
    console.log(`Final pyramid height: ${Math.floor((layer - 1) * increment)}`);
}

solve(11, 1);
solve(11, 0.75);
solve(12, 1);
solve(23, 0.5);
