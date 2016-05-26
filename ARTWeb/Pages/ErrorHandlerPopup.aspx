<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_ErrorHandlerPopup"
Theme="SkyStemBlueBrown" Codebehind="ErrorHandlerPopup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" colspan="2">
                <webControls:ExLabel ID="lblErrorOccured" runat="server" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td class="BlankRow">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="15%" valign="top">
                <webControls:ExLabel ID="lblError" runat="server" LabelID="1051" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>&nbsp;
            </td>
            <td>
                <webControls:ExLabel runat="server" ID="lblMessage" SkinID="Black11ArialNormal"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>

</asp:Content>

