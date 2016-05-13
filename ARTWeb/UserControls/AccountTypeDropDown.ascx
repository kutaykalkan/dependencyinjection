<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountTypeDropDown.ascx.cs" Inherits="UserControls_AccountTypeDropDown" %>
<asp:DropDownList ID="ddlAccountType" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="vldAccountType" runat="server" ControlToValidate="ddlAccountType"
Text="!" Font-Bold="true" Font-Size="Medium" Enabled="false"></asp:RequiredFieldValidator>