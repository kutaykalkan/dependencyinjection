<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_PopupNetAccountComposition"
    Theme="SkyStemBlueBrown" Codebehind="PopupNetAccountComposition.aspx.cs" %>
    
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
