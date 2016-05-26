<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_AccountProfileAttributeUpdate" Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/ARTMasterPage.master" Codebehind="AccountProfileAttributeUpdate.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="DropDownList" Src="~/UserControls/UserDropDown.ascx" %>
<%@ Register TagPrefix="ReconciliationTemplate" TagName="DropDownList" Src="~/UserControls/ReconciliationTemplateDropDown.ascx" %>
<%@ Register TagPrefix="SubledgerSource" TagName="DropDownList" Src="~/UserControls/SubledgerSourceDropDown.ascx" %>
<%@ Register TagPrefix="RiskRating" TagName="DropDownList" Src="~/UserControls/RiskRatingDropDown.ascx" %>
<%--<%@ Register TagPrefix="DayType" TagName="DropDownList" Src="~/UserControls/DayTypeDropDown.ascx" %>--%>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="AccountType" TagName="DropDownList" Src="~/UserControls/AccountTypeDropDown.ascx" %>
<%@ Register TagPrefix="Popup" TagName="RecFrequencySelection" Src="~/UserControls/PopupRecFrequencySelection.ascx" %>
<%@ Register TagPrefix="Popup" TagName="RiskRatingSelection" Src="~/UserControls/PopupRecFrequency.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="TextEditor" Src="~/UserControls/TextEditor.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlAccountProfile" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="valSummaryAccountProfileAttributeUpdate" runat="server" />
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <colgroup>
                    <col width="2%" />
                    <col />
                    <col width="15%" />
                    <col width="25%" />
                    <col width="4%" />
                    <col />
                    <col width="20%" />
                    <col width="28%" />
                    <%--Account Heirarchy Details--%>
                    <tr>
                        <td colspan="8">
                            <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetail" runat="server" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Account Information Label--%>
                    <tr>
                        <td></td>
                        <td colspan="7"><u>
                            <webControls:ExLabel ID="lblAccountInfo" runat="server" LabelID="1553 " SkinID="BlueBold11Arial"></webControls:ExLabel>
                        </u></td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Account Type & Subledger Source--%>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <webControls:ExLabel ID="lblAccountType" runat="server" FormatString="{0}:" LabelID="1363" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td colspan="2">
                            <webControls:ExLabel ID="lblAccountTypeValue" runat="server" SkinID="ReadOnlyValue" ValidatorEnable="true">
                            </webControls:ExLabel>
                        </td>
                        <td>&nbsp; </td>
                        <asp:Panel ID="pnlSubledgerSource" runat="server">
                            <td>
                                <webControls:ExLabel ID="lblSubledgerSource" runat="server" FormatString="{0}:" LabelID="1058" SkinID="Black11Arial"></webControls:ExLabel>
                            </td>
                            <td>
                                <SubledgerSource:DropDownList ID="ddlSubledgerSource" runat="server" Width="200" />
                                <asp:CustomValidator ID="vldSubledgerSource" runat="server" ClientValidationFunction="ValidateSubledger" Font-Bold="true" Font-Size="Medium" Text="!"></asp:CustomValidator>
                            </td>
                        </asp:Panel>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Account Policy URL & Reconciliation Policy--%>
                    <tr>
                        <td></td>
                        <td></td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblAccountPolicyURL" runat="server" FormatString="{0}:" LabelID="1461" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td colspan="2">
                            <UserControls:TextEditor ID="ucAccountPolicyURL" runat="server" EditorSkinID="RadEditAttributeValue" IsRequired="false" LabelID="1461" />
                        </td>
                        <td></td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblReconciliationPorecedure" runat="server" FormatString="{0}:" LabelID="1360" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <UserControls:TextEditor ID="ucReconciliationProcedure" runat="server" EditorSkinID="RadEditAttributeValue" IsRequired="false" LabelID="1360" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Account Description--%>
                    <tr>
                        <td></td>
                        <td class="ManadatoryField">* </td>
                        <td valign="top">
                            <webControls:ExLabel ID="lblDescription" runat="server" FormatString="{0}:" LabelID="1460" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td colspan="2">
                            <UserControls:TextEditor ID="ucDescription" runat="server" EditorSkinID="RadEditAttributeValue" IsRequired="true" LabelID="1460" />
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Account Attribute--%>
                    <tr>
                        <td></td>
                        <td colspan="7"><u>
                            <webControls:ExLabel ID="lblAccountAttribute" runat="server" LabelID="1554" SkinID="BlueBold11Arial"></webControls:ExLabel>
                        </u></td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Reconciliation Form and Risk Rating--%>
                    <tr>
                        <td></td>
                        <td class="ManadatoryField">* </td>
                        <td>
                            <webControls:ExLabel ID="lblReconciliationForm" runat="server" FormatString="{0}:" LabelID="1426" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <ReconciliationTemplate:DropDownList ID="ddlReconciliationTemplate" runat="server" ValidatorEnable="true" />
                        </td>
                        <td></td>
                        <td class="ManadatoryField">* </td>
                        <td>
                            <webControls:ExLabel ID="lblRiskRating" runat="server" FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <RiskRating:DropDownList ID="ddlRiskRating" runat="server" SkinID="DropDownList200" />
                            <Popup:RiskRatingSelection ID="ucRiskRatingSelection" runat="server" />
                            <Popup:RecFrequencySelection ID="ucRecFrequencySelection" runat="server" />
                            <input type="hidden" runat="server" id="txtRecPeriodIDContainer" />
                            <asp:CustomValidator ID="vldRecFrequency" runat="server" ClientValidationFunction="ValidateRecFrequency" Font-Bold="true" Font-Size="Medium" Text="!"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Key Account And Zero Balance Account--%>
                    <tr>
                        <td></td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblIsKeyAccount" runat="server" FormatString="{0}:" LabelID="1339" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optIsKeyAccountYes" runat="server" CssClass="Black11Arial" GroupName="IsKeyAccount" LabelID="1252" TextAlign="Right" />
                            &nbsp;&nbsp;
                            <webControls:ExRadioButton ID="optIsKeyAccountNo" runat="server" CssClass="Black11Arial" GroupName="IsKeyAccount" LabelID="1251" TextAlign="Right" />
                            &nbsp;
                            <asp:CustomValidator ID="vldKeyAccount" runat="server" ClientValidationFunction="ValidateKeyAccount" Font-Bold="true" Font-Size="Medium" Text="!"></asp:CustomValidator>
                        </td>
                        <td></td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblZeroBalanceAccount" runat="server" FormatString="{0}:" LabelID="1256" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optZeroBalanceAccountYes" runat="server" CssClass="Black11Arial" GroupName="IsZeroBalanceAccount" LabelID="1252" TextAlign="Right" />
                            &nbsp;&nbsp;
                            <webControls:ExRadioButton ID="optZeroBalanceAccountNo" runat="server" CssClass="Black11Arial" GroupName="IsZeroBalanceAccount" LabelID="1251" TextAlign="Right" />
                            &nbsp;
                            <asp:CustomValidator ID="vldZeroBalance" runat="server" ClientValidationFunction="ValidateZeroBalance" Font-Bold="true" Font-Size="Medium" Text="!"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--RCCY & BCCY--%>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <webControls:ExLabel ID="lblReportingCurrency" runat="server" FormatString="{0}:" LabelID="1424" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblReportingCurrencyValue" runat="server" SkinID="Black11Arial" />
                        </td>
                        <td></td>
                        <td></td>
                        <td>
                            <webControls:ExLabel ID="lblBaseCurrency" runat="server" FormatString="{0}:" LabelID="1493" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblBaseCurrencyvalue" runat="server" SkinID="Black11Arial" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Account Materiality & Net Account--%>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <webControls:ExLabel ID="lblAcMateriality" runat="server" FormatString="{0}:" LabelID="1372" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <webControls:ExLabel ID="lblAcMaterialityValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                                    </td>
                                    <td>
                                        <asp:Image ID="imgGLDataUnAvailable" runat="server" ImageUrl="~/App_Themes/SkyStemBlueBrown/Images/notes.gif" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td></td>
                        <td></td>
                        <td>
                            <webControls:ExLabel ID="lblNetAccount" runat="server" FormatString="{0}:" LabelID="1257" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblNetAccountValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <webControls:ExLabel ID="lblIsReconcilable" runat="server" FormatString="{0}:" LabelID="2401" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExRadioButton ID="optIsReconcilableYes" runat="server" CssClass="Black11Arial" GroupName="IsReconcilable" LabelID="1252" TextAlign="Right" />
                            &nbsp;&nbsp;
                            <webControls:ExRadioButton ID="optIsReconcilableNo" runat="server" CssClass="Black11Arial" GroupName="IsReconcilable" LabelID="1251" TextAlign="Right" />
                            &nbsp; </td>
                        <td></td>
                        <td></td>
                        <td>
                            <webControls:ExLabel ID="lblCreationDate" runat="server" FormatString="{0}:" LabelID="2944" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblCreationDateValue" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr id="trPreparerDueDays" runat="server">
                        <td>&nbsp; </td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblPreparerDueDays" runat="server" FormatString="{0}:" LabelID="2752" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPreparerDueDays" runat="server" SkinID="TextBox70"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPreparerDueDays" runat="server" ControlToValidate="txtPreparerDueDays">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvPreparerDueDays" runat="server" ControlToValidate="txtPreparerDueDays" OnServerValidate="cvPreparerDueDays_OnServerValidate" />
                        </td>
                        <td>&nbsp; </td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblReviewerDueDays" runat="server" FormatString="{0}:" LabelID="2753" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:TextBox ID="txtReviewerDueDays" runat="server" SkinID="TextBox70"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvReviewerDueDays" runat="server" ControlToValidate="txtReviewerDueDays">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvReviewerDueDays" runat="server" ControlToValidate="txtReviewerDueDays" OnServerValidate="cvReviewerDueDays_OnServerValidate" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr id="trApproverDueDays" runat="server">
                        <td>&nbsp; </td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblApproverDueDays" runat="server" FormatString="{0}:" LabelID="2754" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <asp:TextBox ID="txtApproverDueDays" runat="server" SkinID="TextBox70" AutoPostBack="true" OnTextChanged="txtApproverDueDays_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApproverDueDays" runat="server" ControlToValidate="txtApproverDueDays">
                            </asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvApproverDueDays" runat="server" ControlToValidate="txtApproverDueDays" OnServerValidate="cvApproverDueDays_OnServerValidate" />
                        </td>
                        <td>&nbsp; </td>
                        <td class="ManadatoryFieldRight">
                            <%--*--%>
                        </td>
                        <td>
