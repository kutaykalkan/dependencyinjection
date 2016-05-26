<%@ Control Language="C#" AutoEventWireup="true" Inherits="SkyStem.ART.Web.UserControls.Unmatched" Codebehind="Unmatched.ascx.cs" %>
<%@ Import Namespace="SkyStem.Language.LanguageUtility" %>
<br />
<!-- Content Area //-->
<asp:Panel ID="pnlHeader" runat="server" Style="vertical-align: top !important;">
    <table cellpadding="0" cellspacing="0" width="100%" class="blueBorder">
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <webControls:ExLabel ID="lblSourceNamesWithNetValue" runat="server" CssClass="Black11Arial" style="font-weight: bold; margin-left: 8px;" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        </tr>
        <tr class="blueRow">
            <td width="96%">
                <webControls:ExLabel ID="lblContentTitle" runat="server" LabelID="2320" SkinID="Black11Arial" />
            </td>
            <td width="4%" align="right">
                <webControls:ExImage ID="imgCollapseContent" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlContent" runat="server" CssClass="blueBorderWithMargin">
    <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerikWebControls:ExRadGrid ID="rgUnmatched" AllowMultiRowSelection="true" runat="server"
                EntityNameLabelID="2320" AllowSorting="true" AllowCustomFilter="true" GridApplyFilterOnClientClick="LoadGridApplyFilterPage();return false;"
                AllowPaging="true" OnItemCommand="rgUnmatched_ItemCommand" AllowExportToExcel="True"
                AllowCustomPaging="true" OnItemCreated="rgUnmatched_ItemCreated" AllowExportToPDF="false"
                OnPageSizeChanged="rgUnmatched_PageSizeChanged" OnItemDataBound="rgUnmatched_OnItemDataBound"
                OnNeedDataSource="rgUnmatched_OnNeedDataSource" >
                <ClientSettings>
                    <Scrolling AllowScroll="true" />
                    <Selecting UseClientSelectColumnOnly="true" AllowRowSelect="true" />
                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                </ClientSettings>
                <MasterTableView UseAllDataFields="true" EnableColumnsViewState="false">
                    <PagerTemplate>
                        <asp:Panel ID="PagerPanel" runat="server">
                            <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                <div style="float: left; margin-right: 10px;">
                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                    <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                        </asp:Panel>
                    </PagerTemplate>
                    <Columns>
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                    </Columns>
                </MasterTableView>
                <ExportSettings>
                    <Pdf FontType="Subset" PaperSize="Letter" />
                    <Excel Format="ExcelML" />
                    <Csv ColumnDelimiter="Comma" RowDelimiter="NewLine" />
                </ExportSettings>
            </telerikWebControls:ExRadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td align="right">
                <webControls:ExCustomValidator ID="cvUniqueSource2Column" runat="server" ClientValidationFunction="validateRowSelection"
                    Text="" ValidationGroup="UnMatched"></webControls:ExCustomValidator>
                <webControls:ExButton ID="btnAddToWorkspace" ValidationGroup="UnMatched" LabelID="2322"
                    runat="server" OnClick="btnAddToWorkspace_OnClick" />
            </td>
        </tr>
        <ajaxToolkit:CollapsiblePanelExtender ID="cpeContent" TargetControlID="pnlContent"
            ImageControlID="imgCollapseContent" CollapseControlID="imgCollapseContent" ExpandControlID="imgCollapseContent"
            runat="server" SkinID="CollapsiblePanel" Collapsed="true">
        </ajaxToolkit:CollapsiblePanelExtender>
    </table>
