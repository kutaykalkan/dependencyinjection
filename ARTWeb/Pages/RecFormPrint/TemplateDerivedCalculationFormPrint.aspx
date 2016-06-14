<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/RecFormPrint.master"
    AutoEventWireup="true"
    Inherits="Pages_TemplateDerivedCalculationFormPrint" Theme="SkyStemBlueBrown" Codebehind="TemplateDerivedCalculationFormPrint.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="AccountInfoLeftPane" Src="~/UserControls/AccountInfoLeftPane.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="AccountDescription" Src="~/UserControls/AccountDescription.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="RecFormButtons" Src="~/UserControls/RecForm/RecFormButtons.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="Legends" Src="~/UserControls/LegendOnReconciliationTemplate.ascx" %>
<%@ Register TagPrefix="userControl" TagName="DocumentUpload" Src="~/UserControls/DocumentUploadButton.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLAdjustments" Src="~/UserControls/RecForm/GLAdjustments.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ItemInputWriteOff" Src="~/UserControls/RecForm/ItemInputWriteOff.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="UnexplainedVariance" Src="~/UserControls/RecForm/UnexplainedVariance.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="EditQualityScore" Src="~/UserControls/QualityScore/EditQualityScore.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphRecForms" runat="Server">
    <asp:HiddenField ID="hdReviewCount" runat="server" />
     <table width="100%" cellpadding="0" cellspacing="0">
        <%--GL Balance Row--%>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                    <col width="54%" />
                    <col width="23%" />
                    <col width="23%" />
                    <tr class="RecFormHeaderRow">
                        <td class="RecFormFirstCol">
                            <webControls:ExLabel ID="lblGLBalance" runat="server" LabelID="1382" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                            <webControls:ExLabel ID="lblPeriodEndDate" runat="server" FormatString="({0})" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <asp:HiddenField ID="hdnGLBalanceBC" runat="server" />
                            <webControls:ExLabel ID="lblGLBalanceBC" runat="server" SkinID="WhiteBold11Arial"
                                ToolTip="Base Currency"></webControls:ExLabel>
                        </td>
                        <td class="RCCYCol">
                            <asp:HiddenField ID="hdnGLBalanceRC" runat="server" />
                            <webControls:ExLabel ID="lblGLBalanceRC" runat="server" SkinID="WhiteBold11Arial"
                                ToolTip="Reporting Currency"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <%--Reconciliation Row--%>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                    <col width="2%" />
                    <col width="48%" />
                    <col width="20%" />
                    <col width="20%" />
                    <tr class="RecFormHeaderRow">
                        <td colspan="2" class="RecFormFirstCol">
                            <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="1874" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="1493" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                            <%--to be fetched from database at runtime--%>
                        </td>
                        <td class="RCCYCol">
                            <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="1424" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                            <%--to be fetched from database at runtime--%>
                        </td>
                    </tr>
                    <%--GL Adjustment Grid Row--%>
                    <tr>
                        <td colspan="4" class="htmlGridTitleDiv">
                            <webControls:ExLabel ID="lblGLAdjustments" runat="server" LabelID="1080" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <%------------ ######### ---%>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel runat="server" ID="ExLabel5" LabelID="1656" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <webControls:ExLabel runat="server" ID="lblTotalGLAdjustmentsBC" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="RCCYCol">
                            <webControls:ExLabel runat="server" ID="lblTotalGLAdjustmentsRC" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <UserControls:GLAdjustments ID="uctlGLAdjustments"  DisableExportInPrint="true" IsPrintMode="true" runat="server"
                                Visible="false" />
                        </td>
                    </tr>
                    <%--Timming Difference Grid--%>
                    <tr>
                        <td colspan="4" class="htmlGridTitleDiv">
                            <webControls:ExLabel ID="lblTimmingDifference" runat="server" LabelID="1081" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel runat="server" ID="ExLabel1" LabelID="1656" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <webControls:ExLabel runat="server" ID="lblTotalTimingDifferenceBC" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="RCCYCol">
                            <webControls:ExLabel runat="server" ID="lblTotalTimingDifferenceRC" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <UserControls:GLAdjustments ID="uctlTimmingDifference"  DisableExportInPrint="true" IsPrintMode="true" runat="server"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td>
                        </td>
                    </tr>
                    <%--Reconciled Balance Row--%>
                    <tr class="RecBalanceRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel ID="lblRecBalace" runat="server" LabelID="1385" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <webControls:ExLabel ID="lblReconciledBalanceBC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="RCCYCol">
                            <webControls:ExLabel ID="lblReconciledBalanceRC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td>
                        </td>
                    </tr>
                    <%--Derived Calculation Row--%>
                    <tr>
                        <td colspan="4" class="htmlGridTitleDiv">
                            <webControls:ExLabel ID="lblBankDetails" runat="server" LabelID="1084" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel ID="lblDerivedCalculation" runat="server" LabelID="1436 " SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <asp:HiddenField ID="hdnGlAdjustmentAndTimingDiffBC" runat="server" />
                            <asp:TextBox ID="txtBankBalanceBC" runat="server" Width="100" />
                            <asp:CustomValidator ID="cvBankBalanceBC" ControlToValidate="txtBankBalanceBC" runat="server"
                                SkiID="CustomValidator" ClientValidationFunction="ValidateBankBalanceBC"></asp:CustomValidator>
                        </td>
                        <td class="RCCYCol">
                            <asp:HiddenField ID="hdnGlAdjustmentAndTimingDiffRC" runat="server" />
                            <asp:TextBox ID="txtBankBalanceRC" runat="server" Width="100" />
                            <asp:CustomValidator ID="cvBankBalanceRC" runat="server" ControlToValidate="txtBankBalanceRC"
                                SkiID="CustomValidator" ClientValidationFunction="ValidateBankBalanceRC"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel ID="lblBasisForCalculation" runat="server" LabelID="1437" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td colspan="2" class="RCCYCol">
                            <webControls:ExTextBox ID="txtBasisForCalculationExplanation" runat="server" SkinID="ExMultilineTextBoxRecForm"
                                MaxLength="500" />
                            <asp:HiddenField ID="hdnBasisForCalculationExplanation" runat="server" />
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel runat="server" ID="ExLabel6" LabelID="1691" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <webControls:ExLabel runat="server" ID="lblTotalSupportingDetailBC" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="RCCYCol">
                            <webControls:ExLabel runat="server" ID="lblTotalSupportingDetailRC" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <UserControls:GLAdjustments ID="uctlGLAdjustmentsOther"  DisableExportInPrint="true" IsPrintMode="true" runat="server"
                                Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <%--Variance / Write-Off Row--%>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                    <col width="2%" />
                    <col width="48%" />
                    <col width="20%" />
                    <col width="20%" />
                    <tr class="RecFormHeaderRow">
                        <td colspan="4" class="RecFormFirstCol">
                            <webControls:ExLabel ID="lblVariancewriteOff" runat="server" LabelID="1388" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel ID="lblRecWriteOff" runat="server" LabelID="1389" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <webControls:ExLabel ID="lblTotalRecWriteOffBC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="RCCYCol">
                            <webControls:ExLabel ID="lblTotalRecWriteOffRC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <UserControls:ItemInputWriteOff ID="uctlItemInputWriteOff"  DisableExportInPrint="true" IsPrintMode="true" runat="server"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel ID="lblUnexplainedVariance" runat="server" LabelID="1678" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="BCCYCol">
                            <webControls:ExLabel ID="lblTotalUnExplainedVarianceBC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td class="RCCYCol">
                            <webControls:ExLabel ID="lblTotalUnExplainedVarianceRC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <UserControls:UnexplainedVariance ID="uctlUnexplainedVariance"  DisableExportInPrint="true" IsPrintMode="true"
                                runat="server" Visible="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <%--Attached Document Row--%>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                    <col width="2%" />
                    <col width="98%" />
                    <tr class="RecFormHeaderRow">
                        <td colspan="2" class="RecFormFirstCol">
                            <webControls:ExLabel runat="server" ID="lblAttachedDocument" LabelID="1392" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel runat="server" ID="lblDocuments" LabelID="1393" SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel runat="server" ID="lblCountAttachedDocument" SkinID="ItemCount"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <%--Review Notes Row--%>
        <tr id="trReviewNotes" runat="server">
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                    <col width="2%" />
                    <col width="98%" />
                    <tr class="RecFormHeaderRow">
                        <td colspan="2" class="RecFormFirstCol">
                            <webControls:ExLabel runat="server" ID="lblReviewNotes" LabelID="1394" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                            <webControls:ExLabel runat="server" ID="lblReviewNotesHeading" LabelID="1394" SkinID="Black11Arial"></webControls:ExLabel>
                            <webControls:ExLabel runat="server" ID="lblCountReviewNotes" SkinID="ItemCount"></webControls:ExLabel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
         <%--Quality Score Row--%>
        <tr id="trQualityScore" runat="server">
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                    <col width="2%" />
                    <col width="58%" />
                    <col width="20%" />
                    <col width="20%" />
                    <tr class="RecFormHeaderRow">
                        <td colspan="4" class="RecFormFirstCol">
                            <webControls:ExLabel ID="lblQualityScoreTitle" runat="server" LabelID="2423" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblQualityScore" runat="server" LabelID="2441" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblSystemScore" LabelID="2442" FormatString="{0} : " runat="server"
                                SkinID="Black11Arial" />
                            <webControls:ExLabel ID="lblSystemScoreValue" runat="server" SkinID="Black11Arial" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblUserScore" LabelID="2443" FormatString="{0} : " runat="server"
                                SkinID="Black11Arial" />
                            <webControls:ExLabel ID="lblUserScoreValue" runat="server" SkinID="Black11Arial" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <%--Rec Control CheckList Row--%>
        <tr id="trRecControlCheckList" runat="server">
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                    <col width="2%" />
                    <col width="58%" />
                    <col width="20%" />
                    <col width="20%" />
                    <tr class="RecFormHeaderRow">
                        <td colspan="4" class="RecFormFirstCol">
                            <webControls:ExLabel ID="lblRecControlCheckListTitle" runat="server" LabelID="2827" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>&nbsp;
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblRecControlStatus" runat="server" LabelID="1338" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblRecControlTotal" LabelID="1656" FormatString="{0} : " runat="server"
                                SkinID="Black11Arial" />
                            <webControls:ExLabel ID="lblRecControlTotalValue" runat="server" SkinID="Black11Arial" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblRecControlCompleted" LabelID="2559" FormatString="{0} : " runat="server"
                                SkinID="Black11Arial" />
                            <webControls:ExLabel ID="lblRecControlCompletedValue" runat="server" SkinID="Black11Arial" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <%--        Task Master--%>
        <tr id="trTaskMaster" runat="server">
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                    <col width="2%" />
                    <col width="58%" />
                    <col width="20%" />
                    <col width="20%" />
                    <tr class="RecFormHeaderRow">
                        <td colspan="4" class="RecFormFirstCol">
                            <webControls:ExLabel ID="lblTaskMasterTitle" runat="server" LabelID="2571" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="GLAdjustmentRow">
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblTaskMaster" runat="server" LabelID="2576" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblPendingTask" LabelID="2574" FormatString="{0} : " runat="server"
                                SkinID="Black11Arial" />
                            <webControls:ExLabel ID="lblPendingTaskValue" runat="server" SkinID="Black11Arial" />
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblCompleatedTask" LabelID="2575" FormatString="{0} : "
                                runat="server" SkinID="Black11Arial" />
                            <webControls:ExLabel ID="lblCompleatedTaskValue" runat="server" SkinID="Black11Arial" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
