<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_EditItemAmortizable" Theme="SkyStemBlueBrown" Codebehind="EditItemAmortizable.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="userControl" TagName="DocumentUpload" Src="~/UserControls/DocumentUploadButton.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ExchangeRateBar" Src="~/UserControls/REcForm/ExchangeRateBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ScheduleIntervalDetails" Src="~/UserControls/ScheduleIntervalDetails.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="padding: 0px">
        <col width="2%" />
        <col width="20%" />
        <col width="14%" />
        <col width="14%" />
        <col width="2%" />
        <col width="18%" />
        <col width="10%" />
        <col width="13%" />
        <col width="10%" />
        <tr>
            <td colspan="9">
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
            <td style="width: 20%">
                <webControls:ExLabel ID="lblInputFormRecPeriod" runat="server" LabelID="1420" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 35%" colspan="2">
                <webControls:ExLabel ID="lblInputFormRecPeriodValue" runat="server" SkinID="Black11Arial"
                    Text=""></webControls:ExLabel>
                <asp:HiddenField ID="hdnPrevRecPeriodEndDate" runat="server" Value="" />
                <asp:HiddenField ID="hdnPrevGLDataRecurringItemScheduleID" runat="server" Value="" />
            </td>
            <td>
            </td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblRecItemNumber" runat="server" LabelID="2118" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 35%" colspan="3">
                <webControls:ExLabel ID="lblRecItemNumberValue" runat="server" SkinID="Black11Arial"
                    Text=""></webControls:ExLabel>
            </td>
        </tr>
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
            <td colspan="2">
                <webControls:ExLabel ID="lblMatchSetRefNoValue" runat="server" SkinID="Black11Arial"
                    FormatString="{0}:"></webControls:ExLabel>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="8">
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
                <webControls:ExLabel ID="lblEnteredBy" runat="server" FormatString="{0}:" LabelID="1508"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <webControls:ExLabel ID="lblEnteredByValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
            <td>
            </td>
            <%--Enter Date--%>
            <td>
                <webControls:ExLabel ID="lblDateAdded" runat="server" FormatString="{0}:" LabelID="1399"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="3">
                <webControls:ExLabel ID="lblDateAddedValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
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
            <%--Calculation Frequency--%>
            <td>
                <webControls:ExLabel ID="lblCalculationFrequency" runat="server" FormatString="{0}:"
                    LabelID="2246" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <%--Daily Interval--%>
            <td>
                <webControls:ExRadioButton ID="optDailyInterval" Checked="true" LabelID="2247" SkinID="Black11Arial"
                    GroupName="CalculationFrequency" runat="server"></webControls:ExRadioButton>
            </td>
            <%--Other Interval--%>
            <td>
                <webControls:ExRadioButton ID="optOtherInterval" LabelID="2248" SkinID="Black11Arial"
                    GroupName="CalculationFrequency" runat="server"></webControls:ExRadioButton>
            </td>
            <td>
            </td>
            <%--Total Intervals --%>
            <td>
                <webControls:ExLabel ID="lblTotalIntervals" runat="server" FormatString="{0}:" LabelID="2249"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtTotalIntervals" Enabled="false" MaxLength="6" SkinID="TextBox50"
                    runat="server" />
                <webControls:ExRequiredFieldValidator ID="rfvTotalIntervals" runat="server" ControlToValidate="txtTotalIntervals"
                    LabelID="2253" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium">
                </webControls:ExRequiredFieldValidator>
                <webControls:ExCompareValidator ID="cmpvTotalIntervals" runat="server" ValueToCompare="0"
                    ControlToValidate="txtTotalIntervals" LabelID="2256" Operator="GreaterThan" Type="Integer"></webControls:ExCompareValidator>
                <%--                <webControls:ExRegularExpressionValidator ID="revTotalIntervals" runat="server" ControlToValidate="txtTotalIntervals"
                    ValidationExpression="\b\d+\b" LabelID="2256"></webControls:ExRegularExpressionValidator>
