<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/PopUpMasterPage.master" Inherits="Pages_PopupRiskRatingRecPeriod"
    Theme="SkyStemBlueBrown" Codebehind="PopupRiskRatingRecPeriod.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <webControls:ExLabel ID="lblMessage" runat="server" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <%--<asp:Panel ID="pnlGrid" runat="server" Height="355px" ScrollBars="Vertical" >--%>
                <telerikWebControls:ExRadGrid ID="radRecFrequency" runat="server" ShowHeader="false"
                    AllowInster="false" AllowExportToExcel="false" AllowExportToPDF="false" AllowPrint="false"
                    AllowPrintAll="false" EntityNameLabelID="1427" AutoGenerateColumns="false" OnItemDataBound="radRecFrequency_ItemDataBound">
                    <MasterTableView>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2011" HeaderStyle-Width="40%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblFinancialYear" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridBoundColumn LabelID="1626" DataField="PeriodNumber" ItemStyle-HorizontalAlign="Left"
                                HeaderStyle-Width="20%">
                            </telerikWebControls:ExGridBoundColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1057" ItemStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Right">
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
    </table>
</asp:Content>
