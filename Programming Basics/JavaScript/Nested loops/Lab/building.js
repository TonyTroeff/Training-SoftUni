function solve(input) {
    const floors = Number(input[0]);
    const rooms = Number(input[1]);

    for (let i = floors; i > 0; i--) {
        let letter;
        if (i == floors) letter = "L";
        else if (i % 2 == 0) letter = "O";
        else letter = "A";

        let result = "";
        for (let j = 0; j < rooms; j++) {
            if (j > 0) result += " ";
            result += `${letter}${i}${j}`;
        }

        console.log(result);
    }
}
