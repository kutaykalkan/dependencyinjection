<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_DayTypeDropDown" Codebehind="DayTypeDropDown.ascx.cs" %>
<asp:DropDownList ID="ddlDayType" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="vldDayType" runat="server" ControlToValidate="ddlDayType" Enabled="false"
Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>