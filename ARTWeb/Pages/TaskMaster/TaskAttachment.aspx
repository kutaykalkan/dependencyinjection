<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown"
    AutoEventWireup="true" CodeFile="TaskAttachment.aspx.cs" Inherits="Pages_TaskMaster_TaskAttachment" %>

<%@ Register Src="~/UserControls/TaskMaster/Attachments.ascx" TagName="Attachments" TagPrefix="UserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <UserControl:Attachments ID="ucAttachments" runat="server" />
</asp:Content>
