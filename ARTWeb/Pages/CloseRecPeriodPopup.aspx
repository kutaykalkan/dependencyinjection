<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" Inherits="Pages_CloseRecPeriod" Codebehind="CloseRecPeriodPopup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="35%" />
        <col width="65%" />
        <tr>
            <td style="padding-left: 5px;" colspan="2">
                <webControls:ExLabel ID="lblAccountStatus" LabelID="2091" SkinID="Black11Arial" runat="server"
                    FormatString="{0} :"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 15px;">
                <webControls:ExLabel ID="lblAcStatus" LabelID="2092" FormatString="{0} :" SkinID="Black11ArialNormal"
                    runat="server"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblAccountStatusValue" SkinID="Black11ArialNormal" runat="server"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 15px;">
                <webControls:ExLabel ID="lblSRAStatus" LabelID="2093" FormatString="{0} :" SkinID="Black11ArialNormal"
                    runat="server"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblSRAStatusValue" SkinID="Black11ArialNormal" runat="server"></webControls:ExLabel>
            </td>
        </tr>
        <asp:Panel ID="pnlCertStatus" runat="server">
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px;" colspan="2">
                    <webControls:ExLabel ID="lblCertificationStatus" LabelID="1464" FormatString="{0} :"
                        SkinID="Black11Arial" runat="server"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 15px;">
                    <webControls:ExLabel ID="lblCertStatus" LabelID="2094" FormatString="{0} :" SkinID="Black11ArialNormal"
                        runat="server"></webControls:ExLabel>
                </td>
                <td align="left">
                    <webControls:ExLabel ID="lblCertStatusValue" SkinID="Black11ArialNormal" FormatString="{0}({1}%, ${2} )"
                        runat="server"></webControls:ExLabel>
                </td>
            </tr>
        </asp:Panel>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 5px;" colspan="2">
                <webControls:ExLabel ID="lblMessage" SkinID="Black11ArialNormal" runat="server"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="padding-right: 30px; padding-top: 20px">
                <webControls:ExButton ID="btnOk" runat="server" Width="50" Height="25" LabelID="1252"
                    OnClick="btnOk_Click" />
                &nbsp;
                <webControls:ExButton ID="btnCancel" runat="server" Width="70" LabelID="1251" Height="25"
                    OnClientClick="GetRadWindow().Close();" CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
