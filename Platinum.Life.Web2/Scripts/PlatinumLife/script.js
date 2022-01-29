$("#frm-register").submit(function (e) {
    SubmitRegisterForm();
    e.preventDefault();
});

$("#frm-login").submit(function (e) {
    SubmitLoginForm();
    e.preventDefault();
});

$("#frm-create-payment-requisition").submit(function (e) {
    SubmitPaymentRequisitionForm();
    e.preventDefault();
});

// Submit registration details
function SubmitRegisterForm() {
    try {
        $.ajax({
            url: ProtocolAndHost() + '/User/Register',
            type: "POST",
            data: $("#frm-register").serialize(),
        }).done(function (response) {
            if (response == null || !response.success) {
                NotificationError(response.message)
            }
            else {
                window.location.href = ProtocolAndHost() + "/Home/Index";
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert("Error check console")
            console.log(jqXHR);
            console.log("textStatus:" + textStatus);
            console.log("errorThrown:" + errorThrown);
        });
    } catch (err) {
        NotificationError(err)
    }
}

// Submit log in details
function SubmitLoginForm() {
    try {
        $.ajax({
            url: ProtocolAndHost() + "/User/Login",
            type: "POST",
            data: $("#frm-login").serialize(),
        }).done(function (response) {
            if (response == null || !response.success) {
                NotificationError(response.message);             
            }
            else {
                window.location.href = ProtocolAndHost() + "/Home/Index";
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert("Error check console")
            console.log(jqXHR);
            console.log("textStatus:" + textStatus);
            console.log("errorThrown:" + errorThrown);
        });
    } catch (err) {
        NotificationError(err)
    }
}

// Submit from to create a payment requisition
function SubmitPaymentRequisitionForm() {
    console.log($("#frm-create-payment-requisition").serialize());
    try {
        $.ajax({
            url: ProtocolAndHost() + '/PaymentRequisition/CreateOrUpdate',
            type: "POST",
            data: $("#frm-create-payment-requisition").serialize(),
        }).done(function (response) {
            if (response == null || !response.success) {
                NotificationError(response.message)
            }
            else {
                window.location.href = ProtocolAndHost() + "/Home/Index";
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert("Error check console")
            console.log(jqXHR);
            console.log("textStatus:" + textStatus);
            console.log("errorThrown:" + errorThrown);
        });
    } catch (err) {
        NotificationError(err)
    }
}


