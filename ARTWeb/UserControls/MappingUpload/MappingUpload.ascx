<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MappingUpload.ascx.cs" Inherits="UserControls_MappingUpload_MappingUpload" %>
<%@ Register TagPrefix="UserControls" TagName="InputRequirements" Src="~/UserControls/InputRequirements.ascx" %>

<script type="text/javascript">
    function rowSelecting(sender, args) {
        var isSelectable = args.getDataKeyValue('IsEnabled') == 'True';
        args.set_cancel(!isSelectable);
    }
    
    function pageLoad() {
        var tableView = $find('<%= rgMappingUpload.ClientID %>').get_masterTableView();
        var headerRow = Telerik.Web.UI.Grid.getTableHeaderRow(tableView.get_element());
        var checkBox = getSelectCheckBox(headerRow);
        if (checkBox) checkBox.onclick = function(e) {
            var event = e || window.event;
            selectAllRows(checkBox.checked, event);
        };
    }


    function selectAllRows(checkHeaderSelectCheckBox, event) {
        var gridSelection = new Telerik.Web.UI.GridSelection();
        var grid = $find('<%= rgMappingUpload.ClientID %>');
        var tableView = grid.get_masterTableView();
        var headerRow = Telerik.Web.UI.Grid.getTableHeaderRow(tableView.get_element());
        grid._selectAllRows(tableView.get_id(), null, event);
        gridSelection._checkClientSelectColumn(headerRow, checkHeaderSelectCheckBox);
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

<asp:Panel ID="pnlHeader" runat="server">
    <table class="InputRequrementsHeading" width="100%">
        <tr>
            <td width="70%">
                <webControls:ExLabel ID="lblMappingUpload" runat="server" LabelID="2449" SkinID="BlueBold11Arial" />
            </td>
            <td width="30%" align="right">
                <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlMappingUpload" runat="server">
<asp:UpdatePanel ID="updMappingUpload" runat="server">
<ContentTemplate>
    <table width="100%" cellspacing="0" cellpadding="0" class="blueBorder">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <UserControls:InputRequirements ID="ucInputRequirements" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerikWebControls:ExRadGrid ID="rgMappingUpload" runat="server" AllowMultiRowSelection="true"
                                GroupHeaderItemStyle-Height="25px" ClientSettings-Selecting-AllowRowSelect="true"
                                OnItemDataBound="rgMappingUpload_ItemDataBound" NoMasterRecordsLabelID="2423">
                               <ClientSettings>
                               <ClientEvents OnRowSelecting="rowSelecting" />
                               </ClientSettings>
                                <MasterTableView DataKeyNames="AccountMappingKeyID" EnableViewState="true" ClientDataKeyNames="IsEnabled">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" />
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="GeographyClassName" LabelID="2450">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblGeographyClassName" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="GeographyStructure" LabelID="2451"
                                            HeaderStyle-Width="50%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblGeographyStructure" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerikWebControls:ExRadGrid>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td />
                    </tr>
                    <tr>
                        <td align="right">
                            <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" SkinID="ExButton100"
                                OnClick="btnSave_OnClick" />&nbsp;
                            <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                                OnClick="btnCancel_OnClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
  </asp:UpdatePanel>  
</asp:Panel>
<ajaxToolkit:CollapsiblePanelExtender ID="cpeQualityScore" TargetControlID="pnlMappingUpload"
    ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
    runat="server" SkinID="CollapsiblePanel" Collapsed="false">
</ajaxToolkit:CollapsiblePanelExtender>