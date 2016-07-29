<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Inherits="Pages_CapabilityActivation"
    Theme="SkyStemBlueBrown" Codebehind="CapabilityActivation.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="CapabilityRiskRatingAll" Src="~/UserControls/Capability/CapabilityRiskRatingAll.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityMateriality" Src="~/UserControls/Capability/CapabilityActivationMateriality.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityActivationDualLevelReview" Src="~/UserControls/Capability/CapabilityActivationDualLevelReview.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityDueDateByAccount" Src="~/UserControls/Capability/CapabilityDueDateByAccount.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityMandatoryReportSignoff"
    Src="~/UserControls/Capability/CapabilityMandatoryReportSignoff.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityCertificationActivation"
    Src="~/UserControls/Capability/CapabilityCertificationActivation.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityRCCL" Src="~/UserControls/Capability/CapabilityActivationRCCL.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="CapabilityMultiCurrency" Src="~/UserControls/Capability/CapabilityMultiCurrency.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- <table width="100%" style="margin-left:60px">--%>
            <%--style="padding-left:50px; padding-right:50px"--%>
            <asp:Panel ID="pnlPage" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <col width="2%" />
                    <col width="35%" />
                    <col width="10%" />
                    <col width="15%" />
                    <col width="38%" />
                    <asp:Panel ID="pnlMultiCurrency" runat="server">
                        <tr id="trMultiCurrency" runat="server">
                            <td colspan="5">
                                <UserControls:CapabilityMultiCurrency ID="ucCapabilityMultiCurrency" runat="server" />
                            </td>
                        </tr>
<%--                        <tr id="trMultiCurrency" runat="server">
                            <td class="ManadatoryField">*
                            </td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblMultiCurrency" runat="server" LabelID="1018" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optMultiCurrencyYes" runat="server" GroupName="optMultiCurrency"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td class="tdCapabilityStatusIcon">
                                <webControls:ExRadioButton ID="optMultiCurrencyNo" runat="server" GroupName="optMultiCurrency"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                                <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateIsMultiCurrencySelected"
                                    ID="cvMultiCurrency">!</webControls:ExCustomValidator>
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusMultiCurrencyForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusMultiCurrencyForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>--%>
                        <tr class="BlankRow" id="trMultiCurrencyBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
