<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_SubledgerSourceDropDown" Codebehind="SubledgerSourceDropDown.ascx.cs" %>
<asp:DropDownList ID="ddlSubledgerSource" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="vldSubledgerSource" runat="server" ControlToValidate="ddlSubledgerSource"
Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>