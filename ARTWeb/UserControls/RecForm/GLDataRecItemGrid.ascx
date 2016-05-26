<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_GLDataRecItemGrid" Codebehind="GLDataRecItemGrid.ascx.cs" %>
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
            <telerikWebControls:ExRadGrid ID="rgGLDataRecItems" Width="100%" runat="server" OnItemDataBound="rgGLDataRecItems_ItemDataBound"
                OnNeedDataSource="rgGLDataRecItems_NeedDataSource" ClientSettings-Selecting-AllowRowSelect="true"
                AllowMultiRowSelection="true" AllowSorting="true" AllowPaging="true" OnItemCommand="rgGLDataRecItems_ItemCommand"
                OnPageIndexChanged="rgGLDataRecItems_PageIndexChanged" OnItemCreated="rgGLDataRecItems_ItemCreated"
                OnPageSizeChanged="rgGLDataRecItems_PageSizeChanged" OnPdfExporting="rgGLDataRecItems_PdfExporting"
                SkinID="SkyStemBlueBrownRecItems" AllowCauseValidationExportToExcel="false" AllowCauseValidationExportToPDF="false">
                <ClientSettings>
                    <Selecting UseClientSelectColumnOnly="true" />
                </ClientSettings>
                <MasterTableView ClientDataKeyNames="AmountReportingCurrency, AmountBaseCurrency"
                    DataKeyNames="GLDataRecItemID" Width="100%" ShowFooter="true" TableLayout="Auto"
                    Name="GLDataRecItemsGridView">
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
                        <%--Ref No For Import--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="RefNo" LabelID="1513" ItemStyle-HorizontalAlign="Right"
                            DataType="System.Int32">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblRefNo" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Date for Import--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="DateForImport" LabelID="1399"
                            SortExpression="OpenDate" DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblDateForImport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Comments--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Comments" LabelID="1408" SortExpression="Comments"
                            DataType="System.String">
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
                        <%-- LocalCurrencyCode for Import --%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="LCCYCode" LabelID="1773" Visible="true"
                            SortExpression="LocalCurrencyCode" DataType="System.String">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblLocalCurrencyCode" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Amount For Import--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="AmountImport" LabelID="1675"
                            SortExpression="Amount" DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAmountImport" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            <ItemStyle HorizontalAlign="Right" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Amount--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="Amount" LabelID="1675" SortExpression="Amount"
                            DataType="System.Decimal" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblAmount" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <webControls:ExLabel ID="lblLocalCurrencyTotal" runat="server"></webControls:ExLabel>
                                </div>
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
                        <%--Close/Resolution Date--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1411" SortExpression="CloseDate"
                            DataType="System.DateTime">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblCloseDate" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%--Documents--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="AttachmentCount" LabelID="2056"
                            SortExpression="AttachmentCount" DataType="System.Int32">
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlAttachmentCount" runat="server"></webControls:ExHyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle Width="5%" />
                        </telerikWebControls:ExGridTemplateColumn>
                        <%----Rec Item # ----%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="RecItemNumber" LabelID="2118 ">
                            <ItemTemplate>
                                <webControls:ExLabel ID="lblRecItemNumber" runat="server"></webControls:ExLabel>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                        <%----MatchSetRef# ----%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="MatchSetRefNumber" SortExpression="MatchSetRefNumber"
                            LabelID="2276">
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
