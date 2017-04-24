$.fn.editable.defaults.mode = "inline";

$(document).ready(function() {
    initXEditable();
    initAddProductRowButton();
    initRemoveProductRowButton();
    numberProductRow();
    initNumericTextbox(".unit-price");
    initNumericTextbox(".quantity");
    initNumericTextbox("#discount");
    initDiscountTypeOnChange();
    initSearchCustomerTextbox();
    initCustomerTypeToggle();
});

function initCustomerTypeToggle() {
    $("#chkCustomerType").bootstrapToggle({
        on: "Khách mới",
        off: "Khách cũ"
    });
    if ($("#customer-id").val() == "") {
        $("#chkCustomerType").bootstrapToggle("disable");
    }
    $("#chkCustomerType").change(function() {
        if ($(this).prop("checked")) {
            $("#customer-id").val("");
            $("#txtCustomerName").val("").removeAttr("readonly");
            $("#txtPhoneNo").val("").removeAttr("readonly");
            $("#txtEmail").val("").removeAttr("readonly");
            $("#txtAddress").val("").removeAttr("readonly");
            $("#txtDistrict").val("").removeAttr("readonly");
            $("#txtCity").val("").removeAttr("readonly");
            $("#txtCustomerNote").val("").removeAttr("readonly");
            $("#chkCustomerType").bootstrapToggle("disable");
        }
    });
}

function initSearchCustomerTextbox() {
    $("#txtSearchCustomer").bootcomplete({
        url: $("#customer-suggestion-datasource").val(),
        fillTextbox: false,
        onSelect:function(id) {
            $("#txtSearchCustomer").val("");
            loadCustomerDetail(id);
            $("#chkCustomerType").bootstrapToggle("enable");
            $("#chkCustomerType").bootstrapToggle("off");
        }
    });
}

function loadCustomerDetail(id) {
    $.ajax({
        url: $("#customer-suggestion-datasource").val(),
        data: {id : id},
        dataType: "json",
        success: function (data) {
            $("#customer-id").val(data.Id);
            $("#txtCustomerName").val(data.CustomerName).attr("readonly","readonly");
            $("#txtPhoneNo").val(data.PhoneNo).attr("readonly", "readonly");
            $("#txtEmail").val(data.Email).attr("readonly", "readonly");
            $("#txtAddress").val(data.Address).attr("readonly", "readonly");
            $("#txtDistrict").val(data.District).attr("readonly", "readonly");
            $("#txtCity").val(data.City).attr("readonly", "readonly");
            $("#txtCustomerNote").val(data.Note).attr("readonly", "readonly");
        }
    });
}

function initDiscountTypeOnChange() {
    $("#discount-type").change(function() {
        calculateTotalCash();
    });
}

function initNumericTextbox(selector, container) {
    if (typeof container == "undefined") {
        var container = $("body");
    }
    container.find(selector).numeric();
    container.find(selector).keyup(function() {
        var row = $(this).parent().parent();
        calculateCashForProductRow(row);
        calculateTotalCash();
    });
}

function initAddProductRowButton() {
    $("#add-product-row").click(function() {
        var productRow = "<td class=\"text-center count-no\"></td><td><a href=\"#\" class=\"product-name\" data-value=\"0\">Chọn sản phẩm</a><input type=\"hidden\" class=\"product-id\"/></td><td class=\"text-center\"><span class=\"text-danger short-description\"></span></td><td><input type=\"text\" placeholder=\"Mô tả\" class=\"form-control product-note\"/></td><td><input type=\"text\" placeholder=\"Đơn giá\" class=\"form-control unit-price\"/></td><td><input type=\"text\" placeholder=\"Số lượng\" class=\"form-control quantity\"/></td><td><input type=\"text\" placeholder=\"Thành tiền\" class=\"form-control product-cash\" readonly=\"readonly\"/></td><td><button class=\"btn btn-danger btn-sm remove-row\" data-toggle=\"confirmation\"><span class=\"glyphicon glyphicon-remove\"></span></button></td>";
        var appendRow = $("#tr-for-append");
        appendRow.append(productRow);
        appendRow.removeAttr("id");
        appendRow.addClass("product-order-row");
        initXEditable(appendRow);
        initRemoveProductRowButton(appendRow);
        initNumericTextbox(".unit-price", appendRow);
        initNumericTextbox(".quantity", appendRow);
        $("#product-order-row-container").append("<tr id=\"tr-for-append\"></tr>");
        numberProductRow();
    });
}

function initRemoveProductRowButton(container) {
    if (typeof container == "undefined") {
        var container = $("body");
    }
    container.find(".remove-row").first().confirmation({
        onConfirm: function() {
            $(this).parent().parent().remove();
            numberProductRow();
            calculateTotalCash();
        },
        placement: "left",
        title: "Xóa dòng này?"
    });
}

function numberProductRow() {
    var i = 0;
    $(".count-no").each(function() {
        i++;
        $(this).html(i);
    });
}

function initXEditable(container) {
    if (typeof container == "undefined") {
        var container = $("body");
    }
    container.find(".product-name").editable({
        type: "select2",
        select2: {
            placeholder: "Chọn sản phẩm",
            allowClear: true,
            minimumInputLength: 0,
            id: function (item) {
                return item.Id;
            },
            ajax: {
                url: $("#product-name-datasource").val(),
                dataType: "json",
                data: function (term, page) {
                    return { query: term };
                },
                results: function (data, page) {
                    return { results: data };
                }
            },
            formatResult: function (item) {
                return item.ProductName;
            },
            formatSelection: function (item) {
                localStorage.setItem("unitPrice", item.UnitPrice);
                localStorage.setItem("productId", item.Id);
                localStorage.setItem("shortDescription", item.ShortDescription);
                return item.ProductName;
            },
            initSelection: function (element, callback) {
                return $.post($("#product-name-datasource").val(), { id: element.val() }, function (data) {
                    callback(data);
                });
            }
        },
        success: function () {
            var row = $(this).parent().parent();
            row.find(".unit-price").val(localStorage.getItem("unitPrice"));
            row.find(".product-id").val(localStorage.getItem("productId"));
            row.find(".short-description").html(localStorage.getItem("shortDescription"));
            calculateCashForProductRow(row);
            calculateTotalCash();
        }
    });
}

function calculateCashForProductRow(container) {
    var unitPrice = container.find(".unit-price");
    var quantity = container.find(".quantity");
    if (quantity.val() == "") {
        quantity.val("1");
    }
    var productCash = container.find(".product-cash");
    var result = parseInt(unitPrice.val()) * parseInt(quantity.val());
    productCash.data("value", result);
    productCash.val(result.toLocaleString("en"));
}

function calculateTotalCash() {
    var totalCash = 0;
    var discount = 0;
    $("tr.product-order-row").each(function() {
        if ($(this).find(".product-id").val() != "") {
            totalCash = totalCash + parseInt($(this).find(".product-cash").data("value"));
        }
    });
    if ($("#discount").val() != "") {
        if ($("#discount-type").val() == "0") {
            discount = totalCash * parseInt($("#discount").val()) / 100;
        } else {
            discount = parseInt($("#discount").val());
        }
        $("#total-discount").html(discount.toLocaleString("en"));
    }
    $("#total-cash").html(totalCash.toLocaleString("en"));
    $("#final-cash").html((totalCash-discount).toLocaleString("en"));
}