<%--                            <webControls:ExLabel ID="lblDayType" runat="server" LabelID="2963" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>--%>
                        </td>
                        <td>
<%--                            <DayType:DropDownList ID="ddlDayType" runat="server" SkinID="DropDownList200" />--%>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Account Ownership--%>
                    <tr>
                        <td>&nbsp; </td>
                        <td colspan="7"><u>
                            <webControls:ExLabel ID="lblAccountOwnership" runat="server" LabelID="1212" SkinID="BlueBold11Arial"></webControls:ExLabel>
                        </u></td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr>
                        <td>&nbsp; </td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblPreparer" runat="server" FormatString="{0}:" LabelID="1130" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <UserControls:DropDownList ID="ddlPreparer" runat="server" IsPreparer="true" ValidatorEnable="true" Width="200" />
                        </td>
                        <td>&nbsp; </td>
                        <td>&nbsp; </td>
                        <td id="trBackupPreparerLabel" runat="server">
                            <webControls:ExLabel ID="lblBackupPreparer" runat="server" FormatString="{0}:" LabelID="2501" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td id="trBackupPreparerDropdown" runat="server">
                            <UserControls:DropDownList ID="ddlBackupPreparer" runat="server" IsBackupPreparer="true" Width="200" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr>
                        <td>&nbsp; </td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblReviewer" runat="server" FormatString="{0}:" LabelID="1131" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <UserControls:DropDownList ID="ddlReviewer" runat="server" IsReviewer="true" ValidatorEnable="true" Width="200" />
                        </td>
                        <td>&nbsp; </td>
                        <td>&nbsp; </td>
                        <td id="trBackupReviewerLabel" runat="server">
                            <webControls:ExLabel ID="lblBackupReviewer" runat="server" FormatString="{0}:" LabelID="2502" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td id="trBackupReviewerDropdown" runat="server">
                            <UserControls:DropDownList ID="ddlBackupReviewer" runat="server" IsBackupReviewer="true" Width="200" />
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr id="trBackupPreparerReviewer" runat="server">
                        <td>&nbsp; </td>
                        <td class="ManadatoryFieldRight">* </td>
                        <td>
                            <webControls:ExLabel ID="lblApprover" runat="server" FormatString="{0}:" LabelID="1132" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td>
                            <UserControls:DropDownList ID="ddlApprover" runat="server" IsApprover="true" Width="200" OnDropDownSelectionChanged="ctrySelectBox_DropDownUserSelectionChanged" />
                        </td>
                        <td>&nbsp; </td>
                        <td>&nbsp; </td>
                        <td id="trBackupApproverLabel" runat="server">
                            <webControls:ExLabel ID="lblBackupApprover" runat="server" FormatString="{0}:" LabelID="2503" SkinID="Black11Arial"></webControls:ExLabel>
                        </td>
                        <td id="trBackupApproverDropdown" runat="server">
                            <UserControls:DropDownList ID="ddlBackupApprover" runat="server" IsBackupApprover="true" Width="200" />
                            <asp:CustomValidator ID="cvApproverBackupApprover" runat="server" ClientValidationFunction="ValidateApproverBackupApprover" Font-Bold="true" Text="!"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="8"></td>
                    </tr>
                    <%--Command Buttons--%>
                    <tr>
                        <td align="right" colspan="8">
                            <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_OnClick" />
                            &nbsp;<webControls:ExButton ID="btnCancel" runat="server" CausesValidation="false" LabelID="1239" OnClick="btnCancel_OnClick" />
                            <asp:HiddenField ID="hdnConfirm" runat="server" />
                        </td>
                    </tr>
                </colgroup>
            </table>
            <table width="100%">
                <tr>
                    <td colspan="3">
                        <UserControls:ProgressBar ID="ucAccountProfileMassAndBulkUpdate" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="upnlAccountProfile" Visible="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function ConfirmNetAccount(btnSave, hdnConf, msg) {
            var confirm_value = document.getElementById(hdnConf);
            var btnSave = document.getElementById(btnSave);
            if (confirm(msg)) {
                confirm_value.value = "Yes";
                btnSave.click();
            } else {
                confirm_value.value = "No";
            }
        }

        function GetRecPeriodIDCollectionForAccount(txtRecPeriodContainerID, recPeriodIDCollection, hlRecFrequencyID, accountID, mode) {

            var txtRecPeriodContainer = document.getElementById(txtRecPeriodContainerID);
            txtRecPeriodContainer.value = recPeriodIDCollection;

            var hlRecFrequency = document.getElementById(hlRecFrequencyID);
            hlRecFrequency.href = "javascript:OpenRadWindowForHyperlink('PopupRecFrequencySelection.aspx?AccountID=" + accountID + "&Mode=" + mode + "&RecPeriodContainerID=" + txtRecPeriodContainerID + "&RecPeriodIDCollection=" + recPeriodIDCollection + "&hlRecFrequencyID=" + hlRecFrequencyID + "', 520, 400);";
        }

        function ValidateZeroBalance(sender, args) {
            var optZeroBalanceNo = document.getElementById('<%=this.optZeroBalanceAccountNo.ClientID %>');
            var optZeroBalanceYes = document.getElementById('<%=this.optZeroBalanceAccountYes.ClientID %>');

            if (optZeroBalanceNo.disabled == false && optZeroBalanceYes.disabled == false) {
                if (optZeroBalanceNo.checked == false && optZeroBalanceYes.checked == false) {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }
        }

        function ValidateKeyAccount(sender, args) {
            var optKeyAccountNo = document.getElementById('<%=this.optIsKeyAccountNo.ClientID %>');
            var optKeyAccountYes = document.getElementById('<%=this.optIsKeyAccountYes.ClientID %>');

            if (optKeyAccountNo.disabled == false && optKeyAccountYes.disabled == false) {
                if (optKeyAccountNo.checked == false && optKeyAccountYes.checked == false) {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }
        }

        function ValidateSubledger(sender, args) {
            var ddlRecTempalte = document.getElementById('<%=ddlReconciliationTemplate.ClientID %>');
            var ddlSubledgerSource = document.getElementById('<%=ddlSubledgerSource.ClientID %>');

            var recTemplateValue = ddlRecTempalte.options[ddlRecTempalte.selectedIndex].value;
            var subledgerValue = ddlSubledgerSource.options[ddlSubledgerSource.selectedIndex].value;

            if (recTemplateValue == '3') {
                if (subledgerValue == '-2') {
                    args.IsValid = false;
                }
                else {
                    args.IsValid = true;
                }
            }
            else {
                args.IsValid = true;
            }
        }


        function SetURLForRiskRating(ddlriskRating, ucpopupRiskrating) {

            var ddlriskRatingFrequency = document.getElementById(ddlriskRating);
            var ucpopupRiskratingFrequency = document.getElementById(ucpopupRiskrating);
            //var ucpopupRiskratingControl = $find(popupControl);
            //alert(ucpopupRiskratingControl);
            var selectedValue = ddlriskRatingFrequency.options[ddlriskRatingFrequency.selectedIndex].value;

            if (selectedValue != "-2") {


                ucpopupRiskratingFrequency.href = "javascript:OpenRadWindowForHyperlink('PopupRiskRatingRecPeriod.aspx?RiskRating_ID=" + selectedValue + "', 480, 400);";

                ucpopupRiskratingFrequency.style.visibility = "visible";
            }
            else {
                ucpopupRiskratingFrequency.style.visibility = "hidden";
            }


        }
        function ValidateRecFrequency(sender, args) {
            var imgRecFrequency = document.getElementById('<%=ucRecFrequencySelection.ClientID %>');
            var txtRecFrequency = document.getElementById('<%=txtRecPeriodIDContainer.ClientID %>');

            if (imgRecFrequency != null && imgRecFrequency != 'undefined') {
                var selectedItems = txtRecFrequency.value;

                if (selectedItems.length > 0) {
                    args.IsValid = true;
                }
                else {
                    args.IsValid = false;
                }
            }
            else {
                args.IsValid = true;
            }
        }
        function ValidateApproverBackupApprover(sender, args) {
            var clientIDList = sender.getAttribute("ApproverBackupApproverClientID");
            var arrayClientID = clientIDList.split(",");
            var ddlApprover = document.getElementById(arrayClientID[0]);
            var ddlBackUpApprover = document.getElementById(arrayClientID[1]);
            var ddlApproverValue;
            var ddlBackUpApproverValue;
            if (ddlApprover != null && ddlApprover != 'undefined') {
                ddlApproverValue = ddlApprover.options[ddlApprover.selectedIndex].value;
            }
            if (ddlBackUpApprover != null && ddlBackUpApprover != 'undefined') {
                ddlBackUpApproverValue = ddlBackUpApprover.options[ddlBackUpApprover.selectedIndex].value;
            }
            if (ddlApproverValue == "-2" && ddlBackUpApproverValue != "-2") {
                args.IsValid = false;
                return;
            }

        }
    </script>
</asp:Content>
