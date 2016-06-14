<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_EditRecItemComment" Theme="SkyStemBlueBrown" Codebehind="EditRecItemComment.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr class="blueRow">
            <td align="center" style="padding-left: 2px;" colspan="5">
                <webControls:ExLabel ID="lblRecItemDetails" runat="server" Text="RecItem Number - RecItem Description"
                    SkinID="AccountDetail"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblRecPeriod" runat="server" LabelID="1420" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblRecPeriodValue" runat="server" SkinID="Black11Arial"
                    Text=""></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr id="trEnteredBy" runat="server">
            <td></td>
            <%--Entered By--%>
            <td>
                <webControls:ExLabel ID="lblEnteredBy" runat="server" FormatString="{0}:" LabelID="1508"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblEnteredByValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
            <%--Enter Date--%>
            <td>
                <webControls:ExLabel ID="lblDateAdded" runat="server" FormatString="{0}:" LabelID="1399"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblDateAddedValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td></td>
        </tr>

        <tr>
            <td class="ManadatoryField">*
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblCommentHeading" runat="server" FormatString="{0}:"
                    LabelID="2750" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="3">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <asp:Panel ID="pnlCommentText" runat="server">
                        <tr>
                            <td>
                                <webControls:ExTextBox ID="txtComment" runat="server" TextMode="MultiLine" SkinID="ExMultilineTextBoxDescriptionRecItemForm"
                                    IsRequired="true" MaxLength="500" Width="98%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="BlankRow"></td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="ReviewNote">
                            <asp:Repeater ID="rptComments" runat="server">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblUserDetails" runat="server" SkinID="UserDetails"></webControls:ExLabel>:&nbsp;<webControls:ExLabel
                                        ID="lblComments" runat="server" SkinID="Black9Arial"></webControls:ExLabel>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td colspan="5" align="right">
                <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" OnClick="btnUpdate_Click" OnClientClick="if (!Page_ClientValidate()){ return false; } this.disabled = true; this.value = 'Saving...';" UseSubmitBehavior="false" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click"
                    CausesValidation="false" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
                <asp:HiddenField ID="hdnMsgThread" runat="server" />
                <asp:HiddenField ID="hdnLastNotifyUser" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
