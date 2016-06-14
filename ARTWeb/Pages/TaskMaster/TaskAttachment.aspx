<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown"
    AutoEventWireup="true" Inherits="Pages_TaskMaster_TaskAttachment" Codebehind="TaskAttachment.aspx.cs" %>

<%@ Register Src="~/UserControls/TaskMaster/Attachments.ascx" TagName="Attachments" TagPrefix="UserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <UserControl:Attachments ID="ucAttachments" runat="server" />
</asp:Content>