</asp:Panel>
<br />
<!-- Work Space Area //-->
<asp:Panel ID="pnlWorkspaceHdr" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" class="blueBorder">
        <tr class="blueRow">
            <td width="96%">
                <webControls:ExLabel ID="lblWorkspace" runat="server" LabelID="2321" SkinID="Black11Arial" />
            </td>
            <td width="4%" align="right">
                <webControls:ExImage ID="imgCollapseWorkspace" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlWorkspaceContent" runat="server" CssClass="blueBorderWithMargin">
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeWorkspace" TargetControlID="pnlWorkspaceContent"
        ImageControlID="imgCollapseWorkspace" CollapseControlID="imgCollapseWorkspace"
        ExpandControlID="imgCollapseWorkspace" runat="server" SkinID="CollapsiblePanel"
        Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender>
    <asp:Panel ID="Panel1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="50%">
                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2239" SkinID="Black11Arial" />
                </td>
                <%--<td width="50%">
                    <webControls:ExLabel ID="lblTotalWorkspaceSource1" runat="server" Text="0.00" SkinID="Black11Arial" />
                </td>--%>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="upnlWorkSpace" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerikWebControls:ExRadGrid ID="rgWorkspaceSource1" AllowMultiRowSelection="true"
                EntityNameLabelID="2239" runat="server" AllowPaging="true" OnNeedDataSource="rgWorkspaceSource1_OnNeedDataSource"
                OnItemDataBound="rgWorkspaceSource2_OnItemDataBound" OnItemCommand="rgWorkspaceSource1_ItemCommand"
                AllowExportToExcel="True" OnItemCreated="rgWorkspaceSource1_ItemCreated" AllowExportToPDF="false"
                AllowCustomPaging="true" OnPageSizeChanged="rgWorkspaceSource1_PageSizeChanged">
                <ClientSettings>
                    <Scrolling AllowScroll="true" />
                    <Selecting UseClientSelectColumnOnly="true" AllowRowSelect="true" />
                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                </ClientSettings>
                <MasterTableView EnableColumnsViewState="false">
                    <PagerTemplate>
                        <asp:Panel ID="PagerPanel" runat="server">
                            <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                <div style="float: left; margin-right: 10px;">
                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                    <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                        </asp:Panel>
                    </PagerTemplate>
                    <Columns>
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                    </Columns>
                </MasterTableView>
            </telerikWebControls:ExRadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlWS1Total" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="5%">
                    <webControls:ExLabel ID="lblSelectedWS1Total" runat="server" LabelID="1656" FormatString="{0}: "
                        SkinID="Black11Arial" />
                </td>
                <td width="95%">
                    <webControls:ExLabel ID="lblTotalWorkspaceSource1" runat="server" Text="0.00" SkinID="Black11Arial" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="100%">
                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="2240" SkinID="Black11Arial" />
                </td>
                <%--  <td width="50%">
                    <webControls:ExLabel ID="lblTotalWorkspaceSource2" runat="server" Text="0.00" SkinID="Black11Arial" />
                </td>--%>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerikWebControls:ExRadGrid ID="rgWorkspaceSource2" AllowMultiRowSelection="true"
                EntityNameLabelID="2240" runat="server" AllowPaging="true" OnNeedDataSource="rgWorkspaceSource2_OnNeedDataSource"
                OnItemDataBound="rgWorkspaceSource2_OnItemDataBound" OnItemCommand="rgWorkspaceSource2_ItemCommand"
                AllowExportToExcel="True" OnItemCreated="rgWorkspaceSource2_ItemCreated" AllowExportToPDF="false"
                AllowCustomPaging="true" OnPageSizeChanged="rgWorkspaceSource2_PageSizeChanged">
                <ClientSettings>
                    <Scrolling AllowScroll="true" />
                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                    <Selecting UseClientSelectColumnOnly="true" AllowRowSelect="true" />
                </ClientSettings>
                <MasterTableView EnableColumnsViewState="false">
                    <PagerTemplate>
                        <asp:Panel ID="PagerPanel" runat="server">
                            <asp:Panel runat="server" ID="pnlPageSizeDDL">
                                <div style="float: left; margin-right: 10px;">
                                    <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                                    <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                        </asp:Panel>
                    </PagerTemplate>
                    <Columns>
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" />
                    </Columns>
                </MasterTableView>
            </telerikWebControls:ExRadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Panel ID="pnlWS2Total" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="5%">
                    <webControls:ExLabel ID="lblSelectedWS2Total" runat="server" LabelID="1656" FormatString="{0}: "
                        SkinID="Black11Arial" />
                </td>
                <td width="45%">
                    <webControls:ExLabel ID="lblTotalWorkspaceSource2" runat="server" Text="0.00" SkinID="Black11Arial" />
                </td>
                <td width="10%">
                    <webControls:ExLabel ID="ExLabel3" runat="server" LabelID="2026" FormatString="{0}: "
                        SkinID="Black11Arial" />
                </td>
                <td width="40%">
                    <webControls:ExLabel ID="lblTotalNatValue" runat="server" Text="0.00" SkinID="Black11Arial" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<br />
