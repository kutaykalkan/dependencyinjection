<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    CodeFile="AddRecControlCheckList.aspx.cs" Inherits="Pages_AddRecControlCheckList" Theme="SkyStemBlueBrown" %>

<%@ Register TagPrefix="UserControls" TagName="AccountHierarchyDetail" Src="~/UserControls/AccountHierarchyDetail.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="ProgressBar" Src="~/UserControls/ProgressBar.ascx" %>
<%@ Import Namespace="SkyStem.ART.Web.Utility" %>
<%@ Import Namespace="SkyStem.ART.Web.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="padding: 0px">
        <tr>
            <td colspan="6">
                <UserControls:AccountHierarchyDetail ID="ucAccountHierarchyDetailPopup" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblInputFormRecPeriod" runat="server" LabelID="1420" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 30%">
                <webControls:ExLabel ID="lblInputFormRecPeriodValue" runat="server" SkinID="Black11Arial"
                    Text=""></webControls:ExLabel>
            </td>
            <td></td>
            <td style="width: 20%">
                <webControls:ExLabel ID="lblCheckListnumber" runat="server" LabelID="2829" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td style="width: 30%">
                <webControls:ExLabel ID="lblCheckListnumberValue" runat="server" SkinID="Black11Arial"
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
                <webControls:ExLabel ID="lblItemInputEnteredBy" runat="server" FormatString="{0}:"
                    LabelID="1508" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExLabel ID="lblEnteredByValue" runat="server" SkinID="ReadOnlyValue"></webControls:ExLabel>
            </td>
            <td></td>
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
            <td></td>
        </tr>
        <tr>
            <td class="ManadatoryField">*
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblDescription" runat="server" FormatString="{0}:" LabelID="1408"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <webControls:ExTextBox ID="txtDescription" runat="server" IsRequired="true"  SkinID="ExMultilineTextBoxDescriptionRecItemForm"></webControls:ExTextBox>
            </td>
            <td class="ManadatoryField"></td>
            <td></td>
            <td></td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td></td>
        </tr>

        <%--Button Row--%>
        <tr>
            <td align="right" colspan="6">
                <webControls:ExButton ID="btnUpdate" runat="server" LabelID="1315" OnClick="btnUpdate_Click" />
                <webControls:ExButton ID="btnCancel" runat="server" LabelID="1239" OnClientClick="window.close();"
                    CausesValidation="false" />
            </td>
        </tr>
        <%--Blank Row--%>
        <tr class="BlankRow">
            <td></td>
        </tr>
    </table>
</asp:Content>
