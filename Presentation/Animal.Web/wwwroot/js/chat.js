var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build()

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", (message) => {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("UserJoins", (user) => {
    if (document.getElementById("sidebar").childElementCount == 1) {
        document.getElementById("offline").setAttribute("hidden", "hidden")
    }
    
    var node = document.createElement("li")
    node.setAttribute("id", `${user}user`)
    var anchor = document.createElement("a")
    anchor.setAttribute("class", "d-inline nav-link text-dark")
    anchor.setAttribute("href", "href='javascript: AjaxPost(@Url.Action('PrivateChat', 'Chat', item)'")
    anchor.textContent = user
    node.appendChild(anchor)
    document.getElementById("sidebar").appendChild(node)
});

connection.on("UserLeaves", (user) => {
    if (document.getElementById("sidebar").childElementCount == 2) {
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
});

//// PRIVATE MESSAGES

function AjaxPost(url) {
    event.preventDefault();

    $.ajax({
        type: "POST",
        url: url,
        //data: data,
        contentType: 'application/json',
        traditional: true,
        success: function (data) {
            $(`#level${divNum}`).attr("hidden", "hidden")
            divNum++;
            $(`main`).append(`<div id="level${divNum}">${data}</div>`)
        },
        error: function (err) {
            console.error(err);
        }
    });
}

document.getElementById("sendToButton").addEventListener("click", () => {
    //var user = model["Name"]
    var message = document.getElementById("userToMessage").value;
    var userTo = parseInt(document.getElementById("userToID").value);
    var userToConnectionID = document.getElementById("userToConnectionID").value;

    connection.invoke("SendPrivateMessage", userTo, userToConnectionID, message).catch((err) => {
        return console.error(err.toString());
    })
    event.preventDefault();
});

connection.on("ReceiveMessage", (message) => {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("privateMessagesList").appendChild(li);
});