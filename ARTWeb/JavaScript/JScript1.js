
var char; // global variable
var count = 0;
var RAD_WINDOW_CLOSE_EVENT_HANDLER;

var valSummaryElement;
var XmlHttp = null;

// Javascript Dictionary

function Hash() {
    for (var i = 0; i < arguments.length; i++)
        for (n in arguments[i])
            if (arguments[i].hasOwnProperty(n))
                this[n] = arguments[i][n];
}

// Hash.version = 1.00;	// Original version
// Hash.version = 1.01;	// Added ability to initialize in the constructor
// Hash.version = 1.02;	// Fixed document bug that showed a non-working example (thanks mareks)
//Hash.version = 1.03;	// Removed returning this from the constructor (thanks em-dash)
Hash.version = 1.04; // Missed some 'var' declarations (thanks Twey)


Hash.prototype = new Object();

Hash.prototype.keys = function () {
    var rv = [];
    for (var n in this)
        if (this.hasOwnProperty(n))
            rv.push(n);
    return rv;
}

Hash.prototype.length = function () {
    return this.keys().length();
}

Hash.prototype.values = function () {
    var rv = [];
    for (var n in this)
        if (this.hasOwnProperty(n))
            rv.push(this[n]);
    return rv;
}

Hash.prototype.slice = function () {
    var rv = [];
    for (var i = 0; i < arguments.length; i++)
        rv.push(
							(this.hasOwnProperty(arguments[i]))
								? this[arguments[i]]
								: undefined
						);
    return rv;
}

Hash.prototype.concat = function () {
    for (var i = 0; i < arguments.length; i++)
        for (var n in arguments[i])
            if (arguments[i].hasOwnProperty(n))
                this[n] = arguments[i][n];
    return this;
}


//

function SetRadWindowCloseEventHandler(obj) {
    RAD_WINDOW_CLOSE_EVENT_HANDLER = obj;
}

function SetValElement(obj) {
    valSummaryElement = obj;
}

//function CheckWebsite(obj) {
//    var text = document.getElementById(obj);
//    if (text != null && text.value != "") {
//        window.open("http://" + text.value + "/");
//        //window.setTimeout("delayit()", 3000);
//    }
//}

function delayit() {

    window.open("http://" + char + "/");
}
function calDate(obj) {
    var array = obj.split("/");
    var text = document.getElementById(array[0]);
    var calender = document.getElementById(array[1]);
    var calenderstartdate = document.getElementById(array[2]);
    calEndDate(text, calenderstartdate, calender);
}

function calEndDate(txtDays, calStartDT, calEndDT) {
    if (txtDays.value != "" && calStartDT.value != ""
        && IsPositiveInteger(txtDays)) {
        var dt1 = GetDateBasedOnDateFormat(calStartDT.value);
        var dt2 = dt1;
        dt2.setDate(dt1.getDate() + parseInt(txtDays.value));
        calEndDT.value = ConvertJavascriptDateFormat(dt2);
    }
    else {
        calEndDT.value = "";
    }
}

function calNoDays(txtNumOfDaysID, calSubscriptionEndDateID, calSubscriptionStartDateID) {

    var text = document.getElementById(txtNumOfDaysID);
    var calenderEndDate = document.getElementById(calSubscriptionEndDateID);

    var calenderStartDate = document.getElementById(calSubscriptionStartDateID);

    if (calenderStartDate.value != ""
        && calenderEndDate.value != "") {
        var startDate = null;
        var endDate = null;
        if (IsDate(calenderStartDate.value)) {
            startDate = getDateFromFormat(calenderStartDate.value);
        }

        if (IsDate(calenderEndDate.value)) {
            endDate = getDateFromFormat(calenderEndDate.value) + 1;
        }

        text.value = "";
        if (startDate != null && endDate != null) {
            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
            var diffDays = Math.ceil((endDate - startDate) / (oneDay));

            if (typeof (diffDays) != "undefined"
            && !isNaN(diffDays)) {
                text.value = diffDays;
            }
        }
    }
    else
        calEndDate(text, calenderStartDate, calenderEndDate);
}

function GetDaysBetweenDateRanges(startDate, endDate) {

    // The number of milliseconds in one day
    var ONE_DAY = 1000 * 60 * 60 * 24

    // Add One Day to account for both dates to be inclusive
    endDate = endDate + 1;

    // Calculate the difference in milliseconds and divide by one day
    var diffDays = Math.ceil((endDate - startDate) / (ONE_DAY));

    return diffDays;
}

(function (amortizeInterval, window, document, $, undefined) {    
    amortizeInterval.numberOfPeriodsBetweenDates = function (startDate, endDate, recPeriods) {
        var periods = "";
        if (CompareDates(endDate, startDate) >= 0) {
            var startDateFormat = GetDateBasedOnDateFormat(startDate); //new Date(getDateFromFormat(startDate, DATE_FORMAT));
            var endDateFormat = GetDateBasedOnDateFormat(endDate);

            // FD Ticket 1118
            var firstRecPeriod = GetDateBasedOnDateFormat(recPeriods[0]);
            var lastRecPeriod = GetDateBasedOnDateFormat(recPeriods[recPeriods.length - 1]);

            if (startDateFormat < firstRecPeriod || endDateFormat > lastRecPeriod) {
                return GetMonthsBetweenDateRanges(endDate, startDate);
            }
            
            for (var i = 0; i < recPeriods.length; i++) {
                var recPeriod = GetDateBasedOnDateFormat(recPeriods[i]);

                if (recPeriod >= startDateFormat && recPeriod <= endDateFormat) {
                    periods++;
                }
            }                                
        }
        return periods;
    };
}(window.amortizeInterval = window.amortizeInterval || {}, window, document, jQuery));

// Get the number of months d2-d1
function GetMonthsBetweenDateRanges(d2, d1) {

    var diffMonths = "";
    if (CompareDates(d2, d1) >= 0) {
        // The number of milliseconds in one day
        var endDate = GetDateBasedOnDateFormat(d2); //new Date(getDateFromFormat(d2, DATE_FORMAT));
        var startDate = GetDateBasedOnDateFormat(d1); //new Date(getDateFromFormat(d1, DATE_FORMAT));

        var sm = startDate.getMonth() + 1;
        var sy = startDate.getFullYear();
        var em = endDate.getMonth() + 1;
        var ey = endDate.getFullYear();

        // Calculate the difference in milliseconds and divide by one day
        diffMonths = (ey - sy) * 12 + em - sm + 1;
    }
    return diffMonths;
}


function addMore(obj) {
    var array = obj.split("|");
    var ddlGeography = document.getElementById(array[0]);
    var txtGeography = document.getElementById(array[1]);
    var tblSearchCommand = document.getElementById(array[2]);
    var hdnOrgHierarchy = document.getElementById(array[3]);
    var hdnTableInnerHTML = document.getElementById(array[4]);
    var url = array[5];

    if (ddlGeography.children.length > 0 && txtGeography.value != null && txtGeography.value != '')
        addRow(tblSearchCommand, ddlGeography, txtGeography, hdnOrgHierarchy, hdnTableInnerHTML, url);
    return false;
}

function addRow(tableID, ddlGeography, txtGeography, hdnOrgHierarchy, hdnTableInnerHTML, url) {


    var optiontext = ddlGeography.options[ddlGeography.selectedIndex].text;
    var optionValue = ddlGeography.options[ddlGeography.selectedIndex].value;
    var ContainsText = "Contains"
    var selectedIndex = ddlGeography.selectedIndex;
    var selectedText = ddlGeography.options[ddlGeography.selectedIndex].text;

    if (optionValue > 0) {

        var newRow = "<tr style=\"vertical-align: top;\" >";
        newRow = newRow + "<td style=\"visibility: hidden;\" width=\"5px\">" + optionValue + "</td>";
        newRow = newRow + "<td width=\"80px\">" + optiontext + "</td>";
        newRow = newRow + "<td width=\"80px\">" + ContainsText + "</td>";
        newRow = newRow + "<td width=\"55px\">" + txtGeography.value + "</td>";
        newRow = newRow + "<td width=\"15px\"><input type='image' src='" + url + "' " +
         "alt='Delete' value='Delete' onclick='removeRow(this,\"" + tableID.id + "\",\"" +
        ddlGeography.id + "\",\"" + hdnOrgHierarchy.id + "\",\"" + hdnTableInnerHTML.id + "\");'/>" + "</td>";
        newRow = newRow + "</tr>";
        $(tableID).append(newRow);

        //        var newRow = document.createElement("tr");
        //        //var newRow = tableID.insertRow();
        //        newRow.style.verticalAlign = 'top';
        //       // var oCell = newRow.insertCell();
        //        var oCell = document.createElement("td");
        //        oCell.width = '5px';
        //        oCell.innerHTML = optionValue;
        //        oCell.style.visibility = 'hidden';
        //        newRow.appendChild(oCell);
        //        //oCell = newRow.insertCell();
        //        oCell = document.createElement("td");
        //        oCell.width = '80px';
        //        oCell.innerHTML = optiontext;
        //        newRow.appendChild(oCell);
        //        //oCell = newRow.insertCell();
        //        oCell = document.createElement("td");
        //        oCell.width = '55px';
        //        oCell.innerHTML = txtGeography.value;
        //        newRow.appendChild(oCell);
        //        oCell = document.createElement("td");
        //        //oCell = newRow.insertCell();
        //        oCell.width = '15px';
        //        oCell.innerHTML = "<input type='image' src='../App_Themes/SkyStemBlueBrown/Images/Delete.gif' " +
        //         "alt='Delete' value='Delete' onclick='removeRow(this,\"" + tableID.id + "\",\"" +
        //        ddlGeography.id + "\",\"" + hdnOrgHierarchy.id + "\",\"" + hdnTableInnerHTML.id + "\");'/>";
        //        newRow.appendChild(oCell);
        //tableID.appendChild(newRow);
        //newRow.appendChild(oCell);

        //insert geography's selected value in a hidden control
        //which will be used to repopulate the drop down if option is deleted.

        hdnOrgHierarchy.value += selectedText + "/" + optionValue + "/";
        hdnTableInnerHTML.value += optionValue + "/" + optiontext + "/" + ContainsText + "/" + txtGeography.value + "/";

        ddlGeography.removeChild(ddlGeography.options[selectedIndex]);
        ddlGeography.selectedIndex = 0;
        txtGeography.value = '';
    }
    return false;
}

function removeRow(src, tableID, ddlGeography, hdnOrgHierarchy, hdnTableInnerHTML) {

    var ddlGeography = document.getElementById(ddlGeography);
    var hdnTableInnerHTML = document.getElementById(hdnTableInnerHTML);
    var oRow = src.parentNode.parentNode;

    var optn = document.createElement("OPTION");
    //    var optionText = ((oRow.cells[1].innerHTML).split(" "))[0];
    var optionText = oRow.cells[1].innerHTML;
    var optionValue = GetOrganizationalHierarchyOptionValue(optionText, hdnOrgHierarchy);
    $(ddlGeography).append(optn);
    optn.text = optionText;
    optn.value = optionValue;

    //ddlGeography.options.add(optn);



    //once the row reference is obtained, delete it passing in its rowIndex
    $(src).parent().parent().remove();
    //tableID.deleteRow(oRow.rowIndex);

    //Remove options from orgab\nizational hierarchy hidden control

    //Remove text from table inner html hidden control
    var innerHTML = hdnTableInnerHTML.value;
    var indexOfSlashAfterOptionText = innerHTML.indexOf("/", innerHTML.indexOf(optionText));
    var indexOfContainsSlash = innerHTML.indexOf("/", indexOfSlashAfterOptionText + 1);
    var indexOfLastSlash = innerHTML.indexOf("/", indexOfContainsSlash + 1);
    if (innerHTML.indexOf(optionText) >= 0)
        var newOptionsString = innerHTML.substring(0, innerHTML.indexOf(optionValue))
             + innerHTML.substring(indexOfLastSlash + 1, innerHTML.length);

    hdnTableInnerHTML.value = newOptionsString;
}

function GetOrganizationalHierarchyOptionValue(optionText, hdnOrgHierarchy) {

    var hdnOrgHierarchy = document.getElementById(hdnOrgHierarchy);
    var optionsAndValueCollection = hdnOrgHierarchy.value.split("/");
    var hdnValue = hdnOrgHierarchy.value.toString();
    var indexOfOption = hdnValue.indexOf(optionText);

    var optionValue;

    for (var index = 0; index < optionsAndValueCollection.length; index++) {
        if (optionsAndValueCollection[index] == optionText) {
            optionValue = optionsAndValueCollection[index + 1];
            hdnOrgHierarchy.value = hdnValue.substring(0, indexOfOption) +
                    hdnValue.substring(indexOfOption + optionText.length + 2 + optionValue.length, hdnOrgHierarchy.value.length);
            break;
        }
    }

    return optionValue;
}

function openDocumentWindow(button) {
    var btn = document.getElementById(button.id);
    if (btn != null) {
        var url = btn.URL;
        var windowName = btn.WindowName;
        var WinSettings = "center:yes;resizable:no;dialogHeight:400px"
        var MyArgs = window.showModalDialog(url, windowName, WinSettings);
        //window.open(url, "", "", true);
    }

    return false;
}

function openReviewNotesWindow(button) {
    var btn = document.getElementById(button.id);
    if (btn != null) {
        var ParmA = btn.GLDataID;
        var url = btn.URL;
        var MyArgs = new Array(ParmA);
        var WinSettings = "center:yes;resizable:no;dialogHeight:400px"
        var MyArgs = window.showModalDialog(url, MyArgs, WinSettings);
    }

    return false;
}

var DELIMITER_ORG_MAPPING = "       =       ";
var DELIMITER_ORG_MAPPING_FOR_SPLIT = "=";

