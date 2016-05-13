<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewerPrint.master" AutoEventWireup="true"
    CodeFile="ReconciliationStatusCountReportPrint.aspx.cs" Inherits="Pages_ReportsPrint_ReconciliationStatusCountReportPrint"
    Title="Untitled Page" Theme="SkyStemBlueBrown" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <telerikWebControls:ExRadGrid ID="rgReport" runat="server" OnItemDataBound="rgReport_GridItemDataBound"
        OnPreRender="rgReconciliationStatusByFSCaption_PreRender" SkinID="SkyStemBlueBrownPrint">
        <MasterTableView Name="lavel1" ExpandCollapseColumn-Display="false">
            <%--OnGridItemDataBound="rgReport_GridItemDataBound"--%>
            <Columns>
                <telerikWebControls:ExGridSerialNumberColumn LabelID="2208">
                </telerikWebControls:ExGridSerialNumberColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1278" SortExpression="Role" ItemStyle-HorizontalAlign="Left"
                    DataType="System.String" UniqueName="Role">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblRole" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1533 " ItemStyle-HorizontalAlign="Left"
                    SortExpression="FirstName" DataType="System.String" UniqueName="UserName">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblUserName" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1089 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountPrepared">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountPrepared" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1090 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountInProgress">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountInProgress" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1091 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountPendingReview">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountPendingReview" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1755 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountPendingModificationPreparer">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountPendingModificationPreparer" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1093 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountReviewed">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountReviewed" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1094 " UniqueName="PendingApproval"
                    ItemStyle-HorizontalAlign="Right" SortExpression="CountPendingApproval">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountPendingApproval" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1095 " UniqueName="Approved" ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountApproved">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountApproved" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1475 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountNotstarted">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountNotstarted" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1097 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountSysReconciled">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountSysReconciled" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1739 " ItemStyle-HorizontalAlign="Right"
                    SortExpression="CountReconciled">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountReconciled" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
                <telerikWebControls:ExGridTemplateColumn LabelID="1756 " UniqueName="PendingModificationReviewer"
                    ItemStyle-HorizontalAlign="Right" SortExpression="CountPendingModificationReviewer">
                    <ItemTemplate>
                        <webControls:ExLabel ID="lblCountPendingModificationReviewer" runat="server" SkinID="GridReportLabel" />
                    </ItemTemplate>
                </telerikWebControls:ExGridTemplateColumn>
            </Columns>
        </MasterTableView>
    </telerikWebControls:ExRadGrid>
</asp:Content>
