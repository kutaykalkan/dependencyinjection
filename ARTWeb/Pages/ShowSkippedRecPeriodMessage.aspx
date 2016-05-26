<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_ShowSkippedRecPeriodMessage"
    Theme="SkyStemBlueBrown" Codebehind="ShowSkippedRecPeriodMessage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="100%" align="center">
                <webControls:ExLabel ID="lblErrorMessageForSkippedRecPeriod" LabelID="5000183" SkinID="Black11ArialNormal"
                    runat="server" FormatString="{0}."></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <webControls:ExButton ID="btnOk" runat="server" LabelID="1544" OnClick="btnOk_Click" SkinID="ExButton100" />
            </td>
        </tr>
    </table>
</asp:Content>
