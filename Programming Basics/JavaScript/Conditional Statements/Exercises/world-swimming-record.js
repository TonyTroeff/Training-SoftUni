function solve(input) {
    const existingRecordInSeconds = Number(input[0]);
    const distanceInMeters = Number(input[1]);
    const timePerMeter = Number(input[2]);

    const totalTimeInSeconds = distanceInMeters * timePerMeter + Math.floor(distanceInMeters / 15) * 12.5;
    if (totalTimeInSeconds < existingRecordInSeconds) console.log(`Yes, he succeeded! The new world record is ${totalTimeInSeconds.toFixed(2)} seconds.`);
    else console.log(`No, he failed! He was ${(totalTimeInSeconds - existingRecordInSeconds).toFixed(2)} seconds slower.`);
}