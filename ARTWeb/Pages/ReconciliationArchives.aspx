<%@ Page Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master" AutoEventWireup="true"
    CodeFile="ReconciliationArchives.aspx.cs" Inherits="Pages_ReconciliationArchives"
    Title="Untitled Page" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr class="blueRow">
            <td align="left">
                <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetail" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <telerikWebControls:ExRadGrid ID="rgMain" runat="server" EntityNameLabelID="1229"
                    AllowPaging="true" AllowSorting="true" OnItemDataBound="rgMain_ItemDataBound"
                      AllowExportToExcel="true" AllowExportToPDF ="true" AllowPrint="true" AllowPrintAll="true" >
                    <MasterTableView>
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1248  " SortExpression="PeriodEndDate">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblReconciliationPeriod" runat="server" SkinID="" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1382  " SortExpression="GLBalanceReportingCurrency">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblGLBalance" runat="server" SkinID="" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="1541  " SortExpression="DateCertified">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDateCertified" runat="server" SkinID="" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
                <webControls:ExButton ID="btnCancel" LabelID="1239" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
