<%@ Page Language="C#" MasterPageFile="~/MasterPages/PopUpMasterPage.master" AutoEventWireup="true"
    Theme="SkyStemBlueBrown" CodeFile="PackageFeatureList.aspx.cs" Inherits="Pages_PackageFeatureList" %>

<%@ Register TagPrefix="UserControls" TagName="PackageFeatureList" Src="~/UserControls/PackageFeatureList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <webControls:ExLabel ID="lblTitle" runat="server" LabelID="2173" FormatString="{0}:" SkinID="Black11Arial"></webControls:ExLabel>
            </td>
        </tr>
        <tr class="BlankRow">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <UserControls:PackageFeatureList ID="PackageFeatureList1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
