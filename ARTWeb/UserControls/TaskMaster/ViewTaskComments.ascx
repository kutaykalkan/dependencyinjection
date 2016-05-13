<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewTaskComments.ascx.cs"
    Inherits="UserControls_TaskMaster_ViewTaskComments" %>
<table width="100%">
    <tr>
        <td style="border: 1px solid #000000; padding: 0; margin: 0">
            <asp:Repeater ID="rptTaskComments" runat="server" OnItemDataBound="rptTaskComments_ItemDataBound">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <col width="20%" />
                        <col width="60%" />
                        <col width="20%" />
                        <tr class="TableHeaderSameAsGrid" align="left">
                            <td>
                                <%# SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(1533) %>
                            </td>
                            <td>
                                <%# SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(1848) %>
                            </td>
                            <td>
                                <%# SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(1509) %>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <AlternatingItemTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <col width="20%" />
                        <col width="60%" />
                        <col width="20%" />
                        <tr class="TableAlternateRowSameAsGrid" align="left">
                            <td>
                                <webControls:ExLabel ID="lblCommenter" runat="server" FormatString="{0}:" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblComment" runat="server" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblDateAdded" runat="server" />
                            </td>
                        </tr>
                    </table>
                </AlternatingItemTemplate>
                <ItemTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <col width="20%" />
                        <col width="60%" />
                        <col width="20%" />
                        <tr class="TableRowSameAsGrid" align="left">
                            <td>
                                <webControls:ExLabel ID="lblCommenter" runat="server" FormatString="{0}:" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblComment" runat="server" />
                            </td>
                            <td>
                                <webControls:ExLabel ID="lblDateAdded" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                    <webControls:ExLabel ID="lblEmpty" SkinID="Black11Arial" runat="server" Visible='<%#bool.Parse((rptTaskComments.Items.Count==0).ToString())%>' />
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
