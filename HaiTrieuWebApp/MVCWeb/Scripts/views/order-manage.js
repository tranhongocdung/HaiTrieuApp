$(document).ready(function () {
    initSearchBox();
});

function initSearchBox() {
    $("#txtCustomer").bootcomplete({
        url: $("#customer-suggestion-datasource").val(),
        onSelect: function (id, label) {
            $("#txtCustomer").val("");
            alert(id + " - " + label);
        }
    });
}