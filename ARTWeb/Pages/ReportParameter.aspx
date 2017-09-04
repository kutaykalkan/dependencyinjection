<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_ReportParameter" Title="Untitled Page"
    Theme="SkyStemBlueBrown" MaintainScrollPositionOnPostback="true" Codebehind="ReportParameter.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register Src="~/UserControls/LegendOnAccountSearch.ascx" TagName="LegendOnAccountSearch"
    TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/Report/OrganizationalHierarchy.ascx" TagName="ucOrgHierarchy"
    TagPrefix="rptCriteria" %>
<%@ Register Src="~/UserControls/Report/FinancialYear.ascx" TagName="ucFinancialYear"
    TagPrefix="rptCriteria" %>
<%@ Register Src="~/UserControls/Report/RecPeriod.ascx" TagName="ucRecPeriod" TagPrefix="rptCriteria" %>
<%@ Register Src="~/UserControls/Report/ScrollableCheckboxListWithSelectAll.ascx"
    TagName="ucScrollableCheckboxList" TagPrefix="rptCriteria" %>
<%@ Register Src="~/UserControls/Report/RadioButtonListAllYesNo.ascx" TagPrefix="rptCriteria"
    TagName="ucRadioButtonListYesNoAll" %>
<%@ Register Src="~/UserControls/Report/StringRange.ascx" TagPrefix="rptCriteria"
    TagName="ucStringRange" %>
<%@ Register Src="~/UserControls/Report/DateRange.ascx" TagPrefix="rptCriteria" TagName="ucDateRange" %>
<%@ Register Src="~/UserControls/Report/UserRole.ascx" TagPrefix="rptCriteria" TagName="ucRoleUser" %>
<%@ Register Src="~/UserControls/Report/SingleDate.ascx" TagPrefix="rptCriteria"
    TagName="ucSingleDate" %>
<%@ Register Src="~/UserControls/Report/SimpleDropdownList.ascx" TagPrefix="rptCriteria"
    TagName="ucSimpleDropDownList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divParamaterMyReport" style="padding-left: 5px; padding-right: 5px;">
        <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
            <col width="70%" />
            <col width="30%" />
            <%--Report Heading row--%>
            <tr align="center">
                <td colspan="2">
                    <webControls:ExLabel ID="lblReportDetails" runat="server" SkinID="ReportTitle"></webControls:ExLabel>
                </td>
            </tr>
            <%--Criteria Controls Row--%>
            <tr id="trCriteria" runat="server">
                <td>
                    <asp:UpdatePanel ID="updPnlMain" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucSimpleDropDownList ID="ucTaskType" runat="server" Visible="true" LabelID="2706"
                                            ErrorLabelID="5000371" isRequired="true" CheckBoxListWidth="250px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucOrgHierarchy ID="ucOrgHierarchy" runat="server" Visible="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucFinancialYear ID="ucFinancialYear" runat="server" Visible="true" OnFinancialYearChangedHandler="FinancialYear_Changed" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucRecPeriod ID="ucRecPeriod" runat="server" Visible="true" OnRecPeriodChangedHandler="RecPeriod_Changed" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucSingleDate ID="ucSingleDate" runat="server" Visible="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucRadioButtonListYesNoAll ID="ucKeyAccount" runat="server" Visible="true"
                                            LabelID="1014" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucReasonCode" runat="server" Visible="true"
                                            LableID="1588" ErrorLabelID="5000174" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucRadioButtonListYesNoAll ID="ucMaterialAccount" runat="server" Visible="true"
                                            LabelID="1590" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucRiskRating" runat="server" Visible="true"
                                            LableID="1013" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucStringRange ID="ucAccountRange" runat="server" Visible="true" ErrorLabelID="5000170" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucRoleUser ID="ucRoleUser" runat="server" Visible="true" OnbtnFetchUserHandler="FetchClicked" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucOpenItemClassification" runat="server"
                                            Visible="true" LableID="1842" ErrorLabelID="5000175" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucRoleUser ID="ucPreparer" runat="server" Visible="true" OnbtnFetchUserHandler="FetchClickedPreparer" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucDateRange ID="ucOpenDate" runat="server" Visible="true" LabelID="1511" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucDateRange ID="ucCloseDate" runat="server" Visible="true" LabelID="1411" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucDateRange ID="ucDateCreated" runat="server" Visible="true" LabelID="2557" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucDateRange ID="ucChangeDate" runat="server" Visible="true" LabelID="1333" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucAging" runat="server" Visible="true"
                                            LableID="1512" ErrorLabelID="5000176" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucRecStatus" runat="server" Visible="true"
                                            LableID="1370" CheckBoxListWidth="250px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucTaskStatus" runat="server" Visible="true"
                                            LableID="2576" CheckBoxListWidth="250px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucTaskListName" runat="server" Visible="true"
                                            LableID="2584" CheckBoxListWidth="250px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucExceptionType" runat="server" Visible="true"
                                            LableID="1843" ErrorLabelID="5000171" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucStringRange LabelID="2467" ID="ucSystemScoreRange" runat="server"
                                            Visible="true" ErrorLabelID="5000316" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucStringRange LabelID="2468" ID="ucUserScoreRange" runat="server" Visible="true"
                                            ErrorLabelID="5000317" />
                                    </td>
                                </tr>
                                <tr style="width: 100%;">
                                    <td colspan="5" style="width: 100%;">
                                        <rptCriteria:ucScrollableCheckboxList CheckBoxListWidth="455px" ID="ucChecklistItem"
                                            runat="server" Visible="true" LableID="2463" ErrorLabelID="5000315" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <rptCriteria:ucScrollableCheckboxList ID="ucDisplayColumn" runat="server" Visible="true"
                                            LableID="2305" CheckBoxListWidth="250px" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="vertical-align: top;" id="tdCriteria" runat="server">
                    <asp:Panel Style="height: inherit;" ID="pnlCriteria" runat="server" ScrollBars="Auto"
                        Visible="false">
                        <table id="tblCriteria" runat="server" border="0" cellpadding="1" cellspacing="0"
                            width="100%">
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="height: 20px;">
                <td>
                </td>
            </tr>
            <%--Button Row--%>
            <tr>
                <td colspan="2" align="right">
                    <webControls:ExButton ID="btnRunReport" runat="server" OnClick="btnRunReport_Click"
                        LabelID="1577 " SkinID="ExButton100" CausesValidation="true" />
