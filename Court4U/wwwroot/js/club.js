"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/clubHub").build();

connection.on("ClubChanged", function () {
    location.reload();
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});