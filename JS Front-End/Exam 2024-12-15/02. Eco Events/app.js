window.addEventListener("load", solve);

function solve() {
    const form = document.querySelector("form.registerEvent");
    const emailInput = form.querySelector("input#email");
    const eventInput = form.querySelector("input#event");
    const locationInput = form.querySelector("input#location");
    const nextButton = form.querySelector("button#next-btn");

    const previewList = document.querySelector("ul#preview-list");
    const finalList = document.querySelector("ul#event-list");

    nextButton.addEventListener("click", () => {
        if (!emailInput.value || !eventInput.value || !locationInput.value) return;

        // NOTE: We need to store the information in a variable so we can easily reuse it later.
        const data = { email: emailInput.value, event: eventInput.value, location: locationInput.value };

        const emailHeader = document.createElement("h4");
        emailHeader.textContent = data.email;

        const eventParagraph = createEventDetails("Event:", data.event);
        const locationParagraph = createEventDetails("Location:", data.location);

        const article = document.createElement("article");
        article.appendChild(emailHeader);
        article.appendChild(eventParagraph);
        article.appendChild(locationParagraph);

        const editButton = createActionButton("edit");
        const applyButton = createActionButton("apply");

        const listItem = document.createElement("li");
        listItem.className = "application";
        listItem.appendChild(article);
        listItem.appendChild(editButton);
        listItem.appendChild(applyButton);

        editButton.addEventListener("click", () => {
            listItem.remove();

            emailInput.value = data.email;
            eventInput.value = data.event;
            locationInput.value = data.location;
        });

        applyButton.addEventListener("click", () => {
            listItem.remove();

            editButton.remove();
            applyButton.remove();
            finalList.appendChild(listItem);
        });

        previewList.appendChild(listItem);

        emailInput.value = "";
        eventInput.value = "";
        locationInput.value = "";
    });

    function createEventDetails(name, value) {
        const strong = document.createElement("strong");
        strong.textContent = name;

        const newLine = document.createElement("br");

        const paragraph = document.createElement("p");
        paragraph.appendChild(strong);
        paragraph.appendChild(newLine);

        paragraph.appendChild(document.createTextNode(value));
        // paragraph.innerHTML += value;

        return paragraph;
    }

    function createActionButton(action) {
        const button = document.createElement("button");
        button.classList.add("action-btn", action);
        button.textContent = action;

        return button;
    }
}
