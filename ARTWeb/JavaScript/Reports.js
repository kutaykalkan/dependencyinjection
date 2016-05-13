
//var chkBoxList = $get('<% =this.chklstReasonCode.ClientID %>');
//var chkSelectAll = $get('<% =this.chkAll.ClientID %>');

function SelectUnselectAll(src, checkBoxListID) {
    var checkedAllStatus = src.checked;
    SelectUnselectAllCheckBoxList(checkedAllStatus, checkBoxListID);

}
function SelectUnselectAllCheckBoxList(state, checkBoxListID) {
    var chkBoxList = $get(checkBoxListID);
    if (chkBoxList != null) {
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
    }

    return false;
}
function CheckTest(checkBoxListID, chkSelectAllID) {
    var allChecked;
    var chkBoxList = $get(checkBoxListID);
    var chkSelectAll = $get(chkSelectAllID);
    if (chkBoxList != null) {
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            if (chkBoxCount[i].checked == false) {
                chkSelectAll.checked = false;
                return;
            }
        }
        chkSelectAll.checked = true;
    }
}

function validateOptions(source, arguments) {
    var attr = source.getAttribute("controltocheck");
    if (attr != null) {
        var chkBoxList = $get(attr);
        if (chkBoxList != null) {
            arguments.IsValid = CheckBoxListHasSelectedItems(chkBoxList);
        }
    }
}

function CheckBoxListHasSelectedItems(checkBoxList) {
    if (checkBoxList != null) {
        var checkBoxList = checkBoxList.getElementsByTagName("input");
        for (var i = 0; i < checkBoxList.length; i++) {
            if (checkBoxList[i].checked == true) {
                return true;
            }
        }
    }
    return false;
}