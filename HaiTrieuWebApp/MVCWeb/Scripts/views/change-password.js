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
        showNotificationOnEditForm("alert-success", data.Message);
    }
    else showNotificationOnEditForm("alert-danger", data.Message);
    setChangePasswordProgressBar("off");
}
function setChangePasswordProgressBar(stt) {
    if (stt == "on") {
        $("#edit-loader").fadeIn(0);
    } else {
        $("#edit-loader").fadeOut("fast");
    }
}
function showNotificationOnEditForm(style, message) {
    var notificationDiv = "<div class=\"alert alert-small text-center\" id=\"edit-notification\" style=\"display:none\"></div>";
    $("#edit-content").html(notificationDiv);
    var panel = $("#edit-notification");
    panel.addClass(style);
    panel.html(message);
    panel.fadeIn(0).delay(800).fadeOut("slow");
}