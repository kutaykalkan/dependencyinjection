<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DayTypeDropDown.ascx.cs" Inherits="UserControls_DayTypeDropDown" %>
<asp:DropDownList ID="ddlDayType" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="vldDayType" runat="server" ControlToValidate="ddlDayType" Enabled="false"
Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>