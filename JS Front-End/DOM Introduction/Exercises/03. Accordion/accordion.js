function toggle() {
    const button = document.querySelector("div#accordion > div.head > span.button");

    if (button.textContent === "More") {
        button.textContent = "Less";
        document.getElementById("extra").style.display = "block";
    } else if (button.textContent === "Less") {
        button.textContent = "More";
        document.getElementById("extra").style.display = "none";
    }
}