function MapOrganizationalHierarchy(lstHierarchyKeysID, lstHierarchyNameID, lstMappingID) {
    var lstHierarchyKeys = document.getElementById(lstHierarchyKeysID);
    var lstHierarchyName = document.getElementById(lstHierarchyNameID);
    var lstMapping = document.getElementById(lstMappingID);

    // TODO: Apoorv - Uncomment later on
    //    if (lstHierarchyKeys.value == "" || lstHierarchyName.value == "")
    //    {
    //        return;
    //    }
    //
    try {
        var keyName = "";
        var keyValue = "";

        var i = 0;
        var j = 0;
        for (i = 0; i < lstHierarchyKeys.options.length; i++) {
            if (lstHierarchyKeys.options[i].selected) {
                keyName = lstHierarchyKeys.options[i].text;
                keyValue = lstHierarchyKeys.options[i].value;
                break;
            }
        }

        var hierarchyName = "";
        var hierarchyValue = "";
        for (j = 0; j < lstHierarchyName.options.length; j++) {
            if (lstHierarchyName.options[j].selected) {
                hierarchyName = lstHierarchyName.options[j].text;
                hierarchyValue = lstHierarchyName.options[j].value;
                break;
            }
        }

        if (keyName != "" && hierarchyName != "") {
            var listItem = document.createElement("option");
            listItem.text = keyName + DELIMITER_ORG_MAPPING + hierarchyName;
            listItem.value = keyValue;
            lstMapping.options.add(listItem);

            // Remove from other lists
            lstHierarchyKeys.removeChild(lstHierarchyKeys.options[i]);
            lstHierarchyName.removeChild(lstHierarchyName.options[j]);
        }
    }
    catch (errorObject) {
        alert("Error Occurred - " + errorObject.description);
    }
}

function DeleteMapping(lstHierarchyKeysID, lstHierarchyNameID, lstMappingID) {
    var lstHierarchyKeys = document.getElementById(lstHierarchyKeysID);
    var lstHierarchyName = document.getElementById(lstHierarchyNameID);
    var lstMapping = document.getElementById(lstMappingID);

    // TODO: Apoorv - Uncomment later on
    //    if (lstMapping.value == "")
    //    {
    //        return;
    //    }

    //debugger;
    try {
        var keyName = "";
        var keyValue = "";
        var hierarchyName = "";
        var hierarchyValue = "";
        var mappingName = "";
        var mappingValue = "";

        var i = 0;
        var j = 0;
        for (i = 0; i < lstMapping.options.length; i++) {
            if (lstMapping.options[i].selected) {
                mappingName = lstMapping.options[i].text;
                mappingValue = lstMapping.options[i].value;

                // Add for Firefox handling, firefox ignores spaces on both sides of "   =   " giving error
                // hence just split on "=" and do a trim
                var str = mappingName.split(DELIMITER_ORG_MAPPING_FOR_SPLIT);
                keyName = $.trim(str[0]);
                hierarchyName = $.trim(str[1]);

                var listItem = document.createElement("option");
                listItem.text = keyName;
                listItem.value = mappingValue;
                lstHierarchyKeys.options.add(listItem);

                listItem = document.createElement("option");
                listItem.text = hierarchyName;
                listItem.value = hierarchyName;
                lstHierarchyName.options.add(listItem);

                lstMapping.removeChild(lstMapping.options[i]);
                i--;
            }
        }


    }
    catch (errorObject) {
        alert("Error Occurred - " + errorObject.description);
    }

}

function ShowFSCaptionProgressIcon() {
    var img = document.getElementById("imgFSCaptionProgress");
    img.style.visibility = (img.style.visibility == 'visible') ? 'hidden' : 'visible';
}

function ShowUserNameProgressIcon() {
    var img = document.getElementById("imgUserNameProgress");
    img.style.visibility = (img.style.visibility == 'visible') ? 'hidden' : 'visible';
}

function HideProgressBar(txtID) {
    var img = document.getElementById("imgFSCaptionProgress");
    img.style.visibility = 'hidden';

    img = document.getElementById("imgUserNameProgress");
    img.style.visibility = 'hidden';

    var txtValue = document.getElementById(txtID);

    if (txtValue.value == "No Records found")
        txtValue.value = "";
}

function LimitTextarea(textBoxID, maxlimit) {
    var textBox = window.document.getElementById(textBoxID);
    if (textBox) {
        if (textBox.value.length > maxlimit) // if too long...trim it!
            textBox.value = textBox.value.substring(0, maxlimit);
    }
}


//
function SelectRoles(lstUserRoles, lstSelectedUserRoles, obj) {

    var lstUserRoles = document.getElementById(lstUserRoles);
    var lstSelectedUserRoles = document.getElementById(lstSelectedUserRoles);
    var parentControl = document.getElementById(parentControl);
    var checkCase = obj;
    var arrayvalue = new Array();
    switch (obj) {
        case "1":
            // TODO: Apoorv - Uncomment later on
            //    if (lstHierarchyKeys.value == "" || lstHierarchyName.value == "")
            //    {
            //        return;
            //    }
            //    
            try {
                var keyName = "";
                var keyValue = "";

                var i = 0;
                var j = 0;
                for (i = 0; i < lstUserRoles.options.length; i++) {

                    if (lstUserRoles.options[i].selected) {
                        keyName = lstUserRoles.options[i].text;
                        keyValue = lstUserRoles.options[i].value;
                        var listItem = document.createElement("option");
                        listItem.text = keyName;
                        listItem.value = keyValue;
                        lstSelectedUserRoles.options.add(listItem);
                        var array = new Array();
                        array[array.length] = i;
                        arrayvalue[array.length] = listItem;
                        //lstUserRoles.removeChild(lstUserRoles.options[i]);

                    }
                }
                for (i = 0; i < lstSelectedUserRoles.options.length; i++) {
                    for (j = 0; j < lstUserRoles.options.length; j++) {
                        if (lstUserRoles.options[j].value == lstSelectedUserRoles.options[i].value) {
                            lstUserRoles.removeChild(lstUserRoles.options[j]);
                            break;
                        }
                    }
                }
            }

            catch (errorObject) {
                alert("Error Occurred - " + errorObject.description);
            }
            break;


        case "2":
            // TODO: Apoorv - Uncomment later on
            //    if (lstHierarchyKeys.value == "" || lstHierarchyName.value == "")
            //    {
            //        return;
            //    }
            //    
            try {
                var keyName = "";
                var keyValue = "";

                var i = 0;
                var j = 0;
                for (i = 0; i < lstUserRoles.options.length; i++) {


                    keyName = lstUserRoles.options[i].text;
                    keyValue = lstUserRoles.options[i].value;
                    var listItem = document.createElement("option");
                    listItem.text = keyName;
                    listItem.value = keyValue;
                    lstSelectedUserRoles.options.add(listItem);
                    var array = new Array();
                    array[array.length] = i;
                    //lstUserRoles.removeChild(lstUserRoles.options[i]);


                }

                for (i = 0; i < lstSelectedUserRoles.options.length; i++) {
                    for (j = 0; j < lstUserRoles.options.length; j++) {

                        if (lstUserRoles.options[j].value == lstSelectedUserRoles.options[i].value) {
                            lstUserRoles.removeChild(lstUserRoles.options[j]);
                            break;
                        }
                    }

                }


            }

            catch (errorObject) {
                alert("Error Occurred - " + errorObject.description);
            }
            break;

        case "3":
            // TODO: Apoorv - Uncomment later on
            //    if (lstHierarchyKeys.value == "" || lstHierarchyName.value == "")
            //    {
            //        return;
            //    }
            //
            try {

                var keyName = "";
                var keyValue = "";

                var i = 0;
                var j = 0;
                for (i = 0; i < lstSelectedUserRoles.options.length; i++) {

                    if (lstSelectedUserRoles.options[i].selected) {
                        keyName = lstSelectedUserRoles.options[i].text;
                        keyValue = lstSelectedUserRoles.options[i].value;
                        var listItem = document.createElement("option");
                        listItem.text = keyName;
                        listItem.value = keyValue;
                        lstUserRoles.options.add(listItem);
                        var array = new Array();
                        array[array.length] = i;
                        lstSelectedUserRoles.removeChild(lstSelectedUserRoles.options[i]);


                    }
                }
                //                for (i = 0; i < lstUserRoles.options.length; i++) {
                //                    alert(lstUserRoles.options.length);
                //                    for (j = 0; j < lstSelectedUserRoles.options.length; j++) {
                //                        alert(lstSelectedUserRoles.options.length);
                //                        if (lstSelectedUserRoles.options[j].value == lstUserRoles.options[i].value) {
                //                            lstSelectedUserRoles.removeChild(lstSelectedUserRoles.options[j]);
                //                            alert(lstSelectedUserRoles.options[j].value);
                //                            break;
                //                        }
                //                    }

                //                }


            }

            catch (errorObject) {
                alert("Error Occurred - " + errorObject.description);
            }
            break;

        case "4":

            try {
                var keyName = "";
                var keyValue = "";

                var i = 0;
                var j = 0;
                for (i = 0; i < lstSelectedUserRoles.options.length; i++) {


                    keyName = lstSelectedUserRoles.options[i].text;
                    keyValue = lstSelectedUserRoles.options[i].value;
                    var listItem = document.createElement("option");
                    listItem.text = keyName;
                    listItem.value = keyValue;
                    lstUserRoles.options.add(listItem);
                    var array = new Array();
                    array[array.length] = i;
                    //lstUserRoles.removeChild(lstUserRoles.options[i]);


                }
                for (i = 0; i < lstUserRoles.options.length; i++) {
                    for (j = 0; j < lstSelectedUserRoles.options.length; j++) {

                        if (lstSelectedUserRoles.options[j].value == lstUserRoles.options[i].value) {
                            lstSelectedUserRoles.removeChild(lstSelectedUserRoles.options[j]);
                            break;
                        }
                    }


                }


            }

            catch (errorObject) {
                alert("Error Occurred - " + errorObject.description);
            }
            break;
    }

}

function Setdropdownlist(object, lstSelectedUserRoles, hdnItem, hdnValue, btnAccountAssociation, hdnDefaultRole) {

    var obj = document.getElementById(object);
    var hdnSelectItem = document.getElementById(hdnItem);
    var hdnDefaultRole = document.getElementById(hdnDefaultRole);
    var btnAccountAssociationCheck = document.getElementById(btnAccountAssociation);
    var hdnSelectValue = document.getElementById(hdnValue);
    var defaultRoleID = hdnDefaultRole.value;
    var bRoleSelected = false;

    hdnSelectValue.value = "";
    obj.length = 0;

    var lstSelectedUserRoles = document.getElementById(lstSelectedUserRoles);

    for (j = 0; j < lstSelectedUserRoles.options.length; j++) {
        var optn = document.createElement("OPTION");
        optn.text = lstSelectedUserRoles.options[j].text;
        optn.value = lstSelectedUserRoles.options[j].value;
        hdnSelectItem.value = lstSelectedUserRoles.options[j].text + ',';

        hdnSelectValue.value = hdnSelectValue.value + lstSelectedUserRoles.options[j].value + ',';

        if (defaultRoleID == optn.value) {
            optn.selected = true;
            bRoleSelected = true;
        }

        obj.options.add(optn);
    }


    //alert(bRoleSelected + ' = ' + obj.length);

    if (obj.length > 0) {
        if (!bRoleSelected) {
            hdnDefaultRole.value = obj.options[0].value;
        }
    }
    else {
        hdnDefaultRole.value = '';
    }
    $(hdnSelectValue).trigger("change");
    //alert('hdnDefaultRole = ' + hdnDefaultRole.value);

    if (btnAccountAssociationCheck != null) {
        for (j = 0; j < lstSelectedUserRoles.options.length; j++) {
            if (lstSelectedUserRoles.options[j].value == "10" || lstSelectedUserRoles.options[j].value == "6" || lstSelectedUserRoles.options[j].value == "7" || lstSelectedUserRoles.options[j].value == "8" || lstSelectedUserRoles.options[j].value == "9" || lstSelectedUserRoles.options[j].value == "11") {
                btnAccountAssociationCheck.style.visibility = "visible";
                break;
            }
            else {

                btnAccountAssociationCheck.style.visibility = "hidden";
            }
        }
    }

}


function SetHiddenValue(obj, hdnValue) {

    var obj = document.getElementById(obj);
    var hdnSelectItem = document.getElementById(hdnValue);
    hdnSelectItem.value = obj.options[obj.selectedIndex].value;

}

function OnRowCreated(sender, args) {
}

