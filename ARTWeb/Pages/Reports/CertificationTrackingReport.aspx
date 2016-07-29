<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master" AutoEventWireup="true" Inherits="Pages_Reports_CertificationTrackingReport"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="CertificationTrackingReport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlGrid" runat="server" SkinID="RadGridScrollPanel">
                    <telerikWebControls:ExRadGrid ID="rgReport" runat="server" OnItemDataBound="rgReport_GridItemDataBound"
                        OnNeedDataSource="rgReport_NeedDataSourceEventHandler" AllowExportToPDF="True"
                        AllowExportToExcel="True" OnItemCreated="rgReport_ItemCreated" OnItemCommand="rgReport_OnItemCommand">
                        <MasterTableView Name="lavel1" ExpandCollapseColumn-Display="true" AllowNaturalSort="false">
                            <%--OnGridItemDataBound="rgReport_GridItemDataBound"--%>
                            <Columns>
                                <%--<telerikWebControls:ExGridSerialNumberColumn LabelID="2208">
                                </telerikWebControls:ExGridSerialNumberColumn>--%>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1278" SortExpression="Role" DataType="System.String"
                                    UniqueName="Role">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblRole" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1533 " SortExpression="FirstName"
                                    DataType="System.String" UniqueName="UserName">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblUserName" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1881  " ItemStyle-HorizontalAlign="Right"
                                    SortExpression="CountTotalAccountAssigned" HeaderStyle-Width="84px" ItemStyle-Width="84px">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCountTotalAccountAssigned" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1016   " SortExpression="MadatoryReportSignOffDate"
                                    UniqueName="MadatoryReportSignOff">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMadatoryReportSignOffDate" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1210   " SortExpression="CertificationBalancesDate"
                                    UniqueName="CertificationBalancesSignOff">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCertificationBalancesDate" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1211   " SortExpression="ExceptionCertificationDate"
                                    UniqueName="ExceptionCertificationSignOff">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblExceptionCertificationDate" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1072   " SortExpression="AccountCertificationDate"
                                    UniqueName="AccountCertificationSignOff">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblAccountCertificationDate" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
