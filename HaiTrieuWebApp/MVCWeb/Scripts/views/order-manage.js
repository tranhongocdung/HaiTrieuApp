$(document).ready(function () {
    initSearchBox();
});

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
    $("#txtCustomer").tagsinput({
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
}