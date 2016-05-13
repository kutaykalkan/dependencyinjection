<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="EditItemReviewNote.aspx.cs" Inherits="Pages_EditItemReviewNote" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="5">
                <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetailPopup" runat="server" />
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
        <tr>
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
            <td></td>
            <td colspan="4">
                <webControls:ExCheckBoxWithLabel LabelID="1779" ID="chkDeleteAfterCertification"
                    runat="server" SkinID="CheckboxWithLabelBold" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td class="ManadatoryField">*
            </td>
            <td>
                <webControls:ExLabel ID="lblSubject" runat="server" FormatString="{0}:" LabelID="1778"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExTextBox ID="txtSubjectValue" runat="server" IsRequired="true" MaxLength="100"
                    SkinID="ExTextBox200" />
            </td>
            <td>
                <webControls:ExLabel ID="lblAttachDocs" runat="server" FormatString="{0}:" LabelID="1392"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExHyperLink ID="hlDocument" runat="server" SkinID="ShowDocumentPopupHyperLink"
                    LabelID="1540" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td class="ManadatoryField">*
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblReviewNoteHeading" runat="server" FormatString="{0}:"
                    LabelID="1394" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="3">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <asp:Panel ID="pnlReviewNoteText" runat="server">
                        <tr>
                            <td>
                                <webControls:ExTextBox ID="txtReviewNote" runat="server" TextMode="MultiLine" SkinID="ExMulitilineTextBox200"
                                    IsRequired="true" MaxLength="500" Width="98%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="BlankRow"></td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td class="ReviewNote">
                            <asp:Repeater ID="rptReviewNotes" runat="server" OnItemDataBound="rptReviewNotes_ItemDataBound">
                                <ItemTemplate>
                                    <webControls:ExLabel ID="lblUserDetails" runat="server" SkinID="UserDetails"></webControls:ExLabel>:&nbsp;<webControls:ExLabel ID="lblNote" runat="server" SkinID="Black9Arial"></webControls:ExLabel>
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
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClick="btnCancel_Click" CausesValidation="false" />
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
