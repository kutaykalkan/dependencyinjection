<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_ConfirmPopUp" Theme="SkyStemBlueBrown" Codebehind="ConfirmPopUp.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:Panel ID="pnlSkipped" runat="server" SkinID="ModalPopupHoverPanel">--%>
        <table>
            <tr>
                <td>
                    <webControls:ExLabel runat="server" ID="lblSkippedMsgPopUp" SkinID="Black11Arial"
                        Text="Hello  lhkg"></webControls:ExLabel>
                </td>
            </tr>
            <tr class="BlankRow">
            </tr>
            <tr>
                <td align="right">
                    <webControls:ExButton LabelID="1742 " runat="server" ID="btnSkippedOK" CausesValidation="false"
                        SkinID="ExButton100" OnClick="btnSkippedOK_Click" />
                    <webControls:ExButton LabelID="1239" runat="server" ID="btnSkippedCancel" CausesValidation="false"
                        SkinID="ExButton100" OnClick="btnSkippedCancel_Click" />
                </td>
            </tr>
        </table>
    <%--</asp:Panel>--%>
</asp:Content>
