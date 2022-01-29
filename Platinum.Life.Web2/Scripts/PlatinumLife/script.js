$("#frm-register").submit(function (e) {
    SubmitRegisterForm();
    e.preventDefault();
});

$("#frm-login").submit(function (e) {
    SubmitLoginForm();
    e.preventDefault();
});

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
