<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardPreferences.aspx.cs"
    Inherits="Pages_DashboardPreferences" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlHeader" runat="server">
        <table class="InputRequrementsHeading" width="96%">
            <tr>
                <td width="70%">
                    <webControls:ExLabel ID="lblDashboard" runat="server" LabelID="2511" SkinID="BlueBold11Arial" />
                </td>
                <td width="30%" align="right">
                    <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDashboardPreferences" runat="server">
        <asp:UpdatePanel ID="pnlDashBoardPrefrences" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="blueBorder">
                    <asp:Panel ID="pnlGidDashBoardPrefrences" runat="server">
                        <tr>
                            <td>
                                <telerikWebControls:ExRadGrid ID="rgDashboardPreferences" runat="server" EntityNameLabelID="1229"
                                    AllowPaging="false" AllowSorting="false" OnItemDataBound="rgDashboardPreferences_ItemDataBound"
                                    OnNeedDataSource="rgDashboardPreferences_NeedDataSource" AllowMultiRowSelection="true"
                                    AllowExportToExcel="false" AllowExportToPDF="false" AllowCauseValidationExportToExcel="false"
                                    AllowCauseValidationExportToPDF="false">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <ClientEvents OnRowSelecting="Selecting" />
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="DashboardID">
                                        <Columns>
                                            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                                            <telerikWebControls:ExGridTemplateColumn LabelID="2415">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDashBoardName" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" />
                                            </telerikWebControls:ExGridTemplateColumn>
                                            <telerikWebControls:ExGridTemplateColumn LabelID="1408">
                                                <ItemTemplate>
                                                    <webControls:ExLabel ID="lblDashBoardDesc" runat="server"></webControls:ExLabel>
                                                </ItemTemplate>
                                                <HeaderStyle Width="80%" />
                                            </telerikWebControls:ExGridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerikWebControls:ExRadGrid>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td style="text-align: right; padding-right: 25px">
                            <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" SkinID="ExButton100"
                                OnClick="btnSave_OnClick" />&nbsp;
                            <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                                OnClick="btnCancel_OnClick" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 110px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeSRARuleSelection" TargetControlID="pnlDashboardPreferences"
        ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
        runat="server" SkinID="CollapsiblePanel" Collapsed="false">
    </ajaxToolkit:CollapsiblePanelExtender>
    <input type="text" id="Sel" runat="server" style="display: none" />
    <asp:Panel ID="pnlHeaderBackup" runat="server" Style="padding-top: 20px;">
        <table class="InputRequrementsHeading" width="96%">
            <tr>
                <td width="70%">
                    <webControls:ExLabel ID="lblBackup" runat="server" LabelID="1310" SkinID="BlueBold11Arial" />
                </td>
                <td width="30%" align="right">
                    <webControls:ExImage ID="imgCollapseBackup" runat="server" SkinID="CollapseIcon" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlBackupNotification" runat="server">
        <asp:UpdatePanel ID="upnlBackupRoleNotifications" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="blueBorder">
                    <tr>
                        <td>
                            <telerikWebControls:ExRadGrid ID="rgMyPreferences" runat="server" AllowMultiRowSelection="true"
                                GroupHeaderItemStyle-Height="25px" ClientSettings-Selecting-AllowRowSelect="true"
                                NoMasterRecordsLabelID="1816" OnItemDataBound="rgMyPreferences_ItemDataBound">
                                <MasterTableView>
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" />
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="RoleConfiguration" LabelID="1408">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBackupNotifications" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                                </ClientSettings>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; padding-right: 14px">
                            <webControls:ExButton Style="width: auto !important;" ID="btnSendMail" runat="server"
                                LabelID="2525" SkinID="ExButton100" />&nbsp;
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeBackupNotification" TargetControlID="pnlBackupNotification"
        ImageControlID="imgCollapseBackup" CollapseControlID="pnlHeaderBackup" ExpandControlID="pnlHeaderBackup"
        runat="server" SkinID="CollapsiblePanel" Collapsed="false">
    </ajaxToolkit:CollapsiblePanelExtender>

    <script language="javascript" type="text/javascript">
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
     

    </script>

</asp:Content>
