<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewRecItems.aspx.cs" Inherits="Pages_Matching_ViewRecItems"
    MasterPageFile="~/MasterPages/PopUpMasterPage.master" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="GLDataRecItemGrid" Src="~/UserControls/RecForm/GLDataRecItemGrid.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataRecurringScheduleItemsGrid"
    Src="~/UserControls/RecForm/GLDataRecurringScheduleItemsGrid.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataWriteOnOffGrid" Src="~/UserControls/RecForm/GLDataWriteOnOffGrid.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlClosedItems" runat="server" Width="100%">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="GridHeaderPadding">
                    <webControls:ExLabel ID="lblGridHeading2" runat="server" LabelID="1873" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                </td>
            </tr>
            <tr>
                <td>
                    <UserControls:GLDataRecItemGrid ID="ucGLDataRecItemGrid" AllowExportToExcel="false"
                        AllowExportToPDF="false" AllowCustomFilter="false" AllowCustomPaging="false"
                        AllowSelectionPersist="false" AllowDisplayCurrencyPnl="false" IsOnPage="false"
                        ShowSelectCheckBoxColum="false" ShowCloseDateColum="true" runat="server">
                    </UserControls:GLDataRecItemGrid>
                    <UserControls:GLDataRecurringScheduleItemsGrid ID="ucGLDataRecurringScheduleItemsGrid"
                        AllowExportToExcel="false" AllowExportToPDF="false" AllowCustomFilter="false"
                        AllowCustomPaging="false" AllowSelectionPersist="false" AllowDisplayCurrencyPnl="false"
                        IsOnPage="false" ShowCloseDateColum="true" ShowSelectCheckBoxColum="false" runat="server">
                    </UserControls:GLDataRecurringScheduleItemsGrid>
                    <UserControls:GLDataWriteOnOffGrid ID="ucGLDataWriteOnOffGrid" AllowExportToExcel="false"
                        AllowExportToPDF="false" AllowCustomFilter="false" AllowCustomPaging="false"
                        AllowSelectionPersist="false" AllowDisplayCurrencyPnl="false" BasePageTitleLabelID="2471"
                        IsOnPage="false" ShowCloseDateColum="true" ShowSelectCheckBoxColum="false" runat="server">
                    </UserControls:GLDataWriteOnOffGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