<!-- Used Record Area //-->
<asp:Panel ID="pnlUsedRecordHdr" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" class="blueBorder">
        <tr class="blueRow">
            <td width="96%">
                <webControls:ExLabel ID="lblUsedRecords" runat="server" LabelID="2219" SkinID="Black11Arial" />
            </td>
            <td width="4%" align="right">
                <webControls:ExImage ID="imgCollapseUsedRecords" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlUsedRecordsContent" runat="server" CssClass="blueBorderWithMargin">
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeUsedRecords" TargetControlID="pnlUsedRecordsContent"
        ImageControlID="imgCollapseUsedRecords" CollapseControlID="imgCollapseUsedRecords"
        ExpandControlID="imgCollapseUsedRecords" runat="server" SkinID="CollapsiblePanel"
        Collapsed="true">
    </ajaxToolkit:CollapsiblePanelExtender>
    <asp:Panel ID="pnlUsedRecord" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="100%">
                    <webControls:ExLabel ID="lblUsedRecord" runat="server" LabelID="2219" SkinID="Black11Arial" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:UpdatePanel ID="UpdatePanelUsedRecord" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerikWebControls:ExRadGrid ID="rgUsedRecords" AllowMultiRowSelection="false" EntityNameLabelID="2219"
                runat="server" AllowPaging="true" OnNeedDataSource="rgUsedRecords_OnNeedDataSource"
                OnItemDataBound="rgUsedRecords_OnItemDataBound" OnItemCommand="rgWorkspaceSource2_ItemCommand"
                AllowExportToExcel="false" AllowExportToPDF="false">
                <%--     <ClientSettings>
                    <Scrolling AllowScroll="true" />
                    <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                    <Selecting UseClientSelectColumnOnly="true" AllowRowSelect="true" />
                </ClientSettings>--%>
                <MasterTableView EnableColumnsViewState="false">
                    <Columns>
                        <%--         <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn">
                            <ItemStyle CssClass="GridClientSelectColumnCSS" />
                        </telerikWebControls:ExGridClientSelectColumn>--%>
                        <telerikWebControls:ExGridTemplateColumn UniqueName="CloseDate" LabelID="1411">
                            <ItemStyle CssClass="GridClientSelectColumnCSS" />
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlCloseDate" runat="server"></webControls:ExHyperLink>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerikWebControls:ExRadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>
<table width="100%">
    <tr>
        <td align="right">
            <webControls:ExCustomValidator ID="ExCustomValidator1" runat="server" ClientValidationFunction="validateRowSelectionUnMatchedRemove"
                Text="" ValidationGroup="UnMatchedWorkSpaceRemove"></webControls:ExCustomValidator>
            <webControls:ExCustomValidator ID="ExCustomValidator2" runat="server" ClientValidationFunction="validateRowSelectionUnMatchedMatchBtn"
                Text="" ValidationGroup="UnMatchedWorkSpaceMatch"></webControls:ExCustomValidator>
            <webControls:ExCustomValidator ID="ExCustomValidator3" runat="server" ClientValidationFunction="validateRowSelectionUnMatchedCreateBtn"
                Text="" ValidationGroup="UnMatchedWorkSpaceCreate"></webControls:ExCustomValidator>
            <webControls:ExButton ID="btnMatch" LabelID="2335" runat="server" OnClick="btnMatch_OnClick"
                ValidationGroup="UnMatchedWorkSpaceMatch" />
            <webControls:ExButton ID="btnCreateRecItem" LabelID="2327" runat="server" OnClick="btnCreateRecItem_OnClick"
                ValidationGroup="UnMatchedWorkSpaceCreate" />
            <webControls:ExButton ID="btnCloseRecItem" LabelID="2473" runat="server" OnClick="btnCloseRecItem_OnClick"
                ValidationGroup="UnMatchedWorkSpaceCreate" />
            <webControls:ExButton ID="btnRemoveFromWorkspace" LabelID="2323" runat="server" OnClick="btnRemoveFromWorkspace_OnClick"
                ValidationGroup="UnMatchedWorkSpaceRemove" />
            <asp:HiddenField ID="hdnAmountColumnDataKeyname" runat="server" />
            <asp:HiddenField ID="hdnWorkspaceSource1Total" Value="0.00" runat="server" />
            <asp:HiddenField ID="hdnWorkspaceSource2Total" Value="0.00" runat="server" />
        </td>
    </tr>
</table>

