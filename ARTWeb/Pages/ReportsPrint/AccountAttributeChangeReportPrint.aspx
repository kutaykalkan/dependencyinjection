<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewerPrint.master" AutoEventWireup="true"
    CodeFile="AccountAttributeChangeReportPrint.aspx.cs" Inherits="Pages_ReportsPrint_AccountAttributeChangeReportPrint"
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
            <telerikWebControls:ExGridTemplateColumn LabelID="2697" SortExpression="AccountAttribute"
                DataType="System.String" UniqueName="AccountAttribute">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblAccountAttribute" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1944" SortExpression="Value" DataType="System.String"
                UniqueName="Value">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblValue" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="2698" SortExpression="ValidFrom"
                DataType="System.DateTime" UniqueName="ValidFrom">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblValidFrom" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="2699" SortExpression="ValidUntil"
                DataType="System.DateTime" UniqueName="ValidUntil">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblValidUntil" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1333" SortExpression="ChangeDate"
                DataType="System.DateTime" UniqueName="ChangeDate">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblChangeDate" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1395" SortExpression="ChangePeriod"
                DataType="System.DateTime" UniqueName="ChangePeriod">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblChangePeriod" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1505" SortExpression="UpdatedBy"
                DataType="System.String" UniqueName="UpdatedBy">
                <ItemTemplate>
                    <webControls:ExLabel ID="lblUpdatedBy" runat="server" SkinID="GridReportLabel" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
        </SkyStemGridColumnCollection>
    </UserControl:SkyStemARTGrid>
</asp:Content>
