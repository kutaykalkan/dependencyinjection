<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_BulkCloseGLDataRecurringScheduleItems"
    Theme="SkyStemBlueBrown" Codebehind="BulkCloseGLDataRecurringScheduleItems.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataRecurringScheduleItemsGrid" Src="~/UserControls/RecForm/GLDataRecurringScheduleItemsGrid.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="padding: 0px">
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="valSummary" ValidationGroup="CloseAll" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetailPopup" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr class="RowWithPadding">
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblResolutionCloseDate" runat="server" FormatString="{0}:"
                    LabelID="1411" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExCalendar ID="calResolutionDate" runat="server" Width="80"></webControls:ExCalendar>
                <webControls:ExCustomValidator runat="server" OnServerValidate="cvGLDataRecurringItemScheduleItems_ServerValidate"
                    Text="!" ID="cvValidateGLDataRecurringItemScheduleItems"></webControls:ExCustomValidator>
                <webControls:ExCustomValidator runat="server" ValidationGroup="CloseAll" OnServerValidate="cvCloseAllGLDataRecurringItemScheduleItems_ServerValidate"
                    Text="!" ID="cvValidateCloseAllGLDataRecurringItemScheduleItems"></webControls:ExCustomValidator>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField">
            </td>
            <td colspan="3">
                <UserControls:GLDataRecurringScheduleItemsGrid ID="ucGLDataRecurringScheduleItemsGrid" AllowExportToExcel="true"
                    AllowExportToPDF="true" AllowCustomFilter="true" AllowCustomPaging="true" AllowSelectionPersist="true"
                    AllowDisplayCurrencyPnl="true" IsOnPage="false" ShowCloseDateColum="false"
                    runat="server">
                </UserControls:GLDataRecurringScheduleItemsGrid>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right">
                <webControls:ExButton ID="btnClose" runat="server" LabelID="1771" SkinID="ExButton100"
                    OnClick="btnClose_Click" />
                <webControls:ExButton ID="btnCloseAll" ValidationGroup="CloseAll" runat="server"
                    LabelID="2528" SkinID="ExButton100" OnClick="btnCloseAll_Click" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" SkinID="ExButton100"
                    OnClientClick="window.close();" CausesValidation="false" />
                <asp:Button ID="btnApplyFilter" runat="server" CausesValidation="false" OnClick="btnApplyFilter_Click"
                    Style="visibility: hidden" />
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        function CallParentApplyFilterFunction() {
            // alert('manu');
            $get('<%=btnApplyFilter.ClientID%>').click();
        }
    </script>

</asp:Content>
