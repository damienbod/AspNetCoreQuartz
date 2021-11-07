
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/jobshub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(async () => {
    await start();
});

start();

connection.on("JobInfo", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${message}`;
});

