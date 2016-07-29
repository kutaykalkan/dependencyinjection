<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_AccountInfoLeftPane" Codebehind="AccountInfoLeftPane.ascx.cs" %>
<%@ Register TagPrefix="Popup" TagName="RecFrequency" Src="~/UserControls/PopupRecFrequency.ascx" %>
<div id="AccountInfoLeftPane">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="coloredRow">
                <webControls:ExLabel ID="lblAccountInfo" runat="server" LabelID="1362" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tdAccountInfo" valign="top" width="20%">
                                        <webControls:ExLabel ID="lblFSCaption" runat="server" LabelID="1337" FormatString="{0}:"
                                            SkinID="Black11Arial"></webControls:ExLabel>
                                    </td>
                                    <td id="tdFSCaptionValue" runat="server" valign="top">
                                        &nbsp;<webControls:ExLabel ID="lblFSCaptionValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel><%--Black11ArialValue--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblAccountType" runat="server" LabelID="1363" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblAccountTypeValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr id="trKeyAccount" runat="server">
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblKeyAccount" runat="server" LabelID="1014" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblKeyAccountValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblSystemReconciled" runat="server" LabelID="2037" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblSystemReconciledValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblRiskRating" runat="server" FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblRiskRatingValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                            <Popup:RecFrequency ID="ucPopupRecFrequency" runat="server" />
                        </td>
                    </tr>
                    <tr id="trActivityInPeriod" runat="server">
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblActivityInPeriod" runat="server" LabelID="1364" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblActivityInPeriodValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr id="trBaseCurrency" runat="server">
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblBaseCurrency" runat="server" LabelID="1367" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblBaseCurrencyValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblReportingCurrency" runat="server" LabelID="1348" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblReportingCurrencyValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblSubLedgerSource" runat="server" LabelID="1058" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblSubLedgerSourceValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="coloredRow">
                <webControls:ExLabel ID="lblOwnership" runat="server" LabelID="1369" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblPreparer" runat="server" LabelID="1130" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblPreparerValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblReviewer" runat="server" LabelID="1131" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblReviewerValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr id="trA" runat="server">
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblApprover" runat="server" LabelID="1132" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblApproverValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                     <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblBackupPreparer" runat="server" LabelID="2501" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblBackupPreparerValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblBackupReviewer" runat="server" LabelID="2502" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblBackupReviewerValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr id="tr1" runat="server">
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblBackupApprover" runat="server" LabelID="2503" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblBackupApproverValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="coloredRow">
                <webControls:ExLabel ID="lblRecStatus" runat="server" LabelID="1370" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblPendingReview" runat="server" LabelID="1091" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblPendingReviewValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr id="trRecStatusApprover" runat="server">
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblPendingApproval" runat="server" LabelID="1094" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblPendingApprovalValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblReconciled" runat="server" LabelID="1739" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblReconciledValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="coloredRow">
                <webControls:ExLabel ID="lblMaterialityThreshold" runat="server" LabelID="1259" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblAccountMateriality" runat="server" LabelID="1372" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblAccountMaterialityValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblUnexplainedVarianceMateriality" runat="server" LabelID="1373"
                                FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblUnexplainedVarianceMaterialityValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <asp:Panel ID="pnlCertStatus" runat="server">
            <tr>
                <td class="coloredRow">
                    <webControls:ExLabel ID="lblCertStatus" runat="server" LabelID="1374" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="tdAccountInfo">
                                <webControls:ExLabel ID="lblCertStatusPreparer" runat="server" LabelID="1130" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                                <webControls:ExLabel ID="lblCertStatusPreparerValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdAccountInfo">
                                <webControls:ExLabel ID="lblCertStatusReviewer" runat="server" LabelID="1131" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                                <webControls:ExLabel ID="lblCertStatusReviewerValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                            </td>
                        </tr>
                        <tr id="trCertStatusApprover" runat="server">
                            <td class="tdAccountInfo">
                                <webControls:ExLabel ID="lblCertStatusApprover" runat="server" LabelID="1132" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                                <webControls:ExLabel ID="lblCertStatusApproverValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </asp:Panel>
        
              <tr>
            <td class="coloredRow">
                <webControls:ExLabel ID="lblDueDates" runat="server" LabelID="1368" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblPreparerDueDate" runat="server" LabelID="1417" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblPreparerDueDateVal" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblReviewerDueDate" runat="server" LabelID="1418" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblReviewerDueDateVal" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr id="trApproverDueDate" runat="server">
                        <td class="tdAccountInfo">
                            <webControls:ExLabel ID="lblApproverDueDate" runat="server" LabelID="1738" FormatString="{0}:"
                                SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblApproverDueDateVal" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
                        </td>
                    </tr>                   
                </table>
            </td>
        </tr>
    </table>
</div>

<script type="text/javascript" language="javascript">    function showPopup() {
        return false;
    } </script>

