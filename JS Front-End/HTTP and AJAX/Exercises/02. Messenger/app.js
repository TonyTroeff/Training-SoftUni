function attachEvents() {
    const messagesArea = document.querySelector("textarea#messages");

    const authorInput = document.querySelector('input[name="author"]');
    const messageInput = document.querySelector('input[name="content"]');

    const sendButton = document.querySelector("input#submit");
    const refreshButton = document.querySelector("input#refresh");

    sendButton.addEventListener("click", async () => {
        const author = authorInput.value;
        const content = messageInput.value;
        const message = { author, content };

        await fetch("http://localhost:3030/jsonstore/messenger", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(message),
        });

        authorInput.value = "";
        messageInput.value = "";
    });

    refreshButton.addEventListener("click", async () => {
        const getMessagesResponse = await fetch("http://localhost:3030/jsonstore/messenger");
        const allMessages = await getMessagesResponse.json();

        messagesArea.value = Object.values(allMessages)
            .map((m) => `${m.author}: ${m.content}`)
            .join("\n");
    });
}

attachEvents();
