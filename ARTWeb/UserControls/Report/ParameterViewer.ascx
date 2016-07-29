<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="ParameterViewer" Codebehind="ParameterViewer.ascx.cs" %>
<asp:Panel ID="pnlParamViewer" runat="server" Height="80px" Width="250px" ScrollBars="Auto">
    <webControls:ExLinkButton ID="lbtnParams" runat="server" 
        SkinID="GridLinkButton" OnCommand = "lnkbtnCommand"></webControls:ExLinkButton>
</asp:Panel>
