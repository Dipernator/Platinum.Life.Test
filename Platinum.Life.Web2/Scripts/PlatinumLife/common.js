$(function () {
    $(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
});

function ProtocolAndHost(){
    return window.location.protocol + '//' + window.location.host;
}

// Notification Error
function NotificationError(message) {
    alert("Error NotificationError" + message);
    $.notify(
        message,
        "error",
        { position: "center" }
    );
}

// Notification Success
function NotificationSuccess(message) {
    alert("Success " + message);
}

// Notification Error
function NotificationElementError(message, elementId) {
    alert("Error " + message + "Element " + elementId);
}