<%--                    <webControls:ExButton ID="btnExportToExcelAndEmailReport" runat="server" OnClick="btnExportToExcelAndEmailReport_Click"
                        LabelID="3076 " SkinID="ExButton200" CausesValidation="true" />--%>
                    <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                        CausesValidation="false" SkinID="ExButton100" />
                </td>
            </tr>
            <%--Legend Control Row--%>
            <tr>
                <td colspan="2">
                    <UserControl:LegendOnAccountSearch ID="LegendOnAccountSearch" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <UserControls:ProgressBar ID="ucAccountViewer" runat="server" EnableTheming="true"
                        AssociatedUpdatePanelID="updPnlMain" Visible="true" />
                </td>
            </tr>
        </table>
    </div>
    <ajaxToolkit:RoundedCornersExtender Corners="All" BorderColor="#dae9fa" Radius="6"
        TargetControlID="pnlCriteria" ID="RoundedCornersExtender1" runat="server">
    </ajaxToolkit:RoundedCornersExtender>
    <asp:HiddenField ID="hdnHeight" runat="server" />
    <asp:HiddenField ID="hdnWidth" runat="server" />

    <script type="text/javascript" language="javascript">

        var tblCriteria = $get('<% =this.tblCriteria.ClientID %>');
        var key2HdnCtrl = $get('<% =this.ucOrgHierarchy.Key2HdnControlClientID %>');
        var key3HdnCtrl = $get('<% =this.ucOrgHierarchy.Key3HdnControlClientID %>');
        var key4HdnCtrl = $get('<% =this.ucOrgHierarchy.Key4HdnControlClientID %>');
        var key5HdnCtrl = $get('<% =this.ucOrgHierarchy.Key5HdnControlClientID %>');
        var key6HdnCtrl = $get('<% =this.ucOrgHierarchy.Key6HdnControlClientID %>');
        var key7HdnCtrl = $get('<% =this.ucOrgHierarchy.Key7HdnControlClientID %>');
        var key8HdnCtrl = $get('<% =this.ucOrgHierarchy.Key8HdnControlClientID %>');
        var key9HdnCtrl = $get('<% =this.ucOrgHierarchy.Key9HdnControlClientID %>');
        var validationControl = $get('<% =this.ucOrgHierarchy.hdnControlforValidation %>');

        var seperator = '<% =this.GetSeperator %>';

        var td = $get('<%=this.tdCriteria.ClientID %>');
        var pnlCriteria = $get('<% =this.pnlCriteria.ClientID %>');
        var hdnHeight = $get('<% =this.hdnHeight.ClientID %>');
        var hdnWidth = $get('<% =this.hdnWidth.ClientID %>');
        window.onload = fnSetHeightForCriteriaPnl;

        function fnSetHeightForCriteriaPnl() {

            //debugger;
            var height;
            var width;
            if (pnlCriteria != null && td != null) {
                if (hdnHeight != null && hdnHeight.value == '')
                    hdnHeight.value = td.offsetHeight;

                if (hdnWidth != null && hdnWidth.value == '')
                    hdnWidth.value = td.offsetWidth;
                pnlCriteria.style["height"] = hdnHeight.value + 'px';
                pnlCriteria.style.width = hdnWidth.value - 2;
            }

            ReadValuesfromCtrlsAndMakeTable();
        }
        function OrgHierarchyCriteria(param1, param2, param3, param4) {

            //Get objects 
            var entityddl = $get(param1);
            var txtEntity = $get(param2);
            //get Object values
            var selectedKeyName = entityddl[entityddl.selectedIndex].text;
            var selectedKeyValue = entityddl[entityddl.selectedIndex].value;
            var enteredValue = txtEntity.value;
            if (enteredValue != '') {
                AddValueToHdnControl(selectedKeyValue, selectedKeyName, enteredValue);
            }
            return false;
        }
        function AddValueToHdnControl(keyValue, keyName, enteredValue) {
            if (keyValue == '2') {
                var v1 = (key2HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key2HdnCtrl.value = key2HdnCtrl.value + v1;
            }
            if (keyValue == '3') {
                var v1 = (key3HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key3HdnCtrl.value = key3HdnCtrl.value + v1;
            }
            if (keyValue == '4') {
                var v1 = (key4HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key4HdnCtrl.value = key4HdnCtrl.value + v1;
            }
            if (keyValue == '5') {
                var v1 = (key5HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key5HdnCtrl.value = key5HdnCtrl.value + v1;
            }
            if (keyValue == '6') {
                var v1 = (key6HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key6HdnCtrl.value = key6HdnCtrl.value + v1;
            }
            if (keyValue == '7') {
                var v1 = (key7HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key7HdnCtrl.value = key7HdnCtrl.value + v1;
            }
            if (keyValue == '8') {
                var v1 = (key8HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key8HdnCtrl.value = key8HdnCtrl.value + v1;
            }
            if (keyValue == '9') {
                var v1 = (key9HdnCtrl.value == '') ? enteredValue : seperator + enteredValue;
                key9HdnCtrl.value = key9HdnCtrl.value + v1;
            }
            ReadValuesfromCtrlsAndMakeTable();
        }
        function ReadValuesfromCtrlsAndMakeTable() {
            DeleteAllrowsFromTable();
            if (tblCriteria != null) {

                ShowInTable(key2HdnCtrl);
                ShowInTable(key3HdnCtrl);
                ShowInTable(key4HdnCtrl);
                ShowInTable(key5HdnCtrl);
                ShowInTable(key6HdnCtrl);
                ShowInTable(key7HdnCtrl);
                ShowInTable(key8HdnCtrl);
                ShowInTable(key9HdnCtrl);
            }
        }
        function ShowInTable(KeyControl) {
            if (KeyControl != null) {
                var keyValues = KeyControl.value;
                if (validationControl != null)
                    validationControl.value = validationControl.value + keyValues;
                if (keyValues != '') {
                    var arrKeyValues = keyValues.split(seperator);
                    var len = arrKeyValues.length;

                    var rows = (tblCriteria.rows != null) ? tblCriteria.rows.length : 0;
                    for (var i = 0; i < len; i++) {
                        var newRow = tblCriteria.insertRow(rows + i);
                        newRow.className = "OrgHierarchyRowStyle";

                        var oCell1 = newRow.insertCell(0);
                        var oCell2 = newRow.insertCell(1);
                        var oCell3 = newRow.insertCell(2);
                        var oCell4 = newRow.insertCell(3);
                        oCell1.innerHTML = KeyControl.attributes["GeoStruct"].value;
                        oCell2.innerHTML = "=";
                        oCell3.innerHTML = arrKeyValues[i];
                        oCell4.innerHTML = "<input type='image' src='../App_Themes/SkyStemBlueBrown/Images/Delete.gif' " +
"alt='Delete' value='Delete' onclick=' return DeleteValueFromHdnKeyControl(this," + KeyControl.id + ");'/>";
                    }

                }
            }
        }
        function DeleteAllrowsFromTable() {
            if (tblCriteria != null) {
                var limit = 0;
                var rows = tblCriteria.rows;
                if (limit > rows.length)
                    return;
                for (; rows.length > limit; ) {
                    tblCriteria.deleteRow(limit);
                }
            }
            if (validationControl != null)
                validationControl.value = "";
        }
        function DeleteValueFromHdnKeyControl(src, keyControlID) {
            var oRow = src.parentElement.parentElement;
            var keyValue = oRow.cells[2].innerHTML;
            if (keyControlID != null) {
                var keyValues = keyControlID.value;
                var arrKeyValues = keyValues.split(seperator);
                for (var i = 0; i < arrKeyValues.length; i++) {
                    if (arrKeyValues[i] == keyValue) {
                        arrKeyValues.splice(i, 1);
                        break;
                    }
                }
                var va = arrKeyValues.toString().replace(/,/gi, seperator);
                keyControlID.value = va;
            }
            ReadValuesfromCtrlsAndMakeTable();
            return false;
        }
    </script>

</asp:Content>
