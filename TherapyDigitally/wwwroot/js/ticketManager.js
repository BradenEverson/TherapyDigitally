document.getElementById("send").disabled = true;

var connection = new signalR.HubConnectionBuilder().withUrl("./TherapyHub").build();
connection.start().then(function () {
    document.getElementById("send").disabled = false;
});
function userMessage(ticketId, name) {
    var chatList = document.getElementById("chat");
    var newMessage = document.createElement("li");
    newMessage.innerHTML = "<h6 class='text-info'>" + name + "</h6>" + document.getElementById("message").value;
    newMessage.setAttribute("class", "list-group-item text-right");
    chatList.appendChild(newMessage);
    connection.invoke("UserMessage", ticketId, document.getElementById("message").value).catch(function (err) {
        return console.error(err);
    });
}
connection.on("BotMessage", function (botmessage) {
    var chatList = document.getElementById("chat");
    var newMessage = document.createElement("li");
    newMessage.innerHTML = "<h6 class='text-info'>Marcus</h6>" + botmessage;
    newMessage.setAttribute("class", "list-group-item text-left");
    chatList.appendChild(newMessage);
});