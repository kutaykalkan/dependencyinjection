<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.PartialMatched" Codebehind="PartialMatched.ascx.cs" %>
<br />
<!-- Content Area //-->
<asp:Panel ID="pnlHeader" runat="server">
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
        <tr class="blueRow">
            <td width="96%">
                <webControls:ExLabel ID="lblContentTitle" runat="server" LabelID="2318" SkinID="Black11Arial" />
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
            <telerikWebControls:ExRadGrid ID="rgPartialMatched" AllowMultiRowSelection="true"
                runat="server" EntityNameLabelID="2318" AllowPaging="true" AllowCustomPaging="true"
                AllowExportToExcel="true" OnNeedDataSource="rgPartialMatched_OnNeedDataSource"
                OnItemDataBound="rgPartialMatched_ItemDataBound" OnPageIndexChanged="rgPartialMatched_PageIndexChanged"
                OnPageSizeChanged="rgPartialMatched_PageSizeChanged" OnItemCreated="rgPartialMatched_ItemCreated"
                OnItemCommand="rgPartialMatched_ItemCommand">
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
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn">
                            <ItemStyle CssClass="GridClientSelectColumnCSS" />
                        </telerikWebControls:ExGridClientSelectColumn>
                        <telerikWebControls:ExGridTemplateColumn>
                            <ItemStyle CssClass="GridClientSelectColumnCSS" />
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlViewData" runat="server" SkinID="GridHyperLinkImageWithoutUnderlineReadOnlyMode"></webControls:ExHyperLink>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerikWebControls:ExRadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td align="right">
                <webControls:ExCustomValidator ID="cv2" runat="server" ValidationGroup="PartalMatch"
                    ClientValidationFunction="validateRowCountPartialMatched" Text=""></webControls:ExCustomValidator>
                <webControls:ExButton ID="btnAddToWorkspace" LabelID="2322" runat="server" OnClick="btnAddToWorkspace_OnClick"
                    ValidationGroup="PartalMatch" />
            </td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:CollapsiblePanelExtender ID="cpeContent" TargetControlID="pnlContent"
    ImageControlID="imgCollapseContent" CollapseControlID="imgCollapseContent" ExpandControlID="imgCollapseContent"
    runat="server" SkinID="CollapsiblePanel" Collapsed="true">
</ajaxToolkit:CollapsiblePanelExtender>
<br />
<!-- Work Space Area //-->
<asp:Panel ID="pnlWorkspaceHdr" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%" class="blueBorder">
        <tr class="blueRow">
            <td width="96%">
                <webControls:ExLabel ID="lblWorkspace" runat="server" LabelID="2319" SkinID="Black11Arial" />
            </td>
            <td width="4%" align="right">
                <webControls:ExImage ID="imgCollapseWorkspace" runat="server" SkinID="CollapseIcon" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlWorkspaceContent" runat="server" CssClass="blueBorderWithMargin">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerikWebControls:ExRadGrid ID="rgWorkspace" AllowMultiRowSelection="true" EntityNameLabelID="2319"
                runat="server" AllowPaging="true" AllowCustomPaging="true" AllowExportToExcel="true"
                OnNeedDataSource="rgWorkspace_OnNeedDataSource" OnItemDataBound="rgWorkspace_ItemDataBound"
                OnPageIndexChanged="rgWorkspace_PageIndexChanged" OnPageSizeChanged="rgWorkspace_PageSizeChanged"
                OnItemCreated="rgWorkspace_ItemCreated" OnItemCommand="rgWorkspace_ItemCommand">
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
                        <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn">
                            <ItemStyle CssClass="GridClientSelectColumnCSS" />
                        </telerikWebControls:ExGridClientSelectColumn>
                        <telerikWebControls:ExGridTemplateColumn>
                            <ItemStyle CssClass="GridClientSelectColumnCSS" />
                            <ItemTemplate>
                                <webControls:ExHyperLink ID="hlViewData" runat="server" SkinID="GridHyperLinkImageWithoutUnderlineReadOnlyMode"></webControls:ExHyperLink>
                            </ItemTemplate>
                        </telerikWebControls:ExGridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerikWebControls:ExRadGrid>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td align="right">
                <webControls:ExCustomValidator ID="cvPartialMatchWorkSpace" runat="server" ValidationGroup="PartalMatchWorkSpace"
                    ClientValidationFunction="validateRpwCountPartialWorkSpace" Text=""></webControls:ExCustomValidator>
                <webControls:ExButton ID="btnMatch" LabelID="2335" runat="server" OnClick="btnMatch_OnClick"
                    ValidationGroup="PartalMatchWorkSpace" />
                <webControls:ExButton ID="btnUnMatch" LabelID="2324" runat="server" OnClick="btnUnMatch_OnClick"
                    ValidationGroup="PartalMatchWorkSpace" />
                <webControls:ExButton ID="btnRemoveFromWorkspace" LabelID="2323" runat="server" OnClick="btnRemoveFromWorkspace_OnClick"
                    ValidationGroup="PartalMatchWorkSpace" />
            </td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:CollapsiblePanelExtender ID="cpeWorkspace" TargetControlID="pnlWorkspaceContent"
    ImageControlID="imgCollapseWorkspace" CollapseControlID="imgCollapseWorkspace"
    ExpandControlID="imgCollapseWorkspace" runat="server" SkinID="CollapsiblePanel"
    Collapsed="true">
</ajaxToolkit:CollapsiblePanelExtender>

<script language="javascript" type="text/javascript">
    function validateRowCountPartialMatched(sender, args) {
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'PartalMatch';
        var SelectionMsg = '<%= SelectionMsg %>';
        var gridPartialMatched;
        var gridWorkSpace1;
        gridPartialMatched = $find('<%= rgPartialMatched.ClientID %>');
        var selectedRowCount = gridPartialMatched.get_selectedItems().length;
        if (selectedRowCount > 0)
        { }
        else {
            args.IsValid = false;
            sender.errormessage = SelectionMsg;
            return;
        }
    }


    function validateRpwCountPartialWorkSpace(sender, args) {
        var valSummaryObj = GetValidationSummaryElement();
        valSummaryObj.validationGroup = 'PartalMatchWorkSpace';
        var SelectionMsg = '<%= SelectionMsg %>';
        gridWorkSpace1 = $find('<%=rgWorkspace.ClientID %>');
        var selectedRowCount1 = gridWorkSpace1.get_selectedItems().length;
        if (selectedRowCount1 > 0)
        { }
        else {
            args.IsValid = false;
            sender.errormessage = SelectionMsg;
            return;
        }

    }
</script>

