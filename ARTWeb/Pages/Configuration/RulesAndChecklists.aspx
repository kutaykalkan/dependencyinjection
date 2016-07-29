<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ARTMasterPage.master"
    AutoEventWireup="true" Inherits="Pages_Configuration_RulesAndChecklists"
    Theme="SkyStemBlueBrown" Codebehind="RulesAndChecklists.aspx.cs" %>

<%@ Register TagPrefix="UserControls" TagName="QualityScoreSelection" Src="~/UserControls/QualityScore/QualityScoreSelection.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="SRARuleSelection" Src="~/UserControls/Configuration/SRARuleSelection.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="MappingUpload" Src="~/UserControls/MappingUpload/MappingUpload.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="TaskCustomField" Src="~/UserControls/TaskMaster/TaskCustomFields.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <UserControls:SRARuleSelection ID="ucSRARuleSelection" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td>
                <UserControls:QualityScoreSelection ID="ucQualityScoreSelection" runat="server" />
            </td>
        </tr>
        <tr class="BlankRow">
        </tr>
        <tr>
            <td>
                <UserControls:MappingUpload ID="ucMappingUpload" runat="server" Visible="false" />
            </td>
        </tr>

        <tr class="BlankRow">
        </tr>
        <tr id="trTaskCustomField" runat="server">
            <td>
                <UserControls:TaskCustomField ID="ucTaskCustomField" runat="server" Visible="true" />
            </td>
        </tr>
    </table>
</asp:Content>
