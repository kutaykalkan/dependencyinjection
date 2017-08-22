<%@ Page Language="C#" MasterPageFile="~/MasterPages/RecProcessMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_TemplateAccruableItemsForm"
    Title="Untitled Page" Theme="SkyStemBlueBrown" ValidateRequest="false" Codebehind="TemplateAccruableItemsForm.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="AccountInfoLeftPane" Src="~/UserControls/AccountInfoLeftPane.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="AccountDescription" Src="~/UserControls/AccountDescription.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="RecFormButtons" Src="~/UserControls/RecForm/RecFormButtons.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="Legends" Src="~/UserControls/LegendOnReconciliationTemplate.ascx" %>
<%@ Register TagPrefix="userControl" TagName="DocumentUpload" Src="~/UserControls/DocumentUploadButton.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLAdjustments" Src="~/UserControls/RecForm/GLAdjustments.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ItemInputAccurable" Src="~/UserControls/RecForm/ItemInputAccurable.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ItemInputAccurableRecurring" Src="~/UserControls/RecForm/ItemInputAccurableRecurring.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ItemInputWriteOff" Src="~/UserControls/RecForm/ItemInputWriteOff.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="UnexplainedVariance" Src="~/UserControls/RecForm/UnexplainedVariance.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLVersion" Src="~/UserControls/MultiVersionGL/GLVersion.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="AccountMatching" Src="~/UserControls/Matching/AccountMatchingButton.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="EditQualityScore" Src="~/UserControls/QualityScore/EditQualityScore.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataWriteOnOff" Src="~/UserControls/RecForm/GLDataWriteOnOff.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataRecurringScheduleItems" Src="~/UserControls/RecForm/GLDataRecurringScheduleItems.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="RecFormAccountTaskGrid" Src="~/UserControls/TaskMaster/RecFormAccountTaskGrid.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="RecControlCheckList" Src="~/UserControls/RecControlCheckList/RecControlCheckList.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRecProcess" runat="Server">
      <asp:HiddenField ID="hdCompetedCount" runat="server" />
    <asp:HiddenField ID="hdReviewCount" runat="server" />
    <asp:HiddenField ID="hdcount" runat="server" />
    <asp:HiddenField ID="hdReviwed" runat="server" />
    <asp:Panel ID="pnlRecForm" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <telerik:RadSplitter ID="radSplitter1" runat="server" OnClientLoad="SplitterLoaded">
                        <telerik:RadPane ID="LeftPane" runat="server" Collapsed="true" Height="100%" SkinID="RADSplitterLeftPane"
                            Width="210">
                            <UserControls:AccountInfoLeftPane ID="ucAccountInfo" runat="server" />
                        </telerik:RadPane>
                        <telerik:RadSplitBar ID="Radsplitbar1" runat="server" CollapseMode="Forward">
                        </telerik:RadSplitBar>
                        <telerik:RadPane ID="MiddlePane1" CssClass="RadMiddlePane" runat="server"
                            Scrolling="Both" Height="100%">
                            <asp:UpdatePanel ID="upnlLeftPane" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <%--GL Balance Row--%>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                                                    <col width="50%" />
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <%--GL Balance Row--%>
                                                    <tr class="RecFormHeaderRow first">
                                                        <td class="RecFormFirstCol">
                                                            <webControls:ExLabel ID="lblGLBalance" runat="server" LabelID="1382" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                            <webControls:ExLabel ID="lblPeriodEndDate" runat="server" FormatString="({0})" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                            <webControls:ExImageButton ID="imgShowNetAccountComposition" SkinID="ShowNetAccountComposition"
                                                                runat="server" />
                                                            <UserControls:GLVersion ID="ucGLVersionButton" runat="server" />
                                                            <UserControls:AccountMatching ID="ucAccountMatching" runat="server" />
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel ID="lblGLBalanceBC" runat="server" SkinID="WhiteBold11Arial"
                                                                ToolTip="Base Currency"></webControls:ExLabel>
                                                            <%--to be fetched from database at runtime--%>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel ID="lblGLBalanceRC" runat="server" SkinID="WhiteBold11Arial"
                                                                ToolTip="Reporting Currency"></webControls:ExLabel>
                                                            <%--to be fetched from database at runtime--%>
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
                                                    <col width="4%" />
                                                    <col width="3%" />
                                                    <col width="43%" />
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <tr class="RecFormHeaderRow">
                                                        <td colspan="3" class="RecFormFirstCol">
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
                                                        <td colspan="5" class="htmlGridTitleDiv">
                                                            <webControls:ExLabel ID="lblGLAdjustments" runat="server" LabelID="1080" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td class="RecFormFirstCol" align="left">
                                                            <webControls:ExHyperLink runat="server" ID="hlImportGLAdjustment" SkinID="ImportItemHyperLink" />
                                                        </td>
                                                        <td align="left">
                                                            <webControls:ExImageButton runat="server" ID="imgViewGLAdjustment" EnableViewState="true"
                                                                SkinID="ViewItemGridCollapsableImageButton" />
                                                            <%--<webControls:ExHyperLink runat="server" ID="hlGLAdjustment" SkinID="ViewItemGridHyperLink" />--%>
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel runat="server" ID="lblHeaderTotal" LabelID="1656" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblTotalGLAdjustmentsBC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblTotalGLAdjustmentsRC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <%-- User control to show GLAdjustment in collapsable panel--%>
                                                    <tr>
                                                        <td colspan="5">
                                                            <%--            <UserControls:GLAdjustments ID="uctlGLAdjustments" IsPrintMode="false" runat="server"
                                                                Visible="false" />
                                                                <br />--%>
                                                            <UserControls:GLAdjustments ID="uctlGLAdjustments_new" EntityNameLabelID="1656" IsPrintMode="false"
                                                               AutoSaveAttributeID ="AccruableFormAdjustmentsTotal" runat="server" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <%--Timming Difference Grid--%>
                                                    <tr>
                                                        <td colspan="5" class="htmlGridTitleDiv">
                                                            <webControls:ExLabel ID="lblTimmingDifference" runat="server" LabelID="1081" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td class="RecFormFirstCol">
                                                            <webControls:ExHyperLink runat="server" ID="hlImportTimingDifference" SkinID="ImportItemHyperLink" />
                                                        </td>
                                                        <td>
                                                            <webControls:ExImageButton runat="server" ID="imgViewTimingDifference" SkinID="ViewItemGridCollapsableImageButton" />
                                                            <%--<webControls:ExHyperLink runat="server" ID="hlTimingDifference" SkinID="ViewItemGridHyperLink" />--%>
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel runat="server" ID="ExLabel1" LabelID="1656" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblTotalTimingDifferenceBC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblTotalTimingDifferenceRC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <%-- User control to show Timing Diff in collapsable panel--%>
                                                    <tr>
                                                        <td colspan="5">
                                                            <%--   <UserControls:GLAdjustments ID="uctlTimmingDifference" IsPrintMode="false" runat="server"
                                                                Visible="false" />--%>
                                                            <UserControls:GLAdjustments ID="uctlTimmingDifference_new" EntityNameLabelID="1656"
                                                               AutoSaveAttributeID ="AccruableFormTimingDifferenceTotal" IsPrintMode="false" runat="server" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <%--Reconciled Balance Row--%>
                                                    <tr class="RecBalanceRow">
                                                        <td class="RecFormFirstCol" colspan="3">
                                                            <webControls:ExLabel ID="lblRecBalace" runat="server" LabelID="1385" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel ID="lblReconciledBalanceBC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                                            <%--to be fetched from database at runtime--%>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel ID="lblReconciledBalanceRC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                                            <%--to be fetched from database at runtime--%>
                                                        </td>
                                                    </tr>
                                                    <tr class="BlankRow">
                                                        <td colspan="5">
                                                        </td>
                                                    </tr>
                                                    <%--Derived Calculation Row--%>
                                                    <tr>
                                                        <td colspan="5" class="htmlGridTitleDiv">
                                                            <webControls:ExLabel ID="lblDerivedCalculation" runat="server" LabelID="1084  " SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td class="RecFormFirstCol">
                                                            <webControls:ExHyperLink runat="server" ID="hlImportIndividualAccrualDetail" SkinID="ImportItemHyperLink" />
                                                        </td>
                                                        <td>
                                                            <%-- <webControls:ExHyperLink runat="server" ID="hlIndividualAccrualDetail" SkinID="ViewItemGridHyperLink" />--%>
                                                            <webControls:ExImageButton runat="server" ID="imgViewIndividualAccrualDetail" SkinID="ViewItemGridCollapsableImageButton" />
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel runat="server" ID="lblIndividualAccrualDetail" LabelID="1445 "
                                                                SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblIndividualAccrualDetailBC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblIndividualAccrualDetailRC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <%-- User control to show ItemInputAccurable in collapsable panel--%>
                                                    <tr>
                                                        <td colspan="5">
                                                            <%--      <UserControls:ItemInputAccurable ID="uctlItemInputAccurable" IsPrintMode="false"
                                                                runat="server" Visible="false" />--%>
                                                            <UserControls:GLAdjustments ID="uctlItemInputAccurable" ShowCopyButton="true" EntityNameLabelID="1445"
                                                               AutoSaveAttributeID ="AccruableFormSupportingDetailIndividual" IsPrintMode="false" runat="server" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td class="RecFormFirstCol">
                                                            <webControls:ExHyperLink runat="server" ID="hlImportRecurringAccrualSchedule" SkinID="ImportItemHyperLink" />
                                                        </td>
                                                        <td>
                                                            <%--<webControls:ExHyperLink runat="server" ID="hlRecurringAccrualSchedule" SkinID="ViewItemGridHyperLink" />--%>
                                                            <webControls:ExImageButton runat="server" ID="imgViewRecurringAccrualSchedule" SkinID="ViewItemGridCollapsableImageButton" />
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel runat="server" ID="lblRecurringAccrualSchedule" LabelID="1446 "
                                                                SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblRecurringAccrualScheduleBC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblRecurringAccrualScheduleRC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <%-- <UserControls:ItemInputAccurableRecurring ID="uctlItemInputAccurableRecurring" IsPrintMode="false"
                                                                runat="server" Visible="false" />
                                                            <br />--%>
                                                            <UserControls:GLDataRecurringScheduleItems ID="uctlGLDataRecurringScheduleItems"
                                                                AutoSaveAttributeID ="AccruableFormSupportingDetailRecurring" EntityNameLabelID="1446" IsPrintMode="false" runat="server" Visible="false" ShowCopyButton="true" />
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRowTotalSupportingDetail">
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel runat="server" ID="ExLabel12" LabelID="1656" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblTotalSupportingDetailBC" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel runat="server" ID="lblTotalSupportingDetailRC" SkinID="Black11Arial"></webControls:ExLabel>
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
                                                    <col width="4%" />
                                                    <col width="3%" />
                                                    <col width="43%" />
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <tr class="RecFormHeaderRow">
                                                        <td colspan="5" class="RecFormFirstCol">
                                                            <webControls:ExLabel ID="lblVariancewriteOff" runat="server" LabelID="1388" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <%-- <webControls:ExHyperLink runat="server" ID="hlRecWriteOff" SkinID="ViewItemGridHyperLink" />--%>
                                                            <webControls:ExImageButton runat="server" ID="imgRecWriteOff" SkinID="ViewItemGridCollapsableImageButton" />
                                                        </td>
                                                        <td>
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
                                                        <td colspan="5">
                                                            <%-- <UserControls:ItemInputWriteOff ID="uctlItemInputWriteOff" IsPrintMode="false" runat="server"
                                                                Visible="false" />
                                                                <br />--%>
                                                            <UserControls:GLDataWriteOnOff ID="uctlGLDataWriteOnOff" IsPrintMode="false" runat="server"
                                                               AutoSaveAttributeID ="AccruableFormReconciliationWriteOffsOns" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <%-- <webControls:ExHyperLink runat="server" ID="hlUnexplainedVariance" SkinID="ViewItemGridHyperLink" />--%>
                                                            <webControls:ExImageButton runat="server" ID="imgUnexplainedVariance" SkinID="ViewItemGridCollapsableImageButton" />
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel ID="lblUnexplainedVariance" runat="server" LabelID="1678" SkinID="Black11Arial"></webControls:ExLabel>
                                                            <webControls:ExHyperLink runat="server" ID="hlUnexplainedVarianceHistory" SkinID="HistoryHyperlink" />
                                                        </td>
                                                        <td class="BCCYCol">
                                                            <webControls:ExLabel ID="lblTotalUnExplainedVarianceBC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td class="RCCYCol">
                                                            <webControls:ExLabel ID="lblTotalUnExplainedVarianceRC" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <UserControls:UnexplainedVariance ID="uctlUnexplainedVariance" IsPrintMode="false"
                                                               AutoSaveAttributeID ="AccruableFormUnexpVar" runat="server" Visible="false" />
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
                                                    <col width="4%" />
                                                    <col width="3%" />
                                                    <col width="93%" />
                                                    <tr class="RecFormHeaderRow">
                                                        <td colspan="3" class="RecFormFirstCol">
                                                            <webControls:ExLabel runat="server" ID="lblAttachedDocument" LabelID="1392" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="center">
                                                            <webControls:ExHyperLink ID="hlDocument" runat="server" SkinID="ShowDocumentPopupHyperLink"
                                                                LabelID="1540" />
                                                        </td>
                                                        <td>
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
                                                    <col width="4%" />
                                                    <col width="3%" />
                                                    <col width="93%" />
                                                    <tr class="RecFormHeaderRow">
                                                        <td colspan="5" class="RecFormFirstCol">
                                                            <webControls:ExLabel runat="server" ID="lblReviewNotes" LabelID="1394" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td align="center">
                                                            <webControls:ExHyperLink runat="server" ID="hlReviewNotes" SkinID="ShowCommentPopupHyperLink"
                                                                LabelID="1394" />
                                                        </td>
                                                        <td>
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
                                                    <col width="4%" />
                                                    <col width="3%" />
                                                    <col width="43%" />
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <tr class="RecFormHeaderRow">
                                                        <td colspan="5" class="RecFormFirstCol">
                                                            <webControls:ExLabel ID="lblQualityScoreTitle" runat="server" LabelID="2423" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <webControls:ExImageButton runat="server" ID="imgQualityScore" SkinID="ViewItemGridCollapsableImageButton" />
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
                                                    <tr>
                                                        <td colspan="5">
                                                            <UserControls:EditQualityScore ID="ucEditQualityScore" IsPrintMode="false" runat="server"
                                                               AutoSaveAttributeID ="AccruableFormQualityScore" OnQualityScoreChanged="ucEditQualityScore_OnQualityScoreChanged" Visible="false" />
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
                                                    <col width="4%" />
                                                    <col width="3%" />
                                                    <col width="43%" />
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <tr class="RecFormHeaderRow">
                                                        <td colspan="5" class="RecFormFirstCol">
                                                            <webControls:ExLabel ID="lblRecControlCheckListTitle" runat="server" LabelID="2827" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td class="RecFormFirstCol">
                                                            <webControls:ExHyperLink runat="server" ID="hlImportRecControlCheckList" SkinID="ImportItemHyperLink" />
                                                        </td>
                                                        <td>
                                                            <webControls:ExImageButton runat="server" ID="imgRecControlCheckList" SkinID="ViewItemGridCollapsableImageButton" />
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
                                                    <tr>
                                                        <td colspan="5">
                                                            <UserControls:RecControlCheckList ID="ucRecControlCheckList" IsPrintMode="false" runat="server" OnRecControlListChanged="ucRecControlCheckList_OnRecControlListChanged"
                                                               AutoSaveAttributeID ="AccruableFormRCCStatus" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td></td>
                                        </tr>
                                        <%--Button Row--%>
                                        <tr>
                                            <td align="right">
                                                <UserControls:RecFormButtons ID="ucRecFormButtons" runat="server" />
                                            </td>
                                        </tr>
                                        <tr class="BlankRow">
                                            <td>
                                            </td>
                                        </tr>
                                        <%--Account Task Grid--%>
                                        <tr id="trAccountTask" runat="server">
                                            <td>
                                                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0" class="tanBorder">
                                                    <col width="4%" />
                                                    <col width="3%" />
                                                    <col width="43%" />
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <tr class="RecFormHeaderRow">
                                                        <td colspan="5" class="RecFormFirstCol">
                                                            <webControls:ExLabel ID="ExLabel5" runat="server" LabelID="2571" SkinID="WhiteBold11Arial"></webControls:ExLabel>
                                                        </td>
                                                    </tr>
                                                    <tr class="GLAdjustmentRow">
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <webControls:ExImageButton runat="server" ID="imgAccountTask" SkinID="ViewItemGridCollapsableImageButton" />
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel ID="ExLabel6" runat="server" LabelID="2576" SkinID="Black11Arial"></webControls:ExLabel>
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel ID="ExLabel7" LabelID="2574" FormatString="{0} : " runat="server"
                                                                SkinID="Black11Arial" />
                                                            <webControls:ExLabel ID="lblPendingTaskStatus" runat="server" SkinID="Black11Arial" />
                                                        </td>
                                                        <td>
                                                            <webControls:ExLabel ID="ExLabel9" LabelID="2575" FormatString="{0} : " runat="server"
                                                                SkinID="Black11Arial" />
                                                            <webControls:ExLabel ID="lblCompletedTaskStatus" runat="server" SkinID="Black11Arial" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <UserControls:RecFormAccountTaskGrid ID="ucRecFormAccountTaskGrid" IsPrintMode="false"
                                                               AutoSaveAttributeID ="AccruableFormTaskStatus" runat="server" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true"
                                                    Visible="true" AssociatedUpdatePanelID="upnlLeftPane" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </telerik:RadPane>
                    </telerik:RadSplitter>
                </td>
            </tr>
            <%--Web Links Row--%>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr class="HideInPdf">
                <td align="center">
                    <webControls:ExHyperLink ID="hlAuditTrail" runat="server" LabelID="1380" NavigateUrl="~/Pages/AuditTrail.aspx"></webControls:ExHyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <UserControls:Legends ID="ucLegends" runat="server" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdStateOfRecItemControl" runat="server" />

        <script language="javascript" type="text/javascript">
            function CalledFn(obj) {
                var label = document.getElementById('<% =this.lblCountAttachedDocument.ClientID %>');

                if (label != null)
                    label.innerHTML = '(' + obj + ')';

            }

            function SplitterLoaded(splitter, arg) {
                var pane = splitter.getPaneById('<%= MiddlePane1.ClientID %>');
                var height = pane.getContentElement().scrollHeight;
                splitter.set_height(height);
                pane.set_height(height);
            }

            function ConfirmUnExplainedVariance(msg) {
                /* Commented by Vinay as there are no validators present in page
                Page_ClientValidate();
                if (!Page_IsValid)
                return false;
                */
                var answer = confirm(msg);
                if (answer) {
                    return false;
                }
                else {
                    return true;
                }
            }

            function ucEditQualityScore_OnQualityScoreChanged(sender, args) {
                var lblSystemScoreValue = $get('<%=lblSystemScoreValue.ClientID%>');
                var lblUserScoreValue = $get('<%=lblUserScoreValue.ClientID%>');
                lblSystemScoreValue.firstChild.data = args.SystemScore;
                lblUserScoreValue.firstChild.data = args.UserScore;
            }
            function ucRecControlCheckList_OnRecControlListChanged(sender, args) {
                var lblRecControlCompletedValue = $get('<%=lblRecControlCompletedValue.ClientID%>');
                var lblRecControlReviewedValue = document.getElementById('<%= hdReviewCount.ClientID %>');
                var lblhdCompletedCount = document.getElementById('<%= hdCompetedCount.ClientID %>');
                var lblCount = document.getElementById('<%= hdcount.ClientID %>');
                var lblReviewedCount = document.getElementById('<%= hdReviwed.ClientID %>');
                if (args.CompletedCount != "0" || args.CompletedCount == "0") {
                    if (lblhdCompletedCount.value >= 10 || lblRecControlCompletedValue.firstChild.data >= 10) {
                        GetRecChanges(sender, args.selectedvalue);
                        if (lblCount.value != args.CompletedCount) {

                            if (lblCount.value > args.CompletedCount) {
                                var valint = lblRecControlCompletedValue.firstChild.data;
                                lblRecControlCompletedValue.firstChild.data = --valint;
                                lblCount.value = args.CompletedCount;
                                lblhdCompletedCount.value = lblRecControlCompletedValue.firstChild.data;
                            }
                            else {
                                var valint = lblRecControlCompletedValue.firstChild.data;
                                lblRecControlCompletedValue.firstChild.data = ++valint;
                                lblCount.value = args.CompletedCount;
                                lblhdCompletedCount.value = lblRecControlCompletedValue.firstChild.data;

                            }

                        }
                    }
                    else {
                        lblRecControlCompletedValue.firstChild.data = args.CompletedCount;
                        lblhdCompletedCount.value = args.CompletedCount;
                    }

                }

            }

            function GetRecChanges(grid, userResponseColumnUniqueKey) {
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                var cell;
                var obj = new Object();
                obj.CompletedCount = 0;
                obj.ReviwedCount = 0;
                obj.selectedvalue = 0;
                var SystemQualityScoreStatusID = 0;
                var UserQualityScoreStatusID = 0;
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    cell = masterTable.getCellByColumnUniqueName(row, userResponseColumnUniqueKey);
                    if (cell.children[0] != null && cell.children[0] != 'undefined') {
                        for (var j = 0; j < cell.children[0].length; j++) {
                            if (cell.children[0][j].selected) {
                                UserQualityScoreStatusID = cell.children[0][j].value;
                                if (UserQualityScoreStatusID == "1252" || UserQualityScoreStatusID == "2858") {
                                    obj.CompletedCount++;
                                }
                            }
                        }
                    }
                }
                return obj;
            }

            function GetRecChangeRevieweds(grid, userResponseColumnUniqueKey) {
                var masterTable = grid.get_masterTableView();
                var rows = masterTable.get_dataItems();
                var cell;
                var obj = new Object();
                obj.CompletedCount = 0;
                obj.ReviwedCount = 0;
                obj.selectedvalue = 0;
                var UserQualityScoreStatusID = 0;
                for (var i = 0; i < rows.length; i++) {
                    var row = rows[i];
                    cell = masterTable.getCellByColumnUniqueName(row, userResponseColumnUniqueKey);
                    if (cell.children[0] != null && cell.children[0] != 'undefined') {
                        for (var j = 0; j < cell.children[0].length; j++) {
                            if (cell.children[0][j].selected) {
                                UserQualityScoreStatusID = cell.children[0][j].value;
                                if (UserQualityScoreStatusID == "1252" || UserQualityScoreStatusID == "2858") {
                                    obj.ReviwedCount++;
                                }
                            }
                        }
                    }
                }
                return obj;
            }

            function ucRecControlCheckList_OnRecControlListChangedReviwed(sender, args) {
                var lblRecControlCompletedValue = $get('<%=lblRecControlCompletedValue.ClientID%>');
                var lblRecControlReviewedValue = document.getElementById('<%= hdReviewCount.ClientID %>');
                var lblhdCompletedCount = document.getElementById('<%= hdCompetedCount.ClientID %>');
                var lblCount = document.getElementById('<%= hdcount.ClientID %>');
                var lblReviewedCount = document.getElementById('<%= hdReviwed.ClientID %>');
                if (args.ReviwedCount != "0" || args.ReviwedCount == "0")
                    if (lblReviewedCount.value >= 10 || lblRecControlReviewedValue.value >= 10) {
                        GetRecChangeRevieweds(sender, args.selectedvalue);
                        if (lblCount.value != args.ReviwedCount) {

                            if (lblCount.value > args.ReviwedCount) {
                                var valint = lblReviewedCount.value;
                                lblRecControlReviewedValue.value = --valint;
                                lblCount.value = args.ReviwedCount;
                                lblReviewedCount.value = lblRecControlReviewedValue.value;
                            }
                            else {
                                var valint = lblReviewedCount.value;
                                lblRecControlReviewedValue.value = ++valint;
                                lblCount.value = args.ReviwedCount;
                                lblReviewedCount.value = lblRecControlReviewedValue.value;
                            }

                        }
                    }
                    else {
                        lblRecControlReviewedValue.value = args.ReviwedCount;
                        lblReviewedCount.value = lblRecControlReviewedValue.value;
                    }
            }

        </script>

    </asp:Panel>
</asp:Content>
