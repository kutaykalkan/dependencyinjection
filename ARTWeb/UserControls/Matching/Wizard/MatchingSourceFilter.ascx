<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MatchingSourceFilter.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.Matching.Wizard.MatchingSourceFilter" %>
<%@ Register TagPrefix="UserControl" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Register TagPrefix="usc" TagName="MatchSetInfo" Src="~/UserControls/Matching/MatchSetInfo.ascx" %>
<asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <usc:MatchSetInfo ID="uscMatchSetInfo" runat="server" />
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr class="BlankRow">
                <td>
            </tr>
            <tr id="trDataSourceName" runat="server">
                <td colspan="2" valign="middle">
                    <webControls:ExLabel ID="lblDataSource" runat="server" LabelID="2231" FormatString="{0} :"
                        SkinID="Black11Arial"></webControls:ExLabel>
                    &nbsp;
                    <asp:DropDownList ID="ddlDataSource" runat="server" SkinID="DropDownList200" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlDataSource_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hdnSelectedDataImportID" runat="server" Value="0" />
                </td>
            </tr>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <asp:Panel ID="GridPnl" runat="server">
                <tr>
                    <td colspan="3">
                        <asp:HiddenField ID="hdnNewPageSize" runat="server" Value="10" />
                        <telerikWebControls:ExRadGrid Skin="SkyStemBlueBrown" ID="rgMappingColumns" runat="server"
                            OnItemDataBound="rgMappingColumns_ItemDataBound" OnItemCommand="rgMappingColumns_ItemCommand"
                            AllowMultiRowSelection="true" AllowCustomFilter="true" ClientSettings-Selecting-AllowRowSelect="true"
                            GridApplyFilterOnClientClick="LoadGridApplyFilterPage();return false;" OnNeedDataSource="rgMappingColumns_NeedDataSource"
                            AllowSorting="true" OnSortCommand="rgMappingColumns_SortCommand" OnColumnCreated="rgMappingColumns_ColumnCreated"
                            Width="770px">
                            <ClientSettings>
                                <Scrolling AllowScroll="true" />
                                <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                            </ClientSettings>
                            <MasterTableView TableLayout="Auto" DataKeyNames="__ExcelRowNumber" AllowPaging="true"
                                AutoGenerateColumns="true">
                                <ItemStyle Wrap="false" />
                                <Columns>
                                    <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%">
                                    </telerikWebControls:ExGridClientSelectColumn>
                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" width="500">
                        <webControls:ExButton ID="btnRemove" runat="server" LabelID="2227" OnClientClick="return ConfirmDeletion('Remove','rgMappingColumns');"
                            OnClick="btnRemove_Click" SkinID="ExButton100" />
                        &nbsp;
                        <webControls:ExButton ID="btnRemoveAll" runat="server" LabelID="2228" OnClientClick="return ConfirmDeletion('RemoveAll','rgMappingColumns');"
                            OnClick="btnRemoveAll_Click" SkinID="ExButton150" />
                    </td>
                    <%--<td width="2px">
                    </td>--%>
                    <td align="right" width="400">
                    <webControls:ExButton ID="btnAdd" runat="server" LabelID="1560" SkinID="ExButton125"
                          OnClientClick="return validateRowSelectionForAddition('rgMappingColumns');"  OnClick="btnAdd_Click" CausesValidation="true" />&nbsp;
                        <webControls:ExButton ID="btnAddtoSubSet" runat="server" LabelID="2230" SkinID="ExButton125"
                            OnClick="btnAddtoSubSet_Click" CausesValidation="true" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </asp:Panel>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <asp:Panel ID="pnlSubSetGrid" runat="server">
                <tr>
                    <td class="ManadatoryField">
                        *
                    </td>
                    <td>
                        <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2229" FormatString="{0} :"
                            SkinID="Black11Arial"></webControls:ExLabel>
                        &nbsp; &nbsp;
                        <asp:TextBox ID="txtSubSetName" SkinID="ExTextBox150" MaxLength="100" runat="server">
                        </asp:TextBox>
                        &nbsp;
                        <webControls:ExRequiredFieldValidator ID="rfvSubsetName" runat="server" ControlToValidate="txtSubSetName"></webControls:ExRequiredFieldValidator>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <telerikWebControls:ExRadGrid Skin="SkyStemBlueBrown" ID="rgSubSetData" runat="server" OnItemDataBound="rgSubSetData_ItemDataBound"
                            OnItemCommand="rgSubSetData_ItemCommand" AllowMultiRowSelection="true" ClientSettings-Selecting-AllowRowSelect="true"
                            OnNeedDataSource="rgSubSetData_NeedDataSource" AllowSorting="true" OnSortCommand="rgSubSetData_SortCommand"
                            OnColumnCreated="rgSubSetData_ColumnCreated" Width="770px">
                            <ClientSettings>
                                <Scrolling AllowScroll="true" />
                                <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                            </ClientSettings>
                            <MasterTableView DataKeyNames="__ExcelRowNumber" AllowPaging="true" AutoGenerateColumns="true">
                                <ItemStyle Wrap="false" />
                                <Columns>
                                    <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" HeaderStyle-Width="5%">
                                    </telerikWebControls:ExGridClientSelectColumn>
                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    </td>
                </tr>
                <tr class="BlankRow">
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" width="500">
                        <webControls:ExButton ID="btnSubsetRemove" runat="server" LabelID="2227" OnClientClick="return ConfirmDeletion('Remove','rgSubSetData');"
                            OnClick="btnSubsetRemove_Click" SkinID="ExButton100" />
                        &nbsp;
                        <webControls:ExButton ID="btnSubsetRemoveAll" runat="server" LabelID="2228" OnClientClick="return ConfirmDeletion('RemoveAll','rgSubSetData');"
                            OnClick="btnSubsetRemoveAll_Click" SkinID="ExButton150" />
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td>
                    <UserControl:ProgressBar ID="ucProgressBar" runat="server" AssociatedUpdatePanelID="upnlMain" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

