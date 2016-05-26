<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_SRARuleSelection" Codebehind="SRARuleSelection.ascx.cs" %>
<%@ Register TagPrefix="UserControls" TagName="InputRequirements" Src="~/UserControls/InputRequirements.ascx" %>
<asp:Panel ID="pnlHeader" runat="server">
    <table class="InputRequrementsHeading" width="100%">
        <tr>
            <td width="70%">
                <webControls:ExLabel ID="lblSRARuleSelection" runat="server" LabelID="1816" SkinID="BlueBold11Arial" />
            </td>
            <td width="30%" align="right">
                <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlSRARuleSelection" runat="server">
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
                            <telerikWebControls:ExRadGrid ID="rgSRARuleSelection" runat="server" AllowMultiRowSelection="true"
                                GroupHeaderItemStyle-Height="25px" ClientSettings-Selecting-AllowRowSelect="true" NoMasterRecordsLabelID="1816"
                                OnItemDataBound="rgSRARuleSelection_ItemDataBound">
                                <MasterTableView DataKeyNames="SystemReconciliationRuleID">
                                    <Columns>
                                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" />
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="SystemReconciliationRule" LabelID="1814">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSRARule" LabelID='<%# DataBinder.Eval(Container.DataItem, "SystemReconciliationRuleLabelID") %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="SystemReconciliationRuleNumber"
                                            LabelID="2041" HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSRARuleNo" Text='<%# DataBinder.Eval(Container.DataItem, "SystemReconciliationRuleNumber") %>'
                                                    runat="server" />
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
</asp:Panel>
<ajaxToolkit:CollapsiblePanelExtender ID="cpeSRARuleSelection" TargetControlID="pnlSRARuleSelection"
    ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
    runat="server" SkinID="CollapsiblePanel" Collapsed="false">
</ajaxToolkit:CollapsiblePanelExtender>
