<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_PopupExchangeRates" Theme="SkyStemBlueBrown" Codebehind="PopupExchangeRates.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" class="DueDatesPanelContent" width="100%">
        <tr>
            <td>
                <webControls:ExLabel ID="lblRecPeriod" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td>
                <telerikwebcontrols:exradgrid id="rgExchangeRates" runat="server" entitynamelabelid="1488"
                    allowsorting="false" onitemdatabound="rgExchangeRates_ItemDataBound" onneeddatasource="rgExchangeRates_NeedDataSource"
                    ondetailtabledatabind="rgExchangeRates_GridDetailTableDataBind" AllowExportToExcel="true" AllowExportToPDF="true"
                    OnItemCommand="rgExchangeRates_ItemCommand" OnItemCreated="rgExchangeRates_ItemCreated">
                    <MasterTableView DataKeyNames="ExchangeRateID"  AllowPaging="true">
                        <DetailTables>
                            <telerik:GridTableView Name="ËxchangeRateArchieve" runat="server" AllowPaging="false" AllowSorting="false"
                                            Width="100%">
                                <Columns>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1486" 
                                        HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblFromCCYCode" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1487" 
                                        HeaderStyle-Width="25%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblToCCYCode" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1488" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblExchangeRate" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1509" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lblDateAdded" runat="server" SkinID="Black11ArialNormal" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1486" SortExpression="FromCurrency"
                                HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblFromCurrency" runat="server" SkinID="Black11ArialNormal" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1487" SortExpression="ToCurrency"
                                HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblToCurrency" runat="server" SkinID="Black11ArialNormal" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1488" SortExpression="ExchangeRates"
                                HeaderStyle-Width="22%" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblExchangeRates" runat="server" SkinID="Black11ArialNormal" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1509"
                                HeaderStyle-Width="28%" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateAdded" runat="server" SkinID="Black11ArialNormal" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikwebcontrols:exradgrid>
            </td>
        </tr>
    </table>
</asp:Content>
