<%@ Page Language="C#" MasterPageFile="~/MasterPages/ReportViewer.master" AutoEventWireup="true"
    CodeFile="AccountOwnershipReport.aspx.cs" Inherits="Pages_Reports_AccountOwnershipReport"
    Title="Untitled Page" Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphRptViewer" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="BlankRow" colspan="2">
                 
            </td>
        </tr>
         <tr>
            <td style="padding-left: 5px">
               <webControls:ExLabel ID="lblGrandTotalTitle" SkinID="Black11Arial" runat="server" LabelID="1663"/>  
               <webControls:ExLabel ID="lblTotalRecAccounts" SkinID="Black11Arial" runat="server" /> 
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlGrid" runat="server">
                    <telerikWebControls:ExRadGrid ID="rgReport" runat="server" OnItemDataBound="rgReport_GridItemDataBound"
                        OnNeedDataSource="rgReport_NeedDataSourceEventHandler" OnItemCreated="rgReport_ItemCreated"
                        OnItemCommand="rgReport_OnItemCommand" AllowSorting="true" AllowExportToPDF="False"
                        AllowExportToExcel="True">
                         
                        <MasterTableView Name="lavel1" AllowNaturalSort="false" ShowFooter="false">
                            <%--OnGridItemDataBound="rgReport_GridItemDataBound"--%>
                            <Columns>
                                <%--<telerikWebControls:ExGridSerialNumberColumn LabelID="2208"></telerikWebControls:ExGridSerialNumberColumn>--%>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1278" SortExpression="Role" DataType="System.String"
                                    UniqueName="Role">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblRole" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1533 " SortExpression="Name" DataType="System.String"
                                    UniqueName="UserName" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblUserName" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                 <FooterTemplate>
                                        <webControls:ExLabel ID="lblGrandTotalTitle" runat="server" CssClass="GridFooterRightAlignement" SkinID="GridReportLabel" />
                                    </FooterTemplate>
                                  
                                    <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1881" UniqueName="TotalAccountAssigned" 
                                SortExpression="CountTotalAccountAssigned" DataType="System.Decimal">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCountTotalAccountAssigned" runat="server"  SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                 <FooterTemplate>
                                        <webControls:ExLabel Width="100%" ID="lblSumTotalAccountAssigned" CssClass="GridFooterRightAlignement"  runat="server"/>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                   <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1882" SortExpression="PercentAccountAssigned"
                                 DataType="System.Decimal">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPercentAccountAssigned" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1883" SortExpression="CountKeyAccounts"
                                    UniqueName="KeyAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCountKeyAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                   <FooterTemplate>
                                        <webControls:ExLabel ID="lblSumCountKeyAccounts" CssClass="GridFooterRightAlignement" Width="100%"  runat="server"/>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                   <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1949" SortExpression="PercentKeyAccounts"
                                    UniqueName="PercentKeyAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPercentKeyAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1127" SortExpression="CountHighAccounts"
                                    UniqueName="HighAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCountHighAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                   <FooterTemplate>
                                        <webControls:ExLabel ID="lblSumCountHighAccounts" CssClass="GridFooterRightAlignement" Width="100%" runat="server"/>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1950" SortExpression="PercentHighAccounts"
                                    UniqueName="PercentHighAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPercentHighAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1128" SortExpression="CountMediumAccounts"
                                    UniqueName="MediumAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCountMediumAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                   <FooterTemplate>
                                        <webControls:ExLabel ID="lblSumCountMediumAccounts" CssClass="GridFooterRightAlignement" Width="100%" runat="server"/>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1951" SortExpression="PercentMediumAccounts"
                                    UniqueName="PercentMediumAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPercentMediumAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1129" SortExpression="CountLowAccounts"
                                    UniqueName="LowAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCountLowAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                   <FooterTemplate>
                                        <webControls:ExLabel ID="lblSumCountLowAccounts" CssClass="GridFooterRightAlignement" Width="100%" runat="server"/>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                   <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1952" SortExpression="PercentLowAccounts"
                                    UniqueName="PercentLowAccount">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPercentLowAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1433" SortExpression="CountMaterialAccounts"
                                    UniqueName="Materiality">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCountMaterialAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <webControls:ExLabel ID="lblSumCountMaterialAccounts" CssClass="GridFooterRightAlignement" Width="100%" runat="server"/>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1953" SortExpression="PercentMaterialAccounts"
                                    UniqueName="PercentMateriality">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPercentMaterialAccounts" runat="server" SkinID="GridReportLabel" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </asp:Panel>
            </td>
        </tr>
    <%--    <tr>
            <td>
                <webControls:ExLabel ID="lblTotalTotalNoOfAccounts" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td align="left">
                <webControls:ExLabel ID="lblTotalTotalNoOfAccountsValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblTotalKey" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td align="left">
                <webControls:ExLabel ID="lblTotalKeyValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblTotalHigh" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td align="left">
                <webControls:ExLabel ID="lblTotalHighValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblTotalMedium" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td align="left">
                <webControls:ExLabel ID="lblTotalMediumValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblTotalLow" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td align="left">
                <webControls:ExLabel ID="lblTotalLowValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblTotalMaterial" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td align="left">
                <webControls:ExLabel ID="lblTotalMaterialValue" runat="server" SkinID="LeftInfoPaneValue"></webControls:ExLabel>
            </td>
        </tr>--%>
    </table>
</asp:Content>
