<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditRecItemInputs.aspx.cs"
    Inherits="Pages_EditRecItemInputs" MasterPageFile="~/MasterPages/PopUpMasterPage.master"
    Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="userControl" TagName="DocumentUpload" Src="~/UserControls/DocumentUploadButton.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ExchangeRateBar" Src="~/UserControls/REcForm/ExchangeRateBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="padding: 0px">
        <col width="2%" />
        <col width="20%" />
        <col width="25%" />
        <col width="3%" />
        <col width="20%" />
        <col width="25%" />
        <tr>
            <td colspan="6">
                <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetailPopup" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <webControls:ExLabel ID="lblInputFormRecPeriod" runat="server" LabelID="1420" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblInputFormRecPeriodValue" runat="server" SkinID="Black11Arial"
                    Text=""></webControls:ExLabel>
            </td>
            <td>
            </td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblRecItemNumber" runat="server" LabelID="2118" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td style="width: 35%">
                <webControls:ExLabel ID="lblRecItemNumberValue" runat="server" SkinID="Black11Arial"
                    Text=""></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <webControls:ExLabel ID="lblMatchSetRefNo" runat="server" LabelID="2276" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblMatchSetRefNoValue" runat="server" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td colspan="3">
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="5">
                <webControls:ExLabel ID="lblInstructions" runat="server" LabelID="1710" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <%--Entered By--%>
            <td>
                <webControls:ExLabel ID="lblItemInputEnteredBy" runat="server" FormatString="{0}:"
                    LabelID="1508" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblEnteredByValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
            <td>
            </td>
            <%--Enter Date--%>
            <td>
                <webControls:ExLabel ID="lblItemInputEnteredDate" runat="server" FormatString="{0}:"
                    LabelID="1399" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblAddedDate" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblAmount" runat="server" FormatString="{0}:" LabelID="1510"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblLocalCurrencyValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <asp:TextBox ID="txtAmount" runat="server" />
                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cstVldAmount" runat="server" ControlToValidate="txtAmount"
                    ClientValidationFunction="ValidateNumbers">
                </asp:CustomValidator>
            </td>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblCurrency" runat="server" FormatString="{0}:" LabelID="1409"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblLocalCurrencyType" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <asp:DropDownList ID="ddlLocalCurrency" runat="server" OnSelectedIndexChanged="ddlLocalCurrency_SelectedIndexChanged"
                    CausesValidation="false" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvLocalCurrency" runat="server" ControlToValidate="ddlLocalCurrency"></asp:RequiredFieldValidator>
                <webControls:ExHyperLink ID="hlOverrideExchangeRate" Visible="false" runat="server" LabelID="2487" />
                <asp:HiddenField ID="hdnExRateLCCYtoBCCY" runat="server" />
                <asp:HiddenField ID="hdnExRateLCCYtoRCCY" runat="server" />
                <asp:HiddenField ID="hdnIsExchangeRateOverridden" runat="server" />
                <asp:Button ID="btnOvereideExchangeRate" runat="server" OnClick="btnOvereideExchangeRate_Click"
                    Style="visibility: hidden" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <%--Open/Transaction Date--%>
                <webControls:ExLabel ID="lblOpenTransDate" runat="server" FormatString="{0}:" LabelID="1657"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblOpenTransDateValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExCalendar ID="calOpenTransDate" runat="server" Width="80" EnableViewState="true"></webControls:ExCalendar>
                <asp:RequiredFieldValidator ID="rfvOpenTransDate" runat="server" ControlToValidate="calOpenTransDate">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvOpenDate" runat="server" ControlToValidate="calOpenTransDate"
                    ClientValidationFunction="ValidateDate">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareOpenDateWithCurrentDate" runat="server" ControlToValidate="calOpenTransDate"
                    ClientValidationFunction="CompareDateWithCurrentDate">
                </asp:CustomValidator>
            </td>
            <td>
            </td>
            <td>
                <webControls:ExLabel ID="lblAttachDocs" runat="server" FormatString="{0}:" LabelID="1392"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExHyperLink ID="hlDocument" runat="server" SkinID="ShowDocumentPopupHyperLink"
                    LabelID="1540" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblComments" runat="server" FormatString="{0}:" LabelID="1408"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="4">
                <webControls:ExLabel ID="lblCommentsValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExTextBox ID="txtComments" runat="server" SkinID="ExMultilineTextBoxDescriptionRecItemForm" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">

            <td>
            </td>
        </tr>
        <asp:Panel ID="pnlCloseRecItem" runat="server" Width="100%" BorderWidth="0">
            <%--Resolution Row--%>
            <tr class="BlueRowWithPadding">
                <td colspan="6">
                    <webControls:ExLabel ID="lblResolutionHeading" runat="server" LabelID="1544" SkinID="BlueBold11Arial"></webControls:ExLabel>
                </td>
            </tr>
            <%--Blank Row--%>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <webControls:ExLabel ID="lblJournalEntryRef" runat="server" FormatString="{0}:" LabelID="1661"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblJournalEntryRefValue" runat="server" SkinID="ReadOnlyValue"
                        EnableViewState="true"></webControls:ExLabel>
                    <asp:TextBox ID="txtJournalEntryRef" runat="server" />
                </td>
                <td>
                </td>
                <td>
                    <webControls:ExLabel ID="lblResolutionCloseDate" runat="server" FormatString="{0}:"
                        LabelID="1411" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblResolutionDate" runat="server" SkinID="ReadOnlyValue"
                        EnableViewState="true"></webControls:ExLabel>
                    <webControls:ExCalendar ID="calResolutionDate" runat="server" Width="80"></webControls:ExCalendar>
                    <asp:RequiredFieldValidator ID="rfvResolutionDate" runat="server" ControlToValidate="calResolutionDate">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvResolutionDate" runat="server" ControlToValidate="calResolutionDate"
                        ClientValidationFunction="ValidateDate">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvResolutionDateCompareWithOpenDate" runat="server" ClientValidationFunction="CompareOpenAndCloseDates">
                    </asp:CustomValidator>
                    <asp:CustomValidator ID="cvResolutionDateCompareWithCurrentDate" runat="server" ControlToValidate="calResolutionDate"
                        ClientValidationFunction="CompareDateWithCurrentDate">
                    </asp:CustomValidator>
                </td>
            </tr>
            <%--Blank Row--%>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <webControls:ExLabel ID="lblResolutionComment" runat="server" FormatString="{0}:"
                        LabelID="1408" SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td colspan="4">
                    <webControls:ExLabel ID="lblResolutionCommentValue" runat="server" SkinID="ReadOnlyValue"
                        EnableViewState="true"></webControls:ExLabel>
                    <webControls:ExTextBox ID="txtResolutionComment" runat="server" SkinID="ExMultilineTextBoxDescriptionRecItemForm" />
                </td>
            </tr>
        </asp:Panel>
        <%--Button Row--%>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="AmountTable">
                    <col width="22%" />
                    <col width="4%" />
                    <col width="15%" />
                    <col width="9%" />
                    <col width="25%" />
                    <col width="4%" />
                    <col width="23%" />
                    <tr class="BlankRowWithLessHeight">
                        <td>
                        </td>
                    </tr>
                    <tr id="trExchangeRate" runat="server">
                        <td colspan="7" align="center">
                            <UserControls:ExchangeRateBar ID="ucExchangeRate" runat="server" />
                        </td>
                    </tr>
                    <%--Blank Row--%>
                    <tr class="BlankRowWithLessHeight" id="trExchangeRateBlankRow" runat="server">
                        <td>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlOverriddenExRate" runat="server" Visible="false">
                        <%--Overridden Exchange Rate Row --%>
                        <tr>
                            <td>
                                <webControls:ExLabel ID="lblOverriddenExRateBCCY" runat="server" LabelID="2490" SkinID="Red11Arial"
                                    FormatString="{0}:" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblOverriddenExRateBCCYCode" runat="server" SkinID="RedReadOnlyValue"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblOverriddenExRateBCCYValue" SkinID="RedReadOnlyValue"
                                    runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblOverriddenExRateRCCY" runat="server" LabelID="2491" SkinID="Red11Arial"
                                    FormatString="{0}:" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblOverriddenExRateRCCYCode" runat="server" SkinID="RedReadOnlyValue"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblOverriddenExRateRCCYValue" SkinID="RedReadOnlyValue"
                                    runat="server" />
                            </td>
                        </tr>
                        <%--Blank Row--%>
                        <tr class="BlankRowWithLessHeight">
                            <td>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <%--Base Currency--%>
                        <td>
                            <webControls:ExLabel ID="lblBaseCurrency" runat="server" FormatString="{0}:" LabelID="1673"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblAmountBCCYCode" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblAmountBCCYValue" runat="server" SkinID="ReadOnlyValue"
                                EnableViewState="true"></webControls:ExLabel>
                            <%--<asp:HiddenField ID="hdnBaseCurrency" runat="server" />--%>
                        </td>
                        <td>
                        </td>
                        <%--Reporting Currency--%>
                        <td>
                            <webControls:ExLabel ID="lblReportingCurrency" runat="server" FormatString="{0}:"
                                LabelID="1674" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblAmountRCCYCode" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblAmountRCCYValue" runat="server" SkinID="ReadOnlyValue"
                                EnableViewState="true"></webControls:ExLabel>
                            <%--  <asp:HiddenField ID="hdnReportingCurrency" runat="server" />--%>
                        </td>
                    </tr>
                    <%--Blank Row--%>
                    <tr class="BlankRowWithLessHeight">
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="6">
                <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" OnClick="btnUpdate_Click" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
    </table>
    <telerik:RadWindow ID="RadWindowOpenDocument" VisibleOnPageLoad="false" runat="server"
        OpenerElementID='<%imgbtnDocument.ClientID %>' Modal="true" Width="700px" Height="400px"
        Top="50px" KeepInScreenBounds="false">
    </telerik:RadWindow>

    <script type="text/javascript" language="javascript">
        function ShowDocumentUpload() {
            var oWnd = $find("<%=RadWindowOpenDocument.ClientID%>");

            if (oWnd != null && oWnd != 'undefined') {
                oWnd.setUrl("<%=this.SetDocumentUploadURL()%>");
                oWnd.show();
            }
        }

        function CompareOpenAndCloseDates(sender, args) {

            var calOpenDate = document.getElementById('<%=calOpenTransDate.ClientID%>');
            var calCloseDate = document.getElementById('<%=calResolutionDate.ClientID%>');
            var openDate;
            var closeDate = calCloseDate.value;

            if (calOpenDate == null || calOpenDate == 'undefined') {
                calOpenDate = document.getElementById('<%=lblOpenTransDateValue.ClientID%>');
                openDate = calOpenDate.firstChild.data;
            }
            else {
                openDate = calOpenDate.value;
            }

            var result = CompareDates(openDate, closeDate);

            if (result >= 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function ValidateDate(sender, args) {
            if (IsDate(document.getElementById(sender.controltovalidate).value)) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function CompareDateWithCurrentDate(sender, args) {
            var currentDate = ConvertJavascriptDateFormat(new Date());
            var dateToCompare = document.getElementById(sender.controltovalidate).value;
            var result = CompareDates(dateToCompare, currentDate);

            if (result > 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function OverrideExchangeRate(lccyToBccy, lccyToRccy) {
            var hdnExRateLCCYtoBCCY = $get('<%=hdnExRateLCCYtoBCCY.ClientID %>')
            var hdnExRateLCCYtoRCCY = $get('<%=hdnExRateLCCYtoRCCY.ClientID %>')
            var hdnIsExchangeRateOverridden = $get('<%=hdnIsExchangeRateOverridden.ClientID %>')
            var pnlOverriddenExRate = $get('<%=pnlOverriddenExRate.ClientID %>')
            var lblOverriddenExRateBCCYValue = $get('<%=lblOverriddenExRateBCCYValue.ClientID %>')
            var lblOverriddenExRateRCCYValue = $get('<%=lblOverriddenExRateRCCYValue.ClientID %>')
            hdnIsExchangeRateOverridden.value = 1;
            if (pnlOverriddenExRate != null)
                pnlOverriddenExRate.style.display = 'inline';
            if (lccyToBccy != null && lccyToBccy != 0) {
                hdnExRateLCCYtoBCCY.value = lccyToBccy;
                if(lblOverriddenExRateBCCYValue != null)
                    lblOverriddenExRateBCCYValue.firstChild.data = GetDisplayDecimalValue(lccyToBccy, <%=TestConstant.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND %>);
            }
            if (lccyToRccy != null && lccyToRccy != 0) {
                hdnExRateLCCYtoRCCY.value = lccyToRccy;
                if (lblOverriddenExRateRCCYValue != null)
                    lblOverriddenExRateRCCYValue.firstChild.data = GetDisplayDecimalValue(lccyToRccy, <%=TestConstant.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND %>);
            }
            $get('<%=btnOvereideExchangeRate.ClientID%>').click();
        }

    </script>

</asp:Content>
