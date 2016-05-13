<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserDropDown.ascx.cs"
    Inherits="UserControls_UserDropDown" %>
<asp:DropDownList ID="ddlUser" runat="server" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" AutoPostBack="true">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="vldUser" runat="server" Enabled="false" InitialValue="-2"
    ControlToValidate="ddlUser" Text="!" Font-Bold="true" Font-Size="Medium" />
<asp:CustomValidator ID="vldUserState" ClientValidationFunction="ValidateUserState"
    runat="server" Text="!" Font-Bold="true" Font-Size="Medium" ControlToValidate="ddlUser"></asp:CustomValidator>