<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SimpleDropdownList.ascx.cs"
    Inherits="SimpleDropDownList" %>
<div>
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <col width="5%" />
        <col width="20%" />
        <col width="75%" />
        <tr class="BlankRow">
        </tr>
        <tr>
            <td class="ManadatoryField" runat="server" id="rowMandatory" style="visibility: hidden">
                *
            </td>
            <td>
                <webControls:ExLabel ID="lblSimpleDropDown" SkinID="Black11Arial" FormatString="{0}:"
                    runat="server"></webControls:ExLabel>
            </td>
            <td>
                <asp:DropDownList ID="ddlSimpleDropDown" Style="width: 200px;" runat="server" AutoPostBack="true" onselectedindexchanged="ddlSimpleDropDown_SelectedIndexChanged">
                </asp:DropDownList>
                <webControls:ExRequiredFieldValidator ID="rfv" ControlToValidate="ddlSimpleDropDown"
                    runat="server" InitialValue="-2" Enabled="false"></webControls:ExRequiredFieldValidator>
            </td>
        </tr>
    </table>
</div>
