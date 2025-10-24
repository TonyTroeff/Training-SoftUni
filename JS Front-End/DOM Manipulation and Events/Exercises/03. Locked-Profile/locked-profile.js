document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const profileDivs = Array.from(document.querySelectorAll("div.profile"));

    for (let i = 0; i < profileDivs.length; i++) {
        const button = profileDivs[i].querySelector("button");
        const hiddenFieldsDiv = profileDivs[i].querySelector(`div#user${i + 1}HiddenFields`);

        button.addEventListener("click", () => {
            const isLocked = profileDivs[i].querySelector(`input#user${i + 1}Lock`).checked;
            if (isLocked) return;

            if (hiddenFieldsDiv.classList.contains("hidden-fields")) {
                hiddenFieldsDiv.classList.remove("hidden-fields");
                button.textContent = "Show less";
            } else {
                hiddenFieldsDiv.classList.add("hidden-fields");
                button.textContent = "Show more";
            }
        });
    }
}
