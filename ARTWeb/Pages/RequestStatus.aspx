<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_RequestStatus" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" Codebehind="RequestStatus.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upnlDataImportStatus" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%">

                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlServiceDataImport" Width="1050px" ScrollBars="Horizontal">
                            <telerikWebControls:ExRadGrid ID="rgRequests" runat="server" OnItemDataBound="rgRequests_ItemDataBound"
                                OnSortCommand="rgRequests_SortCommand" OnNeedDataSource="rgRequests_NeedDataSource"
                                AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                                AllowRefresh="true" OnItemCommand="rgRequests_ItemCommand" OnItemCreated="rgRequests_ItemCreated"
                                AllowPaging="true" AllowSorting="true" EntityNameLabelID="1219" AllowMultiRowSelection="true">
                                <MasterTableView ClientDataKeyNames="RequestID, IsRecordOwner">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                        <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" UniqueName="imgStatus">
                                            <ItemTemplate>
                                                <webControls:ExImage ID="imgSuccess" runat="server" LabelID="1618" SkinID="SuccessIcon"
                                                    Visible="false" />
                                                <webControls:ExImage ID="imgFailure" runat="server" LabelID="5000033" SkinID="ExpireIcon"
                                                    Visible="false" />
                                                <%-- <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon"
                                                    Visible="false" />--%>
                                                <webControls:ExImage ID="imgProcessing" runat="server" LabelID="1619" SkinID="ProgressIcon"
                                                    Height="24px" Width="23px" Visible="false" />
                                                <webControls:ExImage ID="imgToBeProcessed" runat="server" LabelID="1783" SkinID="ToBeProcessedIcon"
                                                    Visible="false" />
                                                <%--  <webControls:ExImage ID="imgReject" runat="server" LabelID="2400" SkinID="RejectIcon"
                                                    Visible="false" />--%>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2820" SortExpression="RequestType"
                                            HeaderStyle-Width="13%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRequestType" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2821" SortExpression="GridName"
                                            HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPageGridName" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2027" SortExpression="FileName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblFileName" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2087" UniqueName="FileDownloadIconColumn" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <webControls:ExImageButton ID="imgFileTypeExcel" runat="server" ImageAlign="Left" SkinID="FileDownloadIcon" />
                                                <webControls:ExImageButton ID="imgFileTypeZip" runat="server" ImageAlign="Left" SkinID="FileDownloadIconZip" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1399" HeaderStyle-Width="20%" SortExpression="DateAdded">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDate" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1338" HeaderStyle-Width="15%" SortExpression="RequestStatus">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblStatus" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2180" HeaderStyle-Width="15%" SortExpression="AddedByUserName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAddedBy" runat="server" Width="100Px" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2836" HeaderStyle-Width="15%" SortExpression="StatusMessage">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblStatusMessage" runat="server" Width="100Px" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnDeleteDataImport" runat="server" LabelID="1564" OnClick="btnDeleteDataImport_Click"
                            OnClientClick="return confirmDelete(this);" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td valign="top">
                        <table class="LegendTable">
                            <tr>
                                <td class="LegendHeading" colspan="6">
                                    <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgSuccess" runat="server" LabelID="1618" SkinID="SuccessIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1618" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgFailure" runat="server" LabelID="5000033" SkinID="ExpireIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="5000033" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <%--    <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1617" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>--%>
                                <td style="width: 15%" valign="top">
                                    <webControls:ExImage ID="imgProcessing" runat="server" LabelID="1619" SkinID="ProgressIcon"
                                        Height="24px" Width="23px" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="1619" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td style="width: 15%" valign="top">
                                    <webControls:ExImage ID="ExImage1" runat="server" LabelID="1783" SkinID="ToBeProcessedIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel5" runat="server" LabelID="1783" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <%--  <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="ExImage2" runat="server" LabelID="1783" SkinID="RejectIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel6" runat="server" LabelID="2400" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <UserControls:ProgressBar ID="ucDataImportStatus" runat="server" AssociatedUpdatePanelID="upnlDataImportStatus" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Sel" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">


        function pageLoad() {
            var tableView = $find('<%= rgRequests.ClientID %>').get_masterTableView();
            var headerRow = Telerik.Web.UI.Grid.getTableHeaderRow(tableView.get_element());
            var checkBox = getSelectCheckBox(headerRow);
            if (checkBox) checkBox.onclick = function (e) {
                var event = e || window.event;
                selectAllRows(checkBox.checked, event);
            };
        }
        function selectAllRows(checkHeaderSelectCheckBox, event) {
            var gridSelection = new Telerik.Web.UI.GridSelection();
            var grid = $find('<%= rgRequests.ClientID %>');
            var tableView = grid.get_masterTableView();
            var headerRow = Telerik.Web.UI.Grid.getTableHeaderRow(tableView.get_element());
            grid._selectAllRows(tableView.get_id(), null, event);
            gridSelection._checkClientSelectColumn(headerRow, checkHeaderSelectCheckBox);
        }
        function Selecting(sender, args) {
            var IsRecordOwner = args.getDataKeyValue('IsRecordOwner');
            if (IsRecordOwner == "False") {
                args.set_cancel(true);
            }
        }
        function getSelectCheckBox(el) {
            var inputs = el.getElementsByTagName('input');
            for (var i = 0; i < inputs.length; i++) {
                var input = inputs[i];
                if (input.type.toLowerCase() !== 'checkbox')
                    continue;
                if (input.id && input.id.indexOf('SelectCheckBox') != -1)
                    return input;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function confirmDelete(btn) {
            var grid = $find("<%= rgRequests.ClientID %>");
            if (grid != null && grid != "undefined") {
                var gridSelectedItems = grid.get_selectedItems();
                if (gridSelectedItems.length <= 0) {
                    alert('<% = Helper.GetAlertMessageFromLabelID(WebConstants.NO_SELECTION_ERROR_MESSAGE) %>');
                    return false;
                }
            }
            return confirm('<% = Helper.GetAlertMessageFromLabelID(WebConstants.CONFIRM_FOR_DELETE_DATAIMPORT_ITEM) %>');
        }
    </script>

    <%-- <script type="text/javascript" language="javascript">
        var a = Array;
        window.onload = function() {
            alert("window onload");
            var inp = document.getElementById('<% =this.Sel.ClientID %>');
            var data = inp.value;
            if (data != "") {
                var rowsData = data.split(":");
                var i = 0;
                while (typeof (rowsData[i]) != "undefined") {
                    if (rowsData[i] != "") {
                        a[i] = rowsData[i];
                    }
                    i++;
                }
            }
        }
        function Selecting(sender, args) {
            var i = 0;
            while (typeof (a[i]) != "undefined") {
                if (a[i++] == args.get_itemIndexHierarchical()) {
                    args.set_cancel(true);
                }
            }
        }
    </script>--%>
</asp:Content>