<%--                    <asp:Panel ID="pnlReopenRecOnCCYreload" runat="server">
                        <tr id="trReopenRecOnCCYreload" runat="server">
                            <td class="ManadatoryField">*
                            </td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblReopenRecOnCCYreload" runat="server" LabelID="2988" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optReopenRecOnCCYreloadYes" runat="server" GroupName="optReopenRecOnCCYreload"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td class="tdCapabilityStatusIcon">
                                <webControls:ExRadioButton ID="optReopenRecOnCCYreloadNo" runat="server" GroupName="optReopenRecOnCCYreload"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                                <webControls:ExCustomValidator runat="server" ClientValidationFunction="validateIsReopenRecOnCCYreloadSelected"
                                    ID="cvReopenRecOnCCYreload">!</webControls:ExCustomValidator>
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusReopenRecOnCCYreloadForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusReopenRecOnCCYreloadForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trReopenRecOnCCYreloadBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>--%>
                    <%--<asp:Panel ID="pnlDualReview" runat="server">
                        <tr id="trDualReview" runat="server">
                            <td class="ManadatoryField">
                            </td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblDualReview" runat="server" LabelID="1015" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optDualReviewYes" runat="server" GroupName="optDualReview"
                                    LabelID="1252" SkinID="OptBlack11Arial" AutoPostBack="true" OnCheckedChanged="optDualReviewYes_CheckedChanged" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optDualReviewNo" runat="server" GroupName="optDualReview"
                                    LabelID="1251" SkinID="OptBlack11Arial" AutoPostBack="true" OnCheckedChanged="optDualReviewYes_CheckedChanged" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusDualReviewForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusDualReviewForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trDualReviewBlankRow" runat="server">
                            <td colspan="5">
                            </td>
                        </tr>
                    </asp:Panel>--%>
                    <asp:Panel ID="pnlDualReview" runat="server">
                        <tr id="trDualReview" runat="server">
                            <td colspan="5">
                                <UserControls:CapabilityActivationDualLevelReview ID="ucCapabilityActivationDualLevelReview" runat="server" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trDualReviewBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlDueDateByAccount" runat="server">
                        <tr id="trDueDateByAccount" runat="server">
                            <td colspan="5">
                                <UserControls:CapabilityDueDateByAccount ID="ucCapabilityDueDateByAccount" runat="server" />
                            </td>
                        </tr>
                        <%--<tr id="trDueDateByAccount" runat="server">
                            <td class="ManadatoryField"></td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblDueDateByAccount" runat="server" LabelID="2756 " FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="OptDueDateByAccountYes" runat="server" GroupName="OptDueDateByAccount"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="OptDueDateByAccountNo" runat="server" GroupName="OptDueDateByAccount"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusDueDateByAccountYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusDueDateByAccountNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>--%>
                        <tr class="BlankRow" id="trDueDateByAccountBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlMateriality" runat="server">
                        <tr>
                            <td colspan="5">
                                <UserControls:CapabilityMateriality ID="ucCapabilityMateriality" runat="server" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlRiskRating" runat="server">
                        <tr>
                            <td colspan="5">
                                <UserControls:CapabilityRiskRatingAll ID="ucCapabilityRiskRatingAll" runat="server" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlMendatoryReportSignOff" runat="server">
                        <tr id="trMandatoryReportSignoff" runat="server">
                            <td colspan="5">
                                <UserControls:CapabilityMandatoryReportSignoff ID="ucCapabilityMandatoryReportSignoff"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trMandatoryReportSignoffBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlKeyAccount" runat="server">
                        <tr id="trKeyAccount" runat="server">
                            <td class="ManadatoryField"></td>
                            <td class="tdCapabilityYesOpt">
                                <webControls:ExLabel ID="lblKeyAccount" runat="server" LabelID="1014 " FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td class="tdCapabilityNoOpt">
                                <webControls:ExRadioButton ID="optKeyAccountYes" runat="server" GroupName="optKeyAccount"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optKeyAccountNo" runat="server" GroupName="optKeyAccount"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusKeyAccountForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusKeyAccountForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trKeyAccountBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlNetAccount" runat="server">
                        <tr id="trNetAccount" runat="server">
                            <td class="ManadatoryField"></td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblNetAccount" runat="server" LabelID="1257 " FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optNetAccountYes" runat="server" GroupName="optNetAccount"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optNetAccountNo" runat="server" GroupName="optNetAccount"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusNetAccountForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusNetAccountForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlZBA" runat="server">
                        <tr id="trZeroBalanceAccount" runat="server">
                            <td class="ManadatoryField"></td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblZeroBalanceAccount" runat="server" LabelID="1256 " FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optZeroBalanceAccountYes" runat="server" GroupName="optZeroBalanceAccount"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optZeroBalanceAccountNo" runat="server" GroupName="optZeroBalanceAccount"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusZeroBalanceAccountForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusZeroBalanceAccountForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlCertification" runat="server">
                        <tr id="trCertActication" runat="server">
                            <td class="ManadatoryField"></td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblCertificationActivation" runat="server" LabelID="1019"
                                    FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optCertificationActivationYes" runat="server" GroupName="optCertificationActivation"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optCertificationActivationNo" runat="server" GroupName="optCertificationActivation"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusCertificationActivationForwardYes" runat="server"
                                    SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusCertificationActivationForwardNo" runat="server"
                                    SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trCertActicationBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlCEOCert" runat="server">
                        <tr id="trCEOCert" runat="server">
                            <td class="ManadatoryField"></td>
                            <td>
                                <webControls:ExLabel ID="lblCeoCfoCertification" runat="server" LabelID="1021" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optCeoCfoCertificationYes" runat="server" GroupName="optCeoCfoCertification"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optCeoCfoCertificationNo" runat="server" GroupName="optCeoCfoCertification"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgStatusCeoCfoCertificationForwardYes" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgStatusCeoCfoCertificationForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr id="trRecomCertActivation" runat="server">
                            <td class="ManadatoryField"></td>
                            <td>(<webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1910" SkinID="Black9ArialItalic"></webControls:ExLabel>)
                            </td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr class="BlankRow" id="trCEOCertBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlReviewNotes" runat="server">
                        <tr id="trAllowDeletionOfReviewNotes" runat="server">
                            <td class="ManadatoryField"></td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblAllowDeletionOfReviewNotes" runat="server" LabelID="1351"
                                    FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td width="10%">
                                <webControls:ExRadioButton ID="optAllowDeletionOfReviewNotesYes" runat="server" GroupName="optAllowDeletionOfReviewNotes"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optAllowDeletionOfReviewNotesNo" runat="server" GroupName="optAllowDeletionOfReviewNotes"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgAllowDeletionOfReviewNotesForwardYes" runat="server"
                                    SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgAllowDeletionOfReviewNotesForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                        </tr>
                    </asp:Panel>
                    <%--                    <asp:Panel ID="pnlRecControlChecklist" runat="server">
                        <tr id="trRecControlChecklist" runat="server">
                            <td class="ManadatoryField">
                            </td>
                            <td width="28%">
                                <webControls:ExLabel ID="lblRecControlChecklist" runat="server" LabelID="2827"
                                    FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td width="10%">
                                <webControls:ExRadioButton ID="optRecControlChecklistYes" runat="server" GroupName="optRecControlChecklist"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optRecControlChecklistNo" runat="server" GroupName="optRecControlChecklist"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="imgRecControlChecklistForwardYes" runat="server"
                                    SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgRecControlChecklistForwardNo" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trRecControlChecklistBlankRow" runat="server">
                        </tr>
                    </asp:Panel>--%>
                    <asp:Panel ID="pnlRCCL" runat="server">
                        <tr id="trRCCL" runat="server">
                            <td colspan="5">
                                <UserControls:CapabilityRCCL ID="CapabilityActivationRCCL" runat="server" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trRCCLBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlUnexpVar" runat="server">
                        <tr id="trUnexpVarThreshold" runat="server">
                            <td class="ManadatoryField">*
                            </td>
                            <td width="28%">
                                <webControls:ExLabel runat="server" ID="lblUnexpVarThreshold" LabelID="1349" SkinID="Black11Arial"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtUnexpVarThreshold" SkinID="TextBox200" />
                                <asp:HiddenField ID="hdnUnexpVarThresholdVal" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvUnexpVarThreshold" runat="server" ControlToValidate="txtUnexpVarThreshold"
                                    Text="!" Font-Bold="true" Font-Size="Medium"></asp:RequiredFieldValidator>
                                <%--   <webControls:ExRegularExpressionValidator LabelID="2492" Text="!" ValidationExpression="^(\d{0,14}\.\d{0,4}|\d{0,14})$"
                                    ID="rgVariance" runat="server" ControlToValidate="txtUnexpVarThreshold" Font-Bold="true"
                                    Font-Size="Medium">
                                </webControls:ExRegularExpressionValidator>--%>
                                <asp:CustomValidator ID="cvUnexpVarThreshold" runat="server" Text="!" ControlToValidate="txtUnexpVarThreshold"
                                    Font-Bold="true" Font-Size="Medium" ClientValidationFunction="ValidateNumbers"></asp:CustomValidator>
                                <asp:CustomValidator runat="server" Font-Bold="true" Font-Size="Medium" Display="Dynamic" ClientValidationFunction="ValidateChanges" OnServerValidate="cvValidateChanges_ServerValidate"
                                    ID="cvValidateChanges"></asp:CustomValidator>

                            </td>
                            <td>
                                <webControls:ExImage ID="imgUnexplainedVarianceThresholdForwardYes" runat="server"
                                    SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="imgUnexplainedVarianceThresholdForwardNo" runat="server"
                                    SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlJE" runat="server">
                        <tr id="trJE" runat="server">
                            <td class="ManadatoryField"></td>
                            <td class="tdCapabilityYesOpt">
                                <webControls:ExLabel ID="lblJournalEntry" runat="server" LabelID="2113" FormatString="{0}:"
                                    SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td class="tdCapabilityNoOpt">
                                <webControls:ExRadioButton ID="optJEWriteOffOnApproverYes" runat="server" GroupName="optJE"
                                    LabelID="1252" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExRadioButton ID="optJEWriteOffOnApproverNo" runat="server" GroupName="optJE"
                                    LabelID="1251" SkinID="OptBlack11Arial" />
                            </td>
                            <td>
                                <webControls:ExImage ID="ImgJE1" runat="server" SkinID="CapabilityForwardedYes" />
                                <webControls:ExImage ID="ImgJE2" runat="server" SkinID="CapabilityForwardedNo" />
                            </td>
                        </tr>
                        <tr class="BlankRow" id="trJEBlankRow" runat="server">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlReportingCurrency" runat="server">
                        <tr id="trReportingCurrency" runat="server">
                            <td class="ManadatoryField"></td>
                            <td width="28%">
                                <webControls:ExLabel runat="server" ID="lblReportingCurrency" LabelID="1348" SkinID="Black11Arial"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                            <td colspan="2">
                                <webControls:ExLabel runat="server" ID="lblReportingCurrencyValue" SkinID="Black11Arial"
                                    FormatString="{0}:"></webControls:ExLabel>
                            </td>
                            <td></td>
                        </tr>
                        <tr class="BlankRow">
                            <td colspan="5"></td>
                        </tr>
                    </asp:Panel>
                    <tr class="BlankRow">
                        <td colspan="5"></td>
                    </tr>
                    <%--<tr>
                    <td colspan="4">
                    </td>
                    <td align="right">
                        <webControls:ExButton ID="btnSubmit" runat="server" LabelID="1315" OnClick="btnSubmit_Click"
                            SkinID="ExButton100" />
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                            SkinID="ExButton100" CausesValidation="false" />
                    </td>
                </tr>--%>
                    <tr>
                        <td colspan="5">
                            <UserControls:ProgressBar ID="ucProgressBarCapabilityActivation" runat="server" AssociatedUpdatePanelID="upnlMain" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <%-- <td colspan="4">
                    </td>--%>
                    <td align="right">
                        <webControls:ExButton ID="btnSubmit" runat="server" LabelID="1315" OnClick="btnSubmit_Click"
                            SkinID="ExButton100"  />
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                            SkinID="ExButton100" CausesValidation="false" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
            </table>

            <script type="text/javascript" language="javascript">
                function ValidateChanges(sender, args) {
                    HideValidationSummary();
                }
<%--                function validateIsMultiCurrencySelected(source, args) {
                    var groupValue = false;

                    var optMultiCurrencyYes = document.getElementById("<%=optMultiCurrencyYes.ClientID%>");
                    var optMultiCurrencyNo = document.getElementById("<%=optMultiCurrencyNo.ClientID%>");
                    if ((optMultiCurrencyYes != null) && (optMultiCurrencyNo != null)) {
                        if ((optMultiCurrencyYes.checked == false) && (optMultiCurrencyNo.checked == false)) {
                            args.IsValid = false;
                        }
                        else
                            args.IsValid = true;
                    }
                }
 --%>               function MakevalidateTrue(source, args) {
                    args.IsValid = true;
                }

                function validateAtLeastOneItemInCBL(source, args) {
                    var cbl = document.getElementById(source.ControlToValidateCBL);
                    if (cbl != null) {
                        if (!IsCBLAtLeastOneSelected(cbl)) {
                            args.IsValid = false;
                        }
                        else
                            args.IsValid = true;
                    }
                }

                function IsCBLAtLeastOneSelected(CBL) {
                    var isAnySelected = false;
                    var chkListBox = CBL.getElementsByTagName("input");
                    for (var y = 0; y < chkListBox.length; y++) {
                        if (chkListBox[y].checked) {
                            isAnySelected = true;
                        }
                    }
                    return isAnySelected;
                }

            </script>

            <%--<script type="text/javascript" language="javascript">
             
//             function validateCBLAtLeastOneSelected(source, args,CBL)
//             {
//              if (!validateCBLAtLeastOneSelected(CBL))
//                 {
//                    args.IsValid = false;
//                 }
//                 else
//                    args.IsValid = true;
             }
             
