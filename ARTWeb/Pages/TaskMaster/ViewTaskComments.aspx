<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown"
    AutoEventWireup="true" CodeFile="ViewTaskComments.aspx.cs" Inherits="Pages_TaskMaster_ViewTaskComments" %>

<%@ Register Src="~/UserControls/TaskMaster/ViewTaskComments.ascx" TagName="ViewTaskComments"
    TagPrefix="UserControls" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <UserControls:ViewTaskComments ID="ucViewTaskComments" runat="Server" />
</asp:Content>
