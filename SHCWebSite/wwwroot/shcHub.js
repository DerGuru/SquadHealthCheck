var shcHub = new signalR.HubConnectionBuilder().withUrl("/shcHub").build();

async function start() {
    try {
        await shcHub.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

shcHub.on("updateId", (id, url) => {
    document.getElementById(id).style.backgroundImage = url;
});

shcHub.on("updateValue", (id, value) => {
    document.getElementById(id).innerText = value;
});
shcHub.on("refresh", () => {
    document.location.reload();
});
shcHub.on("addedItem", (id) => {
    var card = document.getElementById("Card" + id);
    card.parentElement.removeChild(card);
    var parentId = "removeItem";
    var newParent = document.getElementById(parentId);
    newParent.appendChild(card);
});

shcHub.on("removedItem", (id) => {
    var card = document.getElementById("Card" + id);
    card.parentElement.removeChild(card);
    var parentId = "addItem";
    var newParent = document.getElementById(parentId);
    newParent.appendChild(card);
});

shcHub.onclose(start);
start();

function cardClicked(squad, item) {
    var card = document.getElementById("Card" + item);
    shcHub.invoke(card.parentElement.id, squad, item);
}

function setUserValue(squad, item, value) {
    shcHub.invoke("setUserValue", squad, item, value);
}