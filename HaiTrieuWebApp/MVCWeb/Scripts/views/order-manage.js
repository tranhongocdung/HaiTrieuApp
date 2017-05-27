var table;

var tableDefOptions = {
    "bLengthChange": false,
    "info": false,
    "bFilter": false,
    "bDestroy": true,
    "order": false,
    "bPaginate": false,
    "dom": "<\"search\"f><\"top\"l>rt<\"bottom\"ip><\"clear\">",
    "aoColumnDefs": [
            { 'bSortable': false, 'aTargets': [0, 1, 2, 3, 4, 5, 6, 7] }],
    "fnDrawCallback": function (o) {
        $(".dataTables_scrollBody").scrollTop(0);
    }
};

$(document).ready(function () {
    initSearchBox();
    var options = $.extend({}, tableDefOptions);
    table = $("#order-table").DataTable(options);
    initCompleteOrderButton();
});

$(document).on("click", "#order-table td.details-control", function() {
    var tr = $(this).closest("tr");
    var row = table.row(tr);

    if (row.child.isShown()) {
        row.child.hide();
        tr.removeClass("shown");
    } else {
        var extendTable = $("#extend-for-search").clone();
        extendTable.removeAttr("id");
        extendTable.removeClass("hidden");
        $("tbody tr[data-order-id!='" + tr.data("order-id") + "']", extendTable).remove();
        row.child(extendTable[0].outerHTML).show();
        tr.addClass("shown");
    }
}).on("click", "#btnSubmit", function () {
    $("#page").val("1");
    $("#frmOrderManage").submit();
});

function initCompleteOrderButton() {
    $(".complete-order").confirmation({
        singleton: true,
        onConfirm: function () {
            var that = $(this);
            $.ajax({
                url: $("#complete-order-url").val(),
                data: {
                    id: that.closest("tr").data("order-id")
                },
                beforeSend: function () {
                    that.find("span").remove();
                    that.find("img").show();
                },
                success: function () {
                    that.closest("tr").addClass("table-success");
                    that.remove();
                }
            });
        },
        placement: "left",
        title: "Hoàn tất đơn này?"
    });
}

function initSearchBox() {
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
    $("#txtCustomerIds").tagsinput({
        itemValue: "Id",
        itemText: "CustomerName",
        typeaheadjs: [{
            hint: true,
            highlight: true,
            minLength: 1
        }, {
        displayKey: "SuggestNameFull",
        name: "customers",
        source: customers
        }]
    });
}

function orderManageCallBack(result) {
    $("#manager-content").html(result);
    var options = $.extend({}, tableDefOptions);
    table = $("#order-table").DataTable(options);
    initCompleteOrderButton();
}