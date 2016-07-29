<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_InputRequirements" Codebehind="InputRequirements.ascx.cs" %>
<asp:Panel ID="pnlHeader" runat="server">
    <table class="InputRequrementsHeading">
        <tr>
            <td width="2%">
                <webControls:ExImage ID="imgInputRequirementsIcon" runat="server" SkinID="NotesIcon" />
            </td>
            <td width="94%">
                <webControls:ExLabel ID="lblInputRequirements" runat="server" LabelID="1242" />
            </td>
            <td width="4%" align="right">
                <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlContent" runat="server">
    <asp:Repeater ID="rptNotes" runat="server" OnItemDataBound="rptNotes_OnItemDataBound">
        <HeaderTemplate>
            <table class="InputRequrementsText">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td width="5%" align="right" valign="top">
                    <span style='font-family: Wingdings 3; color: Blue'></span>
                </td>
                <td>
                    <webControls:ExLabel ID="lblNote" runat="server" />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Panel>
<ajaxToolkit:CollapsiblePanelExtender ID="cpeInputRequirements" TargetControlID="pnlContent"
    ImageControlID="imgCollapse" CollapseControlID="imgCollapse" ExpandControlID="imgCollapse"
    runat="server" SkinID="CollapsiblePanel" Collapsed="true">
</ajaxToolkit:CollapsiblePanelExtender>
