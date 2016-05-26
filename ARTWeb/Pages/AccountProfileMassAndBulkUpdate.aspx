<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_AccountProfileMassAndBulkUpdate" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    Theme="SkyStemBlueBrown" Codebehind="AccountProfileMassAndBulkUpdate.aspx.cs" %>

<%@ Register Src="~/UserControls/LegendOnAccountSearch.ascx" TagName="LegendOnAccountSearch"
    TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/AccountSearchControl.ascx" TagName="AccountSearchControl"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="RiskRating" TagName="DropDownList" Src="~/UserControls/RiskRatingDropDown.ascx" %>
<%--<%@ Register TagPrefix="DayType" TagName="DropDownList" Src="~/UserControls/DayTypeDropDown.ascx" %>--%>
<%@ Register TagPrefix="ReconciliationTemplate" TagName="DropDownList" Src="~/UserControls/ReconciliationTemplateDropDown.ascx" %>
<%@ Register TagPrefix="AccountType" TagName="DropDownList" Src="~/UserControls/AccountTypeDropDown.ascx" %>
<%@ Register TagPrefix="SubledgerSource" TagName="DropDownList" Src="~/UserControls/SubledgerSourceDropDown.ascx" %>
<%@ Register TagPrefix="Popup" TagName="RecFrequency" Src="~/UserControls/PopupRecFrequency.ascx" %>
<%@ Register TagPrefix="Popup" TagName="RecFrequencySelection" Src="~/UserControls/PopupRecFrequencySelection.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlAccountProfile" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td colspan="3">
                        <UserControl:AccountSearchControl ID="ucAccountSearchControl" runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <webControls:ExLabel ID="lblGridTitle" runat="server" SkinID="SubSectionHeading"></webControls:ExLabel>
                        <asp:HiddenField ID="hdnBulkUpdateMode" runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="3"></td>
                </tr>
                <asp:Panel ID="pnlMassUpdate" runat="server" Visible="false">
                    <tr>
                        <td colspan="3">
                            <webControls:ExCustomValidator ID="cvMassUpdateSelection" runat="server" ClientValidationFunction="validateRowSelectionForMassUpdate"
                                Text="" Display="None" LabelID="2013"></webControls:ExCustomValidator>
                            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGridMassUpdate" runat="server" Grid-AllowPaging="true"
                                    Grid-AllowSorting="true" Grid-AllowExportToExcel="true" Grid-AllowCauseValidationExportToExcel="false"
                                    Grid-AllowCauseValidationExportToPDF="false" Grid-AllowPrint="true" Grid-AllowPrintAll="true"
                                    Grid-AllowExportToPDF="true">
                                    <SkyStemGridMasterTableView DataKeyNames="PreparerDueDays,ReviewerDueDays,ApproverDueDays" />
                                    <SkyStemGridColumnCollection>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1357" headerstyle-width="15%" sortexpression="AccountNumber"
                                            datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountNumberMass" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1346" headerstyle-width="10%" sortexpression="AccountName"
                                            datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountNameMass" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1257" uniquename="NetAccount" headerstyle-width="10%"
                                            sortexpression="NetAccount" datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblNetAccountMass" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="ZeroBalance" labelid="1256"
                                            headerstyle-width="10%" sortexpression="ZeroBalance" datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblZeroBalanceAccount" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="KeyAccount" labelid="1014" headerstyle-width="10%"
                                            sortexpression="KeyAccount" datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblKeyAccount" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="RiskRating" labelid="1013" headerstyle-width="15%"
                                            sortexpression="RiskRating" datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRiskRating" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <%--IsReconcilable--%>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="IsReconcilable" labelid="2401" sortexpression="Reconcilable" datatype="System.String"
                                            headerstyle-width="8%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblIsReconcilable" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1426" headerstyle-width="15%" sortexpression="ReconciliationTemplate"
                                            datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReconciliationTemplate" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="RecFrequency" labelid="1427"
                                            headerstyle-width="15%">
                                            <ItemTemplate>
                                                <%--<webControls:ExImageButton ID="imgReconciliationFrequencyMass" runat="server" ImageUrl="../App_Themes/SkyStemBlueBrown/Images/ReconciliationFreequency.gif">
                                        </webControls:ExImageButton>
                                        <input type="text" id="txtRecPeriodsContainer" runat="server" style="display: none" />--%>
                                                <Popup:RecFrequency ID="ucPopupRecFrequency" runat="server" />
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2752" headerstyle-width="10%" uniquename="PreparerDueDays" sortexpression="PreparerDueDays"
                                            datatype="System.Int32">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPreparerDueDays" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2753" headerstyle-width="10%" uniquename="ReviewerDueDays" sortexpression="ReviewerDueDays"
                                            datatype="System.Int32">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReviewerDueDays" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2754" headerstyle-width="10%" uniquename="ApproverDueDays" sortexpression="ApproverDueDays"
                                            datatype="System.Int32">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblApproverDueDays" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
