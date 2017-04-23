$.fn.editable.defaults.mode = "inline";

$(document).ready(function() {
    initXEditable();
    initAddProductRowButton();
    initRemoveProductRowButton();
});

function initAddProductRowButton() {
    $("#add-product-row").click(function() {
        var productRow = "<td class=\"text-center count-no\"> 1 </td><td> <a href=\"#\" class=\"product-name\" data-type=\"select2\" data-value=\"0\">Chọn sản phẩm</a> </td><td> <input type=\"text\" placeholder=\"Đơn giá\" class=\"form-control unit-price\"/> </td><td> <input type=\"text\" placeholder=\"Số lượng\" class=\"form-control quantity\"/> </td><td> <input type=\"text\" placeholder=\"Thành tiền\" class=\"form-control totalcash\" readonly=\"readonly\"/> </td><td> <button class=\"btn btn-danger btn-sm remove-row\"><span class=\"glyphicon glyphicon-remove\"></span></button> </td>";
        var appendRow = $("#tr-for-append");
        appendRow.append(productRow);
        appendRow.removeAttr("id");
        initXEditable(appendRow);
        initRemoveProductRowButton(appendRow);
        $("#product-row-container").append("<tr id=\"tr-for-append\"></tr>");
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
                url: "/datasource/getproductname",
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
                return item.ProductName;
            },
            initSelection: function (element, callback) {
                return $.post("/datasource/getproductname", { id: element.val() }, function (data) {
                    callback(data);
                });
            }
        },
        success: function () {
            var row = $(this).parent().parent();
            row.find(".unit-price").val(localStorage.getItem("unitPrice"));
            calculateTotalCash(row);
        }
    });
}

function calculateTotalCash(container) {
    var unitPrice = container.find(".unit-price");
    var quantity = container.find(".quantity");
    if (quantity.val() == "") {
        quantity.val("1");
    }
    var totalCash = container.find(".totalcash");
    var result = parseInt(unitPrice.val()) * parseInt(quantity.val());
    totalCash.val(result.toLocaleString("en"));
}
