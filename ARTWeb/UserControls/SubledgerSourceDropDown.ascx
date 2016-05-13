<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubledgerSourceDropDown.ascx.cs" Inherits="UserControls_SubledgerSourceDropDown" %>
<asp:DropDownList ID="ddlSubledgerSource" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="vldSubledgerSource" runat="server" ControlToValidate="ddlSubledgerSource"
Enabled="false" Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>