<%--                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2963" headerstyle-width="10%" uniquename="DayType" sortexpression="DayType"
                                            datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDayType" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>--%>
                                    </SkyStemGridColumnCollection>
                                </UserControl:SkyStemARTGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td colspan="3"></td>
                    </tr>
                    <tr id="rowAttributeSetting" runat="server">
                        <td>&nbsp;
                        </td>
                        <td>
                            <webControls:ExLabel ID="lblAttribute" LabelID="1440" runat="server" SkinID="Black11Arial"
                                FormatString="{0}:"></webControls:ExLabel>
                            <asp:DropDownList ID="ddlAccountAttribute" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountAttribute_SelectedIndexChangedHandler"
                                SkinID="DropDownList150">
                                <%--<asp:ListItem Text="Key Account" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Zero Balance Account" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Net Account" Value="3"></asp:ListItem>--%>
                            </asp:DropDownList>
                            &nbsp;
                            <webControls:ExRadioButton ID="optIsYes" runat="server" GroupName="Attribute" TextAlign="Right"
                                LabelID="1252" SkinID="OptBlack11Arial" Visible="false" />
                            <webControls:ExRadioButton ID="optIsNo" runat="server" GroupName="Attribute" TextAlign="Right"
                                LabelID="1251" SkinID="OptBlack11Arial" Visible="false" />
                            <RiskRating:DropDownList ID="ddlRiskRatingMass" runat="server" Visible="false" />
