<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReconciliationStatusByFSCaption.ascx.cs"
    Inherits="UserControls_Dashboard_ReconciliationStatusByFSCaption" %>
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
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:DropDownList ID="ddlReconciliationStatus" AutoPostBack="true" runat="server"
                        SkinID="DropDownList200" OnSelectedIndexChanged="ddlReconciliationStatus_SelectedIndexChanged" />
                </td>
            </tr>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Horizontal" Width="500px" Height="100%">
                        <telerikWebControls:ExRadGrid ID="rgReconciliationStatusByFSCaption" runat="server"
                            EntityNameLabelID="1036" AllowSorting="True" AllowExportToExcel="True" AllowExportToPDF="true"
                            OnPreRender="rgReconciliationStatusByFSCaption_PreRender"
                            OnItemDataBound="rgReconciliationStatusByFSCaption_ItemDataBound" OnItemCreated="rgReconciliationStatusByFSCaption_ItemCreated"
                            OnItemCommand="rgReconciliationStatusByFSCaption_OnItemCommand" OnSortCommand="rgReconciliationStatusByFSCaption_SortCommand">
                            <MasterTableView>
                                <Columns>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="FSCaption" LabelID="1337" SortExpression="FSCaption"
                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="28%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnFSCaption" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1656" UniqueName="Total" SortExpression="Total"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnTotal" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1475" UniqueName="Notstarted" SortExpression="Notstarted"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnNotstarted" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1090" UniqueName="InProgress" SortExpression="InProgress"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnInProgress" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1089" UniqueName="Prepared" SortExpression="Prepared"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnPrepared" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1091" UniqueName="PendingReview"
                                        SortExpression="PendingReview" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnPendingReview" OnCommand="SendToAccountViewer"
                                                runat="server" SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1755" UniqueName="PendingModificationPreparer"
                                        SortExpression="PendingModificationPreparer" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnPendingModificationPreparer" OnCommand="SendToAccountViewer"
                                                runat="server" SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1093" UniqueName="Reviewed" SortExpression="Reviewed"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnReviewed" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1094" UniqueName="PendingApproval"
                                        SortExpression="PendingApproval" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnPendingApproval" OnCommand="SendToAccountViewer"
                                                runat="server" SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1756" UniqueName="PendingModificationReviewer"
                                        SortExpression="PendingModificationReviewer" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnPendingModificationReviewer" OnCommand="SendToAccountViewer"
                                                runat="server" SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1095" UniqueName="Approved" SortExpression="Approved"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnApproved" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1739" UniqueName="Reconciled" SortExpression="Reconciled"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnReconciled" OnCommand="SendToAccountViewer" runat="server"
                                                SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1097" UniqueName="SysReconciled"
                                        SortExpression="SysReconciled" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLinkButton ID="lbtnSysReconciled" OnCommand="SendToAccountViewer"
                                                runat="server" SkinID="GridLinkButton" />
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn UniqueName="FSCaptionData" Visible="false"
                                        LabelID="1337" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="28%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lFSCaption" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1656" UniqueName="TotalData" Visible="false"
                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lTotal" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1475" UniqueName="NotstartedData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lNotstarted" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1090" UniqueName="InProgressData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lInProgress" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1089" UniqueName="PreparedData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lPrepared" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1091" UniqueName="PendingReviewData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lPendingReview" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1755" UniqueName="PendingModificationPreparerData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lPendingModificationPreparer" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1093" UniqueName="ReviewedData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lReviewed" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1094" UniqueName="PendingApprovalData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lPendingApproval" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1756" UniqueName="PendingModificationReviewerData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lPendingModificationReviewer" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1095" UniqueName="ApprovedData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lApproved" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1739" UniqueName="ReconciledData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lReconciled" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                    <telerikWebControls:ExGridTemplateColumn LabelID="1097" UniqueName="SysReconciledData"
                                        Visible="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="6%">
                                        <ItemTemplate>
                                            <webControls:ExLabel ID="lSysReconciled" runat="server"></webControls:ExLabel>
                                        </ItemTemplate>
                                    </telerikWebControls:ExGridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerikWebControls:ExRadGrid>
                    </asp:Panel>
                </td>
            </tr>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<UserControls:ProgressBar ID="upHome" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlAccountOwnershipStatistics"
    Visible="true" />
