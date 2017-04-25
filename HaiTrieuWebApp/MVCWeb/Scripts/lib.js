function showMessage(message, messagetype) {
    var cssclass;
    switch (messagetype) {
        case "success":
            cssclass = "alert-success";
            break;
        case "error":
            cssclass = "alert-danger";
            break;
        case "warning":
            cssclass = "alert-warning";
            break;
        default:
            cssclass = "alert-info";
    }
    $("#alert-container").append("<div id=\"alert-div\" class=\"alert fade in text-center " + cssclass + "\"><a href=\"#\" class=\"close\" data-dismiss=\"alert\" aria-label=\"close\">&times;</a><span>" + message + "</span></div>");

    setTimeout(function () {
        $("#alert-div").fadeTo(2000, 500).slideUp(500, function () {
            $("#alert-div").remove();
        });
    }, 2000);
}