<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_NetAccountComposition" Codebehind="NetAccountComposition.ascx.cs" %>
<%@ Register Src="SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/AccountHierarchyDetail.ascx" TagName="AccountHierarchyDetail" TagPrefix="UserControl" %>


<UserControl:AccountHierarchyDetail ID="ucAccountHierarchyDetail" runat="server" /> 
&nbsp

<UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid"  runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound">
    <SkyStemGridColumnCollection>
        <telerikWebControls:ExGridTemplateColumn  LabelID="1357" DataType="System.String"
            UniqueName="AccountNumber" HeaderStyle-Width="20%">
            <ItemTemplate>
                <webControls:ExLabel ID="lblAccountNumber" runat="server" />
            </ItemTemplate>
        </telerikWebControls:ExGridTemplateColumn>
        <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="20%"  LabelID="1346" SortExpression="AccountName"
            DataType="System.String" UniqueName="AccountName">
            <ItemTemplate>
                <webControls:ExLabel ID="lblAccountName" runat="server" SkinID="GridHyperLink" />
            </ItemTemplate>
        </telerikWebControls:ExGridTemplateColumn>
        <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="20%"  SortExpression="GLBalanceReportingCurrency"
            DataType="System.Decimal" UniqueName="GLBalanceBaseCurrency" Visible="true">
            <ItemTemplate>
                <webControls:ExLabel ID="lblGLBalanceBaseCurrency" runat="server"  />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </telerikWebControls:ExGridTemplateColumn>
        <telerikWebControls:ExGridTemplateColumn HeaderStyle-Width="20%"   SortExpression="ReconciliationBalanceReportingCurrency"
            DataType="System.Decimal" UniqueName="GLBalanceReportingCurrency" Visible="true">
            <ItemTemplate>
                <webControls:ExLabel ID="lblGLBalanceReportingCurrency" runat="server" />
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </telerikWebControls:ExGridTemplateColumn>
    </SkyStemGridColumnCollection>
</UserControl:SkyStemARTGrid>
