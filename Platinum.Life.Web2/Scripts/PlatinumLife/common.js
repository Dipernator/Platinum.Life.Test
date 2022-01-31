$(function () {

    $(".datepicker").datepicker(/*{ dateFormat: 'dd-mm-yy' }*/);
});

function ProtocolAndHost(){
    return window.location.protocol + '//' + window.location.host;
}

// Notification Error
function NotificationError(message) {
   
    $.notify(
        message,
        "error",
        { position: "center" }
    );
}

// Notification Success
function NotificationSuccess(message) {
    $.notify(
        message,
        "success",
        { position: "center" }
    );
}

// Notification Error
function NotificationElementError(message, elementId) {
    alert("Error " + message + "Element " + elementId);
}
