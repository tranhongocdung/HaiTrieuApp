$(document).ready(function () {
    initCategoryManageButton();
});

function initCategoryManageButton() {
    $(".manage-category").click(function () {
        $.ajax({
            method: "GET",
            url: $("#manage-category-url").val(),
            success: function (html) {
                $("#hidden-content").html(html);
                $.validator.unobtrusive.parse("#frmCategoryEdit");
                $("#product-edit-modal").modal();

                //initEditButtons();
            }
        });
    });
}