<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_RecItemImportStatusMessage" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" Codebehind="RecItemImportStatusMessage.aspx.cs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="pnlErrorUpload" runat="server">
                    <table width="60%" class="DataImportStatusMessage" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="DataImportStatusMessageTitle" style="width: 6%">
                                <webControls:ExImage ID="imgFailure" runat="server" SkinID="ExpireIcon" Visible="false" />
                                <webControls:ExImage ID="imgSuccess" runat="server" SkinID="SuccessIcon" Visible="false" />
                                <webControls:ExImage ID="imgWarning" runat="server" SkinID="WarningIcon" Visible="false" />
                                <webControls:ExImage ID="imgProcessing" runat="server" SkinID="ProgressIcon" Height="24px"
                                    Width="23px" Visible="false" />
                                <webControls:ExImage ID="imgToBeProcessed" runat="server" SkinID="ToBeProcessedIcon"
                                    Visible="false" />
                                <webControls:ExImage ID="imgReject" runat="server" LabelID="2400" SkinID="RejectIcon"
                                    Visible="false" />
                            </td>
                            <td class="DataImportStatusMessageTitle">
                                <webControls:ExLabel ID="lblStatusHeading" runat="server" SkinID="BlueBold11Arial"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <webControls:ExLabel ID="ExLabel2" runat="server" SkinID="Black11Arial" FormatString="{0}:"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding-left: 20px;">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="2">
                                            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                        </td>
                                    </tr>
                                    <tr class="BlankRow">
                                        <td>
                                        </td>
                                    </tr>
                                    <asp:Panel ID="pnlFailureMessages" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="200px" CssClass="DataImportErrorPanel">
                                                    <webControls:ExLabel ID="lblFailureMessages" runat="server" SkinID="Black11ArialNormal"></webControls:ExLabel>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlWarning" runat="server">
                                        <tr>
                                            <td colspan="2">
                                                <webControls:ExLabel LabelID="1548" ID="lblConfirmUpload" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <webControls:ExButton ID="btnYes" runat="server" SkinID="ExButton100" LabelID="1481"
                                                OnClick="btnYes_Click" />&nbsp;
                                            <webControls:ExButton ID="btnReject" runat="server" SkinID="ExButton100" LabelID="1482"
                                                OnClick="btnReject_Click" />&nbsp;
                                            <webControls:ExButton ID="btnBack" runat="server" SkinID="ExButton100" LabelID="1545"
                                                OnClick="btnBack_Click" Visible="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
