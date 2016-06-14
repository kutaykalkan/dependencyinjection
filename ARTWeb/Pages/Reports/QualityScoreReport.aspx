<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master" AutoEventWireup="true" Inherits="Pages_Reports_QualityScoreReport"
    Theme="SkyStemBlueBrown" Codebehind="QualityScoreReport.aspx.cs" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="EditQualityScore" Src="~/UserControls/QualityScore/EditQualityScore.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                        OnGrid_NeedDataSourceEventHandler="ucSkyStemARTGrid_NeedDataSourceEventHandler"
                        Grid-MasterTableView-DataKeyNames="GLDataID" Grid-AllowPaging="True" Grid-AllowExportToExcel="True"
                        Grid-AllowExportToPDF="false" ShowSerialNumberColumn="false" OnGridDetailTableDataBind="ucSkyStemARTGrid_GridDetailTableDataBind"
                        HierarchyLoadMode="ServerBind">
                        <DetailTables>
                            <telerik:GridTableView NoDetailRecordsText="" Name="QualityScoreDetails" runat="server"
                                DataKeyNames="GLDataID" AllowPaging="false" AllowSorting="false" Width="100%">
                                <Columns>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2419" UniqueName="QualityScoreNumber"
                                        HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblQualityScoreNumber" runat="server" SkinID="GridReportLabel" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2418" UniqueName="QualityScoreDescription"
                                        HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblQualityScoreDescription" runat="server" SkinID="GridReportLabel" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2444" UniqueName="SystemQualityScoreStatus"
                                        HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblSystemQualityScoreStatus" runat="server" SkinID="GridReportLabel" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="2445" UniqueName="UserQualityScoreStatus"
                                        HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblUserQualityScoreStatus" runat="server" SkinID="GridReportLabel" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1848" UniqueName="Comments" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblComments" runat="server" SkinID="GridReportLabel" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <SkyStemGridColumnCollection>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                                DataType="System.String" UniqueName="AccountNumber" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountNumber" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                                DataType="System.String" UniqueName="AccountName" HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2442" SortExpression="SystemScore"
                                DataType="System.String" UniqueName="SystemScore" HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblSystemScore" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2443" SortExpression="UserScore"
                                DataType="System.String" UniqueName="UserScore" HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblUserScore" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </SkyStemGridColumnCollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
