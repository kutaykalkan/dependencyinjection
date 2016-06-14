<%@ Page Language="C#" MasterPageFile="~/MasterPages/CertificationMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_CertificationSignOff"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="CertificationSignOff.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register Src="~/UserControls/Signature.ascx" TagName="Signature" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCertification" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td align="center">
                        <table style="width: 96%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table style="width: 100%" cellpadding="0" cellspacing="0" class="DataImportStatusMessage">
                                        <tr>
                                            <td align="center" class="DataImportStatusMessageTitleFirstRow">
                                                <webControls:ExLabel ID="lblCertificationHeader" LabelID="1702 " runat="server" SkinID="Black11Arial"
                                                    Width="100%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="DataImportStatusMessageTitle">
                                                <webControls:ExLabel ID="lblCertificationDate" runat="server" SkinID="Black11Arial"
                                                    Width="100%" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <webControls:ExLabel ID="lblCertificationVerbiage" runat="server" SkinID="Black11ArialNormal"
                                                    Width="100%" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <table style="width: 90%" cellpadding="0" cellspacing="0">
                                        <col width="20%" />
                                        <col width="80%" />
                                        <tr>
                                            <td>
                                                <webControls:ExLabel ID="lblAdditionalComments" runat="server" LabelID="1468 " FormatString="{0}:"
                                                    SkinID="Black11Arial" />
                                            </td>
                                            <td align="left">
                                                <webControls:ExTextBox ID="txtAdditionalComments" runat="server" SkinID="ExTextBoxAdditionalComments" />
                                                <webControls:ExLabel ID="lblAdditionalCommentsValue" runat="server" FormatString="{0}:"
                                                    SkinID="Black11ArialNormal" />
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <webControls:ExButton ID="btnAgree" runat="server" LabelID="1476" OnClick="btnAgree_Click"
                                                    SkinID="ExButton100" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <UserControl:Signature ID="ucSignature" runat="server" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <UserControls:ProgressBar ID="ucProgressBar" runat="server" AssociatedUpdatePanelID="upnlMain" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
