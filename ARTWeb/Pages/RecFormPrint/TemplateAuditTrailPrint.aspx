<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AuditTrailPrint.master"  Theme="SkyStemBlueBrown"
    AutoEventWireup="true" Inherits="Pages_RecFormPrint_TemplateAuditTrailPrint" Codebehind="TemplateAuditTrailPrint.aspx.cs" %>

<%@ Register Src="~/UserControls/AuditTrail.ascx" TagName="AuditTrail" TagPrefix="UserControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <UserControls:AuditTrail ID="ucAuditTrail" runat="server" />
</asp:Content>
