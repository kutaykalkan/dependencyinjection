<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="SkyStem.ART.Web.UserControls.UserControls_AccountHierarchyDetail" Codebehind="AccountHierarchyDetail.ascx.cs" %>
<table width="100%" cellpadding="0" cellspacing="0" class="blueBorder">
    <tr class="blueRow">
        <td align="center"  style="padding-left:2px;">
            <%--<webControls:ExLabel ID="lblAccountDetails" runat="server" Text="ENTITY - ACCT# - ACCT NAME"
                SkinID="BlueBold11Arial"></webControls:ExLabel>--%>
                <webControls:ExLabel ID="lblAccountDetails" runat="server" Text="ENTITY - ACCT# - ACCT NAME"
                SkinID="AccountDetail"></webControls:ExLabel>
            <asp:HiddenField runat="server" ID="hdnAccountKey" Value="1001" />
        </td>
    </tr>
</table>
