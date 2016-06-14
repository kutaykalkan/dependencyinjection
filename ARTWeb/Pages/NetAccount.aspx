<%@ Page Language="C#" AutoEventWireup="true" Inherits="Pages_NetAccount"
    MasterPageFile="~/MasterPages/ARTMasterPage.master" Theme="SkyStemBlueBrown" Codebehind="NetAccount.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="RiskRatingDDL" Src="~/UserControls/RiskRatingDropDown.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register TagPrefix="Popup" TagName="RecFrequency" Src="~/UserControls/PopupRecFrequency.ascx" %>
<%@ Register Src="~/UserControls/AccountSearchControl.ascx" TagName="AccountSearchControl"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="TextEditor" Src="~/UserControls/TextEditor.ascx" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <asp:UpdatePanel ID="upnlAccountProfile" runat="server">
        <ContentTemplate>--%>
    <asp:Panel ID="pnlCapabilityNotActivatedMsg" runat="server">
        <div id="div1">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="100%" colspan="5" align="center">
                        <webControls:ExLabel ID="lblError" runat="server" LabelID="1051" FormatString="{0}:"
                            SkinID="Black11Arial"></webControls:ExLabel>
                        &nbsp;&nbsp;
                                <webControls:ExLabel ID="lblErrorMessage" runat="server" LabelID="1811" SkinID="Black11ArialNormal" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlContent" runat="server">
        <table width="100%" border="0">
            <col width="2%" />
            <col width="20%" />
            <col width="28%" />
            <col width="2%" />
            <col width="20%" />
            <col width="28%" />
            <tr id="rowNetAccount" runat="server">
                <td></td>
                <td>
                    <webControls:ExLabel ID="lblNetAccount" LabelID="1257" runat="server" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <asp:DropDownList AutoPostBack="true" ID="ddlNetAccount" runat="server" SkinID="DropDownList300"
                        OnSelectedIndexChanged="ddlNetAccount_OnSelectedIndexChanged" onchange="HideValidationSummary();">
                    </asp:DropDownList>
                </td>
                <td></td>
                <td>
                    <webControls:ExButton ID="btnDeleteNetAccount" runat="server" OnClick="btnDeleteNetAccount_Click"
                        LabelID="1564" />
                </td>
                <td align="right"></td>
            </tr>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <tr id="rowCreateNewNetAc" runat="server">
                <td class="ManadatoryField">*
                </td>
                <td>
                    <webControls:ExLabel ID="lblNetAcName" LabelID="1354" runat="server" SkinID="Black11Arial"
                        FormatString="{0}:"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExTextBox ID="txtNetAcName" runat="server" SkinID="ExTextBox300" IsRequired="true" />
                    <webControls:ExLabel ID="lblNetAccountName" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
            <asp:Panel ID="pnlNetAccountAttributes" runat="server">
                <tr>
                    <td></td>
                    <td valign="top">
                        <webControls:ExLabel ID="lblAccountPolicyURL" LabelID="1461" runat="server" FormatString="{0}:"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td>
                        <UserControls:TextEditor ID="ucAccountPolicyURL" LabelID="1461" IsRequired="false"
                            runat="server" EditorSkinID="RadEditNetAccount" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td valign="top">
                        <webControls:ExLabel ID="lblReconciliationPorecedure" LabelID="1360" runat="server"
                            FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td>
                        <UserControls:TextEditor ID="ucReconciliationProcedure" LabelID="1360" IsRequired="false"
                            runat="server" EditorSkinID="RadEditNetAccount" />
                    </td>
                </tr>
                <tr>
                    <td class="ManadatoryField">*
                    </td>
                    <td valign="top">
                        <webControls:ExLabel ID="lblDescription" LabelID="1460" runat="server" FormatString="{0}:"
                            SkinID="Black11Arial"></webControls:ExLabel>
                    </td>
                    <td colspan="2">
                        <UserControls:TextEditor ID="ucDescription" LabelID="1460" IsRequired="true" runat="server"
                            EditorSkinID="RadEditNetAccount" />
                    </td>
                    <td valign="top"></td>
                    <td></td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="6"></td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlNetAccountGrid" runat="server">
                <tr>
                    <td colspan="6">
                        <asp:Panel ID="pnlNetAccountGridScroll" runat="server" SkinID="RadGridScrollPanel">
                            <UserControl:SkyStemARTGrid ID="ucSkyStemARTGridAccountsAdded" runat="server" Grid-AllowPaging="false"
                                Grid-AllowSorting="true" Grid-EntityNameLabelID="1257" Grid-AllowCauseValidationExportToExcel="false"
                                Grid-AllowExportToExcel="True">
                                <SkyStemGridColumnCollection>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="20%" SortExpression="AccountNumber"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAccountNumberAddedGrid" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" SortExpression="AccountName"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblAccountNameAddedGrid" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1256" SortExpression="ZeroBalance"
                                        DataType="System.String" UniqueName="ZeroBalance" Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblZeroBalanceAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1014" SortExpression="KeyAccount"
                                        DataType="System.String" UniqueName="KeyAccount" Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblKeyAccountAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1013" SortExpression="RiskRating"
                                        DataType="System.String" UniqueName="RiskRating" Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblRiskRatingAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="RiskRatingFrequency" LabelID="1427"
                                        HeaderStyle-Width="15%">
                                        <ItemTemplate>
                                            <Popup:RecFrequency ID="ucPopupRecFrequencyAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1426" HeaderStyle-Width="15%" SortExpression="ReconciliationTemplate"
                                        DataType="System.String">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblReconciliationTemplateAddedGrid" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName"
                                        DataType="System.String" UniqueName="Preparer" Visible="true">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblPreparerAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2501" SortExpression="BackupPreparerFullName"
                                        DataType="System.String" UniqueName="BackupPreparer">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblBackupPreparerAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1131" SortExpression="ReviewerFullName"
                                        DataType="System.String" UniqueName="Reviewer" Visible="true">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblReviewerAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2502" SortExpression="BackupReviewerFullName"
                                        DataType="System.String" UniqueName="BackupReviewer">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblBackupReviewerAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1132" SortExpression="ApproverFullName"
                                        DataType="System.String" UniqueName="Approver" Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblApproverAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2503" SortExpression="BackupApproverFullName"
                                        DataType="System.String" UniqueName="BackupApprover" Visible="false">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblBackupApproverAddedGrid" runat="server" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1417" HeaderStyle-Width="10%" UniqueName="PreparerDueDate"
                                        SortExpression="PreparerDueDate" DataType="System.DateTime">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblPreparerDueDateAddedGrid" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1418" HeaderStyle-Width="10%" UniqueName="ReviewerDueDate"
                                        SortExpression="ReviewerDueDate" DataType="System.DateTime">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblReviewerDueDateAddedGrid" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1738" HeaderStyle-Width="10%" UniqueName="ApproverDueDate"
                                        SortExpression="ApproverDueDate" DataType="System.DateTime">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblApproverDueDateAddedGrid" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </SkyStemGridColumnCollection>
                                <ClientSettings>
                                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                                </ClientSettings>
                            </UserControl:SkyStemARTGrid>
                        </asp:Panel>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="6"></td>
                </tr>
                <tr>
                    <td colspan="6" align="right">
                        <webControls:ExButton ID="btnSave" runat="server" LabelID="1315 " OnClick="btnSave_OnClick" />
                        <webControls:ExButton ID="btnRemoveAccount" CausesValidation="false" runat="server"
                            LabelID="2170" OnClick="btnRemoveAccount_OnClick" />
                    </td>
                </tr>
            </asp:Panel>
            <tr class="BlankRow">
                <td colspan="6"></td>
            </tr>
        </table>
        <asp:Panel ID="pnlSearchGrid" runat="server">
            <div id="divAccountHeader">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlHeader" runat="server">
                                <table class="InputRequrementsHeading" width="100%">
                                    <tr>
                                        <td width="2%">&nbsp;
                                        </td>
                                        <td width="20%">
                                            <webControls:ExLabel ID="lblAddMoreAccounts" runat="server" LabelID="1356" SkinID="BlueBold11Arial" />
                                        </td>
                                        <td width="20%">&nbsp;
                                        </td>
                                        <td width="10%">&nbsp;
                                        </td>
                                        <td width="20%">&nbsp;
                                        </td>
                                        <td width="28%" align="right">
                                            <webControls:ExImage ID="imgCollapse" runat="server" SkinID="CollapseIcon" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>

                            <asp:Panel ID="pnlAccountSearch" runat="server">
                                <table width="100%" border="0" class="InputRequrementsTextNoBackColor">
                                    <td>
                                        <asp:UpdatePanel ID="upd" runat="server">
                                            <ContentTemplate>
                                                <UserControl:AccountSearchControl ID="ucAccountSearchControl" runat="server" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="ucAccountSearchControl" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <%--<col width="2%" />
                                            <col width="20%" />
                                            <col width="20%" />
                                            <col width="10%" />
                                            <col width="20%" />
                                            <col width="28%" />
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="lblAcNumber" LabelID="1357" runat="server" SkinID="Black11Arial"></webControls:ExLabel>:
                                                </td>
                                                <td>
                                                    <webControls:ExTextBox ID="txtAcNumber" runat="server" SkinID="ExTextBox150" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="lblAccname" runat="server" LabelID="1346" SkinID="Black11Arial"></webControls:ExLabel>:
                                                </td>
                                                <td>
                                                    <webControls:ExTextBox ID="ExTextBox1" runat="server" SkinID="ExTextBox150" />
                                                </td>
                                            </tr>
                                            <tr class="BlankRow">
                                                <td colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="lblFsCaption" LabelID="1337" runat="server" SkinID="Black11Arial"></webControls:ExLabel>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFsCaption" runat="server" SkinID="ExTextBox150" />
                                                    <img id="imgFSCaptionProgress" style="visibility: hidden" alt="imgProgress" src="../App_Themes/SkyStemBlueBrown/Images/progress_small.gif" />
                                                    <ajaxToolkit:AutoCompleteExtender TargetControlID="txtFsCaption" ServiceMethod="AutoCompleteFSCaption"
                                                        runat="server" ID="aceFSCaption" OnClientPopulating="ShowFSCaptionProgressIcon"
                                                        OnClientPopulated="ShowFSCaptionProgressIcon">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="ExLabel1" LabelID="1013" runat="server" SkinID="Black11Arial"></webControls:ExLabel>:
                                                </td>
                                                <td>
                                                    <UserControls:RiskRatingDDL ID="ucDDLRiskRating" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="BlankRow">
                                                <td colspan="6">
                                                </td>
                                            </tr>
                                            <tr id="trKeyAccount" runat="server">
                                                <td>
                                                </td>
                                                <td>
                                                    <webControls:ExLabel ID="lblIsKeyAccount" LabelID="1339" runat="server" SkinID="Black11Arial"></webControls:ExLabel>:
                                                </td>
                                                <td>
                                                    <webControls:ExRadioButton ID="optIsKeyAccountYes" runat="server" GroupName="IsKeyAccount"
                                                        TextAlign="Right" LabelID="1252" SkinID="OptBlack11Arial" />
                                                    &nbsp;
                                                    <webControls:ExRadioButton ID="optIsKeyAccountNo" runat="server" GroupName="IsKeyAccount"
                                                        TextAlign="Right" LabelID="1251" SkinID="OptBlack11Arial" />
                                                    &nbsp;
                                                    <webControls:ExRadioButton ID="optIsKeyAccountAll" runat="server" GroupName="IsKeyAccount"
                                                        TextAlign="Right" LabelID="1262" SkinID="OptBlack11Arial" />
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr class="BlankRow">
                                                <td colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="right">
                                                    <webControls:ExButton ID="btnSearch" runat="server" LabelID="1340 " OnClick="btnSearch_OnClick"
                                                        CausesValidation="false" />&nbsp;
                                                </td>
                                            </tr>--%>
                                    <tr class="BlankRow">
                                        <td colspan="6"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGridAccountSearchResult" runat="server"
                                                    Grid-AllowPaging="true" Grid-AllowSorting="true" Grid-EntityNameLabelID="1356"
                                                    Grid-AllowExportToExcel="True" Grid-AllowCauseValidationExportToExcel="false">
                                                    <SkyStemGridColumnCollection>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1357" HeaderStyle-Width="20%" UniqueName="AccountNumber"
                                                            SortExpression="AccountNumber" DataType="System.String">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1346" HeaderStyle-Width="15%" UniqueName="AccountName"
                                                            SortExpression="AccountName" DataType="System.String">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1256" SortExpression="ZeroBalance"
                                                            DataType="System.String" UniqueName="ZeroBalance" Visible="false">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblZeroBalance" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1014" SortExpression="KeyAccount"
                                                            DataType="System.String" UniqueName="KeyAccount" Visible="false">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblKeyAccount" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1013" SortExpression="RiskRating"
                                                            DataType="System.String" UniqueName="RiskRating" Visible="false">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblRiskRating" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn UniqueName="RiskRatingFrequency" LabelID="1427"
                                                            HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <Popup:RecFrequency ID="ucPopupRecFrequency" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1426" HeaderStyle-Width="15%" SortExpression="ReconciliationTemplate"
                                                            DataType="System.String">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblReconciliationTemplate" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName"
                                                            DataType="System.String" UniqueName="Preparer" Visible="true">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblPreparer" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="2501" SortExpression="BackupPreparerFullName"
                                                            DataType="System.String" UniqueName="BackupPreparer" Visible="false">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblBackupPreparer" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1131" SortExpression="ReviewerFullName"
                                                            DataType="System.String" UniqueName="Reviewer" Visible="true">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblReviewer" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="2502" SortExpression="BackupReviewerFullName"
                                                            DataType="System.String" UniqueName="BackupReviewer" Visible="false">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblBackupReviewer" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1132" SortExpression="ApproverFullName"
                                                            DataType="System.String" UniqueName="Approver" Visible="false">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblApprover" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="2503" SortExpression="BackupApproverFullName"
                                                            DataType="System.String" UniqueName="BackupApprover" Visible="false">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblBackupApprover" runat="server" />
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1417" HeaderStyle-Width="10%" UniqueName="PreparerDueDate"
                                                            SortExpression="PreparerDueDate" DataType="System.DateTime">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblPreparerDueDate" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1418" HeaderStyle-Width="10%" UniqueName="ReviewerDueDate"
                                                            SortExpression="ReviewerDueDate" DataType="System.DateTime">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblReviewerDueDate" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                        <telerikWebControls:ExGridTemplateColumn LabelID="1738" HeaderStyle-Width="10%" UniqueName="ApproverDueDate"
                                                            SortExpression="ApproverDueDate" DataType="System.DateTime">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblApproverDueDate" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                         <telerikWebControls:ExGridTemplateColumn LabelID="2944" HeaderStyle-Width="10%" UniqueName="CreationDate"
                                                            SortExpression="CreationPeriodEndDate" DataType="System.DateTime">
                                                            <ItemTemplate>
                                                                <webControls:ExLabel ID="lblCreationDate" runat="server"></webControls:ExLabel>
                                                            </ItemTemplate>
                                                        </telerikWebControls:ExGridTemplateColumn>
                                                    </SkyStemGridColumnCollection>
                                                    <ClientSettings>
                                                        <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                                                    </ClientSettings>
                                                </UserControl:SkyStemARTGrid>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="right">
                                            <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" OnClick="btnAdd_OnClick"
                                                Visible="false" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ajaxToolkit:CollapsiblePanelExtender ID="cpeAccountSearch" TargetControlID="pnlAccountSearch"
                                ImageControlID="imgCollapse" CollapseControlID="pnlHeader" ExpandControlID="pnlHeader"
                                runat="server" SkinID="CollapsiblePanelCollapsed">
                            </ajaxToolkit:CollapsiblePanelExtender>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td></td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </asp:Panel>
    <div>
        <UserControls:ProgressBar ID="ucProgressBar" runat="server" EnableTheming="true" />
    </div>
    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
