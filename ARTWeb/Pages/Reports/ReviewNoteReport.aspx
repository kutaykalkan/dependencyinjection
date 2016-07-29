<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master" AutoEventWireup="true" Inherits="Pages_Reports_ReviewNoteReport"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="ReviewNoteReport.aspx.cs" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid" runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound"
                        OnGrid_NeedDataSourceEventHandler="ucSkyStemARTGrid_NeedDataSourceEventHandler"
                        grid-allowpaging="True" grid-allowexporttoexcel="True" grid-allowexporttopdf="False"
                        ShowSerialNumberColumn="false">
                        <SkyStemGridColumnCollection>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1420" UniqueName="Period" SortExpression="Period">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblPeriod" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber"
                                DataType="System.String" UniqueName="AccountNumber">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountNumber" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName"
                                HeaderStyle-Width="100px" ItemStyle-Width="90px" DataType="System.String" UniqueName="AccountName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAccountName" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1130" SortExpression="Perparer"
                                HeaderStyle-Width="100px" ItemStyle-Width="90px" DataType="System.String" UniqueName="Perparer">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblPreparer" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1131" SortExpression="Reviewer"
                                HeaderStyle-Width="100px" ItemStyle-Width="90px" DataType="System.String" UniqueName="Reviewer">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReviewer" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1399" UniqueName="ReviewNoteDate"
                                SortExpression="ReviewNoteDate">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReviewNoteDate" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1778" SortExpression="Subject"
                                HeaderStyle-Width="100px" ItemStyle-Width="90px" DataType="System.String" UniqueName="Subject">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblSubject" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1508" SortExpression="AddedByFullName"
                                HeaderStyle-Width="100px" ItemStyle-Width="90px" DataType="System.String" UniqueName="AddedByFullName">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblEnteredBy" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1278" SortExpression="AddedByUserRole" DataType="System.Int16"
                                UniqueName="AddedByUserRole">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblRole" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1394" SortExpression="ReviewNote"
                                HeaderStyle-Width="50%" ItemStyle-Width="50%" DataType="System.String" UniqueName="ReviewNote">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReviewNotes" runat="server" SkinID="GridReportLabel" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </SkyStemGridColumnCollection>
                    </UserControl:SkyStemARTGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
