﻿<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master" AutoEventWireup="true" Inherits="Pages_Reports_IncompleteAccountAttributeReport"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="IncompleteAccountAttributeReport.aspx.cs" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                        OnGrid_NeedDataSourceEventHandler="ucSkyStemARTGrid_NeedDataSourceEventHandler"
                        Grid-AllowPaging="True" Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="false"
                        ShowSerialNumberColumn="false">
                        <SkyStemGridColumnCollection>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                                DataType="System.String" UniqueName="AccountNumber">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountNumber" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                                DataType="System.String" UniqueName="AccountName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1014  " UniqueName="KeyAccount"
                                SortExpression="IsKeyAccountAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsKeyAccount" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1013 " UniqueName="RiskRating"
                                SortExpression="IsRiskRatingAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRiskRating" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1427" UniqueName="RiskRatingFrequency"
                                SortExpression="IsFrequencyAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblFrequency" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1434  " UniqueName="ZeroBalance"
                                SortExpression="IsZeroBalanceAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsZeroBalance" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1366" UniqueName="RecFormType"
                                SortExpression="IsTemplateAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRecFormType" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2752" UniqueName="PreparerDueDays"
                                SortExpression="IsPreparerDueDaysAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsPreparerDueDaysMissing" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2753" UniqueName="ReviewerDueDays"
                                SortExpression="IsReviewerDueDaysAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsReviewerDueDaysMissing" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2754" UniqueName="ApproverDueDays"
                                SortExpression="IsApproverDueDaysAttributeMissing">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsApproverDueDaysMissing" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </SkyStemGridColumnCollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
