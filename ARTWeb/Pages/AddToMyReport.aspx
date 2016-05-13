<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" CodeFile="AddToMyReport.aspx.cs" Inherits="Pages_AddToMyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="35%" style="padding-left: 15px" />
        <col width="65%" />
        <tr>
            <td colspan="2" align="center">
                <webControls:ExLabel ID="lblReportName" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px">
                <webControls:ExLabel ID="lblName" SkinID="Black11ArialNormal" LabelID="1287" runat="server"></webControls:ExLabel>
            </td>
            <td style="padding-top: 10px">
                <webControls:ExTextBox ID="txtName" SkinID="ExTextBox200" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right" style="padding-right: 100px; padding-top: 20px">
                <webControls:ExButton ID="btnOk" runat="server" Width="50" Height="25" LabelID="1742"
                    OnClick="btnOk_Click" />
                &nbsp;
                <webControls:ExButton ID="btnCancel" runat="server" Width="70" LabelID="1239" Height="25"
                    OnClientClick="window.close();" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
