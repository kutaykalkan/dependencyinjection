<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.DocumentUploadButton" Codebehind="DocumentUploadButton.ascx.cs" %>
<webControls:ExImageButton ID="imgbtnDocument" runat="server" SkinID="ShowDocumentPopup"
    OnClientClick="ShowDocumentUpload();return false;" LabelID="1540" />
    
     <telerik:RadWindow ID="RadWindowOpenDocument" VisibleOnPageLoad="false" runat="server"  OpenerElementID='<%imgbtnDocument.ClientID %>' Modal="true" Width="700px" Height="400px" Top="50px" >
    </telerik:RadWindow>
    <script type="text/javascript">
 function ShowDocumentUpload() {

     var oWnd = $find("<%=RadWindowOpenDocument.ClientID%>");
            oWnd.setUrl("<%=this.URL%>");
            oWnd.show();
        }


       
        </script>