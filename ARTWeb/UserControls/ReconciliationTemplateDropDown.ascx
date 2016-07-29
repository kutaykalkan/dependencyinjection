<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_ReconciliationTemplateDropDown" Codebehind="ReconciliationTemplateDropDown.ascx.cs" %>
<asp:dropdownlist id="ddlReconciliationTemplate" runat="server" skinid="DropDownList200">
</asp:dropdownlist>
<asp:requiredfieldvalidator id="vldReconciliationTemplate" runat="server" controltovalidate="ddlReconciliationTemplate"
    enabled="false" text="!" font-bold="true" font-size="Medium">
</asp:requiredfieldvalidator>