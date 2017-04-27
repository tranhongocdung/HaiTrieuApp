$(document).ready(function () {
    initSearchBox();
});

function initSearchBox() {
    var customers = new Bloodhound({
        datumTokenizer: function (datum) {
            return Bloodhound.tokenizers.whitespace(datum.value);
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
        typeaheadjs: {
            displayKey: "SuggestName",
            source: customers
        }
    });
}