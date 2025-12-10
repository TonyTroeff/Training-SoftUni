const url = "http://localhost:3030/jsonstore/orders";

const ordersList = document.getElementById("list");
const loadButton = document.getElementById("load-orders");
const createButton = document.getElementById("order-btn");
const editButton = document.getElementById("edit-order");
const nameInput = document.querySelector("input#name");
const quantityInput = document.querySelector("input#quantity");
const dateInput = document.querySelector("input#date");

loadButton.addEventListener("click", reloadOrdersAsync);

createButton.addEventListener("click", async (event) => {
    event.preventDefault();
    event.stopPropagation();

    const body = createOrderInstance();
    if (!body) return;

    await fetch(url, { method: "POST", body: JSON.stringify(body) });

    clearInputs();

    await reloadOrdersAsync();
});

let orderToEdit = null;
editButton.addEventListener("click", async (event) => {
    event.preventDefault();
    event.stopPropagation();

    if (!orderToEdit) return;

    const body = createOrderInstance();
    if (!body) return;

    await fetch(`${url}/${orderToEdit._id}`, { method: "PUT", body: JSON.stringify({ ...body, _id: orderToEdit._id }) });

    clearInputs();
    createButton.disabled = false;
    editButton.disabled = true;
    orderToEdit = null;

    await reloadOrdersAsync();
});

function createOrderInstance() {
    if (!nameInput.value || !quantityInput.value || !dateInput.value) return null;
    return { name: nameInput.value, quantity: quantityInput.value, date: dateInput.value };
}

function clearInputs() {
    nameInput.value = "";
    quantityInput.value = "";
    dateInput.value = "";
}

async function reloadOrdersAsync() {
    const getOrdersResponse = await fetch(url);
    const orders = await getOrdersResponse.json();

    ordersList.replaceChildren(...Object.values(orders).map(createOrderElement));
}

function createOrderElement(order) {
    const nameHeader = document.createElement("h2");
    nameHeader.textContent = order.name;

    const dateHeader = document.createElement("h3");
    dateHeader.textContent = order.date;

    const quantityHeader = document.createElement("h3");
    quantityHeader.textContent = order.quantity;

    const changeButton = document.createElement("button");
    changeButton.className = "change-btn";
    changeButton.textContent = "Change";

    const doneButton = document.createElement("button");
    doneButton.className = "done-btn";
    doneButton.textContent = "Done";

    const container = document.createElement("div");
    container.className = "container";
    container.appendChild(nameHeader);
    container.appendChild(dateHeader);
    container.appendChild(quantityHeader);
    container.appendChild(changeButton);
    container.appendChild(doneButton);

    changeButton.addEventListener("click", () => {
        container.remove();

        nameInput.value = order.name;
        quantityInput.value = order.quantity;
        dateInput.value = order.date;

        editButton.disabled = false;
        createButton.disabled = true;

        orderToEdit = order;
    });

    doneButton.addEventListener("click", async () => {
        await fetch(`${url}/${order._id}`, { method: "DELETE" });
        await reloadOrdersAsync();
    });

    return container;
}
