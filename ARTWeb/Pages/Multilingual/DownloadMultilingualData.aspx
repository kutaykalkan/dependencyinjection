<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true" 
    Theme="SkyStemBlueBrown" Inherits="Pages_Multilingual_DownloadMultilingualData" Codebehind="DownloadMultilingualData.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblFromLanguage" runat="server" SkinID="Black11Arial" LabelID="2476"
                    FormatString="{0}:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlFromLanguage" runat="server" SkinID="DropDownList200" />
                <webControls:ExRequiredFieldValidator ID="rfvFromLanguage" runat="server" ControlToValidate="ddlFromLanguage"
                    Display="Static">!</webControls:ExRequiredFieldValidator>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td class="ManadatoryField">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblToLanguage" runat="server" SkinID="Black11Arial" LabelID="2477"
                    FormatString="{0}:" />
            </td>
            <td>
                <asp:DropDownList ID="ddlToLanguage" runat="server" SkinID="DropDownList200" />
                <webControls:ExRequiredFieldValidator ID="rfvToLanguage" runat="server" ControlToValidate="ddlToLanguage"
                    Display="Static">!</webControls:ExRequiredFieldValidator>
                <webControls:ExCompareValidator ID="cmpvToLanguage" LabelID="2480" runat="server"
                    ControlToCompare="ddlFromLanguage" ControlToValidate="ddlToLanguage" Operator="NotEqual"
                    Display="Static">!</webControls:ExCompareValidator>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td>
                <webControls:ExButton ID="btnDownload" runat="server" OnClick="btnDownload_OnClick" LabelID="2478" />
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
