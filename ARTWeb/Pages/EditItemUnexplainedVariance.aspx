<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="EditItemUnexplainedVariance.aspx.cs" Inherits="Pages_EditItemUnexplainedVariance"
    Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%--<%@ Register TagPrefix="userControl" TagName="DocumentUpload" Src="~/UserControls/DocumentUploadButton.ascx" %>
--%>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetailPopup" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblInputFormRecPeriod" runat="server" LabelID="1420" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblInputFormRecPeriodValue" runat="server" SkinID="Black11Arial"
                    Text=""></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
    <%--    <tr>
            <td>
            </td>
            <td colspan="4">
                <webControls:ExLabel ID="lblInstructions" runat="server" LabelID="1710" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <%--Entered By--%>
            <td>
                <webControls:ExLabel ID="lblItemInputEnteredBy" runat="server" FormatString="{0}:"
                    LabelID="1508" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblEnteredByValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
            <%--Enter Date--%>
            <td>
                <webControls:ExLabel ID="lblItemInputEnteredDate" runat="server" FormatString="{0}:"
                    LabelID="1399" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblAddedDate" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <webControls:ExLabel ID="lblTextAmountBC" runat="server" FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblAmountBC" runat="server" SkinID="ReadOnlyValue" EnableViewState="true"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblTextAmountRC" runat="server" FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblAmountRC" runat="server" SkinID="ReadOnlyValue" EnableViewState="true"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField" valign="top">
                *
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblComments" runat="server" FormatString="{0}:" LabelID="1408"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="3">
                <webControls:ExLabel ID="lblCommentsValue" runat="server" SkinID="ReadOnlyValue"
                    EnableViewState="true"></webControls:ExLabel>
                <webControls:ExTextBox ID="txtComments" MaxLength="500" runat="server" Rows="4" TextMode="MultiLine"
                    Width="98%" IsRequired="true" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <%--Button Row--%>
        <tr>
            <td align="right" colspan="5">
                <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" OnClick="btnUpdate_Click" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClientClick="window.close();"
                    CausesValidation="false" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
    </table>
</asp:content>
