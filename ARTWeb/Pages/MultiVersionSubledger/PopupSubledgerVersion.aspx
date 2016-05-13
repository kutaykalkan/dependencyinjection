<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="PopupSubledgerVersion.aspx.cs" Inherits="Pages_MultiVersionSubledger_Popup"
    Theme="SkyStemBlueBrown" Title="Untitled Page" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr id="trMessage" runat="server">
            <td colspan="3">
                <webControls:ExLabel ID="lblGLVersion" runat="server" LabelID="2366" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td align="center">
                <telerikWebControls:ExRadGrid ID="radSubledgerVersion" runat="server" AllowInster="false"
                    AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false" AllowPrintAll="false"
                    AutoGenerateColumns="false" Width="95%" OnItemDataBound="radSubledgerVersion_ItemDataBound"
                    AllowSorting="true" OnSortCommand="radSubledgerVersion_SortCommand" EntityNameLabelID="2367">
                    <MasterTableView>
                        <Columns>
                            <%--<telerikWebControls:ExGridTemplateColumn LabelID="1509" HeaderStyle-Width="10%" UniqueName="DateAdded"
                                SortExpression="DateAdded" DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateAdded" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2180" HeaderStyle-Width="10%" SortExpression="AddedBy"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblAddedBy" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>--%>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1370" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="ReconciliationStatus"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReconciliationStatus" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2396" HeaderStyle-Width="10%"  SortExpression="GLBalanceBaseCurrency"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblGLBalanceBaseCurrency" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2397" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" SortExpression="GLBalanceReportingCurrency"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblGLBalanceReportingCurrency" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        function ClosePage() {
            GetRadWindow().Close();
        }
    </script>

</asp:Content>
