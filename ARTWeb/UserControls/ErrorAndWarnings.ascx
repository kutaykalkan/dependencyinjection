<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_ErrorAndWarnings" Codebehind="ErrorAndWarnings.ascx.cs" %>
<asp:Panel ID="pnlHeader" runat="server">
    <table class="ErrorAndWarningHeading">  <%--InputRequrementsHeading--%>
        <tr>
            <td width="2%">
                <webControls:ExImage ID="imgIconError" runat="server" SkinID="ExpireIcon" />
                <webControls:ExImage ID="imgIconWarning" runat="server" SkinID="WarningIcon" />
            </td>
            <td width="94%">
                <webControls:ExLabel ID="lblErrorOrWarning" runat="server"  /><%--LabelID="1809 "--%>
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
            <table class="ErrorAndWarningText">  <%--InputRequrementsText--%>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td width="5%" align="right" valign="top">
                    <SPAN style='FONT-FAMILY: Wingdings 3;color:Blue'></SPAN>
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
    ImageControlID="imgCollapse" CollapseControlID="pnlHeader"  ExpandControlID="pnlHeader" 
    runat="server" SkinID="CollapsiblePanel" Collapsed="true">
</ajaxToolkit:CollapsiblePanelExtender>
