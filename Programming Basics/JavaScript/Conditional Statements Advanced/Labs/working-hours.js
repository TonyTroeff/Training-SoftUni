function solve(input) {
    const hour = Number(input[0]);
    const day = input[1];

    const isWorkingDay = day === "Monday" || day === "Tuesday" || day === "Wednesday" || day === "Thursday" || day === "Friday" || day === "Saturday";
    const isWorkingHour = 10 <= hour && hour <= 18;

    if (isWorkingDay && isWorkingHour) console.log("open");
    else console.log("closed");
}
