<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="PopupGLVersion.aspx.cs" Inherits="Pages_MultiVersionGL_PopupGLVersion"
    Theme="SkyStemBlueBrown" Title="Untitled Page" %>

<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
     <tr id="trMessage" runat="server">
            <td colspan="3">
                <webControls:ExLabel ID="lblGLVersion" runat="server" LabelID="2182" FormatString="{0} :"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td align="center">
                <telerikWebControls:ExRadGrid ID="radGLVersion" runat="server" AllowInster="false"
                    AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false" AllowPrintAll="false"
                    AutoGenerateColumns="false" Width="95%" OnItemDataBound="radGLVersion_ItemDataBound"
                    AllowSorting="true" OnSortCommand="radGLVersion_SortCommand">
                    <MasterTableView>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1509" HeaderStyle-Width="10%" UniqueName="DateAdded"
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
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1370" HeaderStyle-Width="10%" SortExpression="ReconciliationStatus"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReconciliationStatus" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1876" HeaderStyle-Width="10%" SortExpression="GLBalanceBaseCurrency"
                                DataType="System.String">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblGLBalanceBaseCurrency" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1875" HeaderStyle-Width="10%" SortExpression="GLBalanceReportingCurrency"
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
