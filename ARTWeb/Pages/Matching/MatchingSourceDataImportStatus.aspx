<%@ Page Language="C#" MasterPageFile="~/MasterPages/MatchingMaster.master" AutoEventWireup="true"
    CodeFile="MatchingSourceDataImportStatus.aspx.cs" Theme="SkyStemBlueBrown" Inherits="Pages_Matching_MatchingSourceDataImportStatus"
    Title="Untitled Page" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMatching" runat="Server">
    <asp:UpdatePanel ID="upnlMatching" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <asp:Panel runat="server" ID="pnlBusinessAdminDDL">
                    <tr class="BlankRow">
                        <td colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" valign="middle">
                            <webControls:ExLabel ID="lblBusinessAdmin" runat="server" LabelID="1134" SkinID="Black11Arial"></webControls:ExLabel>
                            &nbsp;
                            <asp:DropDownList ID="ddlBA" runat="server" SkinID="DropDownList200" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlBA_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </asp:Panel>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <webControls:ExCheckBox ID="chkShowHiddenRows" runat="server" SkinID="CheckboxWithLabelBold"
                            Visible="false" AutoPostBack="true" LabelID="2103" OnCheckedChanged="chkShowHiddenRows_OnCheckedChanged" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerikWebControls:ExRadGrid ID="rgMatchingSourceDataImport" runat="server" OnItemDataBound="rgMatchingSourceDataImport_ItemDataBound"
                            OnSortCommand="rgMatchingSourceDataImport_SortCommand" OnNeedDataSource="rgMatchingSourceDataImport_NeedDataSource"
                            OnItemCommand="rgMatchingSourceDataImport_ItemCommand" OnItemCreated="rgMatchingSourceDataImport_ItemCreated"
                            AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                            AllowPaging="true" AllowSorting="true" EntityNameLabelID="1219" AllowRefresh="true"
                            AllowMultiRowSelection="true">
                            <MasterTableView CommandItemDisplay="TopAndBottom" ClientDataKeyNames="DataImportStatusID, MatchingSourceDataImportID">
                                <Columns>
                                    <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="2%" />
                                    <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" UniqueName="imgStatus">
                                        <ItemTemplate>
                                            <webControls:ExImage ID="imgSuccess" runat="server" LabelID="1618" SkinID="SuccessIcon"
                                                Visible="false" />
                                            <webControls:ExImage ID="imgFailure" runat="server" LabelID="5000033" SkinID="ExpireIcon"
                                                Visible="false" />
                                            <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon"
                                                Visible="false" />
                                            <webControls:ExImage ID="imgProcessing" runat="server" LabelID="1619" SkinID="ProgressIcon"
                                                Height="24px" Width="23px" Visible="false" />
                                            <webControls:ExImage ID="imgToBeProcessed" runat="server" LabelID="1783" SkinID="ToBeProcessedIcon"
                                                Visible="false" />
                                            <webControls:ExImage ID="imgDraft" runat="server" LabelID="2348" SkinID="Draft" Visible="false" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1287" SortExpression="MatchingSourceName"
                                        HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlMatchingSourceName" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1690" SortExpression="MatchingSourceTypeName"
                                        HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlMatchingSourceType" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn Visible="false" DataType="System.Int64"
                                        UniqueName="MatchSetID">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblMatchSetID" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2212" HeaderStyle-Width="20%" SortExpression="IsPartofMatchSet">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlIsPartofMatchSet" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1399" HeaderStyle-Width="15%" SortExpression="DateAdded">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlDate" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1338" HeaderStyle-Width="15%" SortExpression="DataImportStatus">
                                        <ItemTemplate>
                                            <webControls:ExHyperLink ID="hlStatus" runat="server" SkinID="GridHyperLink" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExImageButton ID="imgbtnShowRows" ImageAlign="Left" SkinID="ShowGridRowsIcon"
                                                runat="server" CommandName="HideShowRows" CommandArgument="false" />
                                            <webControls:ExImageButton ID="imgbtnHideRows" ImageAlign="Left" SkinID="HideGridRowsIcon"
                                                runat="server" CommandName="HideShowRows" CommandArgument="true" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" UniqueName="DataTypeMapping">
                                        <ItemTemplate>
                                            <webControls:ExImageButton ID="imgbtnDataTypeMapping" ToolTipLabelID="2338" SkinID="DataTypeMapping"
                                                runat="server" CommandName="DataTypeMapping" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" UniqueName="FileType">
                                        <ItemTemplate>
                                            <webControls:ExImageButton ID="imgFileType" runat="server" ImageAlign="Left" SkinID="FileDownloadIcon"
                                                CommandName="FileDownLoad" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingSourceDataImportID"
                                        Visible="false" />
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="MatchingSourceTypeID" Visible="false" />
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="PhysicalPath" Visible="false" />
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowSelecting="Selecting" />
                            </ClientSettings>
                        </telerikWebControls:ExRadGrid>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnDelete" runat="server" LabelID="1564" OnClick="btnDelete_Click"
                            OnClientClick="return confirmDelete(this);" />
                        &nbsp;
                        <webControls:ExButton ID="btnBack" LabelID="1239" CausesValidation="false" runat="server"
                            OnClick="btnBack_Click" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="LegendTable">
                            <tr>
                                <td class="LegendHeading" colspan="6">
                                    <webControls:ExLabel ID="lblHeading" FormatString="{0}:" runat="server" LabelID="1383"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgDraft" runat="server" LabelID="2348" SkinID="Draft" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel6" runat="server" LabelID="2348" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
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
                                <%-- <td style="width: 17%" valign="top">
                                    <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1617" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>--%>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgProcessing" runat="server" LabelID="1619" SkinID="ProgressIcon"
                                        Height="24px" Width="23px" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="1619" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="ExImage1" runat="server" LabelID="1783" SkinID="ToBeProcessedIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel5" runat="server" LabelID="1783" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucMatching" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlMatching"
                            Visible="true" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Sel" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">


        function pageLoad() {
            var tableView = $find('<%= rgMatchingSourceDataImport.ClientID %>').get_masterTableView();
            var headerRow = Telerik.Web.UI.Grid.getTableHeaderRow(tableView.get_element());
            var checkBox = getSelectCheckBox(headerRow);
            if (checkBox) checkBox.onclick = function(e) {
                var event = e || window.event;
                selectAllRows(checkBox.checked, event);
            };
        }
        function selectAllRows(checkHeaderSelectCheckBox, event) {
            var gridSelection = new Telerik.Web.UI.GridSelection();
            var grid = $find('<%= rgMatchingSourceDataImport.ClientID %>');
            var tableView = grid.get_masterTableView();
            var headerRow = Telerik.Web.UI.Grid.getTableHeaderRow(tableView.get_element());
            grid._selectAllRows(tableView.get_id(), null, event);
            gridSelection._checkClientSelectColumn(headerRow, checkHeaderSelectCheckBox);
        }
        function Selecting(sender, args) {
            //args.set_cancel(true);
            //            var isSuccessfulImport = args.getDataKeyValue('DataImportStatusID') === DataImportStatusID;
            //            var isGLDataImport = args.getDataKeyValue('MatchingSourceDataImportID') === MatchingSourceDataImportID;
            //            if (!isSuccessfulImport || !isGLDataImport) {
            //                
            //            }
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
            var grid = $find("<%= rgMatchingSourceDataImport.ClientID %>");
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

</asp:Content>
