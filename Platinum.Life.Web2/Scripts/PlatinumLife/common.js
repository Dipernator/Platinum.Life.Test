
function ProtocolAndHost(){
    return window.location.protocol + '//' + window.location.host;
}

// Notification Error
function NotificationError(message) {
    alert("Error " + message);
}

// Notification Success
function NotificationSuccess(message) {
    alert("Success " + message);
}

// Notification Error
function NotificationElementError(message, elementId) {
    alert("Error " + message + "Element " + elementId);
}
