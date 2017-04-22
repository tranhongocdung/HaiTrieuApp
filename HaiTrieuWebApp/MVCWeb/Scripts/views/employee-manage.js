function loadEmployeeEditForm(objId) {
    $.ajax({
        method: "GET",
        url: "/employee/edit",
        data: { id: objId },
        success: function (html) {
            $("#hidden-content").html(html);
            $.validator.unobtrusive.parse("#employee-edit-form");
            initDeleteButton();
            $("#employee-edit-modal").modal();
            bindEditButtons();
        }
    });
}
function onEmployeeEditFormBegin() {
    setEditProgressBar("on");
}
function onEmployeeEditFormSuccess(data) {
    if (data.Success) {
        showNotificationOnEditForm("alert-success", data.Message);
        $("#ObjId").val(data.Data);
        initDeleteButton();
        reloadCurrentPage();
    }
    else showNotificationOnEditForm("alert-danger", data.Message);
    setEditProgressBar("off");
}
function onEmployeeManageBegin() {
    setManageProgressBar("on");
}
function onEmployeeManageSuccess() {
    setManageProgressBar("off");
}
function setManageProgressBar(stt) {
    if (stt == "on") {
        $("#manage-loader").css("left", 180 + $("#pnl-manage").width() / 2);
        $("#manage-loader").fadeIn(0);
    } else {
        $("#manage-loader").fadeOut("fast");
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
function bindEditButtons() {
    $("#btnSave").click(function () {
        $("#employee-edit-form").submit();
    });
    $("#btnResetEditForm").click(function () {
        resetEditForm();
        showNotificationOnEditForm("alert-warning", "Đã reset form để thêm mới");
    });
    $("#btnDelete").click(function () {
        deleteObj();
    });
}
function resetEditForm() {
    $("#ObjId").val("0");
    $("#txtFullName").val("");
    $("#txtPhone").val("");
    $("#txtAddress").val("");
    $("#txtNote").val("");
    $("#txtBasicSalary").val("0");
    initDeleteButton();
}
function reloadCurrentPage() {
    var currentPage = $("#current-page").val();
    goToPage(currentPage); //inside pager.cshtml
}
function initDeleteButton() {
    var id = parseInt($("#ObjId").val());
    if (id < 1) {
        $("#btnDelete").fadeOut("fast");
    } else {
        $("#btnDelete").fadeIn("fast");
    }
}
function setEditProgressBar(stt) {
    if (stt == "on") {
        $("#edit-loader").fadeIn(0);
    } else {
        $("#edit-loader").fadeOut("fast");
    }
}
function deleteObj() {
    var c = confirm("Xác nhận xóa mục này?");
    if (c) {
        $.ajax({
            method: "POST",
            url: "/employee/delete",
            data: { id: $("#ObjId").val() },
            beforeSend: function () {
                setEditProgressBar("on");
            },
            success: function (data) {
                if (data.Success) {
                    showNotificationOnEditForm("alert-success", data.Message);
                }
                else showNotificationOnEditForm("alert-danger", data.Message);
                resetEditForm();
                initDeleteButton();
                reloadCurrentPage();
                setEditProgressBar("off");
            }
        });
    }
}