<%--                            <DayType:DropDownList ID="ddlDayType" runat="server" Visible="false" />--%>
                            <%--<AccountType:DropDownList ID="ddlAccountType" runat="server"
                                Visible="false" />--%>
                            <Popup:RecFrequency ID="ucRiskRatingPeriod" runat="server" Visible="false" />
                            <Popup:RecFrequencySelection ID="ucRecFrequencySelectionMass" runat="server" />
                            <asp:TextBox ID="txtDueDays" SkinID="TextBox70" GroupName="Attribute" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlBulkUpdate" runat="server" Visible="false">
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="Panel1" runat="server" SkinID="RadGridScrollPanel">
                                <webControls:ExCustomValidator ID="cvRowSelectionForBulkUpdate" runat="server" ClientValidationFunction="validateRowSelectionForBulkUpdate"
                                    Text="" Display="None" LabelID="2013"></webControls:ExCustomValidator>
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGridBulkUpdate" runat="server" Grid-AllowPaging="true"
                                    Grid-AllowSorting="true" Grid-AllowExportToPDF="true" Grid-AllowExportToExcel="true"
                                    Grid-AllowCauseValidationExportToExcel="false" Grid-AllowCauseValidationExportToPDF="false"
                                    Grid-AllowPrint="true" Grid-AllowPrintAll="true">
                                    <SkyStemGridColumnCollection>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1357" headerstyle-width="10%" sortexpression="AccountNumber"
                                            datatype="System.String" uniquename="AccountNumber">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                                <asp:HiddenField ID="hdnIsReconciled" runat="server" />
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1346" headerstyle-width="10%" sortexpression="AccountName"
                                            datatype="System.String" uniquename="AccountName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1257" headerstyle-width="10%" uniquename="NetAccount"
                                            sortexpression="NetAccount" datatype="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblNetAccount" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="ZeroBalance" labelid="1256"
                                            headerstyle-width="8%">
                                            <ItemTemplate>
                                                <webControls:ExCheckBox ID="chkZeroBalanceAccount" runat="server"></webControls:ExCheckBox>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="KeyAccount" labelid="1014" headerstyle-width="8%">
                                            <ItemTemplate>
                                                <webControls:ExCheckBox ID="chkKeyAccount" runat="server"></webControls:ExCheckBox>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <%--IsReconcilable--%>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="IsReconcilable" labelid="2401"
                                            headerstyle-width="8%">
                                            <ItemTemplate>
                                                <webControls:ExCheckBox ID="chkIsReconcilable" runat="server"></webControls:ExCheckBox>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="RiskRating" labelid="1013" headerstyle-width="14%">
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <RiskRating:DropDownList ID="ddlRiskRating" runat="server"></RiskRating:DropDownList>
                                                        </td>
                                                        <td>
                                                            <Popup:RecFrequency ID="ucriskRatingBulkUpdate" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="RiskRatingLbl" visible="false"
                                            labelid="1013" headerstyle-width="14%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRiskRatingExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1426" uniquename="ReconciliationTemplate"
                                            headerstyle-width="10%">
                                            <ItemTemplate>
                                                <ReconciliationTemplate:DropDownList ID="ddlReconciliationTemplate" runat="server"
                                                    Width="90%"></ReconciliationTemplate:DropDownList>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="ReconciliationTemplateExport"
                                            labelid="1426" visible="false" headerstyle-width="10%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReconciliationTemplateExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="RecFrequency" labelid="1427"
                                            headerstyle-width="6%">
                                            <ItemTemplate>
                                                <Popup:RecFrequencySelection ID="ucRecFrequencySelection" runat="server" />
                                                <input type="text" id="txtRecPeriodsContainer" runat="server" style="display: none" />
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="1058" uniquename="Subledger" headerstyle-width="10%">
                                            <ItemTemplate>
                                                <SubledgerSource:DropDownList ID="ddlSubledger" runat="server" Width="100"></SubledgerSource:DropDownList>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="SubledgerExport" labelid="1058"
                                            visible="false" headerstyle-width="10%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblSubledgerExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2752" headerstyle-width="10%" uniquename="PreparerDueDays"
                                            datatype="System.Int32">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPreparerDueDays" SkinID="TextBox70" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPreparerDueDays" Enabled="false" runat="server" ControlToValidate="txtPreparerDueDays">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="cvPreparerDueDays" runat="server" Enabled="false" ControlToValidate="txtPreparerDueDays" 
                                                    ClientValidationFunction="ValidateDueDays" />
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2752" headerstyle-width="10%" uniquename="PreparerDueDaysExport"
                                            datatype="System.Int32" visible="false">
                                            <ItemTemplate>
                                              <webControls:ExLabel ID="lblPreparerDueDaysExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>

                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2753" headerstyle-width="10%" uniquename="ReviewerDueDays"
                                            datatype="System.Int32">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReviewerDueDays" SkinID="TextBox70" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvReviewerDueDays" runat="server" Enabled="false" ControlToValidate="txtReviewerDueDays">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="cvReviewerDueDays" runat="server" Enabled="false" ControlToValidate="txtReviewerDueDays" 
                                                    ClientValidationFunction="ValidateDueDays" />
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2753" headerstyle-width="10%" uniquename="ReviewerDueDaysExport"
                                            datatype="System.Int32" visible="false">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReviewerDueDaysExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2754" headerstyle-width="10%" uniquename="ApproverDueDays"
                                            datatype="System.Int32">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtApproverDueDays" SkinID="TextBox70" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvApproverDueDays" runat="server" Enabled="false" ControlToValidate="txtApproverDueDays">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="cvApproverDueDays" runat="server" Enabled="false" ControlToValidate="txtApproverDueDays" 
                                                    ClientValidationFunction="ValidateDueDays" />
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2754" headerstyle-width="10%" uniquename="ApproverDueDaysExport"
                                            datatype="System.Int32" visible="false">
                                            <ItemTemplate>
                                                 <webControls:ExLabel ID="lblApproverDueDaysExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
