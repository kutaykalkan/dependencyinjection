<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="PopupNetAccountComposition.aspx.cs" Inherits="Pages_PopupNetAccountComposition"
    Theme="SkyStemBlueBrown" %>
    
  <%@ Register Src="~/UserControls/NetAccountComposition.ascx" TagPrefix="usercontrol" TagName="NetAccountComposition" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <usercontrol:NetAccountComposition id="ucNetAccountComposition" runat="server" />
            </td>
        </tr>
       
    </table>
</asp:Content>
