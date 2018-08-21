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

                initEditProductButtons();
            }
        });
    });
}

function initEditProductButtons() {
    $("#btnSaveProduct").click(function () {
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
        showModalMessage(data.Message, "success");
        $("#ObjId").val(data.Data);
        reloadCurrentPage();
    }
    else showModalMessage(data.Message, "danger");
    setEditProgressBar("off");
}

function reloadCurrentPage() {
    var currentPage = $("#current-page").val();
    goToPage(currentPage); //inside pager.cshtml
}