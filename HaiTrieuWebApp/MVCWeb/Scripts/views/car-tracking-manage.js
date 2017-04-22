function loadCarTrackingEditForm(objId) {
    $.ajax({
        method: "GET",
        url: "/cartracking/edit",
        data: {id:objId},
        success: function(html) {
            $("#hidden-content").html(html);
            UILoad.initDatePicker("#car-tracking-edit-form .datepicker-control", updateInfoAfterPickingRundate);
            updateInfoAfterPickingRundate();
            $.validator.unobtrusive.parse("#car-tracking-edit-form");
            initDeleteButton();
            $("#car-tracking-edit-modal").modal();
            bindEditButtons();
        }
    });
}
function updateInfoAfterPickingRundate() {
    var d = $("#txtRunDate").val();
    var dp = d.split("/");
    $("#txtManagedMonth").val("Tháng " + dp[1] + " Năm " + dp[2]);
}
function onCarTrackingEditFormBegin() {
    setEditProgressBar("on");
}
function onCarTrackingEditFormSuccess(data) {
    if (data.Success) {
        showNotificationOnEditForm("alert-success", data.Message);
        $("#ObjId").val(data.Data);
        initDeleteButton();
        reloadCurrentPage();
    }
    else showNotificationOnEditForm("alert-danger", data.Message);
    setEditProgressBar("off");
}
function onCarTrackingManageBegin() {
    setManageProgressBar("on");
}
function onCarTrackingManageSuccess() {
    setManageProgressBar("off");
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
    $("#btnSave").click(function() {
        $("#car-tracking-edit-form").submit();
    });
    $("#btnResetEditForm").click(function() {
        resetEditForm();
        showNotificationOnEditForm("alert-warning", "Đã reset form để thêm mới");
    });
    $("#btnDelete").click(function() {
        deleteObj();
    });
}
function resetEditForm() {
    $("#ObjId").val("0");
    $("#txtDescription").val("");
    $("#txtNote").val("");
    $("#txtTotalCash").val("0");
    $("#txtNumOfCustomers").val("0");
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
            url: "/cartracking/delete",
            data: { id: $("#ObjId").val() },
            beforeSend: function() {
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

function setManageProgressBar(stt) {
    if (stt == "on") {
        $("#manage-loader").css("left", 180 + $("#pnl-manage").width() / 2);
        $("#manage-loader").fadeIn(0);
    } else {
        $("#manage-loader").fadeOut("fast");
    }
}