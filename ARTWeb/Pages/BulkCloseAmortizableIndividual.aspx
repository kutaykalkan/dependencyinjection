<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_BulkCloseAmortizableIndividual" Theme="SkyStemBlueBrown" Codebehind="BulkCloseAmortizableIndividual.aspx.cs" %>

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
                <webControls:ExLabel ID="lblBaseCurrency" runat="server" FormatString="{0}:" LabelID="1493"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblBaseCurrencyType" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                <webControls:ExLabel ID="lblBaseCurrencyValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
            </td>
            <%--Reporting Currency--%>
            <td style="width: 30%">
                <webControls:ExLabel ID="lblReportingCurrency" runat="server" FormatString="{0}:"
                    LabelID="1424" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblReportingCurrencyType" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                <webControls:ExLabel ID="lblReportingCurrencyValue" runat="server" SkinID="ReadOnlyValue"
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
                <webControls:ExLabel ID="lblResolutionCloseDate" runat="server" FormatString="{0}:"
                    LabelID="1411" SkinID="Black11Arial"></webControls:ExLabel>
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
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <telerikWebControls:ExRadGrid ID="rgGLAdjustments" runat="server" Width="100%" ShowHeader="true"
                    OnItemDataBound="rgGLAdjustments_ItemDataBound" OnNeedDataSource="rgGLAdjustments_NeedDataSource"
                    ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                    AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                    AllowCauseValidationExportToExcel="false" AllowCauseValidationExportToPDF="false"
                    OnItemCreated="rgGLAdjustments_ItemCreated" OnItemCommand="rgGLAdjustments_OnItemCommand"
                    ClientSettings-ClientEvents-OnRowSelected="Selected" ClientSettings-ClientEvents-OnRowDeselected="Selected"
                    AllowSorting="true">
                    <MasterTableView ClientDataKeyNames="AmountReportingCurrency, AmountBaseCurrency"
                        DataKeyNames="GLDataRecItemID" Width="100%">
                        <Columns>
                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%" />
                            <%--Amount--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" LabelID="1675" SortExpression="Amount"
                                DataType="System.Decimal">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAmount" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Width="25%" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Amount In Reporting Currency--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountInReportingCurrency" LabelID="1674"
                                SortExpression="AmountReportingCurrency" DataType="System.Decimal">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAmountReportingCurrency" runat="server" Text='<%#Helper.GetDisplayDecimalValue((Decimal?)Eval("AmountReportingCurrency"))%>'></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Width="25%" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="BlankColumn1">
                                <HeaderStyle Width="5%" />
                                <ItemStyle Width="5%" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Open Date--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511 " SortExpression="OpenDate"
                                DataType="System.DateTime">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Comments--%>
                            <%--Aging--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="Aging" LabelID="1512 " ItemStyle-HorizontalAlign="Right"
                                SortExpression="Aging" DataType="System.Int32">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAging" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="BlankColumn2">
                                <HeaderStyle Width="5%" />
                                <ItemStyle Width="5%" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--Documents--%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="Documents" LabelID="2056" SortExpression="AttachmentCount"
                                DataType="System.Int32">
                                <ItemTemplate>
                                    <%--<userControl:DocumentUpload ID="ucDocumentUpload" runat="server" />--%>
                                    <webControls:ExLabel ID="lblAttachmentCount" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <%----Rec Item # ----%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" LabelID="2118 ">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="GLRecItemID" Visible="false" DataField="GLDataRecItemID">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <%--Blank Row--%>
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
    <input type="text" id="txtMaxSelectedOpenDate" runat="server" style="display: none" />

    <script language="javascript" type="text/javascript">

        function Selected(sender, args) {

            var baseCurrencyValue = 0;
            var reportingCurrencyValue = 0;
            var masterTable = sender.get_masterTableView();
            var selectedRows = masterTable.get_selectedItems();

            for (var i = 0; i < selectedRows.length; i++) {
                var row = selectedRows[i];
                if (row.getDataKeyValue("AmountBaseCurrency") != null)
                    baseCurrencyValue = baseCurrencyValue + Round(row.getDataKeyValue("AmountBaseCurrency"), 2); // Get the key value
                if (row.getDataKeyValue("AmountReportingCurrency") != null)
                    reportingCurrencyValue = reportingCurrencyValue + Round(row.getDataKeyValue("AmountReportingCurrency"), 2);
            }

            var lblBaseCurrency = document.getElementById('<%=lblBaseCurrencyValue.ClientID %>');
            var lblReportingCurrency = document.getElementById('<%=lblReportingCurrencyValue.ClientID %>');

            lblBaseCurrency.innerText = SetDisplayDecimalValue(baseCurrencyValue, 2);
            lblReportingCurrency.innerText = SetDisplayDecimalValue(reportingCurrencyValue, 2);
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

            if (result >= 0) {
                args.IsValid = false;
            }
            else {
                args.IsValid = true;
            }
        }
    </script>

</asp:Content>
