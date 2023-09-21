var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build()

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", (message) => {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("UserJoins", (id, username, connectionId) => {
    if (document.getElementById("userList").childElementCount == 1) {
        document.getElementById("offline").setAttribute("hidden", "hidden")
    }

    var node = document.createElement("li")
    node.setAttribute("id", `${username}user`)
    var anchor = document.createElement("a")
    anchor.setAttribute("class", "d-inline nav-link text-dark")
    anchor.setAttribute("href", `javascript:AjaxPost('./Chat/PrivateChat', {id:${id}, username:'${username}', connectionID:'${connectionId}'})`)
    anchor.textContent = username
    node.appendChild(anchor)
    document.getElementById("userList").appendChild(node)
});

connection.on("UserLeaves", (user) => {
    if (document.getElementById("userList").childElementCount == 2) {
        document.getElementById("offline").removeAttribute("hidden")
    }
    document.getElementById(`${user}user`).remove()
});

connection.start().then(() => {
    document.getElementById("sendButton").disabled = false;
}).catch((err) => {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", () => {
    var message = document.getElementById("userMessage").value;

    if (message === '') return;

    connection.invoke("SendGlobalMessage", message).catch((err) => {
        return console.error(err.toString());
    })
    event.preventDefault();

    const url = '/Chat/sendGlobalMessage';
    const data = {
        senderID: document.getElementById("userID").value,
        text: message
    }

    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        success: function (response) {
            console.log(response);
        },
        error: function (err) {
        }
    });
});

//// PRIVATE MESSAGES

function AjaxPost(url, data) {
    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        success: function (response) {
            $(`#level${divNum}`).attr("hidden", "hidden")
            divNum++;
            $(`main`).append(`<div id="level${divNum}">${response}</div>`)
        },
        error: function (err) {
        }
    });
}
