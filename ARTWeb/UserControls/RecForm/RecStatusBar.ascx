<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_RecForm_RecStatusBar" Codebehind="RecStatusBar.ascx.cs" %>
<%@ Register TagPrefix="UserControls" TagName="DisplayQualityScore" Src="~/UserControls/QualityScore/DisplayQualityScore.ascx" %>
<asp:Panel ID="pnlStatus" runat="server">
    <table cellpadding="0" cellspacing="0" class="RecStatusBar" border="0">
        <tr>
            <td id="tdRecStatus" runat="server">
                <webControls:ExLabel ID="Label1" runat="server" SkinID="Black11Arial" LabelID="1370"
                    FormatString="{0}:" />
                &nbsp;
                <webControls:ExLabel ID="lblRecStatus" runat="server" SkinID="Black11Arial" />
            </td>
            <td align="center" id="tdQualityScore" runat="server">
                <UserControls:DisplayQualityScore ID="ucDisplayQualityScore" runat="server" />
            </td>
            <td align="center">
                <asp:Panel ID="pnlReconciledBalance" runat="server">
                    <webControls:ExLabel ID="lblReconciledBalance" runat="server" SkinID="Black11Arial"
                        FormatString="{0}:" />
                    &nbsp;
                    <webControls:ExLabel ID="lblReconciledBalanceValue" runat="server" SkinID="Black11Arial" />
                </asp:Panel>
                <asp:Panel ID="pnlDueDate1" runat="server">
                    <webControls:ExLabel ID="lblAccountDueDate" runat="server" LabelID="1421" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                    &nbsp;
                    <webControls:ExLabel ID="lblAccountDueDateValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                </asp:Panel>
            </td>
            <td align="right">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td id="tdUnexpVar" runat="server" align="right">
                            <webControls:ExLabel ID="lblUnexpVar" runat="server" SkinID="Black11Arial" FormatString="{0}:" />
                            &nbsp;
                            <webControls:ExLabel ID="lblUnexpVarValue" runat="server" SkinID="Black11Arial" />
                        </td>
                        <td id="tdDueDate2" runat="server" align="right">
                            <webControls:ExLabel ID="lblCertificationStartDate" runat="server" LabelID="1453" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblCertificationStartDateValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td id="tdExportButtons" runat="server" class="HideInPDF" align="right">
                            <asp:HiddenField ID="hdnInnerHTML" runat="server" />
                            <%--  <webControls:ExImageButton ID="imgBtnExportToPDF" runat="server" OnClick="imgBtnExportToPDF_Click"
                                                            SkinID="ExportToPDF" />--%>
                                                             &nbsp;
                            <webControls:ExHyperLink ID="hlPDF" runat="server" SkinID="PDFIcon"></webControls:ExHyperLink>
                            &nbsp;
                            <webControls:ExHyperLink ID="hlPrint" runat="server" SkinID="PrintIcon"></webControls:ExHyperLink>
                            &nbsp;
                            <webControls:ExHyperLink ID="hlEmail" runat="server" SkinID="Email"></webControls:ExHyperLink>
                            &nbsp;
                            <webControls:ExImageButton ID="lnkBtnDownloadAttachments" runat="server" style="float:right;margin-top:1px;"
                                SkinID="DownloadAttachments"></webControls:ExImageButton>
                            <iframe id="ifDownloader" runat="server" style="display:none;" />
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>
