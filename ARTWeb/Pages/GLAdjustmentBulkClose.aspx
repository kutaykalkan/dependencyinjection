<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/PopUpMasterPage.master"
    CodeFile="~/Pages/GLAdjustmentBulkClose.aspx.cs" Inherits="Pages_GLAdjustmentBulkClose"
    Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="GLDataRecItemGrid" Src="~/UserControls/RecForm/GLDataRecItemGrid.ascx" %>
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
                <webControls:ExCustomValidator runat="server" OnServerValidate="cvValidateNormalRecItems_ServerValidate"
                    Text="!" ID="cvValidateNormalRecItems"></webControls:ExCustomValidator>
                <webControls:ExCustomValidator runat="server" ValidationGroup="CloseAll" OnServerValidate="cvValidateCloseAllNormalRecItems_ServerValidate"
                    Text="!" ID="cvValidateCloseAllNormalRecItems"></webControls:ExCustomValidator>
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
                <UserControls:GLDataRecItemGrid ID="ucGLDataRecItemGrid" AllowExportToExcel="true"
                    AllowExportToPDF="true" AllowCustomFilter="true" AllowCustomPaging="true" AllowSelectionPersist="true"
                    AllowDisplayCurrencyPnl="true" BasePageTitleLabelID="2471" IsOnPage="false" ShowCloseDateColum="false"
                    runat="server">
                </UserControls:GLDataRecItemGrid>
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
