$(document).on("click", "#btnSubmit", function () {
    $("#page").val("1");
    $("#frmCustomerManage").submit();
});

function customerManageCallBack(result) {
    $("#manager-content").html(result);
}