<%--                                        <telerikwebcontrols:exgridtemplatecolumn labelid="2963" headerstyle-width="10%" uniquename="DayType"
                                            datatype="System.String">
                                            <ItemTemplate>
                                                <DayType:DropDownList ID="ddlDayType" runat="server"></DayType:DropDownList>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>
                                        <telerikwebcontrols:exgridtemplatecolumn uniquename="DayTypeExport"
                                            labelid="2963" visible="false" headerstyle-width="10%">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblDayTypeExport" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikwebcontrols:exgridtemplatecolumn>--%>
                                    </SkyStemGridColumnCollection>
                                </UserControl:SkyStemARTGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                </asp:Panel>
                <tr class="BlankRow">
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td colspan="2" align="right">
                        <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_Click" />&nbsp;
                        <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                            CausesValidation="false" OnClientClick="return HideValidationSummary();" />&nbsp;
                        <asp:HiddenField ID="hdnConfirm" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
            </table>
            <UserControl:LegendOnAccountSearch ID="LegendOnAccountSearch" runat="server" />
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
    <input type="hidden" id="txtRecPriodIDContainer" runat="server" />

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
            hlRecFrequency.href = "javascript:OpenRadWindowForHyperlink('PopupRecFrequencySelection.aspx?Acct_ID=" + accountID + "&Mode=" + mode + "&RecPeriodContainerID=" + txtRecPeriodContainerID + "&RecPeriodIDCollection=" + recPeriodIDCollection + "&hlRecFrequencyID=" + hlRecFrequencyID + "', 520, 480);";
        }

        function SetURLForRiskRating(ddlriskRating, ucpopupRiskrating) {

            var ddlriskRatingFrequency = document.getElementById(ddlriskRating);
            var ucpopupRiskratingFrequency = document.getElementById(ucpopupRiskrating);

            var selectedValue = ddlriskRatingFrequency.options[ddlriskRatingFrequency.selectedIndex].value;

            if (selectedValue != "-2") {
                ucpopupRiskratingFrequency.disabled = false;

                ucpopupRiskratingFrequency.href = "javascript:OpenRadWindowForHyperlink('PopupRiskRatingRecPeriod.aspx?RiskRating_ID=" + selectedValue + "', 480, 400);";

                ucpopupRiskratingFrequency.style.visibility = "visible";
            }
            else {
                ucpopupRiskratingFrequency.style.visibility = "hidden";
            }


        }

        function validateRowSelectionForMassUpdate(source, args) {

            var grid = $find('<%= ucSkyStemARTGridMassUpdate.Grid.ClientID %>');
            var selectedRowCount = grid.get_selectedItems().length;

            if (selectedRowCount > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
        function validateRowSelectionForBulkUpdate(source, args) {

            var grid = $find('<%= ucSkyStemARTGridBulkUpdate.Grid.ClientID %>');
            var selectedRowCount = grid.get_selectedItems().length;

            if (selectedRowCount > 0)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
        function RowSelecting(sender, args) {
            var id = args.get_id();
            var inputCheckBox = $get(id).getElementsByTagName("input")[0];
            if (!inputCheckBox || inputCheckBox.disabled) {
                //cancel selection for disabled rows 
                args.set_cancel(true);
            }

        }
    </script>

</asp:Content>
