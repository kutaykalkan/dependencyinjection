<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ReportViewerPrint.master"
    AutoEventWireup="true" CodeFile="ExceptionStatusReportPrint.aspx.cs" Inherits="Pages_ReportsPrint_ExceptionStatusReportPrint"
    Theme="SkyStemBlueBrown" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" Grid-AllowPaging="false"
        Grid-AllowExportToExcel="false" Grid-AllowExportToPDF="false" Grid-AllowPrint="false" Grid-AllowPrintAll="false"
        OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound" Grid-EntityNameLabelID="1111"
        Grid-SkinID="SkyStemBlueBrownPrint" ShowSerialNumberColumn="false">
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
            <telerikWebControls:ExGridTemplateColumn LabelID="1013 " UniqueName="RiskRating" HeaderStyle-Wrap="false"
                SortExpression="RiskRating">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblRiskRating" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1433 " UniqueName="Materiality"
                SortExpression="IsMaterial">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblIsMaterial" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1014  " UniqueName="KeyAccount"
                SortExpression="IsKeyAccount">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblIsKeyAccount" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                <ItemStyle HorizontalAlign="Center" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1425" SortExpression="WriteOnOffAmountReportingCurrency"
                DataType="System.Decimal" UniqueName="WriteOnOffAmountReportingCurrency">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblWriteOnOff" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1678" SortExpression="UnexplainedVarianceReportingCurrency"
                DataType="System.Decimal" UniqueName="UnexplainedVarianceReportingCurrency">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblUnexplainedVariance" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1907" SortExpression="DelinquentAmountReportingCurrency"
                DataType="System.Decimal" UniqueName="DelinquentAmountReportingCurrency">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblDelinquentAmount" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="PreparerFullName"
                DataType="System.String" UniqueName="PreparerFullName">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblPreparerFullName" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
        </SkyStemGridColumnCollection>
    </UserControl:SkyStemARTGrid>
</asp:Content>
