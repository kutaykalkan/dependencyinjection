<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GLDataWriteOnOffGrid.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_GLDataWriteOnOffGrid" %>
<table width="100%" cellpadding="0px" cellspacing="0px">
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
            <telerikWebControls:ExRadGrid ID="rgGLDataWriteOnOffItems" runat="server" OnItemDataBound="rgGLDataWriteOnOffItems_ItemDataBound"
                OnNeedDataSource="rgGLDataWriteOnOffItems_NeedDataSource" ClientSettings-Selecting-AllowRowSelect="true"
                AllowMultiRowSelection="true" AllowSorting="true" AllowPaging="true" OnItemCommand="rgGLDataWriteOnOffItems_ItemCommand"
                OnPageIndexChanged="rgGLDataWriteOnOffItems_PageIndexChanged" OnItemCreated="rgGLDataWriteOnOffItems_ItemCreated"
                OnPageSizeChanged="rgGLDataWriteOnOffItems_PageSizeChanged" OnPdfExporting="rgGLDataWriteOnOffItems_PdfExporting"
                SkinID="SkyStemBlueBrownRecItems">
                <ClientSettings>
                    <Selecting UseClientSelectColumnOnly="true" />
                </ClientSettings>
                <MasterTableView ClientDataKeyNames="AmountReportingCurrency, AmountBaseCurrency"
                    DataKeyNames="GLDataWriteOnOffID" Width="100%" ShowFooter="true" TableLayout="Auto"
                    Name="GLDataWriteOnOffGridView">
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
                        <%--Description--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Description" LabelID="1408"
                            SortExpression="Comments" DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblDescription" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <webControls:ExLabel ID="lblTotal" LabelID="2512" runat="server"></webControls:ExLabel>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                            <ItemStyle HorizontalAlign="Left" />
                            <FooterStyle HorizontalAlign="Left" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Amount--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" LabelID="1675" SortExpression="Amount"
                            DataType="System.Decimal">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAmount" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <webControls:ExLabel ID="lblLocalCurrencyTotal" runat="server"></webControls:ExLabel>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Amount In Base Currency--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="AmountBaseCurrency" LabelID="1673"
                            SortExpression="AmountBaseCurrency" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAmountBaseCurrency" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <webControls:ExLabel ID="lblBaseCurrencyTotal" runat="server"></webControls:ExLabel>
                                </div>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Amount In Reporting Currency--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="AmountReportingCurrency" LabelID="1674"
                            SortExpression="AmountReportingCurrency" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAmountReportingCurrency" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <webControls:ExLabel ID="lblReportingCurrencyTotal" runat="server"></webControls:ExLabel>
                                </div>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Open Date--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="OpenDate" LabelID="1511 " SortExpression="OpenDate"
                            DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblOpenDate" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Aging--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Aging" LabelID="1512 " ItemStyle-HorizontalAlign="Right"
                            SortExpression="Aging" DataType="System.Int32">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAging" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>                      
                        <%----WriteOffOnItemNumber # ----%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" SortExpression="RecItemNumber" LabelID="2120 ">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%----MatchSetRef# ----%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetRefNumber" SortExpression="MatchSetRefNumber" LabelID="2276 ">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlMatchSetRefNumber" runat="server"></webControls:ExHyperLink>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Close/Resolution Date--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1411" SortExpression="CloseDate"
                            DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblCloseDate" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="UserName" LabelID="1508" SortExpression="UserName"
                            DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblUserName" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <%--<ItemTemplate>
                                <%# Eval ("UserName")%>
                            </ItemTemplate>--%>
                        </telerikWebControls:ExGridTemplateColumn>
                    </Columns>
                </MasterTableView>
                <FooterStyle HorizontalAlign="Right" />
            </telerikWebControls:ExRadGrid>
        </td>
    </tr>
</table>
