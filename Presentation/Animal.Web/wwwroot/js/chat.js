var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build()

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", (message) => {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = message;
});

connection.start().then(() => {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("onConnect").catch((err) => {
        return console.error(err.toString());
    })
}).catch((err) => {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", () => {
    var user = "";
    var message = document.getElementById("userMessage").value;
    connection.invoke("SendMessage", user, message).catch((err) => {
        return console.error(err.toString());
    })
    event.preventDefault();
});

document.getElementById("sendToButton").addEventListener("click", () => {
    var user = "";
    var message = document.getElementById("userToMessage").value;
    var userTo = document.getElementById("userTo").value;

    connection.invoke("SendToMessage", userTo, user, message).catch((err) => {
        return console.error(err.toString());
    })
    event.preventDefault();
});