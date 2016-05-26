<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_Report_FinancialYear" Codebehind="FinancialYear.ascx.cs" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="ManadatoryField" runat="server">
                &nbsp;
            </td>
            <td>
                <webControls:ExLabel ID="lblFinancialYear" SkinID="Black11Arial" LabelID="2011" FormatString="{0}:"
                    runat="server"></webControls:ExLabel>
            </td>
            <td>
                <asp:DropDownList ID="ddlFinancialYear" SkinID="DropDownList200" runat="server" 
                    AutoPostBack="true" onselectedindexchanged="ddlFinancialYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
