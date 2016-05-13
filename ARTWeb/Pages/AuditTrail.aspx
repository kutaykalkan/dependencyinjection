<%@ Page Language="C#" MasterPageFile="~/MasterPages/RecProcessMasterPage.master"
    AutoEventWireup="true" CodeFile="AuditTrail.aspx.cs" Inherits="Pages_AuditTrail"
    Title="Untitled Page" Theme="SkyStemBlueBrown" EnableEventValidation="true" ValidateRequest="false" %>

<%@ Register Src="~/UserControls/AuditTrail.ascx" TagName="AuditTrail" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphRecProcess" runat="Server">
    <table width="100%">
        <tr>
            <td>
                <uc:AuditTrail ID="ucAuditTrail" runat="server" RegisterRecDropDownEvent="true" />
            </td>
        </tr>
    </table>
</asp:Content>
