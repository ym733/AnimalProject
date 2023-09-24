var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build()

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", (message) => {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("UserJoins", (id, username, connectionId) => {
    sidebarAjax();
});

connection.on("UserLeaves", (user) => {
    sidebarAjax();
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
            console.error(err)
        }
    });
});

window.addEventListener('popstate', function (event) {
    connection.stop()
});

function sidebarAjax() {
    //console.log("executed")
    $.ajax({
        url: '/Chat/sideBar',
        type: 'GET',
        success: function (response) {
            //console.log(response)
            $('#sidebar').html(response)
        },
        error: function (err) {
        }
    });
}

sidebarAjax();

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
