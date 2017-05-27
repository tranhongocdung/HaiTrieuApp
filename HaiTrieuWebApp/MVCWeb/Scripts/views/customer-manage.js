$(document).ready(function() {
    initCustomerEditButton();
});

$(document).on("click", "#btnSubmit", function () {
    $("#page").val("1");
    $("#frmCustomerManage").submit();
});

function initCustomerEditButton() {
    $(".edit-customer").click(function() {
        $.ajax({
            method: "GET",
            url: $("#edit-customer-url").val(),
            data: { id: $(this).data("customer-id") },
            success: function (html) {
                $("#hidden-content").html(html);
                $.validator.unobtrusive.parse("#frmCustomerEdit");
                $("#customer-edit-modal").modal();

                $("#txtNote").summernote({
                    toolbar: false,
                    height: 90
                });

                var region = new Bloodhound({
                    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("region"),
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    identify: function (obj) { return obj.region; },
                    prefetch: "../content/json/region.json"
                });

                function regionsWithDefaults(q, sync) {
                    if (q === "") {
                        sync(region.get("Bình Thạnh"));
                    }

                    else {
                        region.search(q, sync);
                    }
                }

                $("#txtRegion").typeahead({
                    minLength: 0,
                    highlight: true
                },
                {
                    name: "region",
                    display: "region",
                    source: regionsWithDefaults
                });

                var area = new Bloodhound({
                    datumTokenizer: Bloodhound.tokenizers.obj.whitespace("area"),
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    identify: function (obj) { return obj.area; },
                    prefetch: "../content/json/area.json"
                });

                function areasWithDefaults(q, sync) {
                    if (q === "") {
                        sync(area.get("TP.HCM"));
                    }

                    else {
                        area.search(q, sync);
                    }
                }

                $("#txtArea").typeahead({
                    minLength: 0,
                    highlight: true
                },
                {
                    name: "area",
                    display: "area",
                    source: areasWithDefaults
                });

                initEditButtons();
            }
        });
    });
}

function initEditButtons() {
    $("#btnSave").click(function () {
        $("#frmCustomerEdit").submit();
    });
}

function customerManageCallBack(result) {
    $("#manager-content").html(result);
    initCustomerEditButton();
}

function customerEditBegin() {
    setEditProgressBar("on");
}

function customerEditCallBack(data) {
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