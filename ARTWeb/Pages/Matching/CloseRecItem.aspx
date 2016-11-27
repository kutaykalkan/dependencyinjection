<%@ Page Language="C#" AutoEventWireup="true" Inherits="Pages_Matching_CloseRecItem"
    MasterPageFile="~/MasterPages/MatchingMaster.master" Theme="SkyStemBlueBrown" Codebehind="CloseRecItem.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountDescription" Src="~/UserControls/AccountDescription.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataRecItemGrid" Src="~/UserControls/RecForm/GLDataRecItemGrid.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataWriteOnOffGrid" Src="~/UserControls/RecForm/GLDataWriteOnOffGrid.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataRecurringScheduleItemsGrid"
    Src="~/UserControls/RecForm/GLDataRecurringScheduleItemsGrid.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:UpdatePanel ID="pnlCloseRecItem" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%" cellpadding="0" cellspacing="0">
                <tr class="BlankRow">
                    <td style="width: 8%; padding: 5px">
                        <webControls:ExLabel ID="lblDataSource" runat="server" LabelID="2231" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td style="width: 92%;">
                        <webControls:ExLabel ID="lblDataSourceName" runat="server" SkinID="Black9ArialNormal"></webControls:ExLabel>
                        <asp:HiddenField ID="hdnRecTemplateID" runat="server" Value="0" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; padding: 5px">
                        <webControls:ExLabel ID="lblRecCategory" runat="server" LabelID="2329" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td style="width: 85%;">
                        <asp:DropDownList ID="ddlRecCategory" runat="server" SkinID="DropDownList200" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRecCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; padding: 5px">
                        <webControls:ExLabel ID="lblRecCategoryType" runat="server" LabelID="2330" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td style="width: 85%;">
                        <asp:DropDownList ID="ddlRecCategoryType" SkinID="DropDownList200" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRecCategoryType_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; padding: 5px">
                        <table>
                            <tr>
                                <td class="ManadatoryField">
                                    *
                                </td>
                                <td>
                                    <webControls:ExLabel ID="lblResolutionCloseDate" runat="server" FormatString="{0}:"
                                        LabelID="1411" SkinID="Black11Arial"></webControls:ExLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 85%;">
                        <webControls:ExCalendar ID="calResolutionDate" runat="server" Width="80"></webControls:ExCalendar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 5px">
                        <asp:Panel ID="PanelScrollBars" ScrollBars="Auto" Visible="false" runat="server">
                            <table>
                                <tr>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2328" SkinID="Black11Arial"></webControls:ExLabel>
                                                <telerikWebControls:ExRadGrid ID="rgMatchSetResult" AllowMultiRowSelection="true"
                                                    runat="server" EntityNameLabelID="2320" AllowSorting="true" AllowCustomFilter="true"
                                                    GridApplyFilterOnClientClick="LoadGridApplyFilterPage();return false;" AllowPaging="true"
                                                    AllowCustomPaging="true" OnItemCommand="rgMatchSetResult_ItemCommand" AllowExportToExcel="false"
                                                    AllowExportToPDF="false" OnItemDataBound="rgMatchSetResult_OnItemDataBound" OnNeedDataSource="rgMatchSetResult_OnNeedDataSource"
                                                    OnItemCreated="rgMatchSetResult_ItemCreated" OnPageSizeChanged="rgMatchSetResult_PageSizeChanged">
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="true" />
                                                        <Selecting UseClientSelectColumnOnly="true" AllowRowSelect="true" />
                                                        <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                                                    </ClientSettings>
                                                    <MasterTableView UseAllDataFields="true" EnableColumnsViewState="false" DataKeyNames="__MatchSetMatchingSourceDataImportID,__ExcelRowNumber">
                                                        <PagerTemplate>
                                                            <asp:Panel ID="PagerPanel" runat="server">
                                                                <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                                                    <div style="float: left; margin-right: 10px;">
                                                                        <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                                                        <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                                                            </asp:Panel>
                                                        </PagerTemplate>
                                                        <Columns>
                                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerikWebControls:ExRadGrid>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td valign="top">
                                        <table>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:UpdatePanel ID="UpdateOpenItemGrids" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="pnlGLAdjustments" Visible="false" runat="server">
                                                                <UserControls:GLDataRecItemGrid ID="ucGLDataRecItemGrid" AllowExportToExcel="false"
                                                                    AllowExportToPDF="false" AllowCustomFilter="true" AllowCustomPaging="true" AllowSelectionPersist="false"
                                                                    AllowDisplayCurrencyPnl="true" BasePageTitleLabelID="2471" ShowDocsColumn="false"
                                                                    ShowCloseDateColum="false" runat="server">
                                                                </UserControls:GLDataRecItemGrid>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlWriteOffOnItems" Visible="false" runat="server">
                                                                <UserControls:GLDataWriteOnOffGrid ID="ucGLDataWriteOnOffGrid" AllowExportToExcel="false"
                                                                    AllowExportToPDF="false" AllowCustomFilter="true" AllowCustomPaging="true" AllowSelectionPersist="false"
                                                                    AllowDisplayCurrencyPnl="true" ShowDescriptionColum="true" ShowSelectCheckBoxColum="true"
                                                                    ShowCloseDateColum="false" runat="server">
                                                                </UserControls:GLDataWriteOnOffGrid>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlRecurringScheduleItems" Visible="false" runat="server">
                                                                <UserControls:GLDataRecurringScheduleItemsGrid ID="ucGLDataRecurringScheduleItemsGrid"
                                                                    AllowExportToExcel="false" AllowExportToPDF="false" AllowCustomFilter="true"
                                                                    AllowCustomPaging="true" AllowSelectionPersist="false" ShowDocsColumn="false"
                                                                    AllowDisplayCurrencyPnl="true" ShowCloseDateColum="false" runat="server">
                                                                </UserControls:GLDataRecurringScheduleItemsGrid>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <span style="padding: 25px">
                            <webControls:ExCustomValidator ID="cvValidateRowSelection" ValidationGroup="SAVE"
                                ClientValidationFunction="validateRowSelection" runat="server" Text=""></webControls:ExCustomValidator>
                            <webControls:ExButton ID="btnSave1" ValidationGroup="SAVE" runat="server" Enabled="false"
                                LabelID="1315" SkinID="ExButton100" OnClick="btnSave1_Click" />
                            <webControls:ExButton ID="btnCloseRecItem" ValidationGroup="SAVE" Enabled="false"
                                runat="server" LabelID="2473" SkinID="ExButton150" OnClick="btnCloseRecItem_Click" />
                        </span><span style="padding: 5px">
                            <hr />
                        </span><span style="padding: 25px">
                            <webControls:ExButton ID="btnBackToWorkspace" CausesValidation="false" runat="server"
                                LabelID="2331" SkinID="ExButton150" OnClick="btnBackToWorkspace_Click" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="hidden" id="Sel" runat="server" />
                        <asp:HiddenField ID="hdnMaxSelectedOpenDate" runat="server" />
                        <asp:HiddenField ID="hdnNewPageSize" runat="server" />
                        <asp:HiddenField ID="hdnNewPageSizeResult" runat="server" />
                        <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="pnlCloseRecItem" Visible="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">
        function LoadGridApplyFilterPage() {
            var urlGridApplyFilter = "../GridDynamicFilter.aspx?griddynamicfiltersessionkey=rgMatchSetResult";
            OpenRadWindowForHyperlink(urlGridApplyFilter, 480, 800);
        }

        function Selecting(sender, args) {

            var bSelectRow = true;
            var inp = document.getElementById('<% =this.Sel.ClientID %>');
            var data = inp.value;
            var a = Array;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i++] == args.get_itemIndexHierarchical()) {
                        bSelectRow = false;
                        break;
                    }
                }
            }
            if (bSelectRow == true)
                args.set_cancel(false);
            else
                args.set_cancel(true);
        }
        function GetMaxSelectedOpenDate() {
            var gridOpenItems;
            gridOpenItems = GetOpenItemgrid();
            var masterTable = gridOpenItems.get_masterTableView();
            var selectedRows = masterTable.get_selectedItems();
            var txtMaxSelectedOpenDate = document.getElementById('<%=hdnMaxSelectedOpenDate.ClientID %>');
            var maxSelectedOpenDate = txtMaxSelectedOpenDate.value;
            var openDate;
            var cell;
            for (var i = 0; i < selectedRows.length; i++) {
                var row = selectedRows[i];
                cell = masterTable.getCellByColumnUniqueName(row, "OpenDate")
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
            txtMaxSelectedOpenDate.value = maxSelectedOpenDate;
        }
        function validateRows(sender, args) {
            var gridMatchSetResult;
            var gridOpenItems;
            GetMaxSelectedOpenDate();
            selectedRowCountMatchSetResult = $find('<%= rgMatchSetResult.ClientID %>');
            gridOpenItems = GetOpenItemgrid();
            var selectedRowCountMatchSetResult = selectedRowCountMatchSetResult.get_selectedItems().length;
            var selectedRowCountOpenItems = gridOpenItems.get_selectedItems().length;
            if (selectedRowCountMatchSetResult == 0) {
                args.IsValid = false;
                sender.errormessage = '<% =LanguageUtil.GetValue(5000319) %>';
                //sender.errormessage = 'Select atleast 1 row in Result Gird';
                return;
            }
            if (selectedRowCountOpenItems == 0) {
                args.IsValid = false;
                sender.errormessage = '<% =LanguageUtil.GetValue(5000320) %>';
                // sender.errormessage = 'Select atleast 1 row in  OpenItems Gird';
                return;
            }
            if (selectedRowCountMatchSetResult > 1 && selectedRowCountOpenItems > 1) {
                args.IsValid = false;
                sender.errormessage = '<% =LanguageUtil.GetValue(5000321) %>';
                //sender.errormessage = 'Only One-Many or Many-one allow';
                return;
            }
            var calOpenDate = document.getElementById('<%=hdnMaxSelectedOpenDate.ClientID%>');
            var calCloseDate = document.getElementById('<%=calResolutionDate.ClientID%>');
            var openDate;
            openDate = calOpenDate.value;
            var closeDate = calCloseDate.value;
            if (closeDate == "" || closeDate == null) {
                args.IsValid = false;
                sender.errormessage = '<% =LanguageUtil.GetValue(5000095) %>';
                // sender.errormessage = 'Close Date Cannot be blank ';
                return;
            }
            if (!IsDate(closeDate)) {
                args.IsValid = false;
                sender.errormessage = '<% =LanguageUtil.GetValue(5000322) %>';
                // sender.errormessage = 'Invalid date';
                return;
            }
            var currentDate = ConvertJavascriptDateFormat(new Date());
            var result1 = CompareDates(closeDate, currentDate);
            if (result1 > 0) {
                args.IsValid = false;
                //Close date can not be current or future date 
                sender.errormessage = '<% =LanguageUtil.GetValue(5000102) %>';
                return;
            }
            var result = CompareDates(openDate, closeDate);
            if (result > 0) {
                args.IsValid = false;
                sender.errormessage = '<% =LanguageUtil.GetValue(5000103) %>';
                //sender.errormessage = 'Close date can not be less than Open date';
                return;
            }
        }
        function validateRowSelection(sender, args) {
            var valSummaryObj = GetValidationSummaryElement();
            valSummaryObj.validationGroup = 'SAVE';
            validateRows(sender, args);
        }
        function GetOpenItemgrid() {

            var rgGLAdjustment;
            rgGLAdjustment = $find('<%=ucGLDataRecItemGrid.Grid.ClientID %>');
            if (rgGLAdjustment != null)
                return rgGLAdjustment;
            var rgWriteOffOn;
            rgWriteOffOn = $find('<%=ucGLDataWriteOnOffGrid.Grid.ClientID %>');
            if (rgWriteOffOn != null)
                return rgWriteOffOn;
            var rgRecurringSchedule;
            rgRecurringSchedule = $find('<%=ucGLDataRecurringScheduleItemsGrid.Grid.ClientID %>');
            if (rgRecurringSchedule != null)
                return rgRecurringSchedule;
        }
        var tableView = null;
        function ddlPageSize_SelectedIndexChanged(ID, SourceID) {

            if ($find(SourceID) != null) {
                tableView = $find(SourceID).get_masterTableView();
            }
            if (tableView != null)
                tableView.set_pageSize(document.getElementById(ID).value);
        }
    </script>

</asp:Content>
