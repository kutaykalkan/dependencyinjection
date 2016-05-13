<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaskCustomFields.ascx.cs"
    Inherits="UserControls_TaskCustomFields" %>
<%@ Register TagPrefix="UserControls" TagName="InputRequirements" Src="~/UserControls/InputRequirements.ascx" %>
<asp:Panel ID="pnlHeader" runat="server">
    <table class="InputRequrementsHeading" width="100%">
        <tr>
            <td width="70%">
                <webControls:ExLabel ID="lblTaskCustomFields" runat="server" LabelID="2950" SkinID="BlueBold11Arial" />
            </td>
            <td width="30%" align="right">
                <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlTaskCustomFields" runat="server">
    <table width="100%" cellspacing="0" cellpadding="0" class="blueBorder">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <UserControls:InputRequirements ID="ucInputRequirements" runat="server" />
                            <%--2693--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerikWebControls:ExRadGrid ID="rgTaskCustomFields" runat="server" AllowMultiRowSelection="false"
                                GroupHeaderItemStyle-Height="25px" ClientSettings-Selecting-AllowRowSelect="false" NoMasterRecordsLabelID="1816"
                                OnItemDataBound="rgTaskCustomFields_ItemDataBound">
                                <MasterTableView DataKeyNames="TaskCustomFieldID">
                                    <Columns>                                       
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="CustomFieldColumn" LabelID="2951"  HeaderStyle-Width="25%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblTaskCustomField"  runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="TaskCustomFieldeValuColumn" HeaderStyle-Width="75%">
                                            <ItemTemplate>
                                                <webControls:ExTextBox ID="txtTaskCustomFieldValue"  runat="server" ></webControls:ExTextBox>
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
</asp:Panel>
<ajaxToolkit:CollapsiblePanelExtender ID="cpeTaskCustomFields" TargetControlID="pnlTaskCustomFields"
    ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
    runat="server" SkinID="CollapsiblePanel" Collapsed="false">
</ajaxToolkit:CollapsiblePanelExtender>
