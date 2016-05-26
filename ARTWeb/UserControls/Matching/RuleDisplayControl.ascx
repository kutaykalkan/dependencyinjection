<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="RuleDisplayControl" Codebehind="RuleDisplayControl.ascx.cs" %>
<%--<script type="text/javascript" language="javascript">
    function deleteClick(btnDelete) {
        debugger
        var btn = document.getElementById('<% =this.btnDummy.ClientID %>');
        var hdn = document.getElementById('<% =this.hdnField.ClientID %>');

        if (hdn != null) {
            hdn.value = btnDelete.rowID;
        }
        btn.click();
        return false;
    }
</script>--%>
<link href="../../App_Themes/SkyStemBlueBrown/default.css" rel="stylesheet" type="text/css" />
<asp:Panel ID="pnlRules" runat="server" Height="60px" Width="320px" ScrollBars="Auto">
    <div id="main" style="border: none">
        <asp:Button ID="btnDelete" runat="server" Style="display: none" />
        <asp:Repeater ID="rptRules" runat="server" OnItemDataBound="rptRules_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <table id="tblrules" runat="server" width="100%" cellspacing="0" cellpadding="0"
                    border="0">
                    <tr>
                        <td style="width: 5%">
                            <webControls:ExImage ID="imgBullet" runat="server" SkinID="BulletIcon" />
                        </td>
                        <td style="width: 90%">
                            <webControls:ExHyperLink ID="hlRuleText" runat="server" Text='<%#Eval("DisplayRuleText") %>'
                                Style="text-decoration: underline; cursor: hand"></webControls:ExHyperLink>
                        </td>
                        <td style="width: 5%">
                            <webControls:ExImageButton ID="exImgDelete" runat="server" SkinID="DeleteIcon" CommandName="deleteRule"
                                ToolTipLabelID="2340" CommandArgument='<%#Eval("MatchingConfigurationID") + "|" + Eval("ruleID")+ "|" + Eval("MatchingConfigurationRuleID")%>'
                                OnCommand="rptRules_ItemCommand"></webControls:ExImageButton>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>

<script language="javascript" type="text/javascript">
    function LoadRuleSetupPopupEdit(mcID, ruleID, mcrID) {
        var urlRuleSetupPopup = "RuleSetupPopup.aspx?mode=edit&IsEditMode=<%=this.IsEditMode %>&mcID=" + mcID + "&ruleID=" + ruleID + "&mcrID=" + mcrID;
        OpenRadWindowForHyperlink(urlRuleSetupPopup, 375, 500);
    }
</script>

