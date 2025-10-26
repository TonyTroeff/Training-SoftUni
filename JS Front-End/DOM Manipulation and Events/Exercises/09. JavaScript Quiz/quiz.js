document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const answers = ["onclick", "JSON.stringify()", "A programming API for HTML and XML documents"];
    const sections = Array.from(document.querySelectorAll("section.question"));
    let index = 0;
    let correct = 0;

    const mainContainer = document.querySelector("main");
    const outputContainer = document.querySelector("div#results");

    mainContainer.addEventListener("click", (e) => {
        if (e.target.nodeName !== "LI" || !e.target.classList.contains("quiz-answer")) return;

        sections[index].classList.add("hidden");
        if (e.target.textContent === answers[index]) correct++;

        index++;

        if (index === sections.length) {
            if (correct === answers.length) outputContainer.textContent = "You are recognized as top JavaScript fan!";
            else if (correct === 1) outputContainer.textContent = "You have 1 right answer";
            else outputContainer.textContent = `You have ${correct} right answers`;
        } else {
            sections[index].classList.remove("hidden");
        }
    });
}
