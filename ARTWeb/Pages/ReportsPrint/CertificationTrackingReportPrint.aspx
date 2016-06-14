<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewerPrint.master" AutoEventWireup="true" Inherits="Pages_ReportsPrint_CertificationTrackingReportPrint"
    Title="Untitled Page" Theme="SkyStemBlueBrown" Codebehind="CertificationTrackingReportPrint.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <telerikWebControls:ExRadGrid ID="rgReport" runat="server" OnItemDataBound="rgReport_GridItemDataBound"
        SkinID="SkyStemBlueBrownPrint">
        <MasterTableView Name="lavel1" ExpandCollapseColumn-Display="true">
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
                    SortExpression="CountTotalAccountAssigned" HeaderStyle-Width="120px" ItemStyle-Width="100px">
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
</asp:Content>
