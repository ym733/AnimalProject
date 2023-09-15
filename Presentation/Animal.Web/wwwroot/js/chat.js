var model = JSON.parse(document.getElementById("modelData").innerHTML);

var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build()

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", (message) => {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = message;
});


connection.start().then(() => {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("onConnect", model["Id"],model["Name"]).catch((err) => {
        return console.error(err.toString());
    })
}).catch((err) => {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", () => {
    var userName = model["Name"]
    var userID = model["Id"]
    var message = document.getElementById("userMessage").value;

    connection.invoke("SendGlobalMessage", userID, userName, message).catch((err) => {
        return console.error(err.toString());
    })
    event.preventDefault();
});
document.getElementById("sendToButton").addEventListener("click", () => {
    var user = model["Name"]
    var message = document.getElementById("userToMessage").value;
    var userTo = document.getElementById("userTo").value;

    connection.invoke("SendPrivateMessage", userTo, user, message).catch((err) => {
        return console.error(err.toString());
    })
    event.preventDefault();
});