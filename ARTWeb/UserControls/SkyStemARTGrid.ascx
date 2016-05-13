<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SkyStemARTGrid.ascx.cs"
    Inherits="UserControls_SkyStemARTGrid" %>
<asp:HiddenField ID="hdnNewPageSize" runat="server" Value="10" />
<telerikWebControls:ExRadGrid ID="rgAccount" runat="server" AllowMultiRowSelection="true"
    GroupHeaderItemStyle-Height="25px" ClientSettings-Selecting-AllowRowSelect="true"
    OnItemDataBound="rgAccount_ItemDataBound" OnSortCommand="SkyStemARTGrid_SortCommand"
    OnItemCommand="rgAccount_ItemCommand" OnItemCreated="rgAccount_ItemCreated" OnPageSizeChanged="rgAccount_PageSizeChanged"
    GridApplyFilterOnClientClick="LoadGridApplyFilterPage();return false;" OnPageIndexChanged="rgAccount_PageIndexChanged"
    OnPreRender="rgAccount_PreRender">
    <MasterTableView AllowSorting="true" TableLayout="Auto" Name="SkyStemARTGridView"
        CellPadding="0" CellSpacing="0">
        <PagerTemplate>
            <asp:Panel ID="PagerPanel" runat="server">
                <asp:Panel runat="server" ID="pnlPageSizeDDL">
                    <div style="float: left; margin-right: 10px;">
                        <%--<span style="margin-right: 3px;">Page size:</span>--%>
                        <webControls:ExLabel ID="lblPageSize" runat="server" LabelID="2493"></webControls:ExLabel>
                        <%-- <telerik:RadComboBox ID="ddlPageSize"   Width="150px"    runat="server"    OnClientSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    </telerik:RadComboBox>--%>
                        <asp:DropDownList ID="ddlPageSize" SkinID="DropDownList50" runat="server">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
            </asp:Panel>
        </PagerTemplate>

        <Columns>
            <telerikWebControls:ExGridTemplateColumn Visible="false" UniqueName="ImageColumn">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>
                                <webControls:ExHyperLink ID="hlReadOnlyModeStatus" runat="server" SkinID="GridHyperLinkImageWithUnderlineReadOnlyMode"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlEditModeStatus" runat="server" SkinID="GridHyperLinkImageWithUnderlineEditMode"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlStartReconciliationStatus" runat="server" SkinID="GridHyperLinkImageWithUnderlineStartRecMode"></webControls:ExHyperLink>
                            </td>
                            <td>
                                <webControls:ExHyperLink ID="hlUnFlagIcon" runat="server" SkinID="UnFlagIcon"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlFlagIcon" runat="server" SkinID="FlagIcon"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlCompletedStatus" runat="server" ToolTipLabelID="2559"
                                    SkinID="GridHyperLinkImageWithCompletedStatus"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlPendingStatus" runat="server" ToolTipLabelID="2561"
                                    SkinID="GridHyperLinkImageWithPendingStatus"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlOverDueStatus" runat="server" ToolTipLabelID="2562"
                                    SkinID="GridHyperLinkImageWithOverDueStatus"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlHiddenStatus" runat="server" ToolTipLabelID="2617"
                                    SkinID="GridHyperLinkImageWithHiddenStatus"></webControls:ExHyperLink>
                                <webControls:ExHyperLink ID="hlDeletedStatus" runat="server" ToolTipLabelID="2646"
                                    SkinID="GridHyperLinkImageWithDeleteStatus"></webControls:ExHyperLink>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>
            <%--Task Status Image--%>
            <%-- <telerikWebControls:ExGridTemplateColumn UniqueName="TaskStatusImage" >
                <ItemTemplate>
                    <table>
                        <tr>
                            <td style="width: 22px">
                              
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </telerikWebControls:ExGridTemplateColumn>--%>
            <telerikWebControls:ExGridClientSelectColumn UniqueName="CheckboxSelectColumn" Visible="false"
                HeaderStyle-Width="10px" />
            <telerikWebControls:ExGridSerialNumberColumn LabelID="2208" Visible="false" UniqueName="SerialNumberColumn">
            </telerikWebControls:ExGridSerialNumberColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key2" UniqueName="Key2">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey2" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key3" UniqueName="Key3">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey3" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key4" UniqueName="Key4">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey4" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key5" UniqueName="Key5">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey5" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key6" UniqueName="Key6">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey6" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key7" UniqueName="Key7">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey7" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key8" UniqueName="Key8">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey8" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn Visible="false" SortExpression="Key9" UniqueName="Key9">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlKey9" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1337" SortExpression="FSCaption"
                UniqueName="FSCaption" Visible="false">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlFSCaption" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="15%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn LabelID="1363" SortExpression="AccountType"
                UniqueName="AccountType" DataType="System.String" Visible="false">
                <ItemTemplate>
                    <webControls:ExHyperLink ID="hlAccountType" runat="server"></webControls:ExHyperLink>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
            </telerikWebControls:ExGridTemplateColumn>
            <telerikWebControls:ExGridTemplateColumn UniqueName="ID" Visible="false" />
            <telerikWebControls:ExGridTemplateColumn UniqueName="GridColumnNumber" Visible="false" />
            <telerikWebControls:ExGridTemplateColumn UniqueName="NetAccountID" Visible="false" />
        </Columns>
    </MasterTableView>
  
</telerikWebControls:ExRadGrid>
<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

    <script type="text/javascript">
        //        var tableView = null;
        //        function pageLoad(sender, args) {
        //            tableView = $find("<%= rgAccount.ClientID %>").get_masterTableView();
        //        }

        //      

        //        function ddlPageSize_SelectedIndexChanged(ID) {
        //            tableView.set_pageSize(document.getElementById(ID).value);

        //        }

        //        function changePage(argument) {
        //            tableView.page(argument);
        //        }

        //        function RadNumericTextBox1_ValueChanged(sender, args) {
        //            tableView.page(sender.get_value());
        //        }

        var tableView = null;
        function ddlPageSize_SelectedIndexChanged(ID, SourceID) {

            if ($find(SourceID) != null) {
                tableView = $find(SourceID).get_masterTableView();
            }
            if (tableView != null)
                tableView.set_pageSize(document.getElementById(ID).value);
        }
    </script>

</telerik:RadScriptBlock>
