<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" Inherits="Pages_AccountProfileAttributeUpdatePopup"
    Title="Attribute Update" Theme="SkyStemBlueBrown" Codebehind="AccountProfileAttributeUpdatePopup.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="TextEditor" Src="~/UserControls/TextEditor.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/SkyStemBlueBrown/default.css" type="text/css" rel="stylesheet" />
    <table>
        <tr>
            <td>
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblAccountPolicyURL" LabelID="1461" runat="server" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <UserControls:TextEditor ID="ucAccountPolicyURL" LabelID="1461" IsRequired="false"
                    runat="server" EditorSkinID="RadEditAttributeValue" />
            </td>
            <td>
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblReconciliationPorecedure" LabelID="1360" runat="server"
                    FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td>
                <UserControls:TextEditor ID="ucReconciliationProcedure" LabelID="1360" IsRequired="false"
                    runat="server" EditorSkinID="RadEditAttributeValue" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td colspan="4">
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td valign="top">
                <webControls:ExLabel ID="lblDescription" LabelID="1460" runat="server" FormatString="{0}:"
                    SkinID="Black11Arial"></webControls:ExLabel>
            </td>
            <td colspan="2">
                <UserControls:TextEditor ID="ucDescription" LabelID="1460" IsRequired="true" runat="server"
                    EditorSkinID="RadEditAttributeValue" />
            </td>
        </tr>
        <tr>
            <td colspan="6" align="right">
                <webControls:ExButton ID="btnSave" runat="server" LabelID="1315" OnClick="btnSave_OnClick" />&nbsp;<webControls:ExButton
                    ID="btnCancel" runat="server" LabelID="1239" OnClientClick="GetRadWindow().Close();"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
