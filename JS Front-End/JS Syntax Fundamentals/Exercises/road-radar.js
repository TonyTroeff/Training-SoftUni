function solve(speed, area) {
    let limit = 0;
    if (area === "motorway") limit = 130;
    else if (area === "interstate") limit = 90;
    else if (area === "city") limit = 50;
    else if (area === "residential") limit = 20;

    if (speed <= limit) {
        console.log(`Driving ${speed} km/h in a ${limit} zone`);
    } else {
        const difference = speed - limit;

        let status;
        if (difference <= 20) status = "speeding";
        else if (difference <= 40) status = "excessive speeding";
        else status = "reckless driving";

        console.log(`The speed is ${difference} km/h faster than the allowed speed of ${limit} - ${status}`);
    }
}

solve(40, "city");
solve(21, "residential");
solve(120, "interstate");
solve(200, "motorway");
