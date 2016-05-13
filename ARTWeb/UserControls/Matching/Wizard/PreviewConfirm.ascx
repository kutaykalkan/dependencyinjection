<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreviewConfirm.ascx.cs"
    Inherits="SkyStem.ART.Web.UserControls.Matching.Wizard.PreviewConfirm" %>
<%@ Register TagPrefix="usc" TagName="MatchSetInfo" Src="~/UserControls/Matching/MatchSetInfo.ascx" %>
<asp:Panel ID="pnlMatchingSources" runat="server">
    <usc:MatchSetInfo ID="uscMatchSetInfo" runat="server" />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <webControls:ExLabel ID="lblMatchingSources" runat="server" LabelID="2297" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerikWebControls:ExRadGrid ID="rgMatchingSources" runat="server" Width="100%"
                    OnItemDataBound="rgMatchingSources_ItemDataBound" OnNeedDataSource="rgMatchingSources_NeedDataSource"
                    AutoGenerateColumns="false">
                    <MasterTableView Width="100%">
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2298">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblMatchingSourceName" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2299">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblMatchingSourceType" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <ClientEvents OnRowSelecting="CancelRowSelectionForDisabledCheckBox" />
                    </ClientSettings>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlConfigurationStatus" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <webControls:ExLabel ID="lblConfigurationStatus" runat="server" LabelID="2300" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerikWebControls:ExRadGrid ID="rgConfigurationStatus" runat="server" Width="100%"
                    OnItemDataBound="rgConfigurationStatus_ItemDataBound" OnNeedDataSource="rgConfigurationStatus_NeedDataSource"
                    AutoGenerateColumns="false">
                    <MasterTableView Width="100%" DataKeyNames="MatchSetSubSetCombinationID">
                        <Columns>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2301" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblDataSources" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2302" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsColMapping" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2303" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsMatchKey" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2304" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsPartialMatchKey" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="IsAmountColumn" LabelID="2536"
                                HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsAmountColumn" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2305" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsDisplayCol" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2306" UniqueName="IsRecItemMapping"
                                HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblIsRecItemMapping" runat="server"></webControls:ExLabel>
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn LabelID="2307" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <webControls:ExImage ID="imgbtnTick" ImageAlign="Left" SkinID="SuccessIcon" runat="server"
                                        Visible="false" />
                                    <webControls:ExImage ID="imgbtnCross" ImageAlign="Left" SkinID="CrossIcon" runat="server"
                                        Visible="false" />
                                </ItemTemplate>
                            </telerikWebControls:ExGridTemplateColumn>
                            <telerikWebControls:ExGridTemplateColumn UniqueName="IsConfigurationComplete" Visible="false" />
                        </Columns>
                    </MasterTableView>
                </telerikWebControls:ExRadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