//             function validateCBL(source, args,ddlFrequency, cbPeriod)
//             {
//                         var isValid = true ;
//                         if(ddlFrequency.value == 4)
//                         {
//                             if (!validateCBLAtLeastOneSelected(cbPeriod))
//                             {
//                             isValid = false  ;
//                             }
//                         }
//                         else //some thing is selected in DDL except custom
//                         {
//                         }
//                         
//                         args.IsValid = isValid;
//             }
             function validateCBLAtLeastOneSelected(CBL)
             {
                             var isAnySelected = false;
                             var chkListBox= CBL.getElementsByTagName("input");
                             for (var y=0;y<chkListBox.length;y++)
                             {
                                if (chkListBox[y].checked)
                                {
                                isAnySelected=true ;
                                }
                             }
                             return isAnySelected;
             }
             
            function validateIsMultiCurrencySelected(source, args) 
            {
             var groupValue = false;
            
             var optMultiCurrencyYes = document.getElementById("<%=optMultiCurrencyYes.ClientID%>");
             var optMultiCurrencyNo = document.getElementById("<%=optMultiCurrencyNo.ClientID%>");
             if ((optMultiCurrencyYes != null) && (optMultiCurrencyNo != null)) 
             {
                 if ((optMultiCurrencyYes.checked == false) && (optMultiCurrencyNo.checked == false)) 
                 {
                    args.IsValid = false;
                 }
                 else
                    args.IsValid = true;
            }
           }
             
             function  CollapseOnCancelMandatoryReportSignoff(optMaterialityYesClientID,optRiskRatingYesClientID,optMandatoryReportSignoffYesClientID,optCertificationActivationYesClientID
             ,  optMaterialityNoClientID,optRiskRatingNoClientID,optMandatoryReportSignoffNoClientID,optCertificationActivationNoClientID
             ,  ddlMaterialitySelectionBasedOnClientID, txtCompanywideMaterialityThresholdClientID, rdFSCaptionwideMaterialityClientID 
             ,  ddlRiskRatingFrequencyClientID1, ddlRiskRatingFrequencyClientID2, ddlRiskRatingFrequencyClientID3, cbRiskRatingFrequencyClientID1, cbRiskRatingFrequencyClientID2, cbRiskRatingFrequencyClientID3
             , cblApproverMandatoryReportSignoffClientID, cblReviewerMandatoryReportSignoffClientID
             , rdCertificationActivationClientID
             )
             {
              var testControl = document.getElementById("<%=btnCancel.ClientID%>");
                 
              
                 var optMultiCurrencyYes = document.getElementById("<%=optMultiCurrencyYes.ClientID%>");
                 var optDualReviewYes = document.getElementById("<%=optDualReviewYes.ClientID%>");
                 var optKeyAccountYes = document.getElementById("<%=optKeyAccountYes.ClientID%>");
                 var optNetAccountYes = document.getElementById("<%=optZeroBalanceAccountYes.ClientID%>");
                 var optZeroBalanceAccountYes = document.getElementById("<%=optMultiCurrencyYes.ClientID%>");
                 var optCeoCfoCertificationYes = document.getElementById("<%=optCeoCfoCertificationYes.ClientID%>");
                 
                 var optMultiCurrencyNo = document.getElementById("<%=optMultiCurrencyNo.ClientID%>");
                 var optDualReviewNo = document.getElementById("<%=optDualReviewNo.ClientID%>");
                 var optKeyAccountNo = document.getElementById("<%=optKeyAccountNo.ClientID%>");
                 var optNetAccountNo = document.getElementById("<%=optZeroBalanceAccountNo.ClientID%>");
                 var optZeroBalanceAccountNo = document.getElementById("<%=optMultiCurrencyNo.ClientID%>");
                 var optCeoCfoCertificationNo = document.getElementById("<%=optCeoCfoCertificationNo.ClientID%>");
                 
                 //ChildControls from UserControls
                 var optMaterialityYes = document.getElementById(optMaterialityYesClientID);
                 var optRiskRatingYes = document.getElementById(optRiskRatingYesClientID);
                 var optMandatoryReportSignoffYes = document.getElementById(optMandatoryReportSignoffYesClientID);
                 var optCertificationActivationYes = document.getElementById(optCertificationActivationYesClientID);
                 
                 var optMaterialityNo = document.getElementById(optMaterialityNoClientID);
                 var optRiskRatingNo = document.getElementById(optRiskRatingNoClientID);
                 var optMandatoryReportSignoffNo = document.getElementById(optMandatoryReportSignoffNoClientID);
                 var optCertificationActivationNo = document.getElementById(optCertificationActivationNoClientID);
                 
                 var isValid= true ;
                 if (optMultiCurrencyYes.checked == false && optMultiCurrencyNo.checked == false )
                 isValid=false;
                 if (optDualReviewYes.checked == false && optDualReviewNo.checked == false)
                 isValid=false;
                 if (optKeyAccountYes.checked == false && optKeyAccountNo.checked == false)
                 isValid=false;
                 if (optNetAccountYes.checked == false && optNetAccountNo.checked == false)
                 isValid=false;
                 if (optZeroBalanceAccountYes.checked == false && optZeroBalanceAccountNo.checked == false)
                 isValid=false;
                 if (optCeoCfoCertificationYes.checked == false && optCeoCfoCertificationNo.checked == false)
                 isValid=false;
                 
                 if (optMaterialityYes.checked == false  && optMaterialityNo.checked == false)
                 isValid=false;
                 if (optRiskRatingYes.checked == false && optRiskRatingNo.checked == false)
                 isValid=false;
                 if (optMandatoryReportSignoffYes.checked == false && optMandatoryReportSignoffNo.checked == false)
                 isValid=false;
                 if (optCertificationActivationYes.checked == false && optCertificationActivationNo.checked == false)
                 isValid=false;
        
                 if (isValid==false)
                 {
                 testControl.value = "NotValid$$$";
                 }
                 
                 //ChildControls From Materility
                 var ddlMaterialitySelectionBasedOn = document.getElementById(ddlMaterialitySelectionBasedOnClientID);
                 var txtCompanywideMaterialityThreshold = document.getElementById(txtCompanywideMaterialityThresholdClientID);
                 var rdFSCaptionwideMateriality= document.getElementById(rdFSCaptionwideMaterialityClientID);
                
                 //ChildControls From RiskRating
                 var ddlRiskRatingFrequency1 = document.getElementById(ddlRiskRatingFrequencyClientID1);
                 var ddlRiskRatingFrequency2 = document.getElementById(ddlRiskRatingFrequencyClientID2);
                 var ddlRiskRatingFrequency3 = document.getElementById(ddlRiskRatingFrequencyClientID3);
                 
                 var cbRiskRatingFrequency1 = document.getElementById(cbRiskRatingFrequencyClientID1);
                 var cbRiskRatingFrequency2 = document.getElementById(cbRiskRatingFrequencyClientID2);
                 var cbRiskRatingFrequency3 = document.getElementById(cbRiskRatingFrequencyClientID3);
                 
                 //ChildControls From MandatoryReportSignoff
                 var cblApproverMandatoryReportSignoff = document.getElementById(cblApproverMandatoryReportSignoffClientID);
                 var cblReviewerMandatoryReportSignoff = document.getElementById(cblReviewerMandatoryReportSignoffClientID);

                 //ChildControls From CertificationActivation
                 var rdCertificationActivation = document.getElementById(rdCertificationActivationClientID);
                 
                 //Logic for CertificationActivation
                 
                 //TODO: will not work in case the number of images rendered on thge top of grid change
                 //or column Number changes or new colum adds, because presently its based on getElementsByTagName("input") method
                 if (optCertificationActivationYes.checked == true )
                 {
                     //this TagName("input") -gives .length=20 (4 for image at the top of the grid(export to excel etc,2 for each calender, 1 for each checkBox, and 1(at the end) for hidden field)
                     var ctrListForInputTag= rdCertificationActivation.getElementsByTagName("input");
                     alert(ctrListForInputTag.length);
                     var countInputTags = ctrListForInputTag.length;
                     
                     for (var y=4; y<countInputTags-1;( y=(y+5)))// 4 for icon on the grid(like export to excel),y+5 as there are 5 inputs on each row, -1 (in countInputTags-1) for last input of hidden field
                             {
                                if (ctrListForInputTag[y].value > ctrListForInputTag[y+3].value )
                                {
                                isValid=false;
                                alert('Dates Not Valid');
                                }
                                else
                                {
                                }
                              }
//                          ctrListForInputTag[6].disabled = true;    
//                              alert('Check Disabled');
                 }
                 
                 //Logic for RiskRating
                  if (optRiskRatingYes.checked == true )
                 {
                     //High
                     if (!validateCBL(ddlRiskRatingFrequency1,cbRiskRatingFrequency1))
                     {
                     isValid=false;
                     }
                     //Medium
                     if (!validateCBL(ddlRiskRatingFrequency2,cbRiskRatingFrequency2))
                     {
                     isValid=false;
                     }
                     //Low
                     if (!validateCBL(ddlRiskRatingFrequency3,cbRiskRatingFrequency3))
                     {
                     isValid=false;
                     }
                 }
                 
                 //Logic for Materility
                 if (optMaterialityYes.checked == true )
                 {
                     //alert (ddlMaterialitySelectionBasedOn.value);
                     if (ddlMaterialitySelectionBasedOn.value==1 )//fscaption wide
                     {
                     //TODO: check if FSCaption are present and if Yes then none is left blank
//                     var countFSCaptionRows = rdFSCaptionwideMateriality.rows.length;
//                         for (var x=0;x<countFSCaptionRows;x++)
//                         {
//                         if ( rdFSCaptionwideMateriality.rows[x].cells[1].firstChild == "")//cells[1] for column for textBox
//                            {
//                            isValid=false;
//                            alert ('empty in fs caption');
//                            }
//                         }
                     }
                     else if (ddlMaterialitySelectionBasedOn.value==2)//company wide
                     {
                         if (txtCompanywideMaterialityThreshold.value=="" || txtCompanywideMaterialityThreshold.value== undefined )//company wide// TODO: check if syntax is correct
                         {
                         isValid=false;//TODO: do we need to check if value is numeric
                         }
                     }
                     else
                     {
                     isValid=false;// it means no value is selected in DDL
                     }
                 }
                 
                 //Logic for MandatoryReportSignoff
                 if (optMandatoryReportSignoffYes.checked == true )
                 {
                 //TODO: check if approver applies
                 //Approver
                     if (cblApproverMandatoryReportSignoff != null)
                     {
                             if (!validateCBLAtLeastOneSelected(cblApproverMandatoryReportSignoff))
                             {
                             isValid = false  ;
                             }
                     } else
                     {
                     //alert('ApproverMandatoryReport not applicable due to dual review setting');
                     }
                 //Reviewer          
                             if (!validateCBLAtLeastOneSelected(cblReviewerMandatoryReportSignoff))
                             {
                             isValid = false  ;
                             }
                 }
                 
                 alert('Done so redirecting.....');
             }
             
             //validates that either 'except CUSTOM' selected or at least one period is selected if CUSTOM
             function validateCBL1(ddlFrequency, cbPeriod)
             {
                         var isValid = true ;
                         if(ddlFrequency.value == 4)
                         {
                             var isRiskRatingPeriodSelected = false;
                             //alert('custom option');
                             var chkListBox= cbPeriod.getElementsByTagName("input");
                             for (var y=0;y<chkListBox.length;y++)
                             {
                                if (chkListBox[y].checked)
                                {
                                isRiskRatingPeriodSelected=true ;
                                //alert('PeriodSelected');
                                }
                                else
                                {
                                //alert('This Period Not Selected')
                                }
                             }
                             if (isRiskRatingPeriodSelected==false)
                             {
                             //alert('OHHH !!!! Periods Not Selected')
                             var isValid = false  ;
                             }
                         }
                         else //some thing is selected in DDL except custom
                         {
                         }
                         return isValid;
             }
            
            </script>--%>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
