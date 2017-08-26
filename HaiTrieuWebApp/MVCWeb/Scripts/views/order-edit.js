$.fn.editable.defaults.mode = "inline";

$(document).ready(function () {
    initXEditable();
    initAddProductRowButton();
    initRemoveProductRowButton();
    numberProductRow();
    initNumericTextbox(".unit-price");
    initNumericTextbox(".quantity");
    initNumericTextbox("#discount-value");
    initDiscountTypeOnChange();
    initSearchCustomerTextbox();
    initCustomerTypeToggle();
    initSubmitButton();
    initExistingOrder();
    initOrderNoteHint();
});

function destroyRegionAreaHint() {
    $("#txtRegion").typeahead("destroy");
    $("#txtArea").typeahead("destroy");
}

function initRegionAreaHint() {
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
}

function initOrderNoteHint() {
    $("#order-note-hint").change(function() {
        $("#txtOrderNote").val($(this).val());
    });
}

function initExistingOrder() {
    $("#txtCustomerNote").summernote({
        toolbar: false,
        height: 90
    });
    if ($("#order-id").val() != "") {
        $(".customer-info").attr("readonly", "readonly");
        $("#chkCustomerType").bootstrapToggle("off");
        $("#chkCustomerType").bootstrapToggle("disable");
        /*$("#txtCustomerNote").summernote("disable");*/
        $(".product-order-row").each(function() {
            calculateCashForProductRow($(this));
        });
        $("#discount-type").val($("#order-discount-type").val());
        $("#discount-value").val($("#order-discount-value").val());
        calculateTotalCash();
        $("#btnPrint").click(function() {
            window.open($("#print-url").val() + "?id=" + $("#order-id").val(), "popupWindow", "width=840, height=625, scrollbars=yes");
        });

        $("#btnComplete").confirmation({
            singleton: true,
            onConfirm: function() {
                $("#orderEditLoading").show();
                $.ajax({
                    url: $("#complete-order-url").val(),
                    data: {
                        id: $("#order-id").val()
                    },
                    beforeSend: function() {
                        $("#orderEditLoading").show();
                    },
                    success: function() {
                        location.reload();
                    }
                });
            },
            placement: "left",
            title: "Không thể sửa đơn hàng đã hoàn tất, chắc không?"
        });

    } else {
        /*$("#txtCustomerNote").summernote("enable");*/
        initRegionAreaHint();
    }
}

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
            $("#txtCustomerName").val("");
            $("#txtPhoneNo").val("");
            $("#txtEmail").val("");
            $("#txtAddress").val("");
            $("#txtRegion").val("");
            $("#txtArea").val("");
            $("#txtCustomerNote").val("");
            $("#txtCustomerNote").summernote("code","");
            /*$("#txtCustomerNote").summernote("enable");*/
            $(".customer-info").removeAttr("readonly", "readonly");
            $("#chkCustomerType").bootstrapToggle("disable");
            initRegionAreaHint();
        }
    });
}

function initSearchCustomerTextbox() {
    var customers = new Bloodhound({
        datumTokenizer: function (datum) {
            return Bloodhound.tokenizers.whitespace(datum.SuggestNameFull);
        },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            wildcard: "%QUERY",
            url: $("#customer-suggestion-datasource").val() + "?query=%QUERY",
            transform: function (customers) {
                return $.map(customers, function (customer) {
                    return customer;
                });
            }
        }
    });

    $("#txtSearchCustomer").typeahead({
        hint: true,
        highlight: true,
        minLength: 1
    }, {
        displayKey: "SuggestNameFull",
        name: "customers",
        source: customers
    });
    $("#txtSearchCustomer").bind("typeahead:selected", function(obj, datum) {
        loadCustomerDetail(datum);
        $("#chkCustomerType").bootstrapToggle("enable");
        $("#chkCustomerType").bootstrapToggle("off");
        /*$("#txtCustomerNote").summernote("disable");*/
        $(this).typeahead("val", "");
        destroyRegionAreaHint();
    });
}

