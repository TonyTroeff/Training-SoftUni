function attachEvents() {
    const phonebookList = document.querySelector("ul#phonebook");
    const personInput = document.querySelector("input#person");
    const phoneInput = document.querySelector("input#phone");
    const loadButton = document.querySelector("button#btnLoad");
    const createButton = document.querySelector("button#btnCreate");

    async function loadPhonebook() {
        const getEntriesResponse = await fetch("http://localhost:3030/jsonstore/phonebook");
        const allEntries = await getEntriesResponse.json();

        phonebookList.innerHTML = "";
        for (const entry of Object.values(allEntries)) {
            const li = document.createElement("li");
            li.textContent = `${entry.person}: ${entry.phone}`;

            const deleteButton = document.createElement("button");
            deleteButton.textContent = "Delete";
            deleteButton.addEventListener("click", async () => {
                await fetch(`http://localhost:3030/jsonstore/phonebook/${entry._id}`, { method: "DELETE" });
                li.remove();
            });

            li.appendChild(deleteButton);
            phonebookList.appendChild(li);
        }
    }

    loadButton.addEventListener("click", loadPhonebook);

    createButton.addEventListener("click", async () => {
        const person = personInput.value;
        const phone = phoneInput.value;
        const entry = { person, phone };

        await fetch("http://localhost:3030/jsonstore/phonebook", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(entry),
        });

        personInput.value = "";
        phoneInput.value = "";

        await loadPhonebook();
    });
}

attachEvents();
