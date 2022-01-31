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
                NotificationError("Success")
                window.location.href = ProtocolAndHost() + "/PaymentRequisition/Index";
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
                NotificationError("Success")
                window.location.href = ProtocolAndHost() + "/PaymentRequisition/Index";
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert("Error check console")
            NotificationError(jqXHR);
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
                window.location.href = ProtocolAndHost() + "/Upload/UploadFile/" + response.entity;
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

$(document).ready(function () {

    $("#div-login").hover(
        () => { //hover
            $(this).addClass("hover");
            $("#div-register").animate({
                opacity: 0.25,
            }, 1, function () {
                // Animation complete.
            });
            $("#div-login").animate({
                opacity: 1,
            }, 1, function () {
                // Animation complete.
            });
        },
        () => { //out
            $(this).removeClass("hover");


        }
    );

    $("#div-register").hover(
        () => { //hover
            $(this).addClass("hover");
            $("#div-login").animate({
                opacity: 0.25,
            }, 1, function () {
                // Animation complete.
            });
            $("#div-register").animate({
                opacity: 1,
            }, 1, function () {
                // Animation complete.
            });
        },
        () => { //out
            $(this).removeClass("hover");


        }
    );

    $('#Bank').on('change', function () {
        if (this.value == 0) {
            $("#Bank").replaceWith("<input name='BankDetails.Bank' type='text' placeholder='Please enter new Bank'/><br />");
        }
    });

    $('#DepartmentId').on('change', function () {
        if (this.value == 0) {
            $("#DepartmentId").replaceWith("<input name='DepartmentName' type='text'  placeholder='Please enter new Department'/><br />");
        }
    });

    var $sigdiv = $("#signature").jSignature({ 'UndoButton': true })

    $('#click').click(function () {
        // Get response of type image
        var data = $sigdiv.jSignature('getData', 'image');

        // Storing in textarea
        $('#output').val(data);

        // Alter image source 
        $('#sign_prev').attr('src', "data:" + data);
        $('#sign_prev').show();

        var formdata = new FormData();
        formdata.append("base64image", data[1]);
        formdata.append("userId", $("#hdnUserId").val());
        formdata.append("paymentRequisitionId", $("#hdnPaymentRequisitionId").val());

        try {
            $.ajax({
                url: ProtocolAndHost() + '/Upload/UploadSignature',
                type: "POST",
                data: formdata,
                processData: false,
                contentType: false
            }).done(function () {
                NotificationSuccess("Your Signature has been saved") // I hope :( 
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert("Error check console")
                console.log(jqXHR);
                console.log("textStatus:" + textStatus);
                console.log("errorThrown:" + errorThrown);
            });
        } catch (err) {
            NotificationError(err)
        }


        //$.ajax({
        //    url: ProtocolAndHost() + '/Upload/UploadSignature',
        //    type: "POST",
        //    data: formdata,
        //    processData: false,
        //    contentType: false
        //});

      
    });
});