function ValidateUserInput(sender, args) {
    var gridDataItems = args.get_item();
    var elements = gridDataItems.get_element();
    var spanItems = elements.getElementsByTagName('SPAN');
    var vldPreparer = document.getElementById(spanItems[3].id);
    var vldReviewer = document.getElementById(spanItems[5].id);
    var vldComparePreparerAndReviewer = document.getElementById(spanItems[7].id);
    var dropDownListCollection = elements.getElementsByTagName('SELECT');
    var ddlPreparer = document.getElementById(dropDownListCollection[0].id);
    var ddlReviewer = document.getElementById(dropDownListCollection[1].id);
    ddlPreparer.disabled = false;
    ddlReviewer.disabled = false;
    if (dropDownListCollection.length == 4) {
        var ddlBackupPreparer = document.getElementById(dropDownListCollection[2].id);
        var ddlBackupReviewer = document.getElementById(dropDownListCollection[3].id);
        ddlBackupPreparer.disabled = false;
        ddlBackupReviewer.disabled = false;
    }
    else if (dropDownListCollection.length == 6) {
        var ddlBackupPreparer = document.getElementById(dropDownListCollection[3].id);
        var ddlBackupReviewer = document.getElementById(dropDownListCollection[4].id);
        var ddlBackupApprover = document.getElementById(dropDownListCollection[5].id);
        ddlBackupPreparer.disabled = false;
        ddlBackupReviewer.disabled = false;
        ddlBackupApprover.disabled = false;
    }
    var chkExcludeZeroBalance;
    if (spanItems.length > 21) {
        var chkExcludeZeroBalanceCollection = spanItems[21].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            spanItems[21].disabled = false;
            chkExcludeZeroBalance.disabled = false;
        }
        var ddlApprover = document.getElementById(dropDownListCollection[2].id);
        ddlApprover.disabled = false;
        var vldApprover = document.getElementById(spanItems[8].id);
        if (ddlApprover.getAttribute("IsOptional") != "1")
            ValidatorEnable(vldApprover, true);
        var vldCompareApproverAndPreparer = document.getElementById(spanItems[10].id);
        var vldCompareApproverAndReviewer = document.getElementById(spanItems[11].id);

        if (chkExcludeZeroBalance != null && chkExcludeZeroBalance != undefined && chkExcludeZeroBalance.checked == false) {
            if (ddlApprover.getAttribute("IsOptional") != "1")
                ValidatorEnable(vldApprover, true);
            ValidatorEnable(vldCompareApproverAndPreparer, true);
            ValidatorEnable(vldCompareApproverAndReviewer, true);
        }
    }
    else if (spanItems.length > 15) {
        var chkExcludeZeroBalanceCollection = spanItems[20].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            spanItems[20].disabled = false;
            chkExcludeZeroBalance.disabled = false;
        }
        else {
            var ddlApprover = document.getElementById(dropDownListCollection[2].id);
            ddlApprover.disabled = false;
            var vldApprover = document.getElementById(spanItems[8].id);
            var vldCompareApproverAndPreparer = document.getElementById(spanItems[10].id);
            var vldCompareApproverAndReviewer = document.getElementById(spanItems[11].id);
            if (ddlApprover.getAttribute("IsOptional") != "1")
                ValidatorEnable(vldApprover, true);
            ValidatorEnable(vldCompareApproverAndPreparer, true);
            ValidatorEnable(vldCompareApproverAndReviewer, true);
        }
    }
    else if (spanItems.length > 14) {
        var chkExcludeZeroBalanceCollection = spanItems[14].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            spanItems[14].disabled = false;
            chkExcludeZeroBalance.disabled = false;
        }
        //var ddlApprover = document.getElementById(dropDownListCollection[2].id);
        //ddlApprover.disabled = false;
        // var vldApprover = document.getElementById(spanItems[8].id);
        //var vldCompareApproverAndPreparer = document.getElementById(spanItems[7].id);
        //var vldCompareApproverAndReviewer = document.getElementById(spanItems[8].id);

        if (chkExcludeZeroBalance != null && chkExcludeZeroBalance != undefined && chkExcludeZeroBalance.checked == false) {
            //ValidatorEnable(vldApprover, true);
            //ValidatorEnable(vldCompareApproverAndPreparer, true);
            //ValidatorEnable(vldCompareApproverAndReviewer, true);
        }
    }
    else if (spanItems.length > 6) {
        var chkExcludeZeroBalanceCollection = spanItems[13].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            spanItems[13].disabled = false;
            chkExcludeZeroBalance.disabled = false;
        }
        else {
            //var ddlApprover = document.getElementById(dropDownListCollection[2].id);
            //ddlApprover.disabled = false;
            //var vldApprover = document.getElementById(spanItems[6].id);
            //var vldCompareApproverAndPreparer = document.getElementById(spanItems[7].id);
            //var vldCompareApproverAndReviewer = document.getElementById(spanItems[8].id);
            //ValidatorEnable(vldApprover, true);
            // ValidatorEnable(vldCompareApproverAndPreparer, true);
            //ValidatorEnable(vldCompareApproverAndReviewer, true);
        }
    }

    if (chkExcludeZeroBalance == null || chkExcludeZeroBalance == undefined || chkExcludeZeroBalance.checked == false) {
        ValidatorEnable(vldPreparer, true);
        ValidatorEnable(vldReviewer, true);
        ValidatorEnable(vldComparePreparerAndReviewer, true);
    }
}

function DevalidateUserInput(sender, args) {

    var gridDataItems = args.get_item();
    var elements = gridDataItems.get_element();
    var spanItems = elements.getElementsByTagName('SPAN');
    var vldPreparer = document.getElementById(spanItems[3].id);
    var vldReviewer = document.getElementById(spanItems[5].id);
    var vldComparePreparerAndReviewer = document.getElementById(spanItems[7].id);
    var dropDownListCollection = elements.getElementsByTagName('SELECT');
    var ddlPreparer = document.getElementById(dropDownListCollection[0].id);
    var ddlReviewer = document.getElementById(dropDownListCollection[1].id);
    ddlPreparer.disabled = true;
    ddlReviewer.disabled = true;

    if (dropDownListCollection.length == 4) {
        var ddlBackupPreparer = document.getElementById(dropDownListCollection[2].id);
        var ddlBackupReviewer = document.getElementById(dropDownListCollection[3].id);
        ddlBackupPreparer.disabled = true;
        ddlBackupReviewer.disabled = true;
    }
    else if (dropDownListCollection.length == 6) {
        var ddlBackupPreparer = document.getElementById(dropDownListCollection[3].id);
        var ddlBackupReviewer = document.getElementById(dropDownListCollection[4].id);
        var ddlBackupApprover = document.getElementById(dropDownListCollection[5].id);
        ddlBackupPreparer.disabled = true;
        ddlBackupReviewer.disabled = true;
        ddlBackupApprover.disabled = true;
    }

    var chkExcludeZeroBalance;
    if (spanItems.length > 21) {
        var chkExcludeZeroBalanceCollection = spanItems[21].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            spanItems[21].disabled = true;
            chkExcludeZeroBalance.disabled = true;
        }
        var ddlApprover = document.getElementById(dropDownListCollection[2].id);
        ddlApprover.disabled = true;
        var vldApprover = document.getElementById(spanItems[8].id);
        ValidatorEnable(vldApprover, false);
        var vldCompareApproverAndPreparer = document.getElementById(spanItems[10].id);
        var vldCompareApproverAndReviewer = document.getElementById(spanItems[11].id);

        if (chkExcludeZeroBalance != null && chkExcludeZeroBalance != undefined && chkExcludeZeroBalance.checked == false) {
            ValidatorEnable(vldApprover, false);
            ValidatorEnable(vldCompareApproverAndPreparer, false);
            ValidatorEnable(vldCompareApproverAndReviewer, false);
        }
    }
        //    if (spanItems.length > 12) {
        //        var chkExcludeZeroBalanceCollection = spanItems[6].getElementsByTagName('INPUT');

        //        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
        //            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
        //            chkExcludeZeroBalance.disabled = true;
        //            spanItems[9].disabled = true;
        //        }
        //        var ddlApprover = document.getElementById(dropDownListCollection[2].id);
        //        ddlApprover.disabled = true;
        //        var vldApprover = document.getElementById(spanItems[6].id);
        //        var vldCompareApproverAndPreparer = document.getElementById(spanItems[7].id);
        //        var vldCompareApproverAndReviewer = document.getElementById(spanItems[8].id);
        //        ValidatorEnable(vldApprover, false);
        //        ValidatorEnable(vldCompareApproverAndPreparer, false);
        //        ValidatorEnable(vldCompareApproverAndReviewer, false);
        //    }

    else if (spanItems.length > 15) {
        var chkExcludeZeroBalanceCollection = spanItems[20].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            spanItems[20].disabled = true;
            chkExcludeZeroBalance.disabled = true;
        }
        else {
            var ddlApprover = document.getElementById(dropDownListCollection[2].id);
            ddlApprover.disabled = true;
            var vldApprover = document.getElementById(spanItems[8].id);
            var vldCompareApproverAndPreparer = document.getElementById(spanItems[10].id);
            var vldCompareApproverAndReviewer = document.getElementById(spanItems[11].id);
            ValidatorEnable(vldApprover, false);
            ValidatorEnable(vldCompareApproverAndPreparer, false);
            ValidatorEnable(vldCompareApproverAndReviewer, false);
        }
    }
    else if (spanItems.length > 14) {
        var chkExcludeZeroBalanceCollection = spanItems[14].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            spanItems[14].disabled = true;
            chkExcludeZeroBalance.disabled = true;
        }
        //var ddlApprover = document.getElementById(dropDownListCollection[2].id);
        //ddlApprover.disabled = false;
        // var vldApprover = document.getElementById(spanItems[8].id);
        //var vldCompareApproverAndPreparer = document.getElementById(spanItems[7].id);
        //var vldCompareApproverAndReviewer = document.getElementById(spanItems[8].id);

        if (chkExcludeZeroBalance != null && chkExcludeZeroBalance != undefined && chkExcludeZeroBalance.checked == false) {
            //ValidatorEnable(vldApprover, true);
            //ValidatorEnable(vldCompareApproverAndPreparer, true);
            //ValidatorEnable(vldCompareApproverAndReviewer, true);
        }
    }
    else if (spanItems.length > 6) {
        var chkExcludeZeroBalanceCollection = spanItems[13].getElementsByTagName('INPUT');

        if (chkExcludeZeroBalanceCollection != null && chkExcludeZeroBalanceCollection != undefined && chkExcludeZeroBalanceCollection.length > 0) {
            chkExcludeZeroBalance = document.getElementById(chkExcludeZeroBalanceCollection[0].id);
            chkExcludeZeroBalance.disabled = true;
            spanItems[13].disabled = true;
        }
        else {
            //            var ddlApprover = document.getElementById(dropDownListCollection[2].id);
            //            ddlApprover.disabled = true;
            //            var vldApprover = document.getElementById(spanItems[6].id);
            //            var vldCompareApproverAndPreparer = document.getElementById(spanItems[7].id);
            //            var vldCompareApproverAndReviewer = document.getElementById(spanItems[8].id);
            //            ValidatorEnable(vldApprover, false);
            //            ValidatorEnable(vldCompareApproverAndPreparer, false);
            //            ValidatorEnable(vldCompareApproverAndReviewer, false);
        }
    }

    ValidatorEnable(vldPreparer, false);
    ValidatorEnable(vldReviewer, false);
    ValidatorEnable(vldComparePreparerAndReviewer, false);
}

function HideValidationSummary() {
    if (typeof (Page_ValidationSummaries) != "undefined") { //hide the validation summaries
        for (sums = 0; sums < Page_ValidationSummaries.length; sums++) {
            var summary = Page_ValidationSummaries[sums];
            summary.style.display = "none";
        }
    }
    return true;
}

function OnRowSelectingForBulkUpdate(sender, args) {
    var hdnBulkUpdateMode = window.document.getElementById(hdnBulkUpdateModeID);
    if (hdnBulkUpdateMode != null
        && typeof (hdnBulkUpdateMode) != "undefined") {
        if (hdnBulkUpdateMode.value == "ReadOnly") {
            args.set_cancel(true)
        }
    }
}
function OnRowCreatedBulkUpdate(sender, eventArgs) {

}
function EnableUserInputControls(sender, eventArgs) {

    //debugger;
    var grid = sender;
    var MasterTable = grid.get_masterTableView();
    var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
    EnableDisableUserInputControls(row, false, true);

}

function ValidateDueDays(sender, eventArgs) {

    //debugger;
    var cvDueDays = sender;
    var txtControls = sender.getAttribute("txtDueDaysControls");
    var txtControlsArray = txtControls.split(',');
    var prevValue = 0;
    var currValue = 0;
    for (counter = 0; counter < txtControlsArray.length; counter++) {
        var elementArry = txtControlsArray[counter].split('=');
        if (elementArry.length = 2) {
            var elementName = elementArry[0];
            var elementID = elementArry[1];
            var element = document.getElementById(elementID);
            if (element != null && element != undefined) {
                var IsOptional = element.getAttribute("IsOptional");
                if (IsOptional != "1" && (!IsNumber(element) || element.value * 1 == 0)) {
                    eventArgs.IsValid = false;
                    return false;
                }
                else {
                    currValue = element.value * 1;
                    if (prevValue != 0 && prevValue > currValue && (IsOptional != "1" || currValue != 0)) {
                        eventArgs.IsValid = false;
                        return false;
                    }
                    prevValue = currValue;
                }
            }
        }
    }
    return true;
}

function DisableUserInputControls(sender, eventArgs) {

    //debugger;
    var grid = sender;
    var MasterTable = grid.get_masterTableView();
    var row = MasterTable.get_dataItems()[eventArgs.get_itemIndexHierarchical()];
    EnableDisableUserInputControls(row, true, false);
}

function EnableDisableUserInputControls(row, enableDisableCtrl, enableDisableVldCtrl) {

    //debugger;
    var hlkControls = row._element.getAttribute("hlkControls");
    var chkControls = row._element.getAttribute("chkControls");
    var txtDueDaysControls = row._element.getAttribute("txtDueDaysControls");
    var hdnControls = row._element.getAttribute("hdnControls");
    var ddlControls = row._element.getAttribute("ddlControls");
    var validatorControls = row._element.getAttribute("validatorControls");

    var hlkControlsArray = hlkControls.split(',');
    var chkControlsArray = chkControls.split(',');
    var txtDueDaysControlsArray = txtDueDaysControls.split(',');
    var hdnControlsArray = hdnControls.split(',');
    var ddlControlsArray = ddlControls.split(',');
    var vldControls = validatorControls.split(',');
    var counter = 0;

    var hdnIsReconciled = document.getElementById(hdnControlsArray[0]);

    for (counter; counter < hlkControlsArray.length; counter++) {
        var elementArry = hlkControlsArray[counter].split('=');
        if (elementArry.length = 2) {
            var elementName = elementArry[0];
            var elementID = elementArry[1];
            var element = document.getElementById(elementID);
            if (element != null && element != undefined) {
                var href = element.href;
                if (enableDisableCtrl == false) {
                    if (elementName == 'hlRecFrequencySelection') {
                        element.href = href.replace('Mode=ReadOnly', 'Mode=Edit');
                    }

                }
                else {
                    if (elementName == 'hlRecFrequencySelection') {
                        element.href = href.replace('Mode=Edit', 'Mode=ReadOnly');
                    }
                }
            }
        }
    }

    var ddlRecTemplateValue = '';

    for (counter = 0; counter < chkControlsArray.length; counter++) {
        var elementArry = chkControlsArray[counter].split('=');
        if (elementArry.length = 2) {
            var elementName = elementArry[0];
            var elementID = elementArry[1];
            var element = document.getElementById(elementID);
            if (element != null && element != undefined) {
                element.disabled = enableDisableCtrl;
            }
        }
    }

    for (counter = 0; counter < txtDueDaysControlsArray.length; counter++) {
        var elementArry = txtDueDaysControlsArray[counter].split('=');
        if (elementArry.length = 2) {
            var elementName = elementArry[0];
            var elementID = elementArry[1];
            var element = document.getElementById(elementID);
            if (element != null && element != undefined) {
                if (hdnIsReconciled.value == "1")
                    element.disabled = true;
                else
                    element.disabled = enableDisableCtrl;
            }
        }
    }

    for (counter = 0; counter < ddlControlsArray.length; counter++) {
        var elementArry = ddlControlsArray[counter].split('=');
        if (elementArry.length = 2) {
            var elementName = elementArry[0];
            var elementID = elementArry[1];
            var element = document.getElementById(elementID);
            if (element != null && element != undefined) {
                element.disabled = enableDisableCtrl;
                if (elementName == 'ddlRecTemplate') {
                    recTemplateValue = element.options[element.selectedIndex].value;
                }
            }
        }
    }
    for (counter = 0; counter < vldControls.length; counter++) {
        var elementArry = vldControls[counter].split('=');
        if (elementArry.length = 2) {
            var elementName = elementArry[0];
            var elementID = elementArry[1];
            var element = document.getElementById(elementID);
            if (element != null && element != undefined) {
                var IsOptional = element.getAttribute("IsOptional");
                if (enableDisableVldCtrl == true && elementName == 'vldSubLedgerSource') {
                    if (recTemplateValue == 3) {
                        ValidatorEnable(element, true);
                    }
                    else
                        ValidatorEnable(element, false);
                }
                else {
                    if (IsOptional != "1")
                        ValidatorEnable(element, enableDisableVldCtrl);
                }
            }
        }
    }
}

