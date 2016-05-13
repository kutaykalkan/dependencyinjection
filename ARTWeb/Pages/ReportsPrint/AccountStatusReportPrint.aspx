<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewerPrint.master" AutoEventWireup="true"
    CodeFile="AccountStatusReportPrint.aspx.cs" Inherits="Pages_ReportsPrint_AccountStatusReportPrint"
    Title="Untitled Page" Theme="SkyStemBlueBrown" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
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
            <telerikWebControls:ExGridTemplateColumn LabelID="1014  " UniqueName="KeyAccount">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblIsKeyAccount" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1433 " UniqueName="Materiality">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblIsMaterial" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1013 " UniqueName="RiskRating">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblRiskRating" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
          
            <%--LabelID="1382"--%>
            <telerikWebControls:ExGridTemplateColumn SortExpression="GLBalanceBaseCurrency" DataType="System.Decimal"
                UniqueName="AmountBaseCurrency">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblGLBalanceBC" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn SortExpression="GLBalanceReportingCurrency"
                DataType="System.Decimal" UniqueName="AmountReportingCurrency">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblGLBalanceRC" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1370   " SortExpression="ReconciliationStatus">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblReconciliationStatus" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
        </SkyStemGridColumnCollection>
    </UserControl:SkyStemARTGrid>
</asp:Content>
