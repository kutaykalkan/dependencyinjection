<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExceptionsByFSCaption.ascx.cs"
    Inherits="UserControls_Dashboard_ExceptionsByFSCaption" %>
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
            <tr class="BlankRow">
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <telerikWebControls:ExRadGrid ID="rgExceptionByFSCaption" runat="server" EntityNameLabelID="1034"
                        AllowSorting="True" AllowExportToPDF="true" AllowExportToExcel="True" OnItemDataBound="rgExceptionByFSCaption_ItemDataBound"
                        OnItemCreated="rgExceptionByFSCaption_ItemCreated" OnItemCommand="rgExceptionByFSCaption_OnItemCommand"
                        OnSortCommand="rgExceptionByFSCaption_SortCommand">
                        <MasterTableView DataKeyNames="Name">
                            <Columns>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1337" SortExpression="Name" UniqueName="NameLinkButtonColumn"
                                    ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <webControls:ExLinkButton ID="lnkBtnName" OnCommand="SendToAccountViewer" runat="server"
                                            SkinID="GridLinkButton" />
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn UniqueName="NameLblColumn" LabelID="1337"
                                    Visible="false" HeaderStyle-Width="30%">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblName" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1425" UniqueName="WriteOffOnLinkButtonColumn"
                                    SortExpression="WriteOnOffAmountReportingCurrency">
                                    <ItemTemplate>
                                        <webControls:ExLinkButton ID="lnkBtnWriteOffOn" OnCommand="SendToAccountViewer" runat="server"
                                            SkinID="GridLinkButton" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1425" Visible="false" UniqueName="WriteOffOnDataColumn"
                                    SortExpression="WriteOnOffAmountReportingCurrency">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblWriteOffOn" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1678" UniqueName="UnExpVarLinkButtonColumn"
                                    SortExpression="UnexplainedVarianceReportingCurrency">
                                    <ItemTemplate>
                                        <webControls:ExLinkButton ID="lnkBtnUnExpVar" OnCommand="SendToAccountViewer" runat="server"
                                            SkinID="GridLinkButton" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1678" Visible="false" UniqueName="UnExpVarDataColumn"
                                    SortExpression="UnexplainedVarianceReportingCurrency">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblUnExpVar" runat="server"></webControls:ExLabel>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1871" UniqueName="TotalLinkButtonColumn"
                                    SortExpression="TotalVar">
                                    <ItemTemplate>
                                        <webControls:ExLinkButton ID="lnkBtnTotal" OnCommand="SendToAccountViewer" runat="server"
                                            SkinID="GridLinkButton" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                </telerikWebControls:ExGridTemplateColumn>
                                <telerikWebControls:ExGridTemplateColumn LabelID="1871" Visible="false" UniqueName="TotalDataColumn"
                                    SortExpression="TotalVar">
                                    <ItemTemplate>
                                        <webControls:ExLabel ID="lblTotal" runat="server"></webControls:ExLabel>
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
                    <asp:Panel ID="pnlExceptionByNetAccount" runat="server">
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <webControls:ExLabel ID="ExLabel1" runat="server" LabelID="1914" SkinID="BlueBold11ArialUnderline"></webControls:ExLabel>
                                </td>
                            </tr>
                            <tr class="BlankRow">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerikWebControls:ExRadGrid ID="rgExceptionByNetAccount" runat="server" EntityNameLabelID="1914"
                                        AllowSorting="True" AllowExportToExcel="True" AllowExportToPDF="true" OnItemDataBound="rgExceptionByNetAccount_ItemDataBound"
                                        OnSortCommand="rgExceptionByNetAccount_SortCommand" OnItemCreated="rgExceptionByNetAccount_ItemCreated"
                                        OnItemCommand="rgExceptionByNetAccount_OnItemCommand">
                                        <MasterTableView DataKeyNames="Name">
                                            <Columns>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1257" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="30%" UniqueName="NameLinkButtonColumn">
                                                    <ItemTemplate>
                                                        <webControls:ExLinkButton ID="lnkBtnName" SkinID="GridLinkButton" OnCommand="SendToAccountViewerForNetAccounts"
                                                            runat="server"></webControls:ExLinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1257" SortExpression="Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-Width="30%" UniqueName="NameDataColumn" Visible="false">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblName" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1425" SortExpression="WriteOnOffAmountReportingCurrency"
                                                    UniqueName="WriteOffOnLinkButtonColumn">
                                                    <ItemTemplate>
                                                        <webControls:ExLinkButton ID="lnkBtnWriteOffOn" SkinID="GridLinkButton" OnCommand="SendToAccountViewerForNetAccounts"
                                                            runat="server"></webControls:ExLinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1425" Visible="false" UniqueName="WriteOffOnDataColumn"
                                                    SortExpression="WriteOnOffAmountReportingCurrency">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblWriteOffOn" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1678" SortExpression="UnexplainedVarianceReportingCurrency"
                                                    UniqueName="UnExpVarLinkButtonColumn">
                                                    <ItemTemplate>
                                                        <webControls:ExLinkButton ID="lnkBtnUnExpVar" SkinID="GridLinkButton" OnCommand="SendToAccountViewerForNetAccounts"
                                                            runat="server"></webControls:ExLinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1678" Visible="false" UniqueName="UnExpVarDataColumn"
                                                    SortExpression="UnexplainedVarianceReportingCurrency">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblUnExpVar" runat="server"></webControls:ExLabel>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="20%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1871" SortExpression="TotalVar"
                                                    UniqueName="TotalLinkButtonColumn">
                                                    <ItemTemplate>
                                                        <webControls:ExLinkButton ID="lnkBtnTotal" SkinID="GridLinkButton" OnCommand="SendToAccountViewerForNetAccounts"
                                                            runat="server"></webControls:ExLinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Width="30%" />
                                                </telerikWebControls:ExGridTemplateColumn>
                                                <telerikWebControls:ExGridTemplateColumn LabelID="1871" Visible="false" UniqueName="TotalDataColumn"
                                                    SortExpression="TotalVar">
                                                    <ItemTemplate>
                                                        <webControls:ExLabel ID="lblTotal" runat="server"></webControls:ExLabel>
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
                                    <table>
                                        <tr>
                                            <td>
                                                <webControls:ExLabel ID="lblNote" FormatString ="{0}:" runat="server" LabelID="2005" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                            <td>
                                                <webControls:ExLabel ID="ExLabel4" runat="server" LabelID="2004" SkinID="Black11Arial"></webControls:ExLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<UserControls:ProgressBar ID="upHome" runat="server" EnableTheming="true" AssociatedUpdatePanelID="upnlAccountOwnershipStatistics"
    Visible="true" />