--%>
                <webControls:ExCustomValidator ID="cvTotalIntervals" runat="server" ControlToValidate="txtTotalIntervals"
                    LabelID="2256" ClientValidationFunction="ValidateNumbers" Enabled="false" Text="!"
                    Font-Bold="true" Font-Size="Medium">
                </webControls:ExCustomValidator>
            </td>
            <%--Current Interval No --%>
            <td>
                <webControls:ExLabel ID="lblCurrentInterval" runat="server" FormatString="{0}:" LabelID="2250"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <asp:TextBox ID="txtCurrentInterval" Enabled="false" MaxLength="6" SkinID="TextBox50"
                    runat="server" />
                <webControls:ExLabel ID="lblCurrentIntervalValue" runat="server" SkinID="ReadOnlyValue" />
                <webControls:ExRequiredFieldValidator ID="rfvCurrentInterval" runat="server" ControlToValidate="txtCurrentInterval"
                    LabelID="2254" Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium">
                </webControls:ExRequiredFieldValidator>
                <webControls:ExCustomValidator ID="cvCurrentInterval" runat="server" ControlToValidate="txtCurrentInterval"
                    LabelID="2257" ClientValidationFunction="ValidateNumbers" Enabled="false" Text="!"
                    Font-Bold="true" Font-Size="Medium">
                </webControls:ExCustomValidator>
                <%--                <webControls:ExRegularExpressionValidator ID="revCurrentInterval" runat="server"
                    ControlToValidate="txtCurrentInterval" ValidationExpression="\b\d+\b" LabelID="2257"></webControls:ExRegularExpressionValidator>