function loadCustomerDetail(data) {
    $("#customer-id").val(data.Id);
    $("#txtCustomerName").val(data.CustomerName);
    $("#txtPhoneNo").val(data.PhoneNo);
    $("#txtEmail").val(data.Email);
    $("#txtAddress").val(data.Address);
    $("#txtRegion").val(data.Region);
    $("#txtArea").val(data.Area);
    $("#txtCustomerNote").summernote("code", data.Note);
    $(".customer-info").attr("readonly", "readonly");
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
        var productRow = "<td class=\"text-center count-no\"></td><td><a href=\"#\" class=\"product-name\" data-value=\"0\">Chọn sản phẩm</a><input type=\"hidden\" class=\"product-id\"/><input type=\"hidden\" class=\"order-detail-id\" value=\"0\"/></td><td class=\"text-center\"><span class=\"text-danger short-description\"></span></td><td><input type=\"text\" placeholder=\"Mô tả\" class=\"form-control product-note\"/></td><td><input type=\"text\" placeholder=\"Giá gốc\" class=\"form-control original-price\" readonly=\"readonly\"/></td><td><input type=\"text\" placeholder=\"Đơn giá\" class=\"form-control unit-price\"/></td><td><input type=\"text\" placeholder=\"Số lượng\" class=\"form-control quantity\"/></td><td><input type=\"text\" placeholder=\"Thành tiền\" class=\"form-control product-cash\" readonly=\"readonly\"/></td><td><button class=\"btn btn-danger btn-sm remove-row\" data-toggle=\"confirmation\"><span class=\"glyphicon glyphicon-remove\"></span></button></td>";
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
    container.find(".remove-row").confirmation({
        singleton: true,
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
                localStorage.setItem("originalPrice", item.OriginalPrice);
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
            row.find(".original-price").val(localStorage.getItem("originalPrice"));
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
    if ($("#discount-value").val() != "") {
        if ($("#discount-type").val() == "0") {
            discount = totalCash * parseInt($("#discount-value").val()) / 100;
        } else {
            discount = parseInt($("#discount-value").val());
        }
        $("#total-discount").html(discount.toLocaleString("en"));
    }
    $("#total-cash").html(totalCash.toLocaleString("en"));
    $("#final-cash").html((totalCash-discount).toLocaleString("en"));
}

function initSubmitButton() {
    $("#btnSubmit").click(function () {
        if (validateOrderBeforeSend())
            $("#frmOrderEdit").submit();
    });
}

function validateOrderBeforeSend() {
    if ($("#customer-id").val() == "" && $("#txtCustomerName").val().trim() == "") {
        showMessage("Vui lòng chọn khách hàng hoặc nhập ít nhất tên khách hàng!", "error");
        return false;
    }
    $("#order-discount-type").val($("#discount-type").val());
    $("#order-discount-value").val($("#discount-value").val());
    var orderDetails = [];
    $(".product-order-row").each(function() {
        var obj = $(this);
        if (obj.find(".product-id").val() != "" && obj.find(".unit-price").val() != "" && obj.find(".quantity").val() != "") {
            orderDetails.push({
                Id: obj.find(".order-detail-id").val(),
                ProductId: obj.find(".product-id").val(),
                UnitPrice: obj.find(".unit-price").val(),
                Quantity: obj.find(".quantity").val(),
                Note: obj.find(".product-note").val()
            });
        }
    });
    if (orderDetails.length > 0) {
        $("#order-detail-json").val(JSON.stringify(orderDetails));
    } else {
        $("#order-detail-json").val("");
    }
    return true;
}

function orderEditCallBack(result) {
    if (result == "")
        showMessage("Đã lưu đơn hàng thành công!", "success");
    else {
        $("#order-id").val(result);
        window.location = location.protocol + "//" + location.host + location.pathname + "?id=" + result;
    }
}
