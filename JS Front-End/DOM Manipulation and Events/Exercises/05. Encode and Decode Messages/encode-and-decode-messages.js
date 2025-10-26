document.addEventListener("DOMContentLoaded", solve);

function solve() {
    function transform(text, shift) {
        let result = [];
        for (let i = 0; i < text.length; i++) result.push(String.fromCharCode(text.charCodeAt(i) + shift));

        return result.join("");
    }

    const encodeForm = document.querySelector("form#encode");
    const decodeForm = document.querySelector("form#decode");

    const encodeInput = encodeForm.querySelector("textarea");
    const decodeInput = decodeForm.querySelector("textarea");

    encodeForm.addEventListener("submit", (e) => {
        e.preventDefault();

        const encodedMessage = transform(encodeInput.value, 1);

        encodeInput.value = "";
        decodeInput.value = encodedMessage;
    });

    decodeForm.addEventListener("submit", (e) => {
        e.preventDefault();

        const decodedMessage = transform(decodeInput.value, -1);
        decodeInput.value = decodedMessage;
    });
}
