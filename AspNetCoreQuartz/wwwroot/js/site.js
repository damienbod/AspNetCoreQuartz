
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

connection.on("ConcurrentJobs", function (message) {
    var li = document.createElement("li");
    document.getElementById("concurrentJobs").appendChild(li);
    li.textContent = `${message}`;
});

connection.on("NonConcurrentJobs", function (message) {
    var li = document.createElement("li");
    document.getElementById("nonConcurrentJobs").appendChild(li);
    li.textContent = `${message}`;
});

