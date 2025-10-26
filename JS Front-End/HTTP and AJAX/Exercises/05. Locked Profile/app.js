async function lockedProfile() {
    const mainContainer = document.getElementById("main");

    const getProfilesResponse = await fetch("http://localhost:3030/jsonstore/advanced/profiles");
    const allProfiles = await getProfilesResponse.json();

    let index = 0;
    for (const profile of Object.values(allProfiles)) {
        index++;

        const profileDiv = document.createElement("div");
        profileDiv.classList.add("profile");

        const profileImg = document.createElement("img");
        profileImg.src = "./icon-profile.png";
        profileImg.classList.add("userIcon");
        profileDiv.appendChild(profileImg);

        const lockedLabel = document.createElement("label");
        lockedLabel.textContent = "Lock";
        profileDiv.appendChild(lockedLabel);

        const lockedRadio = document.createElement("input");
        lockedRadio.type = "radio";
        lockedRadio.name = `user${index}Locked`;
        lockedRadio.value = "lock";
        lockedRadio.checked = true;
        profileDiv.appendChild(lockedRadio);

        const unlockedLabel = document.createElement("label");
        unlockedLabel.textContent = "Unlock";
        profileDiv.appendChild(unlockedLabel);

        const unlockedRadio = document.createElement("input");
        unlockedRadio.type = "radio";
        unlockedRadio.name = `user${index}Locked`;
        unlockedRadio.value = "unlock";
        profileDiv.appendChild(unlockedRadio);

        const separator = document.createElement("hr");
        profileDiv.appendChild(separator);

        const usernameLabel = document.createElement("label");
        usernameLabel.textContent = "Username";
        profileDiv.appendChild(usernameLabel);

        const usernameInput = document.createElement("input");
        usernameInput.type = "text";
        usernameInput.name = `user${index}Username`;
        usernameInput.value = profile.username;
        usernameInput.disabled = true;
        usernameInput.readOnly = true;
        profileDiv.appendChild(usernameInput);

        const hiddenFieldsDiv = document.createElement("div");
        hiddenFieldsDiv.id = `user${index}HiddenFields`;
        hiddenFieldsDiv.style.display = "none";

        const emailLabel = document.createElement("label");
        emailLabel.textContent = "Email:";
        hiddenFieldsDiv.appendChild(emailLabel);

        const emailInput = document.createElement("input");
        emailInput.type = "email";
        emailInput.name = `user${index}Email`;
        emailInput.value = profile.email;
        emailInput.disabled = true;
        emailInput.readOnly = true;
        hiddenFieldsDiv.appendChild(emailInput);

        const ageLabel = document.createElement("label");
        ageLabel.textContent = "Age:";
        hiddenFieldsDiv.appendChild(ageLabel);

        const ageInput = document.createElement("input");
        ageInput.type = "number";
        ageInput.name = `user${index}Age`;
        ageInput.value = profile.age;
        ageInput.disabled = true;
        ageInput.readOnly = true;
        hiddenFieldsDiv.appendChild(ageInput);

        profileDiv.appendChild(hiddenFieldsDiv);

        const showMoreButton = document.createElement("button");
        showMoreButton.textContent = "Show more";
        showMoreButton.disabled = true;
        profileDiv.appendChild(showMoreButton);

        lockedRadio.addEventListener("change", () => {
            showMoreButton.disabled = true;
        });

        unlockedRadio.addEventListener("change", () => {
            showMoreButton.disabled = false;
        });

        let isExpanded = false;
        showMoreButton.addEventListener("click", () => {
            if (isExpanded) {
                hiddenFieldsDiv.style.display = "block";
                showMoreButton.textContent = "Hide it";
            } else {
                hiddenFieldsDiv.style.display = "none";
                showMoreButton.textContent = "Show more";
            }

            isExpanded = !isExpanded;
        });

        mainContainer.appendChild(profileDiv);
    }
}
