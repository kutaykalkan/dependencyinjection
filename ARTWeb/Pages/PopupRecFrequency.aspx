<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="PopupRecFrequency.aspx.cs" Inherits="Pages_PopupRecFrequency" Theme="SkyStemBlueBrown" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center">
                <%--<asp:Panel ID="pnlGrid" runat="server" Height="355px" ScrollBars="Vertical" >--%>
                <telerikWebControls:ExRadGrid ID="radRecFrequency" runat="server" ShowHeader="false"
                    AllowInster="false" AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false"
                    AllowPrintAll="false" EntityNameLabelID="1427" AutoGenerateColumns="false" Width="320"
                    OnItemDataBound="radRecFrequency_ItemDataBound">
                    <MasterTableView>
                        <Columns>
                            <telerikWebControls:ExGridBoundColumn LabelID="1626" DataField="PeriodNumber" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="50%">
                            </telerikWebControls:ExGridBoundColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1057" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="50%" HeaderStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDate" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
                <%--</asp:Panel>--%>
            </td>
        </tr>
        <%--<tr>
            <td>
                <webControls:ExButton ID="btnOKPopup" runat="server" LabelID="1742" CausesValidation="false"
                    OnClientClick="ClosePage()" />&nbsp;
            </td>
        </tr>--%>
    </table>

    <script type="text/javascript" language="javascript">
        function ClosePage() {
            GetRadWindow().Close();
        }
    </script>

</asp:Content>
