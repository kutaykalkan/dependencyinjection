<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewerPrint.master" AutoEventWireup="true"
    CodeFile="DelinquentAccountByUserReportPrint.aspx.cs" Inherits="Pages_ReportsPrint_DelinquentAccountByUserReportPrint"
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
            <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName" HeaderStyle-Width="100px" ItemStyle-Width="90px"
                DataType="System.String" UniqueName="AccountName">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblAccountName" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="140px" ItemStyle-Width="130px" SortExpression="GLBalanceReportingCurrency"
                DataType="System.Decimal" UniqueName="AmountReportingCurrency">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblGLBalanceRC" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
          
            <%--LabelID="1382"--%>
            <telerikWebControls:ExGridTemplateColumn ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="140px" ItemStyle-Width="130px" SortExpression="GLBalanceBaseCurrency" DataType="System.Decimal"
                UniqueName="AmountBaseCurrency">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblGLBalanceBC" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1278" SortExpression="Role" DataType="System.String"
                UniqueName="Role">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblRole" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1533 " SortExpression="FirstName" HeaderStyle-Width="100px" ItemStyle-Width="100px"   
                DataType="System.String" UniqueName="UserName">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblUserName" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1499    " UniqueName="DueDate"
                SortExpression="DueDate">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblDueDate" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1905   " SortExpression="DaysLate" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblDaysLate" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1906    " ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="80px" ItemStyle-Width="80px" SortExpression="CountDelinquentAccount">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblCountDelinquentAccount" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
        </SkyStemGridColumnCollection>
    </UserControl:SkyStemARTGrid>
</asp:Content>
