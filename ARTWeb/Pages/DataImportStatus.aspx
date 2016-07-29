<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_DataImportStatus" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" Codebehind="DataImportStatus.aspx.cs" %>

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
                <asp:Panel runat="server" ID="pnlBusinessAdminDDL">
                    <tr class="BlankRow">
                        <td colspan="3"></td>
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
                <asp:Panel ID="pnlRecAndHolidayDataImport" runat="server">
                    <tr>
                        <td colspan="3">
                            <webControls:ExLabel ID="lblGridHeading" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="3"></td>
                    </tr>
                    <tr>
                        <td>
                            <telerikWebControls:ExRadGrid ID="rgRecAndHolidayDataImport" runat="server" OnItemDataBound="rgRecAndHolidayDataImport_ItemDataBound"
                                OnSortCommand="rgRecAndHolidayDataImport_SortCommand" OnNeedDataSource="rgRecAndHolidayDataImport_NeedDataSource"
                                AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true"
                                OnItemCommand="rgRecAndHolidayDataImport_ItemCommand" OnItemCreated="rgRecAndHolidayDataImport_ItemCreated"
                                EntityNameLabelID="1219">
                                <MasterTableView AllowSorting="true" AllowPaging="true">
                                    <Columns>
                                        <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="5%" AllowFiltering="false"
                                            UniqueName="imgStatus">
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
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1477" SortExpression="DataImportType"
                                            HeaderStyle-Width="13%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlImportType" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1308" SortExpression="DataImportName"
                                            HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlProfileName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2860" SortExpression="TemplateName"
                                            HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlTemplateName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2027" SortExpression="FileName">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlFileName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <webControls:ExImageButton ID="imgFileType" runat="server" ImageAlign="Left" SkinID="FileDownloadIcon" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1399" HeaderStyle-Width="15%" SortExpression="DateAdded">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlDate" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1745" HeaderStyle-Width="12%" SortExpression="RecordsImported">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlRecordsAffected" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1338" HeaderStyle-Width="15%" SortExpression="DataImportStatus">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlStatus" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2180" HeaderStyle-Width="15%" SortExpression="DataImportStatus">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAddedBy" runat="server" Width="100Px" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
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
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                </asp:Panel>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <webControls:ExLabel ID="lblGridHeading2" LabelID="1219" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <%-- ********Not to be deleted. To be implemented later(Show/Hide rows)--%>
                        <%--<webControls:ExCheckBox ID="chkShowHiddenGLDataImport" runat="server" SkinID="CheckboxWithLabelBold"
                            AutoPostBack="true" LabelID="2103" OnCheckedChanged="chkShowHiddenGLDataImport_OnCheckedChanged" />--%>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel runat="server" ID="pnlServiceDataImport" Width="1000px" ScrollBars="Horizontal">
                            <telerikWebControls:ExRadGrid ID="rgDataImport" runat="server" OnItemDataBound="rgDataImport_ItemDataBound"
                                OnSortCommand="rgDataImport_SortCommand" OnNeedDataSource="rgDataImport_NeedDataSource"
                                AllowExportToExcel="true" AllowExportToPDF="true" AllowPrint="true" AllowPrintAll="true" ClientSettings-Selecting-AllowRowSelect="true"
                                AllowRefresh="True" OnItemCommand="rgDataImport_ItemCommand" OnItemCreated="rgDataImport_ItemCreated"
                                AllowPaging="true" AllowSorting="true" EntityNameLabelID="1219" AllowMultiRowSelection="true">
                                <ClientSettings>
                                    <Selecting UseClientSelectColumnOnly="true" />
                                    <ClientEvents OnRowSelecting="Selecting" />
                                </ClientSettings>
                                <MasterTableView ClientDataKeyNames="DataImportStatusID, DataImportTypeID, DataImportID, RoleID, IsRecordOwner">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
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
                                                <webControls:ExImage ID="imgReject" runat="server" LabelID="2400" SkinID="RejectIcon"
                                                    Visible="false" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1477" SortExpression="DataImportType"
                                            HeaderStyle-Width="13%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlImportType" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1308" SortExpression="DataImportName"
                                            HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlProfileName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2860" SortExpression="TemplateName"
                                            HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlTemplateName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2027" SortExpression="FileName">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlFileName" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2087" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <webControls:ExImageButton ID="imgFileType" runat="server" ImageAlign="Left" SkinID="FileDownloadIcon" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2026" SortExpression="NetValue"
                                            HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlNetValue" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1399" HeaderStyle-Width="20%" SortExpression="DateAdded">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlDate" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1736" HeaderStyle-Width="15%" SortExpression="ForceCommitDate">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlForceCommitDetails" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1745" HeaderStyle-Width="10%" SortExpression="RecordsImported">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlRecordsAffected" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1338" HeaderStyle-Width="15%" SortExpression="DataImportStatus">
                                            <ItemTemplate>
                                                <webControls:ExHyperLink ID="hlStatus" runat="server" SkinID="GridHyperLink" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2180" HeaderStyle-Width="15%" SortExpression="DataImportStatus">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAddedBy" runat="server" Width="100Px" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridBoundColumn UniqueName="DataImportID" DataField="DataImportID"
                                            HeaderStyle-Width="0" Visible="false">
                                        </telerikWebControls:ExGridBoundColumn>
                                        <telerikWebControls:ExGridBoundColumn UniqueName="IsHidden" DataField="IsHidden"
                                            HeaderStyle-Width="0" Visible="false">
                                        </telerikWebControls:ExGridBoundColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2181" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <webControls:ExImage ID="imgMultiVirsionIcon" runat="server" ImageAlign="Middle"
                                                    LabelID="2181" SkinID="Multiversion" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2935" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <webControls:ExImage ID="imgFTPUpload" runat="server" ImageAlign="Middle" Width="15px" Height="18px"
                                                    LabelID="2935" SkinID="ftp" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <%-- ********Not to be deleted. To be implemented later(Show/Hide rows)--%>
                                        <%--  <telerikWebControls:ExGridTemplateColumn>
                                        <ItemTemplate>
                                            <webControls:ExImageButton ID="imgbtnShowRows" SkinID="ShowGridRowsIcon" runat="server"
                                                CommandName="HideShowRows" />
                                            <webControls:ExImageButton ID="imgbtnHideRows" SkinID="HideGridRowsIcon" runat="server"
                                                CommandName="HideShowRows" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>--%>
                                    </Columns>
                                </MasterTableView>

                            </telerikWebControls:ExRadGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <webControls:ExButton ID="btnNewDataImport" runat="server" LabelID="1615" OnClick="btnNewDataImport_Click" />
                        <webControls:ExButton ID="btnDeleteDataImport" runat="server" LabelID="1933" OnClick="btnDeleteDataImport_Click"
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
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="imgWarning" runat="server" LabelID="1617" SkinID="WarningIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1617" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
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
                                <td style="width: 20%" valign="top">
                                    <webControls:ExImage ID="ExImage2" runat="server" LabelID="1783" SkinID="RejectIcon" />
                                    &nbsp;
                                    <webControls:ExLabel ID="ExLabel6" runat="server" LabelID="2400" SkinID="LegendLabel"></webControls:ExLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucDataImportStatus" runat="server" AssociatedUpdatePanelID="upnlDataImportStatus" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="Sel" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        var GLDataImportID = '<% =this.DataImportTypeIDForGL %>';
        var AccountUploadImportID = '<% =this.DataImportTypeIDForAccountUpload %>';
        var GLDataImportSuccessStatusID = '<% =this.DataImportSuccessTypeID %>';
        var RecControlChecklistImportTypeID = '<% =this.DataImportTypeIDForRecControlChecklist %>';
        var curRoleID = '<% =this.CurrentRoleID %>';

        function Selecting(sender, args) {
            if (args.getDataKeyValue('DataImportTypeID') === RecControlChecklistImportTypeID && args.getDataKeyValue('RoleID') === curRoleID)
                return;
            var isSuccessfulImport = args.getDataKeyValue('DataImportStatusID') === GLDataImportSuccessStatusID;
            // var isGLDataImport = args.getDataKeyValue('DataImportTypeID') === GLDataImportID;
            var isGLDataImport = (args.getDataKeyValue('DataImportTypeID') === GLDataImportID || args.getDataKeyValue('DataImportTypeID') === AccountUploadImportID);

            var IsRecordOwner = args.getDataKeyValue('IsRecordOwner');
            if (IsRecordOwner == "False" || !isSuccessfulImport || !isGLDataImport) {
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
            var grid = $find("<%= rgDataImport.ClientID %>");
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
