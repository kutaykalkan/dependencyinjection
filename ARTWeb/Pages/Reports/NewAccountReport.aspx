<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master" AutoEventWireup="true"
    CodeFile="NewAccountReport.aspx.cs" Inherits="Pages_Reports_NewAccountReport"
    Title="Untitled Page" Theme="SkyStemBlueBrown" %>

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
                            <telerikWebControls:ExGridTemplateColumn LabelID="1420" SortExpression="CreationPeriod"
                                DataType="System.DateTime" UniqueName="CreationPeriod">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblCreationPeriod" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1308" SortExpression="ProfileName"
                                DataType="System.String" UniqueName="ProfileName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblProfileName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1679" SortExpression="UploadedBy"
                                DataType="System.String" UniqueName="UploadedBy">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblUploadedBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" SortExpression="DateUploaded"
                                DataType="System.DateTime" UniqueName="DateUploaded">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateUploaded" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </SkyStemGridColumnCollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
