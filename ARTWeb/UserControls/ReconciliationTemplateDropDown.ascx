<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReconciliationTemplateDropDown.ascx.cs"
    Inherits="UserControls_ReconciliationTemplateDropDown" %>
<asp:dropdownlist id="ddlReconciliationTemplate" runat="server" skinid="DropDownList200">
</asp:dropdownlist>
<asp:requiredfieldvalidator id="vldReconciliationTemplate" runat="server" controltovalidate="ddlReconciliationTemplate"
    enabled="false" text="!" font-bold="true" font-size="Medium">
</asp:requiredfieldvalidator>