"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, date) {
    var li = document.createElement("li");

    document.getElementById("messagesList").appendChild(li);

    scrollToBottom();

    li.textContent = `${date}  ${user}: ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("JoinGroup", groupId);
    refreshOnlineUsers(groupId);

}).catch(function (err) {
    return console.error(err.toString());
});

function scrollToBottom() {
    const chatBox = document.getElementById("scrollmenu");
    chatBox.scrollTop = chatBox.scrollHeight;
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    
    var message = document.getElementById("messageInput").value;

    var trimmedMessage = message.trim();

    if (!trimmedMessage) {
        document.getElementById("messageInput").value = "";


        return;
    }


    var groupId = document.getElementById("groupId").value;

    connection.invoke("SendMessage", message, groupId).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messageInput").value = "";

    event.preventDefault();
});


function refreshOnlineUsers(groupId) {

    fetch(`/ChatGroups/GetOnlineUsers?groupId=${groupId}`)
        .then(r => r.json())
        .then(users => {
            let list = document.getElementById("onlineUsers");
            list.innerHTML = "";

            users.forEach(u => {
                let li = document.createElement("li");
                li.textContent = u;
                list.appendChild(li);
            });
        });
}

connection.on("OnlineUsersUpdated", function (groupId) {
    refreshOnlineUsers(groupId);
});