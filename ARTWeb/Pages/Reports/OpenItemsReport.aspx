<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master" AutoEventWireup="true"
    CodeFile="OpenItemsReport.aspx.cs" Inherits="Pages_Reports_OpenItemsReport" Title="Untitled Page"
    Theme="SkyStemBlueBrown" %>

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
                            <%----Rec Item # ----%>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" LabelID="2118 ">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            
                            <telerikWebControls:ExGridTemplateColumn LabelID="1014  " UniqueName="KeyAccount"
                                SortExpression="IsKeyAccount">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsKeyAccount" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1433 " UniqueName="Materiality"
                                SortExpression="IsMaterial">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsMaterial" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1013 " UniqueName="RiskRating"
                                SortExpression="RiskRating">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRiskRating" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Right" SortExpression="RecItemAmountReportingCurrency"
                                DataType="System.Decimal" UniqueName="AmountReportingCurrency" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="120px" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblGLBalanceRC" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <%--LabelID="1382"--%>
                            <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Right" SortExpression="RecItemAmountBaseCurrency"
                                DataType="System.Decimal" UniqueName="AmountBaseCurrency" HeaderStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="120px" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblGLBalanceBC" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1511  " SortExpression="OpenDate">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblOpenDate" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1512  " SortExpression="AgingInDays"
                                ItemStyle-HorizontalAlign="Right" DataType="System.Int16">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAging" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1886   " SortExpression="OpenItemClassification">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblClassification" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFirstName"
                                DataType="System.String" UniqueName="Preparer">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblPreparer" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                             <telerikWebControls:ExGridTemplateColumn LabelID="1978" SortExpression="PeriodEndDate"
                                DataType="System.String" UniqueName="PeriodEndDate">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblPeriodEndDate" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </SkyStemGridColumnCollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
