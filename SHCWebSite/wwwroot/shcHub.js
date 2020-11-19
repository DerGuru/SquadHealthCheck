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

shcHub.onclose(start);
await start();