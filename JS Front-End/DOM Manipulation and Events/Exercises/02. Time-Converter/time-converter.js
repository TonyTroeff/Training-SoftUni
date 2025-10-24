document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const daysForm = document.querySelector("form#days");
    const hoursForm = document.querySelector("form#hours");
    const minutesForm = document.querySelector("form#minutes");
    const secondsForm = document.querySelector("form#seconds");

    const daysInput = daysForm.querySelector("input#days-input");
    const hoursInput = hoursForm.querySelector("input#hours-input");
    const minutesInput = minutesForm.querySelector("input#minutes-input");
    const secondsInput = secondsForm.querySelector("input#seconds-input");

    const units = {
        days: 86400,
        hours: 3600,
        minutes: 60,
        seconds: 1,
    };

    function setValues(seconds) {
        daysInput.value = (seconds / units.days).toFixed(2);
        hoursInput.value = (seconds / units.hours).toFixed(2);
        minutesInput.value = (seconds / units.minutes).toFixed(2);
        secondsInput.value = seconds.toFixed(2);
    }

    daysForm.addEventListener("submit", function (event) {
        event.preventDefault();
        setValues(Number(daysInput.value) * units.days);
    });
    hoursForm.addEventListener("submit", function (event) {
        event.preventDefault();
        setValues(Number(hoursInput.value) * units.hours);
    });
    minutesForm.addEventListener("submit", function (event) {
        event.preventDefault();
        setValues(Number(minutesInput.value) * units.minutes);
    });
    secondsForm.addEventListener("submit", function (event) {
        event.preventDefault();
        setValues(Number(secondsInput.value) * units.seconds);
    });
}
