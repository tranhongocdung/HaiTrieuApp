$(document).ready(function() {
    initProductEditButton();
});

$(document).on("click", "#btnSubmit", function () {
    $("#page").val("1");
    $("#frmProductManage").submit();
});

function initProductEditButton() {
    $(".edit-product, .add-product").click(function () {
        $.ajax({
            method: "GET",
            url: $("#product-edit-url").val(),
            data: { id: $(this).data("product-id") },
            success: function (html) {
                $("#hidden-content").html(html);
                $.validator.unobtrusive.parse("#frmProductEdit");
                $("#product-edit-modal").modal();

                initEditButtons();
            }
        });
    });
}

function initEditButtons() {
    $("#btnSave").click(function () {
        $("#frmProductEdit").submit();
    });
}

function productManageCallBack(result) {
    $("#manager-content").html(result);
    initProductEditButton();
}

function productEditBegin() {
    setEditProgressBar("on");
}

function productEditCallBack(data) {
    if (data.Success) {
        showNotificationOnEditForm("alert-success", data.Message);
        $("#ObjId").val(data.Data);
        reloadCurrentPage();
    }
    else showNotificationOnEditForm("alert-danger", data.Message);
    setEditProgressBar("off");
}
function setEditProgressBar(stt) {
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
function reloadCurrentPage() {
    var currentPage = $("#current-page").val();
    goToPage(currentPage); //inside pager.cshtml
}