<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_AccountTypeDropDown" Codebehind="AccountTypeDropDown.ascx.cs" %>
<asp:DropDownList ID="ddlAccountType" runat="server">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="vldAccountType" runat="server" ControlToValidate="ddlAccountType"
Text="!" Font-Bold="true" Font-Size="Medium" Enabled="false"></asp:RequiredFieldValidator>