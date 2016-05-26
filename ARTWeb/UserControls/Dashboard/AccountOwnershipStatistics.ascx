<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Dashboard_AccountOwnershipStatistics" Codebehind="AccountOwnershipStatistics.ascx.cs" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<table style="width: 100%" cellpadding="0" cellspacing="0" id="tblMessage" runat="server"
    visible="false">
    <tr>
        <td colspan="2">
            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="ErrorLabel"></webControls:ExLabel>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="upnlAccountOwnershipStatistics" runat="server">
    <ContentTemplate>
        <table style="width: 100%" cellpadding="0" cellspacing="0" id="tblContent" runat="server">
            <tr class="BlankRow">
                <td></td>
            </tr>
            <tr>
                <td style="padding-left: 2px; width: 30%;">
                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1663" FormatString="{0}:"
                        SkinID="Black11Arial"></webControls:ExLabel>
                </td>
                <td>
                    <webControls:ExLabel ID="lblTotalAccounts" runat="server" SkinID="Black11ArialValignMiddle"></webControls:ExLabel>
                </td>
            </tr>
            <tr class="BlankRow">
                <td></td>
            </tr>
            <tr>
                <td colspan="2">
                    <telerikWebControls:ExRadGrid ID="rgAccountOwnership" runat="server" EntityNameLabelID="1032"
                        AllowSorting="false" AllowExportToExcel="True" AllowExportToPDF="true" OnItemDataBound="rgAccountOwnership_ItemDataBound"
                        OnItemCreated="rgAccountOwnership_ItemCreated" OnDetailTableDataBind="rgAccountOwnership_DetailTableDataBind"
                        OnItemCommand="rgAccountOwnership_OnItemCommand" MasterTableView-DataKeyNames="NetAccountID"
                        AllowPaging="false" MasterTableView-AllowCustomSorting="false">
                        <MasterTableView Name="Approver" ExpandCollapseColumn-Display="true" DataKeyNames="UserID"
                            Width="100%" >
                            <DetailTables>
                                <telerik:GridTableView runat="server" Name="Reviewer" DataKeyNames="UserID" Width="100%">
                                    <DetailTables>
                                        <telerik:GridTableView NoDetailRecordsText="" runat="server" Name="Preparer" Width="100%">
                                            <Columns>
                                                <telerikWebControls:ExGridBoundColumn DataField="FullName"
                                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                                </telerikWebControls:ExGridBoundColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1559"
                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblNoOfAccounts" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridBoundColumn LabelID="1659" DataField="Association"
                                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="30%">
                                                </telerikWebControls:ExGridBoundColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerikWebControls:ExGridBoundColumn DataField="FullName"
                                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%" UniqueName="column1">
                                        </telerikWebControls:ExGridBoundColumn>
                                        <telerikWebControls:ExGridTemplateColumn LabelID="1559"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <webControls:ExLabel ID="lblNoOfAccounts" runat="server"></webControls:ExLabel>
                                            </ItemTemplate>
                                        </telerikWebControls:ExGridTemplateColumn>
                                        <telerikWebControls:ExGridBoundColumn LabelID="1659" DataField="Association"
                                            ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="30%">
                                        </telerikWebControls:ExGridBoundColumn>
                                        <telerikWebControls:ExGridBoundColumn DataField="ApproverUserID" Visible="False">
                                        </telerikWebControls:ExGridBoundColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                <telerikWebControls:ExGridBoundColumn DataField="FullName"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                </telerikWebControls:ExGridBoundColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1559"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblNoOfAccounts" runat="server" CommandName="Ownership"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridBoundColumn LabelID="1659" DataField="Association"
                                    ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Right">
                                </telerikWebControls:ExGridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </td>
            </tr>
            <tr class="BlankRow">
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<UserControls:ProgressBar ID="upAccountOwnershipStatistics" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlAccountOwnershipStatistics"
    Visible="true" />
