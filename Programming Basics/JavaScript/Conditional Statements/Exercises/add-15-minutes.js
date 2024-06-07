function solve(input) {
    let hour = Number(input[0]);
    let minutes = Number(input[1]);

    minutes += 15;
    if (minutes >= 60) {
        minutes -= 60;

        hour += 1;
        if (hour == 24) hour = 0;
    }

    if (minutes < 10) console.log(`${hour}:0${minutes}`);
    else console.log(`${hour}:${minutes}`);
}