--%>
                <webControls:ExCompareValidator ID="cmpvCurrentInterval" runat="server" ControlToValidate="txtCurrentInterval"
                    LabelID="2255" ControlToCompare="txtTotalIntervals" Type="Integer" Enabled="false"
                    Text="!" Operator="LessThanEqual" Font-Bold="true" Font-Size="Medium">
                </webControls:ExCompareValidator>
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
                <webControls:ExLabel ID="lblOriginalAmountLCCY" runat="server" FormatString="{0}:"
                    LabelID="1700 " SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <webControls:ExLabel ID="lblOriginalAmountLCCYValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <asp:TextBox ID="txtOriginalAmountLCCY" MaxLength="13" runat="server" />
                <asp:RequiredFieldValidator ID="rfvOriginalAmount" runat="server" ControlToValidate="txtOriginalAmountLCCY"
                    Text="!" Font-Bold="true" Font-Size="Medium">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvOriginalAmount" runat="server" Text="!" OnServerValidate="cvOriginalAmount_ServerValidate"
                    ControlToValidate="txtOriginalAmountLCCY" Font-Bold="true" Font-Size="Medium"
                    ClientValidationFunction="ValidateNumbers">
                </asp:CustomValidator>
                <asp:HiddenField ID="hdnTotalAmortizedAmountRCCYValue" runat="server" />
            </td>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblLocalCurrencyCode" runat="server" FormatString="{0}:"
                    LabelID="1409" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="3">
                <webControls:ExLabel ID="lblLocalCurrencyCodeValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <asp:DropDownList ID="ddlLocalCurrency" runat="server" CausesValidation="true" OnSelectedIndexChanged="ddlLocalCurrency_SelectedIndexChanged"
                    AutoPostBack="true">
                    <%--AutoPostBack="true"--%>
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
                <webControls:ExLabel ID="lblOpenDate" runat="server" FormatString="{0}:" LabelID="1657"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <webControls:ExLabel ID="lblOpenDateValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExCalendar ID="calOpenDate" runat="server" Width="80"></webControls:ExCalendar>
                <asp:RequiredFieldValidator ID="rfvOpenDate" runat="server" ControlToValidate="calOpenDate">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvOpenDate" runat="server" ControlToValidate="calOpenDate"
                    ClientValidationFunction="ValidateDate"></asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareOpenDateWithCurrentDate" runat="server" ControlToValidate="calOpenDate"
                    ClientValidationFunction="CompareDateWithCurrentDate">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareOpenDateWithScheduleEndDate" runat="server" ControlToValidate="calOpenDate"
                    ClientValidationFunction="CompareOpenDateAndScheduleEndDates">
                </asp:CustomValidator>
            </td>
            <td>
            </td>
            <td>
                <webControls:ExLabel ID="lblAttachDocs" runat="server" FormatString="{0}:" LabelID="1392"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="3">
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
            <td class="ManadatoryField">
                *
            </td>
            <%--Schedule Name--%>
            <td>
                <webControls:ExLabel ID="lblScheduleName" runat="server" FormatString="{0}:" LabelID="1669"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <webControls:ExLabel ID="lblScheduleNameValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExTextBox ID="txtScheduleName" runat="server" SkinID="ExTextbox150"
                    IsRequired="true" MaxLength="50" />
            </td>
            <%--Comments--%>
            <td>
            </td>
            <td valign="top" rowspan="6">
                <webControls:ExLabel ID="lblComments" runat="server" FormatString="{0}:" LabelID="1408"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td rowspan="6" colspan="3" valign="top">
                <webControls:ExLabel ID="lblCommentsValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExTextBox ID="txtComments" MaxLength="1000" runat="server" SkinID="ExMultilineTextBoxDescriptionScheduleForm" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <%--Schedule Begin Date--%>
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblScheduleBeginDate" runat="server" FormatString="{0}:"
                    LabelID="1670" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <webControls:ExLabel ID="lblScheduleBeginDateValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExCalendar ID="calScheduleBeginDate" runat="server" Width="80"></webControls:ExCalendar>
                <asp:RequiredFieldValidator ID="rfvScheduleBeginDate" runat="server" ControlToValidate="calScheduleBeginDate">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvScheduleBeginDate" runat="server" ControlToValidate="calScheduleBeginDate"
                    ClientValidationFunction="ValidateDate">
                </asp:CustomValidator>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <%--Schedule End Date--%>
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblScheduleEndDate" runat="server" FormatString="{0}:" LabelID="1671"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <webControls:ExLabel ID="lblScheduleEndDateValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExCalendar ID="calScheduleEndDate" runat="server" Width="80"></webControls:ExCalendar>
                <asp:RequiredFieldValidator ID="rfvScheduleEndDate" runat="server" ControlToValidate="calScheduleEndDate">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvScheduleEndDate" runat="server" ControlToValidate="calScheduleEndDate"
                    ClientValidationFunction="ValidateDate">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareScheduleBeginDateWithScheduleEndDate" runat="server"
                    ClientValidationFunction="CompareScheduleBeginAndEndDates"
                    OnServerValidate="cvCompareScheduleBeginDateWithScheduleEndDate_OnServerValidate">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareRecPeriodEndDateWithScheduleDates" runat="server"
                    ClientValidationFunction="CompareRecPeriodEndDateWithScheduleDates" ControlToValidate="calScheduleEndDate"
                    OnServerValidate="cvCompareRecPeriodEndDateWithScheduleDates_OnServerValidate">
                </asp:CustomValidator>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow" id="trCloseDateBlankRow" runat="server">
            <td>
            </td>
        </tr>
        <tr id="trCloseDate" runat="server">
            <td>
            </td>
            <td>
                <webControls:ExLabel ID="lblCloseDate" runat="server" FormatString="{0}:" LabelID="1411"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <webControls:ExLabel ID="lblCloseDateValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExCalendar ID="calCloseDate" runat="server" Width="80"></webControls:ExCalendar>
                <asp:CustomValidator ID="cvCloseDate" runat="server" ControlToValidate="calCloseDate"
                    ClientValidationFunction="ValidateDate">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareCloseDateWithOpenDate" runat="server" ControlToValidate="calCloseDate"
                    ClientValidationFunction="CompareOpenAndCloseDates">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareCloseDateWithCurrentDate" runat="server" ControlToValidate="calCloseDate"
                    ClientValidationFunction="CompareDateWithCurrentDate">
                </asp:CustomValidator>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr align="right">
            <td colspan="9">
                <webControls:ExButton ID="btnRecalculateSchedule" CausesValidation="true" LabelID="2311"
                    runat="server" OnClick="btnRecalculateSchedule_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <UserControls:ScheduleIntervalDetails ID="ucScheduleIntervalDetails" OnScheduleAmountChanged="ucScheduleIntervalDetails_OnScheduleAmountChanged"
                    runat="server" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="AmountTable">
                    <col width="28%" />
                    <col width="4%" />
                    <col width="15%" />
                    <col width="9%" />
                    <col width="25%" />
                    <col width="4%" />
                    <col width="15%" />
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
                    <%--Overridden Exchange Rate Row --%>
                    <asp:Panel ID="pnlOverriddenExRate" runat="server" Visible="false">
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
                        <td valign="top">
                            <webControls:ExLabel ID="ExLabel2" runat="server" FormatString="{0}:" LabelID="2045"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblOriginalAmountRCCYCode" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td valign="top" align="right">
                            <webControls:ExLabel ID="lblOriginalAmountRCCYValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td>
                        </td>
                        <td valign="top">
                            <webControls:ExLabel ID="ExLabel1" runat="server" FormatString="{0}:" LabelID="2044 "
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblCurrentAmountRCCYCode" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblCurrentAmountRCCYValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <%--Blank Row--%>
                    <tr class="BlankRowWithLessHeight">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <%--Rec Period Amount/ or MonthlyAmount--%>
                        <td valign="top">
                            <webControls:ExLabel ID="ExLabl14" runat="server" FormatString="{0}:" LabelID="2070"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblTotalAmortizedAmountRCCYCode" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td valign="top" align="right">
                            <webControls:ExLabel ID="lblTotalAmortizedAmountRCCYValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td valign="top" colspan="4">
                        </td>
                    </tr>
                    <%--Blank Row--%>
                    <tr class="BlankRowWithLessHeight">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <%--Rec Period Amount/ or MonthlyAmount--%>
                        <td valign="top">
                            <webControls:ExLabel ID="ExLabel3" runat="server" FormatString="{0}:" LabelID="2071"
                                SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblToBeAmortizedAmountRCCYCode" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td valign="top" align="right">
                            <webControls:ExLabel ID="lblToBeAmortizedAmountRCCYValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                        </td>
                        <td valign="top" colspan="4">
                        </td>
                    </tr>
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
        <%--Button Row--%>
        <tr>
            <td align="right" colspan="9">
                <asp:HiddenField ID="hdnOriginalGLDataRecurringItemScheduleID" runat="server" />
                <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" CausesValidation="true"
                    OnClick="btnUpdate_Click" />
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

        function ucScheduleIntervalDetails_OnScheduleAmountChanged(sender, args) {
            //alert(args.OriginalAmount);
            //alert(args.CurrentConsumedAmount);
            //alert(args.TotalConsumedAmount);
            var lblOriginalAmountRCCYValue = $get('<%=lblOriginalAmountRCCYValue.ClientID%>')
            var lblCurrentAmountRCCYValue = $get('<%=lblCurrentAmountRCCYValue.ClientID%>')
            var lblTotalAmortizedAmountRCCYValue = $get('<%=lblTotalAmortizedAmountRCCYValue.ClientID%>')
            var lblToBeAmortizedAmountRCCYValue = $get('<%=lblToBeAmortizedAmountRCCYValue.ClientID%>')
            var hdnExRateLCCYtoRCCY = $get('<%=hdnExRateLCCYtoRCCY.ClientID%>')
            var exRate = parseFloat(hdnExRateLCCYtoRCCY.value);
            lblOriginalAmountRCCYValue.firstChild.data = GetDisplayDecimalValue(ConvertCurrency(args.OriginalAmount, exRate, args.DecimalPlacesExRate), args.DecimalPlaces);
            lblCurrentAmountRCCYValue.firstChild.data = GetDisplayDecimalValue(ConvertCurrency(args.CurrentConsumedAmount, exRate, args.DecimalPlacesExRate), args.DecimalPlaces);
            lblTotalAmortizedAmountRCCYValue.firstChild.data = GetDisplayDecimalValue(ConvertCurrency(args.TotalConsumedAmount, exRate, args.DecimalPlacesExRate), args.DecimalPlaces);
            lblToBeAmortizedAmountRCCYValue.firstChild.data = GetDisplayDecimalValue(ConvertCurrency(args.OriginalAmount - args.TotalConsumedAmount, exRate, args.DecimalPlacesExRate), args.DecimalPlaces);
        }

        function ShowDocumentUpload() {
            var oWnd = $find("<%=RadWindowOpenDocument.ClientID%>");

            if (oWnd != null && oWnd != 'undefined') {
                oWnd.setUrl("<%=this.SetDocumentUploadURL()%>");
                oWnd.show();
            }
        }

        function CompareOpenAndCloseDates(sender, args) {

            var calOpenDate = document.getElementById('<%=calOpenDate.ClientID%>');
            var calCloseDate = document.getElementById('<%=calCloseDate.ClientID%>');
            var openDate;
            var closeDate = calCloseDate.value;

            if (calOpenDate == null || calOpenDate == 'undefined') {
                calOpenDate = document.getElementById('<%=lblOpenDateValue.ClientID%>');
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

        function CompareScheduleBeginAndEndDates(sender, args) {

            var calOpenDate = document.getElementById('<%=calScheduleBeginDate.ClientID%>');
            var calCloseDate = document.getElementById('<%=calScheduleEndDate.ClientID%>');
            var openDate;
            var closeDate = calCloseDate.value;
            if (calOpenDate == null || calOpenDate == 'undefined') {
                calOpenDate = document.getElementById('<%=lblScheduleBeginDateValue.ClientID%>');
                openDate = calOpenDate.firstChild.data;
            }
            else
                openDate = calOpenDate.value;
            var result = CompareDates(openDate, closeDate);

            if (result >= 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function CompareOpenDateAndScheduleEndDates(sender, args) {

            var calOpenDate = document.getElementById('<%=calOpenDate.ClientID%>');
            var calCloseDate = document.getElementById('<%=calScheduleEndDate.ClientID%>');
            var openDate = calOpenDate.value;
            var closeDate = calCloseDate.value;
            var result = CompareDates(openDate, closeDate);

            if (result >= 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }

        function CompareRecPeriodEndDateWithScheduleDates(sender, args) {
            var lblInputFormRecPeriodValue = document.getElementById('<%= lblInputFormRecPeriodValue.ClientID%>');
            var calOpenDate = document.getElementById('<%=calScheduleBeginDate.ClientID%>');
            var calCloseDate = document.getElementById('<%=calScheduleEndDate.ClientID%>');
            // bug  7579 var recPeriodEndDate = lblInputFormRecPeriodValue.innerText;
            var recPeriodEndDate = lblInputFormRecPeriodValue.firstChild.data;
            var openDate;
            if (calOpenDate == null || calOpenDate == 'undefined') {
                calOpenDate = document.getElementById('<%=lblScheduleBeginDateValue.ClientID%>');
                openDate = calOpenDate.firstChild.data;
            }
            else
                openDate = calOpenDate.value;
            var closeDate = calCloseDate.value;

            var compareWithOpenDate = CompareDates(recPeriodEndDate, openDate);
            var compareWithCloseDate = CompareDates(closeDate, recPeriodEndDate);

            if (compareWithOpenDate >= 0
            && compareWithCloseDate >= 0) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function EnableDisableIntervalControls() {
            var optOtherInterval = document.getElementById('<%= optOtherInterval.ClientID %>');
            var txtTotalIntervals = document.getElementById('<%= txtTotalIntervals.ClientID %>');
            var rfvTotalIntervals = document.getElementById('<%= rfvTotalIntervals.ClientID %>');
            var cmpvTotalIntervals = document.getElementById('<%= cmpvTotalIntervals.ClientID %>');
            //var revTotalIntervals = document.getElementById('revTotalIntervals.ClientID');
            var cvTotalIntervals = document.getElementById('<%= cvTotalIntervals.ClientID %>');

            var txtCurrentInterval = document.getElementById('<%= txtCurrentInterval.ClientID %>');
            var rfvCurrentInterval = document.getElementById('<%= rfvCurrentInterval.ClientID %>');
            var cmpvCurrentInterval = document.getElementById('<%= cmpvCurrentInterval.ClientID %>');
            //var revCurrentInterval = document.getElementById('revCurrentInterval.ClientID');
            var cvCurrentInterval = document.getElementById('<%= cvCurrentInterval.ClientID %>');

            var calScheduleBeginDate = document.getElementById('<%=calScheduleBeginDate.ClientID%>');
            var calScheduleEndDate = document.getElementById('<%=calScheduleEndDate.ClientID%>');
            var rfvScheduleBeginDate = document.getElementById('<%=rfvScheduleBeginDate.ClientID%>');
            var rfvScheduleEndDate = document.getElementById('<%=rfvScheduleEndDate.ClientID%>');

            txtTotalIntervals.disabled = !optOtherInterval.checked;
            rfvTotalIntervals.enabled = optOtherInterval.checked;
            ValidatorUpdateDisplay(rfvTotalIntervals);
            cmpvTotalIntervals.enabled = optOtherInterval.checked;
            ValidatorUpdateDisplay(cmpvTotalIntervals);
            //revTotalIntervals.enabled = optOtherInterval.checked;
            //ValidatorUpdateDisplay(revTotalIntervals);
            cvTotalIntervals.enabled = optOtherInterval.checked;
            ValidatorUpdateDisplay(cvTotalIntervals);

            txtCurrentInterval.disabled = !optOtherInterval.checked;
            rfvCurrentInterval.enabled = optOtherInterval.checked;
            ValidatorUpdateDisplay(rfvCurrentInterval);
            //revCurrentInterval.enabled = optOtherInterval.checked;
            //ValidatorUpdateDisplay(revCurrentInterval);
            cmpvCurrentInterval.enabled = optOtherInterval.checked;
            ValidatorUpdateDisplay(cmpvCurrentInterval);
            cvCurrentInterval.enabled = optOtherInterval.checked;
            
            //calScheduleBeginDate.disabled = optOtherInterval.checked;
            //calScheduleEndDate.disabled = optOtherInterval.checked;            
            rfvScheduleBeginDate.enabled = !optOtherInterval.checked;            
            rfvScheduleEndDate.enabled = !optOtherInterval.checked;            
            if(optOtherInterval.checked)           
            {
                //calScheduleBeginDate.value = '';
                //calScheduleEndDate.value = '';
            }
            else
            {
                txtTotalIntervals.value = '';
                txtCurrentInterval.value = '';
            }
            ValidatorUpdateDisplay(cvCurrentInterval);
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
                if (lblOverriddenExRateBCCYValue != null)
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
