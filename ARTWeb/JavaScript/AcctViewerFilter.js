
function AcctFltrValidateAccountRange(source, arguments) {
    //alert(source.getAttribute("toTxtClientID"));
    var txtTo = $get(source.getAttribute("toTxtClientID"));
    var txtFrom = $get(source.getAttribute("fromTxtClientID"));
    if (txtFrom != null && txtTo != null)
        var val = txtFrom.value + txtTo.value;
    if (val == null || val == '')
        arguments.IsValid = false;

}
function ValidateNumberRange(source, arguments) {
    var txtTo = $get(source.getAttribute("toTxtClientID"));
    var txtFrom = $get(source.getAttribute("fromTxtClientID"));
    var txtFromValue;
    var txtToValue;

    if (txtFrom != null) {
        txtFromValue = txtFrom.value;
        if (isNaN(txtFromValue)) {
            arguments.IsValid = false;
            return;
        }
        else
            arguments.IsValid = true;
    }
    if (txtTo != null) {
        txtToValue = txtTo.value;
        if (isNaN(txtToValue))
            arguments.IsValid = false;
        else
            arguments.IsValid = true;
    }
}