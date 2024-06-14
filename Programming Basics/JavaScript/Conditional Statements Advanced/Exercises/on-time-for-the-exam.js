function solve(input) {
    const examHour = Number(input[0]);
    const examMinutes = Number(input[1]);
    const arrivalHour = Number(input[2]);
    const arrivalMinutes = Number(input[3]);

    const normalizedExamTime = examHour * 60 + examMinutes;
    const normalizedArrivalTime = arrivalHour * 60 + arrivalMinutes;

    // Positive values => we have arrived early
    // Negative values => we are late
    const diffInMinutes = normalizedExamTime - normalizedArrivalTime;

    if (diffInMinutes > 30) console.log("Early");
    else if (diffInMinutes >= 0) console.log("On time");
    else console.log("Late");

    if (diffInMinutes != 0) {
        const absoluteDiffInMinutes = Math.abs(diffInMinutes);
        let resultText = "";

        if (absoluteDiffInMinutes < 60) resultText += `${absoluteDiffInMinutes} minutes`;
        else {
            const formattedHours = Math.floor(absoluteDiffInMinutes / 60);
            const formattedMinutes = absoluteDiffInMinutes % 60;

            resultText += `${formattedHours}:${formattedMinutes.toString().padStart(2, "0")} hours`;
        }

        if (diffInMinutes > 0) resultText += " before";
        else resultText += " after";

        resultText += " the start";
        console.log(resultText);
    }
}
