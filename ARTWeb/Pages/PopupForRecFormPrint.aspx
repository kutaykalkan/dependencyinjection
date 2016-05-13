<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" CodeFile="PopupForRecFormPrint.aspx.cs" Inherits="Pages_PopupForRecFormPrint"
    ValidateRequest="false" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="15%" style="padding-left: 15px" />
        <col width="85%" />
       <tr>
            <td colspan ="2" >
               &nbsp;&nbsp;&nbsp; <webControls:ExLabel ID="ExLabel1" LabelID="2036" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 2px;">
                &nbsp;
            </td>
            <td style="padding-top: 2px">
                <webControls:ExRadioButton ID="optSummary"  Checked ="true" runat="server" LabelID="2032" GroupName="PrintType"
                    SkinID="OptBlack11Arial" />
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px;">
                &nbsp;
            </td>
            <td style="padding-top: 10px">
                <webControls:ExRadioButton ID="optDetail"  runat="server"  LabelID="2033" GroupName="PrintType"
                    SkinID="OptBlack11Arial" />
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px;">
                &nbsp;
                <asp:Label ID="tempUrlreffer" runat ="server" Visible="false"  ></asp:Label>
            </td>
            <td style="padding-top: 10px"  align ="right" >
               &nbsp; <webControls:ExButton ID="btnOk" runat="server" LabelID="2034" 
                    onclick="btnOk_Click" /> &nbsp;&nbsp;&nbsp;              
            </td>
        </tr>
    </table>
 </asp:Content>
