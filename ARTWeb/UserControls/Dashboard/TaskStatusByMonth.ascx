<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_Dashboard_TaskStatusBuMonth" Codebehind="TaskStatusByMonth.ascx.cs" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<table style="width: 100%" cellpadding="0" cellspacing="0" id="tblMessage" runat="server"
    visible="false">
    <tr>
        <td colspan="2">
            <webControls:ExLabel ID="lblMessage" runat="server" SkinID="ErrorLabel"></webControls:ExLabel>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="upnlTaskStatusByMonth" runat="server">
    <ContentTemplate>
        <table style="width: 100%" cellpadding="0" cellspacing="0" id="tblContent" runat="server">
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr class="BlankRow">
                <td>
                    <webControls:ExLabel ID="ExLabel2" runat="server" LabelID="2547" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                </td>
            </tr>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <telerikWebControls:ExRadGrid ID="rgGeneralTaskStatusByMonth" runat="server" EntityNameLabelID="2547"
                        AllowSorting="True" AllowExportToPDF="true" AllowExportToExcel="True" OnItemDataBound="rgGeneralTaskStatusByMonth_ItemDataBound"
                        OnItemCreated="rgGeneralTaskStatusByMonth_ItemCreated" OnItemCommand="rgGeneralTaskStatusByMonth_OnItemCommand"
                        OnSortCommand="rgGeneralTaskStatusByMonth_SortCommand">
                        <MasterTableView DataKeyNames="MonthStartDate,MonthEndDate">
                            <Columns>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="MonthNameLblColumn" LabelID="2719"
                                    HeaderStyle-Width="30%" SortExpression="MonthNumber">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblMonthName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2561" UniqueName="PendingLinkButtonColumn"
                                    SortExpression="Pending">
                                    <ItemTemplate>
                                        <webControls:ExLinkButton ID="lnkBtnPending" OnCommand="SendToGeneralTaskViewer" runat="server"
                                            SkinID="GridLinkButton" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2561" Visible="false" UniqueName="PendingDataColumn"
                                    SortExpression="Pending">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblPending" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2562" UniqueName="OverdueLinkButtonColumn"
                                    SortExpression="Overdue">
                                    <ItemTemplate>
                                        <webControls:ExLinkButton ID="lnkBtnOverdue" OnCommand="SendToGeneralTaskViewer" runat="server"
                                            SkinID="GridLinkButton" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2562" Visible="false" UniqueName="OverdueDataColumn"
                                    SortExpression="Overdue">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblOverdue" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2559" UniqueName="CompletedLinkButtonColumn"
                                    SortExpression="Completed">
                                    <ItemTemplate>
                                        <webControls:ExLinkButton ID="lnkBtnCompleted" OnCommand="SendToGeneralTaskViewer" runat="server"
                                            SkinID="GridLinkButton" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="2559" Visible="false" UniqueName="CompletedDataColumn"
                                    SortExpression="Completed">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblCompleted" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                </telerikWebControls:ExGridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerikWebControls:ExRadGrid>
                </td>
            </tr>
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlAccountTask" runat="server">
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="2546" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerikWebControls:ExRadGrid ID="rgAccountTaskStatusByMonth" runat="server" EntityNameLabelID="2546"
                                        AllowSorting="True" AllowExportToExcel="True" AllowExportToPDF="true" OnItemDataBound="rgAccountTaskStatusByMonth_ItemDataBound"
                                        OnSortCommand="rgAccountTaskStatusByMonth_SortCommand" OnItemCreated="rgAccountTaskStatusByMonth_ItemCreated"
                                        OnItemCommand="rgAccountTaskStatusByMonth_OnItemCommand">
                                        <MasterTableView DataKeyNames="MonthStartDate,MonthEndDate">
                                            <Columns>
                                                <telerikWebControls:ExGridTemplateColumn UniqueName="MonthNameLblColumn" LabelID="2719"
                                                    HeaderStyle-Width="30%" SortExpression="MonthNumber">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblMonthName" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="2561" UniqueName="PendingLinkButtonColumn"
                                                    SortExpression="Pending">
                                                    <ItemTemplate>
                                                        <webControls:ExLinkButton ID="lnkBtnPending" OnCommand="SendToAccountTaskViewer" runat="server"
                                                            SkinID="GridLinkButton" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="2561" Visible="false" UniqueName="PendingDataColumn"
                                                    SortExpression="Pending">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblPending" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="2562" UniqueName="OverdueLinkButtonColumn"
                                                    SortExpression="Overdue">
                                                    <ItemTemplate>
                                                        <webControls:ExLinkButton ID="lnkBtnOverdue" OnCommand="SendToAccountTaskViewer" runat="server"
                                                            SkinID="GridLinkButton" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="2562" Visible="false" UniqueName="OverdueDataColumn"
                                                    SortExpression="Overdue">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblOverdue" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="2559" UniqueName="CompletedLinkButtonColumn"
                                                    SortExpression="Completed">
                                                    <ItemTemplate>
                                                        <webControls:ExLinkButton ID="lnkBtnCompleted" OnCommand="SendToAccountTaskViewer" runat="server"
                                                            SkinID="GridLinkButton" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="2559" Visible="false" UniqueName="CompletedDataColumn"
                                                    SortExpression="Completed">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblCompleted" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerikWebControls:ExRadGrid>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <%--                     <table>
                                        <tr>
                                            <td>
                                                <webControls:ExLabel ID="lblNote" FormatString="{0}:" runat="server" LabelID="2005"
                                                    SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="2004" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<UserControls:ProgressBar ID="upHome" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlTaskStatusByMonth"
    Visible="true" />
