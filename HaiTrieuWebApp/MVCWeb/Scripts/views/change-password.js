$(document).ready(function() {
    initChangePasswordButton();
});

function initChangePasswordButton() {
    $(".change-password").click(function () {
        $.ajax({
            method: "GET",
            url: $("#change-password-url").val(),
            success: function (html) {
                $("#hidden-content").html(html);
                $.validator.unobtrusive.parse("#frmChangePassword");
                $("#change-password-modal").modal();

                initSavePasswordButton();
            }
        });
    });
}

function initSavePasswordButton() {
    $("#btnChangePassword").click(function () {
        $("#frmChangePassword").submit();
    });
}

function changePasswordBegin() {
    setChangePasswordProgressBar("on");
}

function changePasswordCallBack(data) {
    if (data.Success) {
        showModalMessage(data.Message, "success");
    }
    else showModalMessage(data.Message, "danger");
    setChangePasswordProgressBar("off");
}

function setChangePasswordProgressBar(stt) {
    if (stt == "on") {
        $("#edit-loader").fadeIn(0);
    } else {
        $("#edit-loader").fadeOut("fast");
    }
}