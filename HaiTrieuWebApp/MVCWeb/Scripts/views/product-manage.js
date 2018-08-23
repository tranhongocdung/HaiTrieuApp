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
                initSelectCategoryTreeViewButtons();
            }
        });
    });
}

function initEditProductButtons() {
    $("#btnSaveProduct").click(function () {
        productEditBeforeSend();
        $("#frmProductEdit").submit();
    });
}

function initSelectCategoryTreeViewButtons() {
    $("#category-treeview-container button").click(function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected").removeClass("btn-danger");
        } else {
            $(this).addClass("selected").addClass("btn-danger");
        }
    });
    var categoryIds = $("#MappedCategoryIds").val().split(",").map(Number);
    $("#category-treeview-container button").each(function() {
        if (categoryIds.indexOf($(this).data("category-id")) >= 0) {
            $(this).addClass("selected").addClass("btn-danger");
        }
    });
}

function productManageCallBack(result) {
    $("#manager-content").html(result);
    initProductEditButton();
}

function productEditBegin() {
    setEditProgressBar("on");
}

function productEditBeforeSend() {
    var categoryIds = [];
    $("#category-treeview-container button").each(function () {
        if ($(this).hasClass("selected")) {
            categoryIds.push(parseInt($(this).data("category-id")));
        }
    });
    $("#MappedCategoryIds").val(categoryIds.join(","));
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