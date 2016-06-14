<%@ Control Language="C#" AutoEventWireup="true" Inherits="RecPeriod" Codebehind="RecPeriod.ascx.cs" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="ManadatoryField" runat="server" id="rowMandatory" style="visibility:hidden">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblPeriod" SkinID="Black11Arial" LabelID="1420" FormatString="{0}:"
                    runat="server"></webControls:ExLabel>
            </td>
            <td>
                <asp:DropDownList ID="ddlPeriod" SkinID="DropDownList200" runat="server" 
                    AutoPostBack="true" onselectedindexchanged="ddlPeriod_SelectedIndexChanged">
                </asp:DropDownList>
                <webControls:ExRequiredFieldValidator ID="rfv" ControlToValidate="ddlPeriod" runat="server"
                    LabelID="5000169"></webControls:ExRequiredFieldValidator>
            </td>
        </tr>
    </table>
</div>
