<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/CertificationMasterPagePrint.master"
    AutoEventWireup="true" CodeFile="CertificationBalancesPrint.aspx.cs" Inherits="Pages_CertificationPrint_CertificationBalancesPrint"
    Theme="SkyStemBlueBrown" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCertification" runat="Server">
    <UserControl:SkyStemARTGrid Grid-SkinID="SkyStemBlueBrownPrint" Width="100%" ID="ucSkyStemARTGrid"
        runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound">
        <SkyStemGridColumnCollection>
            <telerikWebControls:ExGridTemplateColumn LabelID="1357 ">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlAccountNumber" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1346 ">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlAccountName" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn UniqueName="AmountReportingCurrency">
                <%--LabelID="1382 "  DataType="System.Decimal"  --%>
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlGLBalance" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1013 " UniqueName="RiskRating">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlRiskRating" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1433 " UniqueName="Materiality">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlIsMaterial" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1014  " UniqueName="KeyAccount">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlIsKeyAccount" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1130  ">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlPreparer" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="2501" SortExpression="BackupPreparerFullName"
                DataType="System.String" UniqueName="BackupPreparer" Visible="false">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlBackupPreparer" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1131  ">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlReviewer" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="2502" SortExpression="BackupReviewerFullName"
                DataType="System.String" UniqueName="BackupReviewer" Visible="false">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlBackupReviewer" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1132  " UniqueName="Approver">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlApprover" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="2503" SortExpression="BackupApproverFullName"
                DataType="System.String" UniqueName="BackupApprover" Visible="false">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlBackupApprover" runat="server" SkinID="GridHyperLink" />
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
        </SkyStemGridColumnCollection>
    </UserControl:SkyStemARTGrid>
</asp:Content>
