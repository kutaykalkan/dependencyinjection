<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Pages_AccountProfileUpdate" Theme="SkyStemBlueBrown" MasterPageFile="~/MasterPages/ARTMasterPage.master" Codebehind="AccountProfileUpdate.aspx.cs" %>

<%@ Register Src="~/UserControls/LegendOnAccountSearch.ascx" TagName="LegendOnAccountSearch"
    TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/SkyStemARTGrid.ascx" TagName="SkyStemARTGrid" TagPrefix="UserControl" %>
<%@ Register Src="~/UserControls/AccountSearchControl.ascx" TagName="AccountSearchControl"
    TagPrefix="UserControl" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:updatepanel id="upnlAccountProfile" runat="server">
       
        <contenttemplate>
            <table width="100%">
                <tr>
                    <td>
                        <UserControl:AccountSearchControl ID="ucAccountSearchControl" runat="server" />
                    </td>
                </tr>                
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <asp:Panel ID="pnlAccounts" runat="server" Visible="false">
                    <tr>
                        <td>
                            <UserControl:SkyStemARTGrid ID="ucSkyStemARTGrid"
                             Grid-AllowPaging="True" Grid-AllowCustomPaging="false" Grid-AllowExportToExcel="True" Grid-AllowExportToPDF="True" 
                             runat="server" OnGridItemDataBound="ucSkyStemARTGrid_GridItemDataBound" >
                            <SkyStemGridColumnCollection>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1357" SortExpression="AccountNumber">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlAccountNumber" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1346" SortExpression="AccountName">
                                    <ItemTemplate>
                                        <webControls:ExHyperLink ID="hlAccountName" runat="server" SkinID="GridHyperLink"></webControls:ExHyperLink>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                </SkyStemGridColumnCollection>
                            </UserControl:SkyStemARTGrid>
                        </td>
                    </tr>
                    <tr class="BlankRow">
                        <td>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>
                        <UserControl:LegendOnAccountSearch ID="LegendOnAccountSearch" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <UserControls:ProgressBar ID="ucAccountProfileMassAndBulkUpdate" runat="server" EnableTheming="true"
                            AssociatedUpdatePanelID="upnlAccountProfile" Visible="true" />
                    </td>
                </tr>
            </table>
        </contenttemplate>
    </asp:updatepanel>
</asp:content>
