<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RiskRatingDropDown.ascx.cs" Inherits="UserControls_RiskRatingDropDown" %>
<asp:DropDownList ID="ddlRiskRating" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="vldRiskRating" runat="server" ControlToValidate="ddlRiskRating" Enabled="false"
Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>