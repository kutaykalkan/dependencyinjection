<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" Inherits="Pages_ReOpenRecPeriodPopup" Codebehind="ReOpenRecPeriodPopup.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="35%" />
        <col width="65%" />
        <tr class="BlankRow">
            <td> </td>
        </tr>
        <tr>
            <td style="padding-left: 5px;" colspan="2">
                <webControls:ExLabel ID="lblMsg" SkinID="Black11Arial" runat="server"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow"></tr>
        <tr>
            <td style="padding-left: 15px; width: 50%">
                <webControls:ExLabel ID="lblRecCloseDate" LabelID="1419" FormatString="{0} :" SkinID="Black11ArialNormal"
                    runat="server"></webControls:ExLabel>
            </td>
            <td style="padding-left: 10px; width: 50%">
                <webControls:ExCalendar ID="calCloseOrLockDownDate" runat="server" SkinID="ExCalendar100" />
                <asp:RequiredFieldValidator ID="rfvCloseOrLockDownDate" runat="server" ControlToValidate="calCloseOrLockDownDate">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvValidateCloseOrLockDownDate" runat="server" Font-Bold="true" Font-Size="Medium" ControlToValidate="calCloseOrLockDownDate"
                    OnServerValidate="cvValidateCloseOrLockDownDate_OnServerValidate"></asp:CustomValidator> 
                           
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td align="right" colspan="2" style="padding-right: 30px; padding-top: 20px">
                <webControls:ExButton ID="btnOk" runat="server" Width="50" Height="25" LabelID="1742"
                    OnClick="btnOk_Click" />
                &nbsp;
                <webControls:ExButton ID="btnCancel" runat="server" Width="70" LabelID="1239" Height="25"
                    OnClientClick="GetRadWindow().Close();" CausesValidation="false" />
            </td>
        </tr>
    </table>   
</asp:Content>
