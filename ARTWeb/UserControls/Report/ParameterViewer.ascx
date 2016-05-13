<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ParameterViewer.ascx.cs"
    Inherits="ParameterViewer" %>
<asp:Panel ID="pnlParamViewer" runat="server" Height="80px" Width="250px" ScrollBars="Auto">
    <webControls:ExLinkButton ID="lbtnParams" runat="server" 
        SkinID="GridLinkButton" OnCommand = "lnkbtnCommand"></webControls:ExLinkButton>
</asp:Panel>
