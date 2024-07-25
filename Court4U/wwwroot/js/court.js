"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/clubHub").build();

connection.on("CourtChanged", function () {
    location.reload();
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});