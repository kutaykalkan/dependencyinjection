<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown"
    AutoEventWireup="true" CodeFile="BulkCompleteTasks.aspx.cs" Inherits="Pages_TaskMaster_BulkCompleteTasks" %>

<%@ Register Src="~/UserControls/TaskMaster/BulkCompleteTasks.ascx" TagName="BulkCompleteTasks"
    TagPrefix="UserControl" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" language="javascript">

        var radWindow = null;
        $(document).ready(function() {
            radWindow = GetRadWindow();
            if (radWindow != null && radWindow.argument) {
                $('#<%= hfTaskDetailsIDs.ClientID %>').val(radWindow.argument);
                //alert( 'popup argument '+ radWindow.argument);            
            }
        });
    </script>--%>
        <UserControl:BulkCompleteTasks ID ="ucBulkCompleteTasks" runat="server" />
</asp:Content>