<script language="javascript" type="text/javascript">

    function validateRowSelection(gridId) {
        var SelectionMsg = '<%= SelectionMsg %>';
        var grid;
        if (gridId == 'rgSubSetData')
            grid = $find('<%= rgSubSetData.ClientID %>');
        else
            grid = $find('<%= rgMappingColumns.ClientID %>');

        var selectedRowCount = grid.get_selectedItems().length;

        if (selectedRowCount > 0)
            return true;
        else {
            alert(SelectionMsg);
            return false;
        }
    }

    function validateRowSelectionForAddition(gridId) {
        var SelectionMsgForAddtn = '<%= SelectionMsgForAddtn %>';
        var grid;
        grid = $find('<%= rgMappingColumns.ClientID %>');

        var selectedRowCount = grid.get_selectedItems().length;

        if (selectedRowCount > 0)
            return true;
        else {
            alert(SelectionMsgForAddtn);
            return false;
        }
    }

    function ConfirmDeletion(btn, gridId) {
        var RemoveMsg = '<%= RemoveMsg %>';
        var RemoveAllMsg = '<%= RemoveAllMsg %>';

        if (btn == 'Remove') {
            if (validateRowSelection(gridId)) {
                var answer = confirm(RemoveMsg);
                if (answer)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        if (btn == 'RemoveAll') {
            var answer = confirm(RemoveAllMsg);
            if (answer)
                return true;
            else
                return false;
        }
    }

    function LoadGridApplyFilterPage() {
        var ddlDataSource = document.getElementById('<%=ddlDataSource.ClientID %>');
        var urlGridApplyFilter = "../GridDynamicFilter.aspx?griddynamicfiltersessionkey=" + '<%= rgMappingColumns.ClientID %>';
        OpenRadWindowForHyperlink(urlGridApplyFilter, 480, 800);
    }
</script>