function ValidateUserInputForBulkUpdate(sender, args) {
    var vldRiskRating;
    var vldReconciliationForm;
    var vldSubledgerSource;
    var vldAccountType;
    var ddlRecTempalte;
    var ddlSubledger;
    var spanKeyAccountOrZeroBalance;
    var chkKeyAccountOrZeroBalanceCollection;
    var chkKeyAccountOrZeroBalance;
    var spanZeroBalance;
    var chkZeroBalanceCollection;
    var chkZeroBalance;
    var ddlRiskRating;

    var gridDataItems = args.get_item();
    var elements = gridDataItems.get_element();
    var spanItems = elements.getElementsByTagName('SPAN');
    var dropDownListCollection = elements.getElementsByTagName('SELECT');
    var anchorCollection = elements.getElementsByTagName('A');
    var inputItemsCollection = elements.getElementsByTagName('INPUT');
    //alert(spanItems.length);
    if (anchorCollection != null && anchorCollection != undefined && anchorCollection.length > 0) {
        var hlRecfrequency = document.getElementById(anchorCollection[anchorCollection.length - 1].id);
        var href = hlRecfrequency.href;
        hlRecfrequency.href = href.replace('Mode=ReadOnly', 'Mode=Edit');
        //hlRecfrequency.disabled = false;                
    }

    if (spanItems.length > 7) {
        spanKeyAccountOrZeroBalance = spanItems[3];
        chkKeyAccountOrZeroBalanceCollection = spanKeyAccountOrZeroBalance.getElementsByTagName('INPUT');
        chkKeyAccountOrZeroBalance = document.getElementById(chkKeyAccountOrZeroBalanceCollection[0].id);
        spanKeyAccountOrZeroBalance.disabled = false;
        chkKeyAccountOrZeroBalance.disabled = false;
        spanZeroBalance = spanItems[4];
        chkZeroBalanceCollection = spanZeroBalance.getElementsByTagName('INPUT');
        chkZeroBalance = document.getElementById(chkZeroBalanceCollection[0].id);
        spanZeroBalance.disabled = false;
        chkZeroBalance.disabled = false;

        vldRiskRating = document.getElementById(spanItems[5].id);
        vldReconciliationForm = document.getElementById(spanItems[6].id);
        vldSubledgerSource = document.getElementById(spanItems[7].id);
        //vldAccountType = document.getElementById(spanItems[6].id);

        ValidatorEnable(vldRiskRating, true);

        ddlRecTempalte = document.getElementById(dropDownListCollection[1].id);
        ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
        ddlSubledger = document.getElementById(dropDownListCollection[2].id);
        ddlRiskRating.disabled = false;
    }
    else if (spanItems.length > 6) {
        spanKeyAccountOrZeroBalance = spanItems[3];
        chkKeyAccountOrZeroBalanceCollection = spanKeyAccountOrZeroBalance.getElementsByTagName('INPUT');

        chkKeyAccountOrZeroBalance = document.getElementById(chkKeyAccountOrZeroBalanceCollection[0].id);
        spanKeyAccountOrZeroBalance.disabled = false;
        chkKeyAccountOrZeroBalance.disabled = false;
        spanZeroBalance = spanItems[4];
        chkZeroBalanceCollection = spanZeroBalance.getElementsByTagName('INPUT');

        if (chkZeroBalanceCollection != null && chkZeroBalanceCollection != undefined && chkZeroBalanceCollection.length > 0) {
            chkZeroBalance = document.getElementById(chkZeroBalanceCollection[0].id);
            spanZeroBalance.disabled = false;
            chkZeroBalance.disabled = false;

            //vldRiskRating = document.getElementById(spanItems[4].id);
            vldReconciliationForm = document.getElementById(spanItems[5].id);
            vldSubledgerSource = document.getElementById(spanItems[6].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            //ValidatorEnable(vldRiskRating, true);

            ddlRecTempalte = document.getElementById(dropDownListCollection[0].id);
            //var ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[1].id);
            //ddlRiskRating.disabled = false;
        }
        else {

            vldRiskRating = document.getElementById(spanItems[4].id);
            vldReconciliationForm = document.getElementById(spanItems[5].id);
            vldSubledgerSource = document.getElementById(spanItems[6].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            ValidatorEnable(vldRiskRating, true);

            ddlRecTempalte = document.getElementById(dropDownListCollection[1].id);
            ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[2].id);
            ddlRiskRating.disabled = false;
        }
    }
    else if (spanItems.length > 5) {

        spanKeyAccountOrZeroBalance = spanItems[3];
        chkKeyAccountOrZeroBalanceCollection = spanKeyAccountOrZeroBalance.getElementsByTagName('INPUT');

        if (chkKeyAccountOrZeroBalanceCollection != null && chkKeyAccountOrZeroBalanceCollection != undefined && chkKeyAccountOrZeroBalanceCollection.length > 0) {
            chkKeyAccountOrZeroBalance = document.getElementById(chkKeyAccountOrZeroBalanceCollection[0].id);
            spanKeyAccountOrZeroBalance.disabled = false;
            chkKeyAccountOrZeroBalance.disabled = false;

            //vldRiskRating = document.getElementById(spanItems[4].id);
            vldReconciliationForm = document.getElementById(spanItems[4].id);
            vldSubledgerSource = document.getElementById(spanItems[5].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            //ValidatorEnable(vldRiskRating, true);

            ddlRecTempalte = document.getElementById(dropDownListCollection[0].id);
            //var ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[1].id);
            //ddlRiskRating.disabled = false;
        }
        else {
            vldRiskRating = document.getElementById(spanItems[3].id);
            vldReconciliationForm = document.getElementById(spanItems[4].id);
            vldSubledgerSource = document.getElementById(spanItems[5].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            ValidatorEnable(vldRiskRating, true);

            ddlRecTempalte = document.getElementById(dropDownListCollection[1].id);
            ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[2].id);
            ddlRiskRating.disabled = false;
        }
    }
    else {
        vldReconciliationForm = document.getElementById(spanItems[3].id);
        vldSubledgerSource = document.getElementById(spanItems[4].id);
        //vldAccountType = document.getElementById(spanItems[5].id);

        ddlRecTempalte = document.getElementById(dropDownListCollection[0].id);
        ddlSubledger = document.getElementById(dropDownListCollection[1].id);
    }

    if (recPeriodStatus != 'Open') {
        ddlRecTempalte.disabled = false;
    }
    ddlSubledger.disabled = false;
    var recTemplateValue = ddlRecTempalte.options[ddlRecTempalte.selectedIndex].value;

    ValidatorEnable(vldReconciliationForm, true);

    if (recTemplateValue == 3) //Subledger Form
    {
        ValidatorEnable(vldSubledgerSource, true);
    }
    //  ValidatorEnable(vldAccountType, true);
}

function DevalidateUserInputForBulkUpdate(sender, args) {
    var vldRiskRating;
    var vldReconciliationForm;
    var vldSubledgerSource;
    var vldAccountType;
    var ddlRecTempalte;
    var ddlSubledger;
    var spanKeyAccountOrZeroBalance;
    var chkKeyAccountOrZeroBalanceCollection;
    var chkKeyAccountOrZeroBalance;
    var spanZeroBalance;
    var chkZeroBalanceCollection;
    var chkZeroBalance;
    var ddlRiskRating;

    var gridDataItems = args.get_item();
    var elements = gridDataItems.get_element();
    var spanItems = elements.getElementsByTagName('SPAN');
    var dropDownListCollection = elements.getElementsByTagName('SELECT');
    var anchorCollection = elements.getElementsByTagName('A');

    if (anchorCollection != null && anchorCollection != undefined && anchorCollection.length > 0) {
        var hlRecfrequency = document.getElementById(anchorCollection[anchorCollection.length - 1].id);
        var href = hlRecfrequency.href;
        hlRecfrequency.href = href.replace('Mode=Edit', 'Mode=ReadOnly');
        //hlRecfrequency.disabled = true;
    }

    if (spanItems.length > 7) {
        spanKeyAccountOrZeroBalance = spanItems[3];
        chkKeyAccountOrZeroBalanceCollection = spanKeyAccountOrZeroBalance.getElementsByTagName('INPUT');
        chkKeyAccountOrZeroBalance = document.getElementById(chkKeyAccountOrZeroBalanceCollection[0].id);
        spanKeyAccountOrZeroBalance.disabled = true;
        chkKeyAccountOrZeroBalanceCollection.disabled = true;
        spanZeroBalance = spanItems[4];
        chkZeroBalanceCollection = spanZeroBalance.getElementsByTagName('INPUT');
        chkZeroBalance = document.getElementById(chkZeroBalanceCollection[0].id);
        spanZeroBalance.disabled = true;
        chkZeroBalance.disabled = true;

        vldRiskRating = document.getElementById(spanItems[5].id);
        vldReconciliationForm = document.getElementById(spanItems[6].id);
        vldSubledgerSource = document.getElementById(spanItems[7].id);
        //vldAccountType = document.getElementById(spanItems[6].id);

        ValidatorEnable(vldRiskRating, false);

        ddlRecTempalte = document.getElementById(dropDownListCollection[1].id);
        ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
        ddlSubledger = document.getElementById(dropDownListCollection[2].id);
        ddlRiskRating.disabled = true;
    }
    else if (spanItems.length > 6) {
        spanKeyAccountOrZeroBalance = spanItems[3];
        chkKeyAccountOrZeroBalanceCollection = spanKeyAccountOrZeroBalance.getElementsByTagName('INPUT');

        chkKeyAccountOrZeroBalance = document.getElementById(chkKeyAccountOrZeroBalanceCollection[0].id);
        spanKeyAccountOrZeroBalance.disabled = true;
        chkKeyAccountOrZeroBalanceCollection.disabled = true;
        spanZeroBalance = spanItems[4];
        chkZeroBalanceCollection = spanZeroBalance.getElementsByTagName('INPUT');

        if (chkZeroBalanceCollection != null && chkZeroBalanceCollection != undefined && chkZeroBalanceCollection.length > 0) {
            chkZeroBalance = document.getElementById(chkZeroBalanceCollection[0].id);
            spanZeroBalance.disabled = true;
            chkZeroBalance.disabled = true;

            //vldRiskRating = document.getElementById(spanItems[4].id);
            vldReconciliationForm = document.getElementById(spanItems[5].id);
            vldSubledgerSource = document.getElementById(spanItems[6].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            //ValidatorEnable(vldRiskRating, true);

            ddlRecTempalte = document.getElementById(dropDownListCollection[0].id);
            //var ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[1].id);
            //ddlRiskRating.disabled = true;
        }
        else {

            vldRiskRating = document.getElementById(spanItems[4].id);
            vldReconciliationForm = document.getElementById(spanItems[5].id);
            vldSubledgerSource = document.getElementById(spanItems[6].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            ValidatorEnable(vldRiskRating, false);

            ddlRecTempalte = document.getElementById(dropDownListCollection[1].id);
            ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[2].id);
            ddlRiskRating.disabled = true;
        }
    }
    else if (spanItems.length > 5) {

        spanKeyAccountOrZeroBalance = spanItems[3];
        chkKeyAccountOrZeroBalanceCollection = spanKeyAccountOrZeroBalance.getElementsByTagName('INPUT');

        if (chkKeyAccountOrZeroBalanceCollection != null && chkKeyAccountOrZeroBalanceCollection != undefined && chkKeyAccountOrZeroBalanceCollection.length > 0) {
            chkKeyAccountOrZeroBalance = document.getElementById(chkKeyAccountOrZeroBalanceCollection[0].id);
            spanKeyAccountOrZeroBalance.disabled = true;
            chkKeyAccountOrZeroBalanceCollection.disabled = true;

            //vldRiskRating = document.getElementById(spanItems[4].id);
            vldReconciliationForm = document.getElementById(spanItems[4].id);
            vldSubledgerSource = document.getElementById(spanItems[5].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            //ValidatorEnable(vldRiskRating, true);

            ddlRecTempalte = document.getElementById(dropDownListCollection[0].id);
            //var ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[1].id);
            //ddlRiskRating.disabled = true;
        }
        else {
            vldRiskRating = document.getElementById(spanItems[3].id);
            vldReconciliationForm = document.getElementById(spanItems[4].id);
            vldSubledgerSource = document.getElementById(spanItems[5].id);
            //vldAccountType = document.getElementById(spanItems[6].id);

            ValidatorEnable(vldRiskRating, false);

            ddlRecTempalte = document.getElementById(dropDownListCollection[1].id);
            ddlRiskRating = document.getElementById(dropDownListCollection[0].id);
            ddlSubledger = document.getElementById(dropDownListCollection[2].id);
            ddlRiskRating.disabled = true;
        }
    }
    else {
        vldReconciliationForm = document.getElementById(spanItems[3].id);
        vldSubledgerSource = document.getElementById(spanItems[4].id);
        //vldAccountType = document.getElementById(spanItems[5].id);

        ddlRecTempalte = document.getElementById(dropDownListCollection[0].id);
        ddlSubledger = document.getElementById(dropDownListCollection[1].id);
    }

    ddlRecTempalte.disabled = true;
    ddlSubledger.disabled = true;
    var recTemplateValue = ddlRecTempalte.options[ddlRecTempalte.selectedIndex].value;

    ValidatorEnable(vldReconciliationForm, false);

    if (recTemplateValue == 3) //Subledger Form
    {
        ValidatorEnable(vldSubledgerSource, false);
    }
}

function OnSelectedIndexChange(ddlRecTemplateID, vldSubledgerSourceID, checkBosSelectID) {
    var checkBoxSelect = document.getElementById(checkBosSelectID);

    if (checkBoxSelect.checked == true) {
        var ddlRectemplate = document.getElementById(ddlRecTemplateID);
        var vldSubledgerSource = document.getElementById(vldSubledgerSourceID);

        var recTemplateValue = ddlRectemplate.options[ddlRectemplate.selectedIndex].value
        if (recTemplateValue == 3) //Subledger Form
        {
            ValidatorEnable(vldSubledgerSource, true);
        }
        else {
            ValidatorEnable(vldSubledgerSource, false);
        }
    }
}

function SetValidationGroup(validator, validationGroupName) {
    if (validator != null) {
        validator.validationGroup = validationGroupName;
    }
}

function ResetAllValidationGroup() {

    var i;
    for (i = 0; i < Page_Validators.length; i++) {
        if (Page_Validators[i].validationGroup == "PRA") {
            SetValidationGroup(Page_Validators[i], '');
        }
    }
    Page_ClientValidate();
    return true;
}

function OnReviewerSelectedIndexChange(preparerClientID, reviewerClientID, validatorClientID, checkBoxClientID, selectOne) {

    var checkBoxSelect = document.getElementById(checkBoxClientID);

    if (checkBoxSelect.checked == true) {
        var ddlPreparer = document.getElementById(preparerClientID);
        var ddlReviewer = document.getElementById(reviewerClientID);
        var vldComparePreparerAndReviewer = document.getElementById(validatorClientID);
        SetValidationGroup(vldComparePreparerAndReviewer, 'PRA');

        var preparerID = ddlPreparer.options[ddlPreparer.selectedIndex].value;
        var reviewerID = ddlReviewer.options[ddlReviewer.selectedIndex].value;

        if (preparerID != selectOne || reviewerID != selectOne) {
            if (preparerID == reviewerID) {
                vldComparePreparerAndReviewer.IsValid = false;
            }
            else {
                vldComparePreparerAndReviewer.IsValid = true;
            }
        }
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'PRA';
        Page_ClientValidate('PRA');
        valSummaryObj.validationGroup = '';
    }
}

function OnApproverSelectedIndexChange(preparerClientID, reviewerClientID, approverClientID, validatorPreparerClientID, validatorReviewerClientID, checkBoxClientID, selectOne) {
    var checkBoxSelect = document.getElementById(checkBoxClientID);

    if (checkBoxSelect.checked == true) {
        var ddlPreparer = document.getElementById(preparerClientID);
        var ddlReviewer = document.getElementById(reviewerClientID);
        var ddlApprover = document.getElementById(approverClientID);
        var vldComparePreparerAndApprover = document.getElementById(validatorPreparerClientID);
        var vldCompareApproverAndReviewer = document.getElementById(validatorReviewerClientID);

        SetValidationGroup(vldComparePreparerAndApprover, 'PRA');
        SetValidationGroup(vldCompareApproverAndReviewer, 'PRA');

        var preparerID = ddlPreparer.options[ddlPreparer.selectedIndex].value;
        var reviewerID = ddlReviewer.options[ddlReviewer.selectedIndex].value;
        var approverID = ddlApprover.options[ddlApprover.selectedIndex].value;
        if (preparerID != selectOne || approverID != selectOne) {
            if (preparerID == approverID) {
                vldComparePreparerAndApprover.IsValid = false;
            }
            else {
                vldComparePreparerAndApprover.IsValid = true;
            }
        }

        if (reviewerID != selectOne || approverID != selectOne) {
            if (reviewerID == approverID) {
                vldCompareApproverAndReviewer.IsValid = false;
            }
            else {
                vldCompareApproverAndReviewer.IsValid = true;
            }
        }
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'PRA';
        Page_ClientValidate('PRA');
        valSummaryObj.validationGroup = '';
    }
}

function ValidatePRAForAccountOwnership(preparerClientID, reviewerClientID, approverClientID,
backupPreparerClientID, backupReviewerClientID, backupApproverClientID,
 validatorPreparerAndReviewerClientID, validatorPreparerClientID, validatorReviewerClientID, vldCompareBackupPreparerClientID,
  vldCompareBackupReviewerClientID, vldCompareBackupApproverClientID, checkBoxClientID, selectOne) {
    var checkBoxSelect = document.getElementById(checkBoxClientID);

    if (checkBoxSelect.checked == true) {
        var ddlPreparer = document.getElementById(preparerClientID);
        var ddlReviewer = document.getElementById(reviewerClientID);
        var ddlApprover = document.getElementById(approverClientID);

        var ddlBackupPreparer = document.getElementById(backupPreparerClientID);
        var ddlBackupReviewer = document.getElementById(backupReviewerClientID);
        var ddlBackupApprover = document.getElementById(backupApproverClientID);

        var preparerID = ddlPreparer.options[ddlPreparer.selectedIndex].value;
        var reviewerID = ddlReviewer.options[ddlReviewer.selectedIndex].value;
        if (ddlApprover != null) {
            var approverID = ddlApprover.options[ddlApprover.selectedIndex].value;
        }

        if (ddlBackupPreparer != null) {
            var backupPreparerID = ddlBackupPreparer.options[ddlBackupPreparer.selectedIndex].value;
        }
        if (ddlBackupReviewer != null) {
            var backupReviewerID = ddlBackupReviewer.options[ddlBackupReviewer.selectedIndex].value;
        }

        if (ddlBackupApprover != null) {
            var backupApproverID = ddlBackupApprover.options[ddlBackupApprover.selectedIndex].value;
        }

        var vldComparePreparerAndReviewer;
        if (validatorPreparerAndReviewerClientID != null) {
            vldComparePreparerAndReviewer = document.getElementById(validatorPreparerAndReviewerClientID);
        }

        var vldComparePreparerAndApprover;
        if (validatorPreparerClientID != null) {
            vldComparePreparerAndApprover = document.getElementById(validatorPreparerClientID);
        }

        var vldCompareBackupPreparer;
        if (vldCompareBackupPreparerClientID != null) {
            vldCompareBackupPreparer = document.getElementById(vldCompareBackupPreparerClientID);
        }

        var vldCompareBackupReviewer;
        if (vldCompareBackupReviewerClientID != null) {
            vldCompareBackupReviewer = document.getElementById(vldCompareBackupReviewerClientID);
        }

        var vldCompareBackupApprover;
        if (vldCompareBackupApproverClientID != null) {
            vldCompareBackupApprover = document.getElementById(vldCompareBackupApproverClientID);
        }

        if (vldComparePreparerAndReviewer != null) {
            SetValidationGroup(vldComparePreparerAndReviewer, 'PRA');
        }

        if (vldComparePreparerAndApprover != null) {
            SetValidationGroup(vldComparePreparerAndApprover, 'PRA');
        }

        if (vldCompareBackupPreparer != null) {
            SetValidationGroup(vldCompareBackupPreparer, 'PRA');
        }

        if (vldCompareBackupReviewer != null) {
            SetValidationGroup(vldCompareBackupReviewer, 'PRA');
        }

        if (vldCompareBackupApprover != null) {
            SetValidationGroup(vldCompareBackupApprover, 'PRA');
        }

        var ArrayToCompare = new Hash();
        var count = 0;
        if (preparerID != null && preparerID != selectOne) {
            ArrayToCompare["Preparer"] = preparerID;
        }

        if (reviewerID != null && reviewerID != selectOne) {
            ArrayToCompare["Reviewer"] = reviewerID;
        }

        if (approverID != null && approverID != selectOne) {
            ArrayToCompare["Approver"] = approverID;
        }

        if (backupPreparerID != null && backupPreparerID != selectOne) {
            ArrayToCompare["BackupPreparer"] = backupPreparerID;
        }

        if (backupReviewerID != null && backupReviewerID != selectOne) {
            ArrayToCompare["BackupReviewer"] = backupReviewerID;
        }

        if (backupApproverID != null && backupApproverID != selectOne) {
            ArrayToCompare["BackupApprover"] = backupApproverID;
        }

        var TempArrayToCompare = new Hash();
        TempArrayToCompare = ArrayToCompare;

        var keysArrayToCompare = ArrayToCompare.keys();
        var keysTempArrayToCompare = TempArrayToCompare.keys();

        var isSameValueExists = false;
        for (var i in keysArrayToCompare) {
            for (var j in keysTempArrayToCompare) {
                if ((keysArrayToCompare[i] != keysTempArrayToCompare[j]) && (ArrayToCompare[keysArrayToCompare[i]] == TempArrayToCompare[keysTempArrayToCompare[j]])) {
                    isSameValueExists = true;
                    break;
                }

            }
            if (isSameValueExists) {
                switch (keysTempArrayToCompare[j]) {
                    case "Preparer":
                        vldComparePreparerAndReviewer.IsValid = false;
                        break;
                    case "Reviewer":
                        vldComparePreparerAndReviewer.IsValid = false;
                        break;
                    case "Approver":
                        vldComparePreparerAndApprover.IsValid = false;
                        break;
                    case "BackupPreparer":
                        vldCompareBackupPreparer.IsValid = false;
                        break;
                    case "BackupReviewer":
                        vldCompareBackupReviewer.IsValid = false;
                        break;
                    case "BackupApprover":
                        vldCompareBackupApprover.IsValid = false;
                        break;
                }
                break;
            }

            if (isSameValueExists == false) {
                vldComparePreparerAndReviewer.IsValid = true;
                if (vldComparePreparerAndApprover != null) {
                    vldComparePreparerAndApprover.IsValid = true;
                }
                if (vldCompareBackupPreparer != null) {
                    vldCompareBackupPreparer.IsValid = true;
                }
                if (vldCompareBackupReviewer != null) {
                    vldCompareBackupReviewer.IsValid = true;
                }
                if (vldCompareBackupApprover != null) {
                    vldCompareBackupApprover.IsValid = true;
                }
            }
        }
    }

    var valSummaryObj = GetValidationSummaryElement();
    valSummaryObj.validationGroup = 'PRA';
    Page_ClientValidate('PRA');
    valSummaryObj.validationGroup = '';
}

function OnPreparerSelectedIndexChange(preparerClientID, reviewerClientID, approverClientID, validatorPreparerAndReviewerClientID, validatorPreparerClientID, validatorReviewerClientID, checkBoxClientID, selectOne) {

    var checkBoxSelect = document.getElementById(checkBoxClientID);

    if (checkBoxSelect.checked == true) {
        var ddlPreparer = document.getElementById(preparerClientID);
        var ddlReviewer = document.getElementById(reviewerClientID);
        var preparerID = ddlPreparer.options[ddlPreparer.selectedIndex].value;
        var reviewerID = ddlReviewer.options[ddlReviewer.selectedIndex].value;
        var vldComparePreparerAndReviewer = document.getElementById(validatorPreparerAndReviewerClientID);

        SetValidationGroup(vldComparePreparerAndReviewer, 'PRA');

        if (preparerID != selectOne || reviewerID != selectOne) {
            if (preparerID == reviewerID) {
                vldComparePreparerAndReviewer.IsValid = false;
            }
            else {
                vldComparePreparerAndReviewer.IsValid = true;
            }
        }

        if (approverClientID != null && approverClientID != '') {
            var ddlApprover = document.getElementById(approverClientID);
            if (ddlApprover != null) {
                var vldComparePreparerAndApprover = document.getElementById(validatorPreparerClientID);
                var vldCompareApproverAndReviewer = document.getElementById(validatorReviewerClientID);

                SetValidationGroup(vldComparePreparerAndApprover, 'PRA');
                SetValidationGroup(vldCompareApproverAndReviewer, 'PRA');

                var approverID = ddlApprover.options[ddlApprover.selectedIndex].value;

                if (preparerID != selectOne || approverID != selectOne) {
                    if (preparerID == approverID) {
                        vldComparePreparerAndApprover.IsValid = false;
                    }
                    else {
                        vldComparePreparerAndApprover.IsValid = true;
                    }
                }
                if (reviewerID != selectOne || approverID != selectOne) {
                    if (reviewerID == approverID) {
                        vldCompareApproverAndReviewer.IsValid = false;
                    }
                    else {
                        vldCompareApproverAndReviewer.IsValid = true;
                    }
                }
            }
        }
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'PRA';
        Page_ClientValidate('PRA');
        valSummaryObj.validationGroup = '';
    }
}

function ValidateBackupPreparer(source, args) {
    if (source.IsValid == false) {
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }
}

function ValidatePreparerAndReviewer(source, args) {
    if (source.IsValid == false) {
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }
}

function ValidatePreparerAndApprover(source, args) {
    if (source.IsValid == false) {
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }
}

function ValidateApproverAndReviewer(source, args) {
    if (source.IsValid == false) {
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }
}

function ShowRecPeriodGrid(modalPopupExtenderID, radRecPeriodGridID, txtRecPeriodID, txtRecPeriodContainerID) {
    var modalPopupExtender = $find(modalPopupExtenderID);
    var txtRecPeriodContainer = document.getElementById(txtRecPeriodID);
    var txtContainerOfRecPeriodContainerID = document.getElementById(txtRecPeriodContainerID);
    txtContainerOfRecPeriodContainerID.value = txtRecPeriodID;

    modalPopupExtender.show();

    var grid = $find(radRecPeriodGridID);
    var MasterTable = grid.get_masterTableView();

    var recPeriodIdCollection = txtRecPeriodContainer.value.split(';');
    var allRows = MasterTable.get_dataItems();

    for (var index = 0; index < allRows.length; index++) {
        var row = allRows[index];
        //var cell = MasterTable.getCellByColumnUniqueName(row, "PeriodNumber");
        var recPeriodId = row.getDataKeyValue("ReconciliationPeriodID"); //cell.innerHTML;

        for (var recPeriodIndex = 0; recPeriodIndex < recPeriodIdCollection.length; recPeriodIndex++) {
            if (recPeriodIdCollection[recPeriodIndex] == recPeriodId) {
                row.set_selected(true);
                break;
            }
        }

        if (recPeriodIndex == recPeriodIdCollection.length) {
            row.set_selected(false);
        }
    }
}

function OnOkPopup(radRecPeriodGridID, txtRecPeriodContainerID) {
    var txtContainerOfRecPeriodContainerID = document.getElementById(txtRecPeriodContainerID);

    if (txtContainerOfRecPeriodContainerID.value != null && txtContainerOfRecPeriodContainerID.value != '') {
        var txtRecPeriodContainer = document.getElementById(txtContainerOfRecPeriodContainerID.value);
    }

    if (txtRecPeriodContainer != null && txtRecPeriodContainer != 'undefined') {
        var grid = $find(radRecPeriodGridID);
        var MasterTable = grid.get_masterTableView();

        txtRecPeriodContainer.value = '';
        var selectedRows = MasterTable.get_selectedItems();

        for (var i = 0; i < selectedRows.length; i++) {
            var row = selectedRows[i];
            //var cell = MasterTable.getCellByColumnUniqueName(row, "PeriodNumber");
            var recPeriodId = row.getDataKeyValue("ReconciliationPeriodID"); //cell.innerHTML;
            txtRecPeriodContainer.value += recPeriodId + ';';
        }
    }
}

function OnSaveRecFrequency(radRecPeriodGridID, txtRecPeriodContainerID, hlRecFrequencyID, accountID, mode, hdnRecPeriodIDs) {
    var grid = $find(radRecPeriodGridID);
    var MasterTable = grid.get_masterTableView();
    var selectedRows = MasterTable.get_selectedItems();
    var hdnRecPeriodIDsCollection = document.getElementById(hdnRecPeriodIDs);

    //    var recPeriodIDCollection = '';
    //    for (var i = 0; i < selectedRows.length; i++) {
    //        var row = selectedRows[i];
    //        //var cell = MasterTable.getCellByColumnUniqueName(row, "PeriodNumber");
    //        var recPeriodId = row.getDataKeyValue("ReconciliationPeriodID"); //cell.innerHTML;
    //        recPeriodIDCollection += recPeriodId + ';';
    //    }
    //    if (hdnRecPeriodIDsCollection != null) {
    //        hdnRecPeriodIDsCollection.value = recPeriodIDCollection;
    //    }

    GetRadWindow().BrowserWindow.GetRecPeriodIDCollectionForAccount(txtRecPeriodContainerID, recPeriodIDCollection, hlRecFrequencyID, accountID, mode);
    GetRadWindow().Close();
}


//Add Userlist
function AddUserList(rgUserList, AddUsersList, checkSuceessOrFailure) {

    var AddUsersListHidden = document.getElementById(AddUsersList);

    if (rgUserList != null && rgUserList != 'undefined') {
        var grid = $find(rgUserList);

        var MasterTable = grid.get_masterTableView();

        AddUsersListHidden.value = "";
        var selectedRows = MasterTable.get_selectedItems();

        for (var i = 0; i < selectedRows.length; i++) {
            var row = selectedRows[i];
            var cell = row.getDataKeyValue("EmailID");


            //var recPeriodId = row.getDataKeyValue("ReconciliationPeriodID"); //cell.innerHTML;
            if (i >= 1) {
                AddUsersListHidden.value = AddUsersListHidden.value + ",";
            }
            AddUsersListHidden.value = AddUsersListHidden.value + cell;

            //alert(AddUsersListHidden.value);
        }
        //var txtUserNameSucess = document.getElementById('<% =this.txtUserNameSucess.ClientID %>');
        //alert(txtUserNameSucess);
        //txtUserNameSucess.value = AddUsersListHidden.value;
        //setMailList(AddUsersListHidden.value);
        //var arg = new Object();
        //arg.newtext = AddUsersListHidden.value;
        ////GetRadWindow().argument = AddUsersListHidden.value;

        if (checkSuceessOrFailure == 'Sucess') {
            GetRadWindow().BrowserWindow.setMailList(AddUsersListHidden.value);

        }
        else {
            GetRadWindow().BrowserWindow.setMailListFailure(AddUsersListHidden.value);

        }
        GetRadWindow().Close();

    }


}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.RadWindow; //Will work in Moz in all cases, including clasic dialog       
    else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow; //IE (and Moz as well)       
    return oWindow;
}

function SetStatusBarForTelerikPopup(sender, args) {
    setTimeout(function () {
        sender.set_status("");
    }, 0);
}

function IsItemSelectedInCheckBoxList(cblList) {
    var isAnySelected = false;
    var cbArray = cblList.getElementsByTagName("input");
    for (var i = 0; i < cbArray.length; i++) {
        if (cbArray[i].checked) {
            isAnySelected = true;
        }
    }
    return isAnySelected;
}


function OpenRadWindow(url, height, width) {
    var oWindow = window.radopen(url);
    oWindow.SetSize(width, height);
    AddCloseEvent(oWindow);
    oWindow.Center();
    return false;
}

function OpenRadWindowWithName(url, name, height, width) {
    var oWindow = window.radopen(url, name);
    oWindow.SetSize(width, height);
    AddCloseEvent(oWindow);
    oWindow.Center();
    return false;
}

function AddCloseEvent(oWindow) {
    if (RAD_WINDOW_CLOSE_EVENT_HANDLER != undefined)
        oWindow.add_close(RAD_WINDOW_CLOSE_EVENT_HANDLER);
}

function OpenRadWindowForHyperlink(url, height, width) {
    var oWindow = window.radopen(url);
    oWindow.SetSize(width, height);
    AddCloseEvent(oWindow);
    oWindow.Center();
}

function OpenRadWindowForHyperlinkWithName(url, name, height, width) {
    var oWindow = window.radopen(url, name);
    oWindow.SetSize(width, height);
    AddCloseEvent(oWindow);
    oWindow.Center();
}

function OpenRadWindowForHyperlinkWithOffset(url, height, width, offsetElementID) {
    var oWindow = window.radopen(url);
    oWindow.SetSize(width, height);
    oWindow.SetOffsetElementId(offsetElementID);
}

function OpenAlertRadWindowForHyperlinkWithOffset(url, height, width, offsetElementID) {
    var oWindow = window.radopen(url);
    oWindow.SetSize(width, height);
    oWindow.SetOffsetElementId(offsetElementID);
    oWindow.add_close(OnClientCloseWithReload);
    oWindow.argument = 0;
}

function OnClientCloseWithReload(sender, eventArgs) {
    if (sender.argument == 1) {
        window.location.reload();
    }
}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}

function OpenRadWindowFromRadWindow(url, height, width) {
    var oWindow = GetRadWindow().BrowserWindow.radopen(url);
    oWindow.Show();
    //oWindow.setActive(true);
    setTimeout(
        function () {
            oWindow.setActive(true);
            oWindow.SetModal(true);
            oWindow.SetSize(width, height);
            oWindow.Center();
        }, 0);
    //return false;
}

var globalHLFlagClientID;
var globalHLUnFlagClientID;

function FlagGLDataForUser(userID, userLoginID, glDataID, hlFlagClientID, hlUnFlagClientID) {
    globalHLFlagClientID = hlFlagClientID;
    globalHLUnFlagClientID = hlUnFlagClientID;

    PageMethods.InsertUserGLDataFlag(userID, userLoginID, glDataID, OnSuccess, OnFailure);
}

function OnSuccess(result) {
    var hlFlagIcon = document.getElementById(globalHLFlagClientID);
    var hlUnFlagIcon = document.getElementById(globalHLUnFlagClientID);

    hlFlagIcon.style.display = 'block';
    hlUnFlagIcon.style.display = 'none';
}

function OnFailure() {
}

function UnFlagGLDataForUser(userID, userLoginID, glDataID, hlFlagClientID, hlUnFlagClientID) {
    globalHLFlagClientID = hlFlagClientID;
    globalHLUnFlagClientID = hlUnFlagClientID;

    PageMethods.DeleteUserGLDataFlag(userID, userLoginID, glDataID, OnSuccessDelete, OnFailure);
}

function OnSuccessDelete(result) {
    var hlFlagIcon = document.getElementById(globalHLFlagClientID);
    var hlUnFlagIcon = document.getElementById(globalHLUnFlagClientID);

    hlFlagIcon.style.display = 'none';
    hlUnFlagIcon.style.display = 'block';
}

function ExcludeOwnershipValidationChecks(chkExcludeOwnershipClientID, vldPreparerClientID, vldReviewerClientID, vldPreparerAndReviewerClientID, vldApproverClientID, vldApproverAndReviewerClientID, vldApproverAndPreparerClientID, txtExcludeOwbershipValueClientID) {

    var chkExcludeOwnership = document.getElementById(chkExcludeOwnershipClientID);
    var vldPreparer = document.getElementById(vldPreparerClientID);
    var vldReviewer = document.getElementById(vldReviewerClientID);
    var vldPreparerAndReviewer = document.getElementById(vldPreparerAndReviewerClientID);
    var txtExcludeOwbershipValue = document.getElementById(txtExcludeOwbershipValueClientID);


    if (chkExcludeOwnership.checked == true) {

        txtExcludeOwbershipValue.value = 'true';
        ValidatorEnable(vldPreparer, false);
        ValidatorEnable(vldReviewer, false);
        ValidatorEnable(vldPreparerAndReviewer, false);

        if (vldApproverClientID != null && vldApproverClientID != undefined && vldApproverClientID != '') {
            var vldApprover = document.getElementById(vldApproverClientID);
            ValidatorEnable(vldApprover, false);
        }

        if (vldApproverAndPreparerClientID != null && vldApproverAndPreparerClientID != undefined && vldApproverAndPreparerClientID != '') {
            var vldApproverAndPreparer = document.getElementById(vldApproverAndPreparerClientID);
            ValidatorEnable(vldApproverAndPreparer, false);
        }

        if (vldApproverAndReviewerClientID != null && vldApproverAndReviewerClientID != undefined && vldApproverAndReviewerClientID != '') {
            var vldApproverAndReviewer = document.getElementById(vldApproverAndReviewerClientID);
            ValidatorEnable(vldApproverAndReviewer, false);
        }
    }
    else {

        txtExcludeOwbershipValue.value = 'false';
        ValidatorEnable(vldPreparer, true);
        ValidatorEnable(vldReviewer, true);
        ValidatorEnable(vldPreparerAndReviewer, true);

        if (vldApproverClientID != null && vldApproverClientID != undefined && vldApproverClientID != '') {
            var vldApprover = document.getElementById(vldApproverClientID);
            ValidatorEnable(vldApprover, true);
        }

        if (vldApproverAndPreparerClientID != null && vldApproverAndPreparerClientID != undefined && vldApproverAndPreparerClientID != '') {
            var vldApproverAndPreparer = document.getElementById(vldApproverAndPreparerClientID);
            ValidatorEnable(vldApproverAndPreparer, true);
        }

        if (vldApproverAndReviewerClientID != null && vldApproverAndReviewerClientID != undefined && vldApproverAndReviewerClientID != '') {
            var vldApproverAndReviewer = document.getElementById(vldApproverAndReviewerClientID);
            ValidatorEnable(vldApproverAndReviewer, true);
        }
    }
}

function GetInnerHTML(pnlRecFormID, hdnInnerHTMLID) {

    var alreadyExpanded = true;
    var table = window.document.getElementById(pnlRecFormID);
    var hdnInnerHTML = window.document.getElementById(hdnInnerHTMLID);

    var collPanel = $find("cpeAccountDetail");
    if (collPanel != null) {
        if (collPanel.get_Collapsed()) {
            collPanel.set_Collapsed(false);
            alreadyExpanded = false;
        }
    }
    //hdnInnerHTML.value = table.innerHTML;
    hdnInnerHTML.value = encode(table.innerHTML);
    if (!alreadyExpanded) {
        collPanel.set_Collapsed(true);
    }

}

function OpenEmailPopup(url, height, width, pnlRecFormID, hdnInnerHTMLID) {
    GetInnerHTML(pnlRecFormID, hdnInnerHTMLID);
    OpenRadWindowForHyperlink(url, height, width);
}

function GetInnerHTMLFromParent(title, hdnInnerHTMLIDFromParent, hdnInnerHTMLID) {
    var oWindow = GetRadWindow();
    var hdnInnerHTMLFromParent = oWindow.BrowserWindow.document.getElementById(hdnInnerHTMLIDFromParent);
    var hdnInnerHTML = oWindow.GetContentFrame().contentWindow.document.getElementById(hdnInnerHTMLID);
    //hdnInnerHTML.value = hdnInnerHTMLFromParent.value;
    hdnInnerHTML.value = decode(hdnInnerHTMLFromParent.value);
}

function OpenPrintWindow(url, height, width) {
    //window.open(url, "PrintReport", "location=0,status=0,width=" + width + ",height=" + height);
    OpenRadWindowForHyperlink(url, height, width);
}

function encode(htmlValue) {
    //var obj = document.getElementById(hdnInnerHTMLID);
    var unencoded = htmlValue;
    //obj.value = encodeURIComponent(unencoded);
    return encodeURIComponent(unencoded);
}

function decode(textValue) {
    //var obj = document.getElementById(hdnInnerHTMLID);
    var encoded = textValue;
    //obj.value = decodeURIComponent(encoded.replace(/\+/g, " "));
    return decodeURIComponent(encoded.replace(/\+/g, " "));
}


function ResetInnerHTML(objid) {
    //        var obj = document.getElementById(objid);
    //        if (obj != null) {
    //            obj.value = "";
    //     }
}

//var OldBankBalancBC = 0.0;
//var OldBankBalancRC = 0.0;


function RecalculateBalances(hdnGLBalanceBCID, hdnGLBalanceRCID
                            , hdnGlAdjustmentAndTimingDiffBCID, hdnGlAdjustmentAndTimingDiffRCID
                            , hdnSupportingDetailBCID, hdnSupportingDetailRCID
                            , lblRecBalanceBCID, lblRecBalanceRCID
                            , oSourceTextBox, destinationTextBoxID
                            , txtBankBalanceBCID, txtBankBalanceRCID
                            , lblTotalRecWriteOffBCID, lblTotalRecWriteOffRCID
                            , lblTotalUnExplainedVarianceBCID, lblTotalUnExplainedVarianceRCID
                            , exchangeRate, noOfDecimalPlacesForExchangeRate, isBCCYAvailable, hdnBankBalanceRC, hdnBankBalanceBC) {

    /*
    check if current textbox locked
    - YES then return
    recalculate everything
    if not blank
    lock destination
    */
    var isSourceTextboxEmpty = false;
    if (oSourceTextBox.disabled == 'disabled') {
        return;
    }

    var oDestinationTextBox = document.getElementById(destinationTextBoxID);
    if (oSourceTextBox.value == "") {
        isSourceTextboxEmpty = true;
        oDestinationTextBox.value = '';
        oDestinationTextBox.disabled = '';
    }
    else {
        oDestinationTextBox.disabled = 'disabled';
    }

    var sourceTextBoxValue = 0.00;
    var destinationTextBoxValue = 0.00;
    var exRate = RoundNumber(exchangeRate, noOfDecimalPlacesForExchangeRate);

    var hdnGLBalanceBC = document.getElementById(hdnGLBalanceBCID);
    var hdnGLBalanceRC = document.getElementById(hdnGLBalanceRCID);

    var hdnGlAdjustmentAndTimingDiffBC = document.getElementById(hdnGlAdjustmentAndTimingDiffBCID);
    var hdnGlAdjustmentAndTimingDiffRC = document.getElementById(hdnGlAdjustmentAndTimingDiffRCID);

    var hdnSupportingDetailBC = document.getElementById(hdnSupportingDetailBCID);
    var hdnSupportingDetailRC = document.getElementById(hdnSupportingDetailRCID);

    var lblRecBalanceBC = document.getElementById(lblRecBalanceBCID);
    var lblRecBalanceRC = document.getElementById(lblRecBalanceRCID);

    var txtBankBalanceBC = document.getElementById(txtBankBalanceBCID);
    var txtBankBalanceRC = document.getElementById(txtBankBalanceRCID);
    var hdnBankBalanceBC = document.getElementById(hdnBankBalanceBC);
    var hdnBankBalanceRC = document.getElementById(hdnBankBalanceRC);
    var lblTotalrecWriteOffBC = document.getElementById(lblTotalRecWriteOffBCID);
    var lblTotalrecWriteOffRC = document.getElementById(lblTotalRecWriteOffRCID);

    var lblTotalUnExpBC = document.getElementById(lblTotalUnExplainedVarianceBCID);
    var lblTotalUnExpRC = document.getElementById(lblTotalUnExplainedVarianceRCID);

    //Rec Balance
    var GLBalanceBC;
    var GLBalanceRC;
    var GlAdjustmentAndTimingDiffBC;
    var GlAdjustmentAndTimingDiffRC;
    var SupportingBalanceBC;
    var SupportingBalanceRC;
    var RecBalanceBC;
    var RecBalanceRC;
    var BankBalanceBC;
    var BankBalanceRC;
    var TotalSupportingBalanceBC;
    var TotalSupportingBalanceRC;
    var TotalrecWriteOffBC;
    var TotalrecWriteOffRC;
    var TotalUnExpBC;
    var TotalUnExpRC;

    //Bank Balance
    BankBalanceBC = 0.0;
    BankBalanceRC = 0.0;

    if (IsNumber(txtBankBalanceBC)) {
        BankBalanceBC = RoundNumber(txtBankBalanceBC.value, 2);
    }


    if (IsNumber(txtBankBalanceRC)) {
        BankBalanceRC = RoundNumber(txtBankBalanceRC.value, 2);
    }

    OldBankBalancBC = RoundNumber(OldBankBalancBC, 2);
    OldBankBalancRC = RoundNumber(OldBankBalancRC, 2);

    if (BankBalanceBC != OldBankBalancBC || BankBalanceRC != OldBankBalancRC) {
        if (IsNumber(oSourceTextBox)) {
            sourceTextBoxValue = RoundNumber(oSourceTextBox.value, 2);
            oSourceTextBox.value = GetDisplayDecimalValueForTextBox(sourceTextBoxValue, 2);
            destinationTextBoxValue = sourceTextBoxValue * exRate;
            destinationTextBoxValue = RoundNumber(destinationTextBoxValue, 2);
            if (isBCCYAvailable == 1) {
                oDestinationTextBox.value = GetDisplayDecimalValueForTextBox(destinationTextBoxValue, 2);
            }
        }

        // hack: get Bank Balance values, after the Source / Destination textboxes have been assigned values
        BankBalanceBC = RoundNumber(txtBankBalanceBC.value, 2);
        BankBalanceRC = RoundNumber(txtBankBalanceRC.value, 2);
        hdnBankBalanceBC.value = BankBalanceBC;
        hdnBankBalanceRC.value = BankBalanceRC;

        GLBalanceBC = RoundNumber((hdnGLBalanceBC != null) ? hdnGLBalanceBC.value : 0, 2);
        GLBalanceRC = RoundNumber((hdnGLBalanceRC != null) ? hdnGLBalanceRC.value : 0, 2);

        GlAdjustmentAndTimingDiffBC = RoundNumber((hdnGlAdjustmentAndTimingDiffBC != null) ? hdnGlAdjustmentAndTimingDiffBC.value : 0, 2);
        GlAdjustmentAndTimingDiffRC = RoundNumber((hdnGlAdjustmentAndTimingDiffRC != null) ? hdnGlAdjustmentAndTimingDiffRC.value : 0, 2);

        SupportingBalanceBC = RoundNumber((hdnSupportingDetailBC != null) ? hdnSupportingDetailBC.value : 0, 2);
        SupportingBalanceRC = RoundNumber((hdnSupportingDetailRC != null) ? hdnSupportingDetailRC.value : 0, 2);

        TotalSupportingBalanceBC = (isNaN(SupportingBalanceBC) ? 0 : SupportingBalanceBC) + (isNaN(BankBalanceBC) ? 0 : BankBalanceBC)
        TotalSupportingBalanceRC = (isNaN(SupportingBalanceRC) ? 0 : SupportingBalanceRC) + (isNaN(BankBalanceRC) ? 0 : BankBalanceRC)

        //WriteOff On
        TotalrecWriteOffBC = RoundNumber((lblTotalrecWriteOffBC != null) ? lblTotalrecWriteOffBC.firstChild.data : "0", 2);
        TotalrecWriteOffRC = RoundNumber((lblTotalrecWriteOffRC != null) ? lblTotalrecWriteOffRC.firstChild.data : "0", 2);

        if (isNaN(GLBalanceBC))
            GLBalanceBC = 0.0;
        if (isNaN(GlAdjustmentAndTimingDiffBC))
            GlAdjustmentAndTimingDiffBC = 0.0;
        //--******** New Formulas
        //-- Reconciled Balance = (+/-)GL Balance + (-/+) GL Adjustment + (-/+) Timing Difference
        RecBalanceBC = GLBalanceBC + GlAdjustmentAndTimingDiffBC;
        RecBalanceRC = GLBalanceRC + GlAdjustmentAndTimingDiffRC;
        RecBalanceBC = RoundNumber(RecBalanceBC, 2);
        RecBalanceRC = RoundNumber(RecBalanceRC, 2);

        //-- Unexp Var = (-/+) Reconciled Balance – (-/+) Supporting Details + (-/+) Write Off/On
        TotalUnExpBC = (isNaN(RecBalanceBC) ? 0 : RecBalanceBC) - (isNaN(TotalSupportingBalanceBC) ? 0 : TotalSupportingBalanceBC) + (isNaN(TotalrecWriteOffBC) ? 0 : TotalrecWriteOffBC);
        TotalUnExpRC = (isNaN(RecBalanceRC) ? 0 : RecBalanceRC) - (isNaN(TotalSupportingBalanceRC) ? 0 : TotalSupportingBalanceRC) + (isNaN(TotalrecWriteOffRC) ? 0 : TotalrecWriteOffRC);

        TotalUnExpBC = RoundNumber(TotalUnExpBC, 2);
        TotalUnExpRC = RoundNumber(TotalUnExpRC, 2);

        if (lblRecBalanceBC != null && isBCCYAvailable == 1)
            lblRecBalanceBC.firstChild.data = GetDisplayDecimalValue(RecBalanceBC, 2);
        if (lblRecBalanceRC != null)
            lblRecBalanceRC.firstChild.data = GetDisplayDecimalValue(RecBalanceRC, 2);

        if (lblTotalUnExpBC != null && isBCCYAvailable == 1)
            lblTotalUnExpBC.firstChild.data = GetDisplayDecimalValue(TotalUnExpBC, 2);
        if (lblTotalUnExpRC != null)
            lblTotalUnExpRC.firstChild.data = GetDisplayDecimalValue(TotalUnExpRC, 2);
    }
    // Store OLD Values for future comparison
    OldBankBalancBC = BankBalanceBC;
    OldBankBalancRC = BankBalanceRC;

}



/*
Created by Apoorv
for re-calculating the Rec Item Amount
GL Adjustment, Write Off / On - Rec Item Popups
Getting inputs from a Arrary being declared on the Page
*/
function RecalculateRecItemAmount(obj) {
    var array = obj.split("/");

    var txtAmountLCCY = $get(array[0]);
    var lblAmountBCCYValue = document.getElementById(array[1]);
    var lblAmountRCCYValue = document.getElementById(array[2]);

    var exchangeRateLCCYToBCCY = array[3];
    var exchangeRateLCCYToRCCY = array[4];
    var lccyCode = array[5];
    var noOfDecimalPlaces = array[6];
    var noOfDecimalPlacesForExchangeRate = array[7];

    var amountLCCY = null;
    var amountBCCY = null;
    var amountRCCY = null;

    if (IsNumber(txtAmountLCCY)
        && lccyCode != JS_GLOBAL_CONSTANT_SELECT_ONE) {

        amountLCCY = RoundNumber(txtAmountLCCY.value, noOfDecimalPlaces);

        if (exchangeRateLCCYToBCCY != '') {
            exchangeRateLCCYToBCCY = RoundNumber(exchangeRateLCCYToBCCY, noOfDecimalPlacesForExchangeRate);
            amountBCCY = amountLCCY * exchangeRateLCCYToBCCY;
            amountBCCY = RoundNumber(amountBCCY, noOfDecimalPlaces);
        }
        if (exchangeRateLCCYToRCCY != '') {
            exchangeRateLCCYToRCCY = RoundNumber(exchangeRateLCCYToRCCY, noOfDecimalPlacesForExchangeRate);
            amountRCCY = amountLCCY * exchangeRateLCCYToRCCY;
            amountRCCY = RoundNumber(amountRCCY, noOfDecimalPlaces);
        }
    }

    lblAmountBCCYValue.innerHTML = SetDisplayDecimalValue(amountBCCY, noOfDecimalPlaces);
    lblAmountRCCYValue.innerHTML = SetDisplayDecimalValue(amountRCCY, noOfDecimalPlaces);
}

/*
Created by Apoorv
for re-calculating the Rec Item Recurring Schedule
Accruable and Amortizable Rec Form - Rec Item Popups
Getting inputs from a Arrary being declared on the Page
*/
function RecalculateRecItemScheduleAmount() {
    var txtOriginalAmountLCCY = window.document.getElementById(ControlArray[0]);
    var calScheduleBeginDate = window.document.getElementById(ControlArray[1]);
    var calScheduleEndDate = window.document.getElementById(ControlArray[2]);
    var lblOriginalAmountRCCYValue = window.document.getElementById(ControlArray[3]);
    var lblCurrentAmountRCCYValue = window.document.getElementById(ControlArray[4]);
    var lblTotalAccruedAmountRCCYValue = window.document.getElementById(ControlArray[5]);
    var lblToBeAccruedAmountRCCYValue = window.document.getElementById(ControlArray[6]);
    var lccyCode = ControlArray[7];
    var exchangeRateLCCYToBCCY = ControlArray[8];
    var exchangeRateLCCYToRCCY = ControlArray[9];
    var recPeriodEndDate = ControlArray[10];
    var noOfDecimalPlaces = ControlArray[11];
    var hdnPrevRecPeriodEndDate = window.document.getElementById(ControlArray[12]);
    var hdnPrevGLDataRecurringItemScheduleID = window.document.getElementById(ControlArray[13]);
    var noOfDecimalPlacesForExchangeRate = ControlArray[14];

    var originalAmtLCCY = null;
    var originalAmtRCCY = null;
    var currentAmtRCCY = null;
    var totalAccruedAmountRCCY = null;
    var toBeAccruedAmountRCCY = null;

    if (IsNumber(txtOriginalAmountLCCY)
        && lccyCode != JS_GLOBAL_CONSTANT_SELECT_ONE
        && IsDate(calScheduleBeginDate.value)
        && IsDate(calScheduleEndDate.value)) {

        exchangeRateLCCYToBCCY = RoundNumber(exchangeRateLCCYToBCCY, noOfDecimalPlacesForExchangeRate);
        exchangeRateLCCYToRCCY = RoundNumber(exchangeRateLCCYToRCCY, noOfDecimalPlacesForExchangeRate);

        originalAmtLCCY = txtOriginalAmountLCCY.value;
        originalAmtLCCY = RoundNumber(txtOriginalAmountLCCY.value, noOfDecimalPlaces);

        originalAmtRCCY = originalAmtLCCY * exchangeRateLCCYToRCCY;
        originalAmtRCCY = RoundNumber(originalAmtRCCY, noOfDecimalPlaces);

        var scheduleBeginDate;
        var scheduleEndDate;

        scheduleBeginDate = getDateFromFormat(calScheduleBeginDate.value);
        scheduleEndDate = getDateFromFormat(calScheduleEndDate.value);
        recPeriodEndDate = getDateFromFormat(recPeriodEndDate);

        currentAmtRCCY = GetCurrentAmount(originalAmtLCCY, exchangeRateLCCYToRCCY
                            , hdnPrevRecPeriodEndDate, hdnPrevGLDataRecurringItemScheduleID
                            , scheduleBeginDate, scheduleEndDate, recPeriodEndDate
                            , noOfDecimalPlaces);

        var noOfDaysInSchedule = GetDaysBetweenDateRanges(scheduleBeginDate, scheduleEndDate);
        var noOfDaysConsumedTillCurrentRecPeriod = GetDaysBetweenDateRanges(scheduleBeginDate, recPeriodEndDate);

        if (noOfDaysInSchedule > 0 && noOfDaysConsumedTillCurrentRecPeriod > 0) {

            // Total Accrued Amt - LCCY / BCCY / RCCY
            // Calculate in LCCY and then convert to BCCY and RCCY
            totalAccruedAmountLCCY = (originalAmtLCCY * 1.0 / noOfDaysInSchedule) * noOfDaysConsumedTillCurrentRecPeriod;
            totalAccruedAmountLCCY = RoundNumber(totalAccruedAmountLCCY, noOfDecimalPlaces);

            totalAccruedAmountRCCY = totalAccruedAmountLCCY * exchangeRateLCCYToRCCY;
            totalAccruedAmountRCCY = RoundNumber(totalAccruedAmountRCCY, noOfDecimalPlaces);

            // To Be Accrued Amt or Balance - LCCY / BCCY / RCCY
            // Calculate in LCCY and then convert to BCCY and RCCY
            toBeAccruedAmountLCCY = originalAmtLCCY - totalAccruedAmountLCCY;
            toBeAccruedAmountLCCY = RoundNumber(toBeAccruedAmountLCCY, noOfDecimalPlaces);

            toBeAccruedAmountRCCY = toBeAccruedAmountLCCY * exchangeRateLCCYToRCCY;
            toBeAccruedAmountRCCY = RoundNumber(toBeAccruedAmountRCCY, noOfDecimalPlaces);
        }
    } // end of if isnumber


    lblCurrentAmountRCCYValue.innerHTML = SetDisplayDecimalValue(currentAmtRCCY, noOfDecimalPlaces);

    lblOriginalAmountRCCYValue.innerHTML = SetDisplayDecimalValue(originalAmtRCCY, noOfDecimalPlaces);
    lblTotalAccruedAmountRCCYValue.innerHTML = SetDisplayDecimalValue(totalAccruedAmountRCCY, noOfDecimalPlaces);
    lblToBeAccruedAmountRCCYValue.innerHTML = SetDisplayDecimalValue(toBeAccruedAmountRCCY, noOfDecimalPlaces);
}

function GetCurrentAmount(originalAmtLCCY, exchangeRateLCCYToRCCY, hdnPrevRecPeriodEndDate, hdnPrevGLDataRecurringItemScheduleID
                            , scheduleBeginDate, scheduleEndDate, recPeriodEndDate, noOfDecimalPlaces) {

    var noOfDaysInSchedule = GetDaysBetweenDateRanges(scheduleBeginDate, scheduleEndDate);
    var noOfDaysConsumedInCurrentRecPeriod = null;
    var currentAmtRCCY = null;
    var currentAmtLCCY = null;

    // Get No of Days Consumed in Current Rec Period
    if (hdnPrevGLDataRecurringItemScheduleID.value == '') {
        noOfDaysConsumedInCurrentRecPeriod = GetDaysBetweenDateRanges(scheduleBeginDate, recPeriodEndDate);
    }
    else {
        prevRecPeriodEndDate = getDateFromFormat(hdnPrevRecPeriodEndDate.value);
        noOfDaysConsumedInCurrentRecPeriod = GetDaysBetweenDateRanges(prevRecPeriodEndDate, recPeriodEndDate);
    }

    if (noOfDaysInSchedule > 0 && noOfDaysConsumedInCurrentRecPeriod > 0) {
        currentAmtLCCY = (originalAmtLCCY * 1.0 / noOfDaysInSchedule) * noOfDaysConsumedInCurrentRecPeriod;
        currentAmtLCCY = RoundNumber(currentAmtLCCY, noOfDecimalPlaces);

        currentAmtRCCY = currentAmtLCCY * exchangeRateLCCYToRCCY;
        currentAmtRCCY = RoundNumber(currentAmtRCCY, noOfDecimalPlaces);
    }
    return currentAmtRCCY;
}

function CheckSkippedRecPeriod(ddlReconciliationPeriod, url, height, width) {
    var selectedValue = ddlReconciliationPeriod.value;

    if (RecPeriodIDAndStatusArray[selectedValue] == 5) // Skipped Rec Period Status
    {
        OpenRadWindow(url, height, width);

        // reset to prev selection
        for (i = 0; i < ddlReconciliationPeriod.options.length; i++) {
            if (ddlReconciliationPeriod.options[i].value == oldRecPeriodID) {
                ddlReconciliationPeriod.options[i].selected = true;
                break;
            }
        }
        return false;
    }
    else {
        oldRecPeriodID = selectedValue;
    }
    __doPostBack('ctl00$ddlReconciliationPeriod', '');
    return true;
}

function ShowConfirmationMessageOnDelete(msg) {
    var answer = confirm(msg);
    if (answer) {
        return true;
    }
    else {
        return false;
    }
}

function CancelRowSelectionForDisabledCheckBox(sender, args) {
    //get the input check box
    var id = args.get_id();
    var inputCheckBox = $get(id).getElementsByTagName("input")[0];
    if (!inputCheckBox || inputCheckBox.disabled) {
        //cancel selection for disabled rows  
        args.set_cancel(true);
    }
}
function ConvertCurrency(amount, exchangeRate, decimalPlaces) {
    return RoundNumber(amount * exchangeRate, decimalPlaces);
}
// Quality Score Section Begins
function GetQualityScore(grid, systemResponseColumnDataKey, userResponseColumnUniqueKey, commentsColumnUniqueKey, valToCompare) {
    var masterTable = grid.get_masterTableView();
    var rows = masterTable.get_dataItems();
    var cell;
    var commentsCell;
    var rfvComments;
    var obj = new Object();
    obj.SystemScore = 0;
    obj.UserScore = 0;
    var SystemQualityScoreStatusID = 0;
    var UserQualityScoreStatusID = 0;
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        SystemQualityScoreStatusID = row.getDataKeyValue(systemResponseColumnDataKey);
        if (SystemQualityScoreStatusID == valToCompare)
            obj.SystemScore++;
        cell = masterTable.getCellByColumnUniqueName(row, userResponseColumnUniqueKey);
        commentsCell = masterTable.getCellByColumnUniqueName(row, commentsColumnUniqueKey);
        rfvComments = null;
        // First Children in Span and Further second children is ExRequiredFieldValidator in ExTextBox Control
        if (commentsCell.children[0].children[1] != null && commentsCell.children[0].children[1] != 'undefined') {
            rfvComments = commentsCell.children[0].children[1];
        }
        if (cell.children[0] != null && cell.children[0] != 'undefined') {
            for (var j = 0; j < cell.children[0].length; j++) {
                if (cell.children[0][j].selected) {
                    // If user response is Select One
                    UserQualityScoreStatusID = cell.children[0][j].value;
                    if (UserQualityScoreStatusID == '-2') {
                        UserQualityScoreStatusID = SystemQualityScoreStatusID;
                    }
                    if (UserQualityScoreStatusID == valToCompare) {
                        obj.UserScore++;
                    }
                    if (SystemQualityScoreStatusID != '-2' && UserQualityScoreStatusID != '-2' && SystemQualityScoreStatusID != UserQualityScoreStatusID)
                        ValidatorEnable(rfvComments, true);
                    else
                        ValidatorEnable(rfvComments, false);
                    ValidatorValidate(rfvComments);
                }
            }
        }
    }
    return obj;
}
function OnUserResponseChange(rgEditQualityScoreClientID, systemResponseColumnDataKey, userResponseColumnUniqueKey, commentsColumnUniqueKey, OnQualityScoreChanged, valToCompare) {
    var grid = $find(rgEditQualityScoreClientID);
    if (OnQualityScoreChanged != '') {
        var args = GetQualityScore(grid, systemResponseColumnDataKey, userResponseColumnUniqueKey, commentsColumnUniqueKey, valToCompare);
        OnQualityScoreChanged(grid, args);
    }
}
////Quality Score Section Ends
var tableView = null;
function ddlPageSize_SelectedIndexChanged(ID, SourceID) {

    if ($find(SourceID) != null) {
        tableView = $find(SourceID).get_masterTableView();

    }
    if (tableView != null)
        tableView.set_pageSize(document.getElementById(ID).value);

}
function Test(grd) {
    var gridID = grd.id;
    alert(gridID);
}

//Function will create and XML HTTP object depending upon the browser.
function CreateXmlHttp() {
    //Creating object of XMLHTTP in IE
    try {
        XmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
    }
    catch (e) {
        try {
            XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        catch (oc) {
            XmlHttp = null;
        }
    }
    //Creating object of XMLHTTP in Mozilla and Safari 
    if (!XmlHttp && typeof XMLHttpRequest != "undefined") {
        XmlHttp = new XMLHttpRequest();
    }
}

function SendRequest(RequestUrl) {
    CreateXmlHttp();
    if (XmlHttp) {
        //Setting the event handler for the response
        XmlHttp.onreadystatechange = HandleResponse;

        //Initializes the request object with GET (METHOD of posting), 
        //Request URL and sets the request as asynchronous.
        XmlHttp.open("GET", RequestUrl, false);

        //Sends the request to server
        XmlHttp.send(null);
    }
}

function HandleResponse() {

    // To make sure receiving response data from server is completed
    if (XmlHttp.readyState == 4) {
        // To make sure valid response is received from the server, 200 means response received is OK
        if (XmlHttp.status == 200) {
            //var str;
            var result = XmlHttp.responseText;
            if (result != 'True') {
                vlduserStatus.IsValid = false;
                //                SetValidationGroup(vlduserStatus, 'PRA');
                //                var valSummaryObj = GetValidationSummaryElement();
                //                valSummaryObj.validationGroup = 'PRA';
                //                Page_ClientValidate('PRA');
                //                valSummaryObj.validationGroup = '';
            }
        }
        else {
            // alert('Timeout occured while getting response from server within reasonable time.');
        }
    }
}

var vlduserStatus = null;
function ValidateUserState(source, args) {
    if (document.getElementById(source.controltovalidate).disabled == false) {
        vlduserStatus = args;
        if (args.Value != -2)
            SendRequest("..//Pages//CheckUserStatus.aspx?userid=" + args.Value);
    }

}


function OnUserResponseRecChange(rgRecControlCheckListItems, userResponseColumnUniqueKey, OnRecControlListChanged) {
    var grid = $find(rgRecControlCheckListItems);
    if (OnRecControlListChanged != '') {
        var args = GetRecChange(grid, userResponseColumnUniqueKey);
        ucRecControlCheckList_OnRecControlListChanged(grid, args);
    }
}

function GetRecChange(grid, userResponseColumnUniqueKey) {
    var masterTable = grid.get_masterTableView();
    var rows = masterTable.get_dataItems();
    var cell;
    var obj = new Object();
    obj.CompletedCount = 0;
    obj.ReviwedCount = 0;
    obj.selectedvalue = 0;
    var SystemQualityScoreStatusID = 0;
    var UserQualityScoreStatusID = 0;
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        cell = masterTable.getCellByColumnUniqueName(row, userResponseColumnUniqueKey);
        if (cell.children[0] != null && cell.children[0] != 'undefined') {
            for (var j = 0; j < cell.children[0].length; j++) {
                if (cell.children[0][j].selected) {
                    UserQualityScoreStatusID = cell.children[0][j].value;
                    if (UserQualityScoreStatusID == "1252" || UserQualityScoreStatusID == "2858") {
                        obj.CompletedCount++;
                        obj.selectedvalue = userResponseColumnUniqueKey;
                    }
                }
            }
        }
    }
    return obj;
}

function OnUserResponseRecChangeReviewed(rgRecControlCheckListItems, userResponseColumnUniqueKey, OnRecControlListChanged) {
    var grid = $find(rgRecControlCheckListItems);
    if (OnRecControlListChanged != '') {
        var args = GetRecChangeReviewed(grid, userResponseColumnUniqueKey);
        ucRecControlCheckList_OnRecControlListChangedReviwed(grid, args);
    }
}

function GetRecChangeReviewed(grid, userResponseColumnUniqueKey) {
    var masterTable = grid.get_masterTableView();
    var rows = masterTable.get_dataItems();
    var cell;
    var obj = new Object();
    obj.CompletedCount = 0;
    obj.ReviwedCount = 0;
    obj.selectedvalue = 0;
    var UserQualityScoreStatusID = 0;
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        cell = masterTable.getCellByColumnUniqueName(row, userResponseColumnUniqueKey);
        if (cell.children[0] != null && cell.children[0] != 'undefined') {
            for (var j = 0; j < cell.children[0].length; j++) {
                if (cell.children[0][j].selected) {
                    UserQualityScoreStatusID = cell.children[0][j].value;
                    if (UserQualityScoreStatusID == "1252" || UserQualityScoreStatusID == "2858") {
                        obj.ReviwedCount++;
                        obj.selectedvalue = userResponseColumnUniqueKey;
                    }
                }
            }
        }
    }
    return obj;
}
