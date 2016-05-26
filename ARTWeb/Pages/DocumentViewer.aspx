<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" Inherits="Pages_DocumentViewer" Codebehind="DocumentViewer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindow ID="rwDocument" runat="server" VisibleOnPageLoad="true" AutoSize="true" BorderWidth="0px"
        VisibleStatusbar="false" VisibleTitlebar="false" KeepInScreenBounds="true" InitialBehaviors="Maximize" OnClientPageLoad="PrintPage">
    </telerik:RadWindow>
    <script language="javascript" type="text/jscript">
    <!--
        function PrintPage(sender, args) {
            var oWindow = sender;          
            oWindow.GetContentFrame().contentWindow.focus();
            oWindow.GetContentFrame().contentWindow.print();
    }
    //-->
    </script>    
</asp:Content>
