<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_BulkCloseAccrubleRecurring"
    Theme="SkyStemBlueBrown" Codebehind="BulkCloseAccrubleRecurring.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="userControl" TagName="DocumentUpload" Src="~/UserControls/DocumentUploadButton.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="padding: 0px">
        <tr>
            <td colspan="5">
                <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetailPopup" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr class="RowWithPadding">
            <td>
            </td>
            <%--Base Currency--%>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblTotalBaseCurrency" runat="server" FormatString="{0}:"
                    LabelID="1769 " SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 30%">
                <webControls:ExLabel ID="lblTotalBaseCurrencyValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
            </td>
            <%--Reporting Currency--%>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblTotalReportingCurrency" runat="server" FormatString="{0}:"
                    LabelID="1770 " SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblTotalReportingCurrencyValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr class="RowWithPadding">
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblCloseDate" runat="server" FormatString="{0}:" LabelID="1411"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExCalendar ID="calResolutionDate" runat="server" Width="80"></webControls:ExCalendar>
                <asp:RequiredFieldValidator ID="vldResolutionDate" runat="server" Text="!" Font-Bold="true"
                    Font-Size="Medium" ControlToValidate="calResolutionDate">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvResolutionDate" runat="server" Text="!" Font-Bold="true"
                    ControlToValidate="calResolutionDate" ClientValidationFunction="ValidateDate">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareWithOpenDate" runat="server" Text="!" Font-Bold="true"
                    ClientValidationFunction="CompareOpenAndCloseDates">
                </asp:CustomValidator>
                <asp:CustomValidator ID="cvCompareWithCurrentDate" runat="server" Text="!" Font-Bold="true"
                    ControlToValidate="calResolutionDate" ClientValidationFunction="CompareDateWithCurrentDate">
                </asp:CustomValidator>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="4">
                <telerikWebControls:ExRadGrid ID="rgGLAdjustments" runat="server" Width="100%" ShowHeader="true"
                    OnItemDataBound="rgGLAdjustments_ItemDataBound" OnNeedDataSource="rgGLAdjustments_NeedDataSource"
                    ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                    AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                    AllowCauseValidationExportToExcel="false" AllowCauseValidationExportToPDF="false"
                    OnItemCreated="rgGLAdjustments_ItemCreated" OnItemCommand="rgGLAdjustments_OnItemCommand"
                    ClientSettings-ClientEvents-OnRowSelected="Selected" ClientSettings-ClientEvents-OnRowDeselected="Selected"
                    AllowSorting="true">
                    <MasterTableView DataKeyNames="GLDataRecurringItemScheduleID" ClientDataKeyNames="ScheduleAmountReportingCurrency, ScheduleAmountBaseCurrency,OpenDate"
                        Width="100%">
                        <%-- ClientDataKeyNames="AmountReportingCurrency, AmountBaseCurrency"--%>
                        <Columns>
                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                            <%--Original Amount LCCY--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="OriginalAmount" SortExpression="ScheduleAmount"
                                DataType="System.Decimal">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblScheduleAmount" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Open Date--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511 " SortExpression="OpenDate"
                                DataType="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Schedule Name--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleName" LabelID="2052"
                                SortExpression="">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblScheduleName" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Begin Date--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleBeginDate" LabelID="2053"
                                SortExpression="ScheduleBeginDate" DataType="System.DateTime">
                                <ItemTemplate>
                                    <%# Helper.GetDisplayDate((DateTime?)Eval("ScheduleBeginDate"))%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--End Date--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleEndDate" LabelID="1450"
                                SortExpression="ScheduleEndDate" DataType="System.DateTime">
                                <ItemTemplate>
                                    <%# Helper.GetDisplayDate((DateTime?)Eval("ScheduleEndDate"))%>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Original Amount (RCCY)--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="OriginalAmountRCCY" ItemStyle-Width="50px"
                                SortExpression="ScheduleAmountReportingCurrency" DataType="System.Decimal">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblOriginalAmountRCCY" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Total Amortized Amount (RCCY)--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="AccruedAmountRCCY" SortExpression="RecPeriodAmountReportingCurrency"
                                DataType="System.Decimal">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRecPeriodAmountRCCY" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Remaining Amortizable Amount (RCCY)--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="BalanceReportingCurrency" LabelID="1701"
                                SortExpression="BalanceReportingCurrency">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblBalanceRCCY" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Docs--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="Documents" LabelID="2056" SortExpression="AttachmentCount"
                                DataType="System.Int32">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAttachmentCount" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </telerikWebControls:ExGridTemplateColumn>
                            
                             <%----Rec Item # ----%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" LabelID="2118 ">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <telerik:GridBoundColumn UniqueName="GLDataRecurringItemScheduleID" Visible="false"
                                DataField="GLDataRecurringItemScheduleID">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right">
                <webControls:ExButton ID="btnClose" runat="server" LabelID="1771" SkinID="ExButton100"
                    OnClick="btnClose_Click" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                    OnClientClick="window.close();" CausesValidation="false" />
            </td>
        </tr>
    </table>
    <%--<input type="text" id="txtMaxSelectedOpenDate" runat="server" style="display: none" />--%>
    <asp:HiddenField ID="txtMaxSelectedOpenDate" runat="server" />

    <script language="javascript" type="text/javascript">

        function Selected(sender, args) {

            var baseCurrencyValue = 0;
            var reportingCurrencyValue = 0;
            var masterTable = sender.get_masterTableView();
            var selectedRows = masterTable.get_selectedItems();
            var txtMaxSelectedOpenDate = document.getElementById('<%=txtMaxSelectedOpenDate.ClientID %>');
            var maxSelectedOpenDate = txtMaxSelectedOpenDate.value;
            var openDate;
            var cell;
            for (var i = 0; i < selectedRows.length; i++) {
                var row = selectedRows[i];
                if (row.getDataKeyValue("ScheduleAmountBaseCurrency") != null)
                    baseCurrencyValue = baseCurrencyValue + Round(row.getDataKeyValue("ScheduleAmountBaseCurrency"), 2); // Get the key value
                if (row.getDataKeyValue("ScheduleAmountReportingCurrency") != null)
                    reportingCurrencyValue = reportingCurrencyValue + Round(row.getDataKeyValue("ScheduleAmountReportingCurrency"), 2);

                cell = masterTable.getCellByColumnUniqueName(row, "OpenDate")
                //openDate = cell.innerText.trim();
                if (cell.children[0].firstChild != null || cell.children[0].firstChild != 'undefined') {
                    openDate = cell.children[0].firstChild.data;
                    if (maxSelectedOpenDate == null || maxSelectedOpenDate == '') {
                        maxSelectedOpenDate = openDate;
                    }
                    else if (CompareDates(openDate, maxSelectedOpenDate) > 0) {
                        maxSelectedOpenDate = openDate;
                    }
                }
            }

            var lblTotalBaseCurrency = document.getElementById('<%=lblTotalBaseCurrencyValue.ClientID %>');
            var lblTotalReportingCurrency = document.getElementById('<%=lblTotalReportingCurrencyValue.ClientID %>');

            lblTotalBaseCurrency.innerText = SetDisplayDecimalValue(baseCurrencyValue, 2);
            lblTotalReportingCurrency.innerText = SetDisplayDecimalValue(reportingCurrencyValue, 2);
            txtMaxSelectedOpenDate.value = maxSelectedOpenDate;
        }


        function CompareOpenAndCloseDates(sender, args) {

            var calOpenDate = document.getElementById('<%=txtMaxSelectedOpenDate.ClientID%>');
            var calCloseDate = document.getElementById('<%=calResolutionDate.ClientID%>');
            var openDate;
            var closeDate = calCloseDate.value;
            openDate = calOpenDate.value;

            var result = CompareDates(openDate, closeDate);

            if (result > 0) {
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
    </script>

</asp:Content>