<script language="javascript" type="text/javascript">
    function LoadGridApplyFilterPage() {
        var urlGridApplyFilter = "../GridDynamicFilter.aspx?griddynamicfiltersessionkey=" + '<%= rgUnmatched.ClientID %>';
        OpenRadWindowForHyperlink(urlGridApplyFilter, 480, 800);
    }


    function validateRowSelection(sender, args) {
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'UnMatched';
        var SelectionMsg = '<%= SelectionMsg %>';
        var gridUnmatched;
        var gridWorkSpace1;
        var gridWorkSpace2;
        gridUnmatched = $find('<%= rgUnmatched.ClientID %>');
        var selectedRowCount = gridUnmatched.get_selectedItems().length;
        if (selectedRowCount > 0)
        { }
        else {
            args.IsValid = false;
            sender.errormessage = SelectionMsg;
            return;
        }
    }

    function validateRowSelectionUnMatchedRemove(sender, args) {
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'UnMatchedWorkSpaceRemove';
        var SelectionMsg = '<%= SelectionMsg %>';
        gridWorkSpace1 = $find('<%=rgWorkspaceSource1.ClientID %>');
        gridWorkSpace2 = $find('<%=rgWorkspaceSource2.ClientID %>');
        var selectedRowCount1 = gridWorkSpace1.get_selectedItems().length;
        var selectedRowCount2 = gridWorkSpace2.get_selectedItems().length;
        if (selectedRowCount1 > 0 || selectedRowCount2 > 0)
        { }
        else {
            args.IsValid = false;
            sender.errormessage = SelectionMsg;
            return;
        }
    }

    function validateRowSelectionUnMatchedMatchBtn(sender, args) {
        var SelectionMsgMatch = '<%= SelectionMsgMatch %>';
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'UnMatchedWorkSpaceMatch';
        gridWorkSpace1 = $find('<%=rgWorkspaceSource1.ClientID %>');
        gridWorkSpace2 = $find('<%=rgWorkspaceSource2.ClientID %>');
        var selectedRowCount1 = gridWorkSpace1.get_selectedItems().length;
        var selectedRowCount2 = gridWorkSpace2.get_selectedItems().length;

        if (selectedRowCount1 > 0 && selectedRowCount2 > 0) { // && (selectedRowCount1 + selectedRowCount2 < 11)) {
        }
        else {
            args.IsValid = false;
            sender.errormessage = SelectionMsgMatch;
            return;
        }
    }

    function validateRowSelectionUnMatchedCreateBtn(sender, args) {
        var SelectionMsgCreateRecItem = '<%= SelectionMsgCreateRecItem %>';
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'UnMatchedWorkSpaceCreate';
        gridWorkSpace1 = $find('<%=rgWorkspaceSource1.ClientID %>');
        gridWorkSpace2 = $find('<%=rgWorkspaceSource2.ClientID %>');
        var RowCount1 = gridWorkSpace1.get_masterTableView().get_dataItems();
        var RowCount2 = gridWorkSpace2.get_masterTableView().get_dataItems();
        if (RowCount1.length > 0 || RowCount2.length > 0) {
        }
        else {
            args.IsValid = false;
            sender.errormessage = SelectionMsgCreateRecItem;
            return;
        }
    }

    function rgWorkspaceSource1ItemSelected(sender, args) {
        var lblTotal = document.getElementById("<%=lblTotalWorkspaceSource1.ClientID%>");
        var hdnWorkspaceSource1Total = document.getElementById("<%=hdnWorkspaceSource1Total.ClientID%>");
        hdnWorkspaceSource1Total.value = rgWorkspaceSourceItemSelected(sender, args);
        lblTotal.firstChild.data = GetDisplayDecimalValue(hdnWorkspaceSource1Total.value, 2);
        UpdateNetValue();
    }
    function rgWorkspaceSource2ItemSelected(sender, args) {
        var lblTotal = document.getElementById("<%=lblTotalWorkspaceSource2.ClientID%>");
        var hdnWorkspaceSource2Total = document.getElementById("<%=hdnWorkspaceSource2Total.ClientID%>");
        hdnWorkspaceSource2Total.value = rgWorkspaceSourceItemSelected(sender, args);
        lblTotal.firstChild.data = GetDisplayDecimalValue(hdnWorkspaceSource2Total.value, 2);
        UpdateNetValue();
    }

    function rgWorkspaceSourceItemSelected(sender, args) {
        var hdnAmountColumnDataKeyname = document.getElementById("<%=hdnAmountColumnDataKeyname.ClientID%>");
        var DataKey = hdnAmountColumnDataKeyname.value;
        var TotalValue = 0;
        var masterTable = sender.get_masterTableView()
        var selectedRows = masterTable.get_selectedItems();
        for (var i = 0; i < selectedRows.length; i++) {
            var row = selectedRows[i];
            TotalValue = TotalValue + Round(row.getDataKeyValue(DataKey), 2);
        }
        return TotalValue;
    }
    function UpdateNetValue() {
        var hdnWorkspaceSource1Total = document.getElementById("<%=hdnWorkspaceSource1Total.ClientID%>");
        var hdnWorkspaceSource2Total = document.getElementById("<%=hdnWorkspaceSource2Total.ClientID%>");
        var lblTotalNatValue = document.getElementById("<%=lblTotalNatValue.ClientID%>");
        var netval = parseFloat(hdnWorkspaceSource1Total.value) - parseFloat(hdnWorkspaceSource2Total.value);
        lblTotalNatValue.firstChild.data = GetDisplayDecimalValue(netval, 2);

    }
    
</script>

