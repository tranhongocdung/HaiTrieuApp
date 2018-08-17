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
                $("#treeview").hummingbird();
                //initEditButtons();
            }
        });
    });
}