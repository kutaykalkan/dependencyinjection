<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true" Theme="SkyStemBlueBrown" Inherits="Pages_AccountInformation"
    Title="Untitled Page" Codebehind="AccountInformation.aspx.cs" %>

<%@ Register Src="~/UserControls/AccountSearchControl.ascx" TagName="AccountSearchControl"
    TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/LegendOnAccountSearch.ascx" TagName="LegendOnAccountSearch"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="Popup" TagName="RecFrequency" Src="~/UserControls/PopupRecFrequency.ascx" %>
<%@ Register TagPrefix="Popup" TagName="RecFrequencySelection" Src="~/UserControls/PopupRecFrequencySelection.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upnlAccountProfile" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <UserControl:AccountSearchControl ID="ucAccountSearchControl" runat="server" />
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td></td>
                </tr>
                <asp:Panel ID="pnlAccounts" runat="server" Visible="false">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                                <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" Grid-AllowPaging="True" Grid-AllowExportToExcel="True"
                                    OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound" Grid-AllowExportToPDF="True"
                                    runat="server">
                                    <SkyStemGridColumnCollection>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="AccountNumber" LabelID="1357"
                                            HeaderStyle-Width="6%" SortExpression="AccountNumber" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountNumber" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1346" UniqueName="AccountName"
                                            HeaderStyle-Width="10%" SortExpression="AccountName" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblAccountName" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1370" SortExpression="ReconciliationStatus"
                                            DataType="System.String" UniqueName="ReconciliationStatus">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReconciliationStatus" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1374" SortExpression="CertificationStatus"
                                            DataType="System.String" UniqueName="CertificationStatus">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblCertificationStatus" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1382" HeaderStyle-Width="10%" SortExpression="AccountGLBalance"
                                            DataType="System.Decimal" UniqueName="GLBalance">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblGLBalance" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1257" UniqueName="NetAccount" HeaderStyle-Width="10%"
                                            SortExpression="NetAccount" DataType="System.String" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblNetAccount" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1372" SortExpression="Materiality"
                                            DataType="System.String" UniqueName="Materiality">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblMateriality" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="ZeroBalance" LabelID="1256"
                                            HeaderStyle-Width="10%" SortExpression="ZeroBalance" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblZeroBalanceAccount" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="KeyAccount" LabelID="1014" HeaderStyle-Width="10%"
                                            SortExpression="KeyAccount" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblKeyAccount" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="RiskRating" LabelID="1013" HeaderStyle-Width="10%"
                                            SortExpression="RiskRating" DataType="System.String">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblRiskRating" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <%--IsReconcilable--%>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="IsReconcilable" LabelID="2401"
                                            HeaderStyle-Width="8%" SortExpression="Reconcilable">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblIsReconcilable" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1130" UniqueName="Preparer" HeaderStyle-Width="10%"
                                            SortExpression="PreparerFullName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPreparer" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2501" SortExpression="BackupPreparerFullName"
                                            DataType="System.String" UniqueName="BackupPreparer">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBackupPreparer" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1131" UniqueName="Reviewer" HeaderStyle-Width="10%"
                                            SortExpression="ReviewerFullName" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReviewer" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2502" SortExpression="BackupReviewerFullName"
                                            DataType="System.String" UniqueName="BackupReviewer">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBackupReviewer" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1132" HeaderStyle-Width="10%" UniqueName="Approver"
                                            SortExpression="ApproverFullName">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblApprover" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2503" SortExpression="BackupApproverFullName"
                                            DataType="System.String" UniqueName="BackupApprover">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblBackupApprover" runat="server" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2752" HeaderStyle-Width="10%" UniqueName="PreparerDueDays"
                                            SortExpression="PreparerDueDays" DataType="System.Int32">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPreparerDueDays" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2753" HeaderStyle-Width="10%" UniqueName="ReviewerDueDays"
                                            SortExpression="ReviewerDueDays" DataType="System.Int32">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReviewerDueDays" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2754" HeaderStyle-Width="10%" UniqueName="ApproverDueDays"
                                            SortExpression="ApproverDueDays" DataType="System.Int32">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblApproverDueDays" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1417" SortExpression="PreparerDueDate"
                                            DataType="System.DateTime" UniqueName="PreparerDueDate" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblPreparerDueDate" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1418" SortExpression="ReviewerDueDate"
                                            DataType="System.DateTime" UniqueName="ReviewerDueDate" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblReviewerDueDate" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1738" SortExpression="ApproverDueDate"
                                            DataType="System.DateTime" UniqueName="ApproverDueDate" Visible="false">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblApproverDueDate" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn UniqueName="RecFrequency" LabelID="1427"
                                            HeaderStyle-Width="6%" Visible="false">
                                            <ItemTemplate>
                                                <Popup:RecFrequencySelection ID="ucRecFrequencySelection" runat="server" />
                                                <input type="text" id="txtRecPeriodsContainer" runat="server" style="display: none" />
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="2944" HeaderStyle-Width="10%" UniqueName="CreationDate"
                                            SortExpression="CreationPeriodEndDate" DataType="System.DateTime">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblCreationDate" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                    </SkyStemGridColumnCollection>
                                </UserControl:SkyStemARTGrid>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td></td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>
                        <UserControl:LegendOnAccountSearch ID="LegendOnAccountSearch" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucAccountProfileMassAndBulkUpdate" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="upnlAccountProfile" Visible="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
