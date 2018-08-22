$(document).ready(function () {
    initCategoryManageButton();
});

function initCategoryManageButton() {
    $(".manage-category").click(function () {
        $.ajax({
            method: "GET",
            url: $("#category-manage-url").val(),
            success: function (html) {
                $("#hidden-content").html(html);
                $.validator.unobtrusive.parse("#frmCategoryEdit");
                $("#category-manage-modal").modal();
                initCategoryEditFormLoadButton();
            }
        });
    });
}

function initEditCategoryButtons() {
    $("#btnSave").click(function () {
        $("#frmCategoryEdit").submit();
    });
}

function initCategoryEditFormLoadButton() {
    $(".edit-category").click(function () {
        loadCategoryEditForm($(this).data("category-id"));
        $("#category-treeview-container button").removeClass("btn-warning");
        $(this).addClass("btn-warning");
    });
}

function initCategoryEditCancelButton() {
    $("#btnCancelEditCategory").click(function () {
        loadCategoryEditForm(0);
        $("#category-treeview-container button").removeClass("btn-warning");
    });
}

function categoryEditBegin() {
    setEditProgressBar("on");
}

function categoryEditCallBack(data) {
    if (data.Success) {
        showModalMessage(data.Message, "success");
        $("#category-treeview-container").html(data.Data);
        initCategoryEditFormLoadButton();
        $("#btnCancelEditCategory").click();
    }
    else showModalMessage(data.Message, "danger");
    setEditProgressBar("off");
}

function loadCategoryEditForm(id) {
    $.ajax({
        method: "GET",
        url: $("#category-edit-url").val(),
        data: {
            id: id
        },
        beforeSend: function () {
            $("#editFormLoading").show();
        },
        success: function (html) {
            $("#category-edit-container").html(html);
            initCategoryEditCancelButton();
        }
    });
}