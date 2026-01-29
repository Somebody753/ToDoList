"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

//receiving message and updating in real time the list of messeges
connection.on("ReceiveMessage", function (user, message, date) {
    var li = document.createElement("li");

    document.getElementById("messagesList").appendChild(li);

    scrollToBottom();

    li.textContent = `${date}  ${user}: ${message}`;
});


//joining group on connection
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("JoinGroup", groupId);

}).catch(function (err) {
    return console.error(err.toString());
});


//Scrolling to the bottom of the chat list
function scrollToBottom() {
    const chatBox = document.getElementById("scrollmenu");
    chatBox.scrollTop = chatBox.scrollHeight;
}


//Sending message to the group
document.getElementById("sendButton").addEventListener("click", function (event) {
    
    var message = document.getElementById("messageInput").value;

    var trimmedMessage = message.trim();

    //check if messege is empty, if so - return
    if (!trimmedMessage) {
        document.getElementById("messageInput").value = "";


        return;
    }


    var groupId = document.getElementById("groupId").value;

    connection.invoke("SendMessage", message, groupId).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messageInput").value = "";//reseting the text form after sending message

    event.preventDefault();
});

