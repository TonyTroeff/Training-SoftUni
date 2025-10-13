function solve(input) {
    let flightsInfo = {};
    for (let info of input[0]) {
        let [flight, ...destination] = info.split(" ");
        flightsInfo[flight] = { Destination: destination.join(" "), Status: "Ready to fly" };
    }

    for (let info of input[1]) {
        let [flight, status] = info.split(" ");
        if (flightsInfo.hasOwnProperty(flight)) flightsInfo[flight].Status = status;
    }

    let requestedStatus = input[2][0];
    for (let flight of Object.values(flightsInfo)) {
        if (flight.Status === requestedStatus) console.log(flight);
    }
}

solve([["WN269 Delaware", "FL2269 Oregon", "WN498 Las Vegas", "WN3145 Ohio", "WN612 Alabama", "WN4010 New York", "WN1173 California", "DL2120 Texas", "KL5744 Illinois", "WN678 Pennsylvania"], ["DL2120 Cancelled", "WN612 Cancelled", "WN1173 Cancelled", "SK430 Cancelled"], ["Cancelled"]]);
solve([["WN269 Delaware", "FL2269 Oregon", "WN498 Las Vegas", "WN3145 Ohio", "WN612 Alabama", "WN4010 New York", "WN1173 California", "DL2120 Texas", "KL5744 Illinois", "WN678 Pennsylvania"], ["DL2120 Cancelled", "WN612 Cancelled", "WN1173 Cancelled", "SK330 Cancelled"], ["Ready to fly"]]);
