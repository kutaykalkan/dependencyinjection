<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_RiskRatingDropDown" Codebehind="RiskRatingDropDown.ascx.cs" %>
<asp:DropDownList ID="ddlRiskRating" runat="server"></asp:DropDownList>
<asp:RequiredFieldValidator ID="vldRiskRating" runat="server" ControlToValidate="ddlRiskRating" Enabled="false"
Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>