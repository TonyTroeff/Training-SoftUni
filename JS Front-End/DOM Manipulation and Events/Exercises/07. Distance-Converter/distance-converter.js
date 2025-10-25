document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const units = { km: 1000, m: 1, cm: 0.01, mm: 0.001, mi: 1609.34, yrd: 0.9144, ft: 0.3048, in: 0.0254 };

    const distanceInput = document.querySelector("input#inputDistance");
    const unitsInput = document.querySelector("select#inputUnits");
    const outputUnits = document.querySelector("select#outputUnits");
    const output = document.querySelector("input#outputDistance");
    const convertButton = document.querySelector("input#convert");

    convertButton.addEventListener("click", () => {
        const distance = Number(distanceInput.value);
        const fromUnit = unitsInput.value;
        const toUnit = outputUnits.value;

        const distanceInMeters = distance * units[fromUnit];
        const convertedDistance = distanceInMeters / units[toUnit];
        output.value = convertedDistance;
    });
}
