<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GLDataRecurringScheduleItemsGrid.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_GLDataRecurringScheduleItemsGrid" %>
<table>
    <asp:Panel ID="pnlCurrency" runat="server">
        <tr>
            <td style="width: 30%">
                <webControls:ExLabel ID="lblOpenItemGrid" runat="server" LabelID="2466" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <%--Base Currency--%>
            <td style="width: 15%">
                <webControls:ExLabel ID="lblBaseCurrency" runat="server" FormatString="{0}:" LabelID="1493"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblBaseCurrencyValue" Text="0" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
            </td>
            <%--Reporting Currency--%>
            <td style="width: 15%">
                <webControls:ExLabel ID="lblReportingCurrency" runat="server" FormatString="{0}:"
                    LabelID="1424" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblReportingCurrencyValue" Text="0" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
            </td>
        </tr>
    </asp:Panel>
    <tr>
        <td style="width: 100%" colspan="5" class="">
            <telerikWebControls:ExRadGrid ID="rgGLDataRecurringScheduleItems" runat="server"
                OnItemDataBound="rgGLDataRecurringScheduleItems_ItemDataBound" OnNeedDataSource="rgGLDataRecurringScheduleItems_NeedDataSource"
                ClientSettings-Selecting-AllowRowSelect="true" AllowMultiRowSelection="true"
                AllowSorting="true" AllowPaging="true" OnItemCommand="rgGLDataRecurringScheduleItems_ItemCommand"
                OnPageIndexChanged="rgGLDataRecurringScheduleItems_PageIndexChanged" OnItemCreated="rgGLDataRecurringScheduleItems_ItemCreated"
                OnPageSizeChanged="rgGLDataRecurringScheduleItems_PageSizeChanged" OnPdfExporting="rgGLDataRecurringScheduleItems_PdfExporting"
                SkinID="SkyStemBlueBrownRecItems">
                <ClientSettings>
                    <Selecting UseClientSelectColumnOnly="true" />
                </ClientSettings>
                <MasterTableView ClientDataKeyNames="ScheduleAmountBaseCurrency, ScheduleAmountReportingCurrency"
                    DataKeyNames="GLDataRecurringItemScheduleID" Width="100%" ShowFooter="true" TableLayout="Auto"
                    Name="GLDataRecurringScheduleItemGridView">
                    <PagerTemplate>
                        <asp:Panel ID="PagerPanel" runat="server">
                            <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                <div style="float: left; margin-right: 10px;">
                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                    <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                        </asp:Panel>
                    </PagerTemplate>
                    <Columns>
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="true"
                            HeaderStyle-Width="5%">
                        </telerikWebControls:ExGridClientSelectColumn>
                        <%--ScheduleAmount--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleAmount" SortExpression="ScheduleAmount"
                            DataType="System.Decimal" LabelID="1700" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblScheduleAmountLCCY" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <webControls:ExLabel ID="lblTotal" LabelID="2512" runat="server"></webControls:ExLabel>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" Wrap="true" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Ref No For Import--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="RefNo" LabelID="1513" ItemStyle-HorizontalAlign="Right"
                            DataType="System.Int32">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblRefNo" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Open Date--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511 " SortExpression="OpenDate"
                            DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Schedule Name--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleName" SortExpression="ScheduleName"
                            LabelID="2052">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblScheduleName" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Schedule Begin Date--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleBeginDate" LabelID="2053"
                            SortExpression="ScheduleBeginDate" DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblScheduleBeginDate" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Schedule End Date--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleEndDate" LabelID="1450"
                            SortExpression="ScheduleEndDate" DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblScheduleEndDate" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <ItemStyle Wrap="false" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%-- LocalCurrencyCode for Import --%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="LCCYCode" LabelID="1773" Visible="true"
                            SortExpression="LocalCurrencyCode" DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblLocalCurrencyCode" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Amount For Import--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="AmountImport" LabelID="1675"
                            SortExpression="Amount" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAmountImport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Comments For Import--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Comments" LabelID="1408" SortExpression="Comments"
                            DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblDescription" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Close Date--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1411" SortExpression="CloseDate"
                            DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblCloseDate" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Original Amount RCCY--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="ScheduleAmountReportingCurrency"
                            ItemStyle-Width="50px" SortExpression="ScheduleAmountReportingCurrency" DataType="System.Decimal">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblOriginalAmountRCCY" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <webControls:ExLabel ID="lblOriginalAmountRCCYTotalValue" runat="server"></webControls:ExLabel>
                                </div>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Total Consumed Amount   UniqueName="RecPeriodAmountReportingCurrency" --%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="RecPeriodAmountReportingCurrency"
                            SortExpression="RecPeriodAmountReportingCurrency" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblConsumedAmountRCCY" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <webControls:ExLabel ID="lblConsumedAmountRCCYTotalValue" runat="server"></webControls:ExLabel>
                                </div>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Crrent RecPeriod Amount    --%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="CrrentRecPeriodAmountReportingCurrency"
                            SortExpression="CrrentRecPeriodAmount" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblCrrentRecPeriodAmountRCCY" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Remaining  Amount RCCY--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="BalanceReportingCurrency" LabelID="1701"
                            SortExpression="BalanceReportingCurrency">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblBalanceRCCY" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <webControls:ExLabel ID="lblBalanceRCCYTotalValue" runat="server"></webControls:ExLabel>
                                </div>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--AttachmentCount--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="AttachmentCount" LabelID="2056"
                            SortExpression="AttachmentCount" ItemStyle-Width="10px" DataType="System.Int32">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlAttachmentCount" runat="server"></webControls:ExHyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%----Rec Item # ----%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" SortExpression="RecItemNumber"
                            LabelID="2118 ">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%----MatchSetRef# ----%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetRefNumber" SortExpression="MatchSetRefNumber"
                            LabelID="2276 ">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlMatchSetRefNumber" runat="server"></webControls:ExHyperLink>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                         <%--RecItemComment For Import--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemCommentForImport" LabelID="2749"
                            DataType="System.String" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblRecItemComments" runat="server" SkinID="Black9Arial"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FooterStyle HorizontalAlign="Right" />
            </telerikWebControls:ExRadGrid>
        </td>
    </tr>
</table>
