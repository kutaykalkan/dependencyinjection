<%@ Page Title="Untitled Page" Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master"
    AutoEventWireup="true" Inherits="Pages_Reports_CompletionDateReport"
    Theme="SkyStemBlueBrown" Codebehind="CompletionDateReport.aspx.cs" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                        OnGrid_NeedDataSourceEventHandler="ucSkyStemARTGrid_NeedDataSourceEventHandler"
                        Grid-AllowPaging="True" Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="False"
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
                            <telerikWebControls:ExGridTemplateColumn LabelID="1370" SortExpression="ReconciliationStatus">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReconciliationStatus" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <%--SRA--%>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2638" SortExpression="IsSRA">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsSRA" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <%--PreparedBy--%>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2639" SortExpression="DatePrepared">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDatePrepared" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1500" SortExpression="PreparedBy"
                                DataType="System.String" UniqueName="PreparedBy">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblPreparedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            
                            <%--ReviewedBy--%>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2641" SortExpression="DateReviewed">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateReviewed" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2640" SortExpression="ReviewedBy"
                                DataType="System.String" UniqueName="ReviewedBy">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReviewedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            
                            <%--ApprovedBy--%>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1435" SortExpression="DateApproved">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateApproved" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2642" SortExpression="ApprovedBy"
                                DataType="System.String" UniqueName="ApprovedBy">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblApprovedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            
                            <%--ReconciledBy--%>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2643" SortExpression="DateReconciled">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateReconciled" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <telerikWebControls:ExGridTemplateColumn LabelID="2656" SortExpression="ReconciledBy"
                                DataType="System.String" UniqueName="ReconciledBy">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReconciledBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <telerikWebControls:ExGridTemplateColumn LabelID="2657" SortExpression="SysReconciledBy"
                                DataType="System.String" UniqueName="SysReconciledBy">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblSysReconciledBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </SkyStemGridColumnCollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
